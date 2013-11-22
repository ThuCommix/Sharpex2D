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
            _surface.BeginInvoke(br);
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
            _surface.BeginInvoke(br);
        }

        /// <summary>
        /// Sets the Size of the GameWindow.
        /// </summary>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public void SetSize(int width, int height)
        {
            MethodInvoker br = delegate
            {
                _surface.ClientSize = new Size(width, height);
            };
            _surface.BeginInvoke(br);
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
            _surface.BeginInvoke(br);
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
            _surface.BeginInvoke(br);
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
            _surface.BeginInvoke(br);
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
        }

        private readonly Form _surface;
    }
}
