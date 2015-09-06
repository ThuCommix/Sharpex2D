//------------------------------------------------------------------------------
// <copyright file="VSIntegrationPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using VSLangProj;

namespace VSIntegration
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExistsAndFullyLoaded_string)]
    public sealed class VSIntegrationPackage : PackageBase
    {
        /// <summary>
        /// VSIntegrationPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "074e074e-1d0f-44d0-8782-171e1ca818e1";

        public Content.Project ContentProject { private set; get; }

        #region Package Members

        public VSIntegrationPackage()
        {
            ContentProject = new Content.Project();
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            var ideService = GetService<DTE>();
            var sharpex2DFound = false;

            foreach (
                var vsProject in
                    ideService.Solution.Projects.Cast<Project>()
                        .Select(project => (VSProject) project.Object)
                        .Where(vsProject => vsProject != null))
            {
                sharpex2DFound = vsProject.References.Cast<Reference>().Any(reference => reference.Name == "Sharpex2D");
                if (sharpex2DFound)
                    break;
            }

            //Only load package if the sharpex2d.dll was found in references

            if (sharpex2DFound)
            {
                base.Initialize();
            }
        }

        #endregion
    }
}
