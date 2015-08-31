
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ContentPipelineUI.Commands;
using ContentPipelineUI.Models;
using ContentPipelineUI.Views;
using Microsoft.Win32;
using Sharpex2D.Framework;

namespace ContentPipelineUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly StringBuilder _stringBuilder;

        /// <summary>
        /// A value indicating whether a project is loaded.
        /// </summary>
        public bool IsProjectLoaded
        {
            get { return Project != null; }
        }

        /// <summary>
        /// A value indicating whether the project is not loaded.
        /// </summary>
        public bool IsNotProjectLoaded { get { return !IsProjectLoaded; } }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public Project Project { get; private set; }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public string Output { get { return _stringBuilder.ToString(); } }

        /// <summary>
        /// A value indicating whether the content pipeline is currently building.
        /// </summary>
        public bool IsBuilding { private set; get; }

        /// <summary>
        /// Gets the product version.
        /// </summary>
        public string ProductVersion { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        public ICommand OnLoadProject { private set; get; }

        public ICommand OnCreateProject { private set; get; }

        /// <summary>
        /// Initializes a new MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _stringBuilder = new StringBuilder();
            OnLoadProject = new RelayCommand(LoadProject);
            OnCreateProject = new RelayCommand(CreateNewProject);
        }

        /// <summary>
        /// Loads a project specified by the path.
        /// </summary>
        public void LoadProject(object parameter)
        {
            try
            {
                var opf = new OpenFileDialog {Filter = "Content Project Files (.contentproj)|*.contentproj"};
                var result = opf.ShowDialog();
                if (result == null || result.Value == false)
                    return;

                Project = Project.LoadFromXml(opf.FileName);
            }
            catch
            {
                MessageBox.Show("The project could not be loaded :(.", "Project Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Opens a dialog for creating a new project.
        /// </summary>
        public void CreateNewProject(object parameter)
        {
            var window = new ProjectManagementWindow();
            window.ShowDialog();
            var projectViewModel = (ProjectViewModel) window.DataContext;
            if (projectViewModel.IsProjectSaved)
            {
                Project = projectViewModel.Project;
                OnPropertyChanged(nameof(Project));
                OnPropertyChanged(nameof(IsProjectLoaded));
                OnPropertyChanged(nameof(IsNotProjectLoaded));
            }
        }

        /// <summary>
        /// Runs the content pipeline.
        /// </summary>
        public void RunContentPipeline()
        {
            if (Project == null)
                throw new NullReferenceException("Project is not loaded.");

            if (!File.Exists(Project.Path))
                throw new FileNotFoundException("The ContentPipeline.exe could not be found.");

            var process = new Process
            {
                StartInfo =
                {
                    FileName = Project.Path,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    WorkingDirectory = Path.GetDirectoryName(Project.Path),
                    UseShellExecute = true,
                    RedirectStandardOutput = true,
                    Arguments = '"' + Project.Source + '"' + " " + '"' + Project.Target + '"' + " --compile"
                },
                EnableRaisingEvents = true
            };

            process.OutputDataReceived += OutputReceived;
            process.Exited += PipelineExited;
            IsBuilding = true;
            process.Start();
        }

        private void PipelineExited(object sender, EventArgs e)
        {
            IsBuilding = false;
            int exitCode = ((Process) sender).ExitCode;

            //TODO 
        }

        private void OutputReceived(object sender, DataReceivedEventArgs e)
        {
            _stringBuilder.AppendLine(e.Data);
        }
    }
}

