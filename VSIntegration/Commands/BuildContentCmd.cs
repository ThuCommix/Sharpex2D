using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using EnvDTE;
using EnvDTE80;
using VSIntegration.Properties;

namespace VSIntegration.Commands
{
    public class BuildContentCmd : CommandBase
    {
        public BuildContentCmd(PackageBase package) : base(package, GuidList.guidDefaultCommandSet, 0x0100)
        {
        }

        public override void OnClick(object sender, EventArgs e)
        {
            var proj = ((VSIntegrationPackage) Package).ContentProject;

            var ideService = Package.GetServiceAs<DTE, DTE2>();
            var selectedItems = (UIHierarchyItem[])ideService.ToolWindows.SolutionExplorer.SelectedItems;
            if (selectedItems == null || selectedItems.Length == 0)
                return;

            var contentprojItem = selectedItems[0].Object as Project;
            if (contentprojItem == null)
                return;

            var projFilePath = Path.GetDirectoryName(contentprojItem.FullName);
            var fullPath = Path.Combine(projFilePath, "content.proj");
            var fileInfo = new FileInfo(fullPath);

            if (proj.FileLastChanged == null || proj.FileLastChanged < fileInfo.LastWriteTime)
            {
                try
                {
                    proj.FileLastChanged = fileInfo.LastWriteTime;
                    XDocument xml = XDocument.Parse(File.ReadAllText(fullPath));

                    proj.ContentPipeline = xml.Element("ContentProject").Attribute("ContentPipeline").Value;
                    proj.SourceFolder = xml.Element("ContentProject").Element("ContentSourceFolder").Value;
                    proj.TargetFolder = xml.Element("ContentProject").Element("ContentTargetFolder").Value;
                    proj.IsError = false;
                }
                catch
                {
                    proj.IsError = true;
                }
            }

            if (proj.IsError)
            {
                MessageBox.Show(Resources.ErrorReadingContentProj, "Sharpex2D", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            var sourceFolder = Path.Combine(projFilePath, proj.SourceFolder);
            var buildConfig = ideService.Solution.SolutionBuild.ActiveConfiguration.Name;
            var outputFolder = Path.Combine(projFilePath, "bin", buildConfig, proj.TargetFolder);

            if (!Directory.Exists(outputFolder))
            {
                try
                {
                    Directory.CreateDirectory(outputFolder);
                }
                catch
                {
                    MessageBox.Show(
                        Resources.UnableToCreateOutputFolder,
                        "Sharpex2D", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (File.Exists(proj.ContentPipeline) && Directory.Exists(sourceFolder))
            {
                var outputWindow = ideService.Windows.Item("{34E76E81-EE4A-11D0-AE2E-00A0C90FFFC3}");
                outputWindow.Visible = true;
                var outputPane = ideService.ToolWindows.OutputWindow.OutputWindowPanes.Add("Sharpex2D - Content");
                outputPane.Clear();
                outputPane.Activate();

                var contentPipeline = new System.Diagnostics.Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Arguments = '"' + sourceFolder + '"' + " " + '"' + outputFolder + '"' + " --compile",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        RedirectStandardOutput = true,
                        FileName = proj.ContentPipeline,
                        UseShellExecute = false,
                    },
                    EnableRaisingEvents = true
                };

                contentPipeline.OutputDataReceived += (o, args) =>
                {
                    outputPane.OutputString($"{args.Data}{Environment.NewLine}");
                };

                contentPipeline.Exited += (o, args) =>
                {
                    var process = (System.Diagnostics.Process) o;
                    if (process.ExitCode != 0)
                    {
                        MessageBox.Show(Resources.ContentBuildFailed, "Sharpex2D",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                contentPipeline.Start();
                contentPipeline.BeginOutputReadLine();
            }
            else
            {
                MessageBox.Show(
                    Resources.ContentPipelineExeWasNotFound,
                    "Sharpex2D", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
