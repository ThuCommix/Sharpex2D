using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpexGL.Framework.Window
{
    public class GameWindow : IGameWindow
    {
        /// <summary>
        /// Gets or sets the GameWindowHandle.
        /// </summary>
        public IntPtr Handle { get; set; }

        /// <summary>
        /// Updates the GameWindow.
        /// </summary>
        public void Update()
        {
            //MethodInvoker br = () => Control.FromHandle(Handle).Update();
            //Control.FromHandle(Handle).Invoke(br);
        }

        /// <summary>
        /// Sets the Title of the GameWindow.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void SetTitle(string value)
        {
            MethodInvoker br = delegate
                {
                    Control.FromHandle(Handle).Text = value;
                };
            Control.FromHandle(Handle).Invoke(br);
        }
        /// <summary>
        /// Sets the Icon of the GameWindow.
        /// </summary>
        /// <param name="icon">The Icon.</param>
        public void SetIcon(Icon icon)
        {
            MethodInvoker br = delegate
            {
                ((Form)Control.FromHandle(Handle)).Icon = icon;
            };
            Control.FromHandle(Handle).Invoke(br);
        }
        /// <summary>
        /// Sets the Size of the GameWindow.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public void SetSize(int width, int height)
        {
            _width = width;
            _height = height;
            MethodInvoker br = delegate
            {
                Control.FromHandle(Handle).ClientSize = new Size(width, height);
            };
            Control.FromHandle(Handle).Invoke(br);
        }
        /// <summary>
        /// Sets the Position of the GameWindow.
        /// </summary>
        /// <param name="x">The XKoord.</param>
        /// <param name="y">The YKoord.</param>
        public void SetPosition(int x, int y)
        {
            MethodInvoker br = delegate
            {
                Control.FromHandle(Handle).Location = new Point(x,y);
            };
            Control.FromHandle(Handle).Invoke(br);
        }
        /// <summary>
        /// Sets the Style of the GameWindow.
        /// </summary>
        /// <param name="style">The WindowStyle.</param>
        public void SetWindowStyle(WindowStyle style)
        {
            MethodInvoker br = delegate
            {
                if (style == WindowStyle.Maximized)
                {
                    var surface = (Form) Control.FromHandle(Handle);
                    surface.FormBorderStyle = FormBorderStyle.None;
                    surface.Location = new Point(0, 0);
                    surface.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                }
                else
                {
                    var surface = (Form)Control.FromHandle(Handle);
                    surface.FormBorderStyle = FormBorderStyle.Sizable;
                    surface.Size = new Size(_width, _height);
                    surface.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - surface.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - surface.Height) / 2);
                }
            };
            Control.FromHandle(Handle).Invoke(br);
        }
        /// <summary>
        /// Gets the Soue of the GameWindow.
        /// </summary>
        /// <returns>Size</returns>
        public Size GetSize()
        {
            var size = new Size(0,0);
            MethodInvoker br = delegate
                {
                    size = Control.FromHandle(Handle).Size;
                };
            Control.FromHandle(Handle).Invoke(br);
            return size;
        }

        private int _width;
        private int _height;

        /// <summary>
        /// Initializes a new GameWindow class.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        public GameWindow(IntPtr handle)
        {
            Handle = handle;
            ((Form)Control.FromHandle(Handle)).FormClosed += GameWindow_FormClosed;
        }

        void GameWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            SGL.Shutdown();
        }
    }
}
