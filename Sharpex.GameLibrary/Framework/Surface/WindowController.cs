using System.Drawing;
using System.Windows.Forms;
using SharpexGL.Framework.Math;

namespace SharpexGL.Framework.Surface
{
    public class WindowController : ISurfaceControl
    {
        #region ISurfaceControl Implementation

        /// <summary>
        /// Sets the Title of the GameWindow.
        /// </summary>
        /// <param name="value">The Value.</param>
        public void SetTitle(string value)
        {
            MethodInvoker br = delegate
            {
                _surface.Text = value;
            };
            _surface.Invoke(br);
        }

        /// <summary>
        /// Sets the Icon of the GameWindow.
        /// </summary>
        /// <param name="icon">The Icon.</param>
        public void SetIcon(Icon icon)
        {
            MethodInvoker br = delegate
            {
                _surface.Icon = icon;
            };
            _surface.Invoke(br);
        }

        /// <summary>
        /// Sets the Size of the GameWindow.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public void SetSize(int width, int height)
        {
            FreeWindow();
            MethodInvoker br = delegate
            {
                _surface.ClientSize = new Size(width, height);
            };
            _surface.Invoke(br);
            FixWindow();
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
                _surface.Location = new Point(x, y);
            };
            _surface.Invoke(br);
        }

        /// <summary>
        /// Sets the Style of the GameWindow.
        /// </summary>
        /// <param name="style">The WindowStyle.</param>
        public void SetWindowStyle(SurfaceStyle style)
        {
            MethodInvoker br = delegate
            {
                if (style == SurfaceStyle.Fullscreen)
                {
                    _surface.FormBorderStyle = FormBorderStyle.None;
                    _surface.Location = new Point(0, 0);
                    _surface.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                }
                else
                {
                    _surface.FormBorderStyle = FormBorderStyle.Sizable;
                    _surface.ClientSize = new Size(SGL.GraphicsDevice.DisplayMode.Width,
                        SGL.GraphicsDevice.DisplayMode.Height);
                    _surface.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - _surface.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - _surface.Height) / 2);
                }
            };
            _surface.Invoke(br);
        }

        /// <summary>
        /// Gets the Soue of the GameWindow.
        /// </summary>
        /// <returns>Size</returns>
        public Vector2 GetSize()
        {
            Vector2 size = null;
            MethodInvoker br = delegate
            {
                size = new Vector2(_surface.Location.X, _surface.Location.Y);
            };
            _surface.Invoke(br);
            return size;
        }

        /// <summary>
        /// Sets the ControlLayout.
        /// </summary>
        /// <param name="surfaceLayout">The SurfaceLayout.</param>
        public void SetControlLayout(SurfaceLayout surfaceLayout)
        {
            MethodInvoker br = delegate
            {
                _surface.MaximizeBox = surfaceLayout.CanMaximize;
                _surface.MinimizeBox = surfaceLayout.CanMinimize;
                _surface.ControlBox = surfaceLayout.CanClose;
            };
            _surface.Invoke(br);
        }

        /// <summary>
        /// Sets the Cursor visibility.
        /// </summary>
        /// <param name="state">The State.</param>
        public void SetCursorVisibility(bool state)
        {
            if (state)
            {
                Cursor.Show();
            }
            else
            {
                Cursor.Hide();
            }
        }
        /// <summary>
        /// Sets the Cursor icon.
        /// </summary>
        /// <param name="iconPath">The IconPath.</param>
        public void SetCursorIcon(string iconPath)
        {
            MethodInvoker br = delegate
            {
                Cursor.Current = new Cursor(iconPath);
            };
            _surface.Invoke(br);
        }

        #endregion

        /// <summary>
        /// Initializes a new WindowContoller class.
        /// </summary>
        /// <param name="renderTarget">The RenderTarget.</param>
        public WindowController(RenderTarget renderTarget)
        {
            _surface = (Form) Control.FromHandle(renderTarget.Handle);
            SetCursorVisibility(false);
            SetControlLayout(new SurfaceLayout(true, false, true));
            _surface.FormClosing += _surface_FormClosing;
        }

        /// <summary>
        /// Called, if the surface is closing.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        void _surface_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            SGL.Shutdown();
        }

        /// <summary>
        /// Fixes the size of the window.
        /// </summary>
        private void FixWindow()
        {
            MethodInvoker br = delegate
            {
                _surface.MaximumSize = _surface.Size;
                _surface.MinimumSize = _surface.Size;
            };
            _surface.Invoke(br);
        }
        /// <summary>
        /// Frees the window size.
        /// </summary>
        private void FreeWindow()
        {
            MethodInvoker br = delegate
            {
                _surface.MaximumSize = new Size(0, 0);
                _surface.MinimumSize = new Size(0, 0);
            };
            _surface.Invoke(br);
        }
        /// <summary>
        /// Indicating whether the surface is running in fullscreen.
        /// </summary>
        /// <returns>True if fullscreen is activated</returns>
        internal bool IsFullscreen()
        {
            return Screen.PrimaryScreen.Bounds.Width == (int)GetSize().X && Screen.PrimaryScreen.Bounds.Height == (int)GetSize().Y;
        }

        private readonly Form _surface;
    }
}
