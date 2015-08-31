using System.Windows.Input;
using ContentPipelineUI.Commands;
using ContentPipelineUI.Models;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ContentPipelineUI.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the project.
        /// </summary>
        public Project Project { private set; get; }

        /// <summary>
        /// A value indicating whether the project is saved.
        /// </summary>
        public bool IsProjectSaved { private set; get; }

        public ICommand OnSave { private set; get; }

        public ICommand OnBrowseDirectories { private set; get; }

        /// <summary>
        /// Initializes a new ProjectViewModel class.
        /// </summary>
        public ProjectViewModel()
        {
            OnSave = new RelayCommand(SaveProject);
            OnBrowseDirectories = new RelayCommand(BrowseDirectories);
            Project = new Project();
        }

        /// <summary>
        /// Injects the project.
        /// </summary>
        /// <param name="project">The Project.</param>
        public void InjectProject(Project project)
        {
            Project = project;
            OnPropertyChanged(nameof(Project));
        }

        /// <summary>
        /// Saves the project.
        /// </summary>
        /// <param name="parameter">The Parameter.</param>
        private void SaveProject(object parameter)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Content Project Files (.contentproj)|*.contentproj"};
            var result = saveFileDialog.ShowDialog();
            if (result == null || result == false)
                return;

            IsProjectSaved = true;
            Project.Save(saveFileDialog.FileName);
        }

        private void BrowseDirectories(object parameter)
        {
            var buttonTarget = (string) parameter;
            string fileResult = "";
            switch (buttonTarget)
            {
                case "ContentPipeline":
                    if (GetOpenFileDialogResult(null, out fileResult))
                    {
                        Project.Path = fileResult;
                        OnPropertyChanged(nameof(Project));
                    }
                    break;
                case "Source":
                    if (GetOpenDirectoryResult(out fileResult))
                    {
                        Project.Source = fileResult;
                        OnPropertyChanged(nameof(Project));
                    }
                    break;
                case "Target":
                    if (GetOpenDirectoryResult(out fileResult))
                    {
                        Project.Target = fileResult;
                        OnPropertyChanged(nameof(Project));
                    }
                    break;
                case "Plugins":
                    if (GetOpenFileDialogResult("Plugin (*.dll)|*.dll", out fileResult))
                    {
                        Project.Plugins.Add(fileResult);
                        OnPropertyChanged(nameof(Project));
                    }
                    break;
            }
        }

        private bool GetOpenFileDialogResult(string filter, out string path)
        {
            var openFileDialog = new OpenFileDialog {Filter = filter};
            var result = openFileDialog.ShowDialog();
            path = openFileDialog.FileName;
            return result ?? false;
        }

        private bool GetOpenDirectoryResult(out string path)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            path = dialog.FileName;
            return result == CommonFileDialogResult.Ok;
        }
    }
}

