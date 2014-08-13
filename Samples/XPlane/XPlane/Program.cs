using System;
using System.Drawing;
using System.Windows.Forms;
using Sharpex2D;
using Sharpex2D.Rendering;
using Sharpex2D.Surface;
using XPlane.Core;

namespace XPlane
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RenderTarget renderTarget = RenderTarget.Create();
            renderTarget.Window.Title = string.Format("XPlane {0}", Application.ProductVersion);
            renderTarget.Window.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            renderTarget.Window.SurfaceLayout = new SurfaceLayout(true, false, true);

            SGL.Initialize(new Configurator(new BackBuffer(800, 480), new Game1(), renderTarget));
        }
    }
}