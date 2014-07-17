using System;
using System.Drawing;
using System.Windows.Forms;
using Sharpex2D;
using Sharpex2D.Rendering;
using Sharpex2D.Surface;
using Color = Sharpex2D.Rendering.Color;

namespace FlyingBird
{
    internal static class Program
    {
        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            RenderTarget renderTarget = RenderTarget.Create();
            renderTarget.Window.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            renderTarget.Window.Title = "Flying Bird";
            renderTarget.Window.CursorVisibility = true;
            renderTarget.Window.SurfaceLayout = new SurfaceLayout(true, false, true);

            SGL.Initialize();

            SGL.Components.Get<GraphicsDevice>().ClearColor = Color.FromArgb(255, 128, 197, 205);
        }
    }
}