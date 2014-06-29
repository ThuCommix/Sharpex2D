using System;
using System.Drawing;
using System.Windows.Forms;
using Sharpex2D.Framework.Math;
using Sharpex2D.Framework.Rendering;

namespace Sharpex2D.Framework.Surface
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Tested)]
    public class GameWindow : IDisposable
    {
        #region IDisposable Implementation

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the object.
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (disposing)
                {
                    MethodInvoker br = () => _surface.Dispose();
                    _surface.Invoke(br);
                }
            }
        }

        #endregion

        private readonly Form _surface;
        private bool _cursorVisibility = true;
        private bool _isDisposed;
        private SurfaceLayout _surfaceLayout;
        private SurfaceStyle _surfaceStyle;

        /// <summary>
        ///     Initializes a new GameWindow class.
        /// </summary>
        /// <param name="handle">The Handle.</param>
        internal GameWindow(IntPtr handle)
        {
            _surface = (Form) Control.FromHandle(handle);
            _surfaceStyle = SurfaceStyle.Normal;
            _surfaceLayout = new SurfaceLayout(true, false, true);
            CursorVisibility = false;
            _surface.FormClosing += _surface_FormClosing;
            _surface.Activated += _surface_Activated;
            _surface.Deactivate += _surface_Deactivate;
        }

        /// <summary>
        ///     Gets or sets the Title.
        /// </summary>
        public string Title
        {
            set
            {
                MethodInvoker br = delegate { _surface.Text = value; };
                _surface.Invoke(br);
            }
            get
            {
                string title = "";

                MethodInvoker br = delegate { title = _surface.Text; };
                _surface.Invoke(br);

                return title;
            }
        }

        /// <summary>
        ///     Gets or sets the Icon.
        /// </summary>
        public Icon Icon
        {
            set
            {
                MethodInvoker br = delegate { _surface.Icon = value; };
                _surface.Invoke(br);
            }
            get
            {
                Icon icon = null;

                MethodInvoker br = delegate { icon = _surface.Icon; };
                _surface.Invoke(br);

                return icon;
            }
        }

        /// <summary>
        ///     Gets or sets the Size.
        /// </summary>
        public Vector2 Size
        {
            set
            {
                FreeWindow();
                MethodInvoker br = delegate { _surface.ClientSize = new Size((int) value.X, (int) value.Y); };
                _surface.Invoke(br);
                FixWindow();

                //notify the GraphicsDevice
                SGL.Components.Get<GraphicsDevice>().BackBuffer = new BackBuffer(value);
            }
            get
            {
                Vector2 vector = null;

                MethodInvoker br = delegate { vector = new Vector2(_surface.Size.Width, _surface.Size.Height); };
                _surface.Invoke(br);

                return vector;
            }
        }

        /// <summary>
        ///     Gets or sets the Position.
        /// </summary>
        public Vector2 Position
        {
            set
            {
                MethodInvoker br = delegate { _surface.Location = new Point((int) value.X, (int) value.Y); };
                _surface.Invoke(br);
            }
            get
            {
                Vector2 vector = null;

                MethodInvoker br = delegate { vector = new Vector2(_surface.Location.X, _surface.Location.Y); };
                _surface.Invoke(br);

                return vector;
            }
        }

        /// <summary>
        ///     Gets or sets the CursorVisibility.
        /// </summary>
        public bool CursorVisibility
        {
            get { return _cursorVisibility; }
            set
            {
                _cursorVisibility = value;
                if (value)
                {
                    Cursor.Show();
                }
                else
                {
                    Cursor.Hide();
                }
            }
        }

        /// <summary>
        ///     Gets or sets the SurfaceStyle.
        /// </summary>
        public SurfaceStyle SurfaceStyle
        {
            get { return _surfaceStyle; }
            set
            {
                FreeWindow();
                MethodInvoker br = delegate
                {
                    if (value == SurfaceStyle.Fullscreen)
                    {
                        _surface.FormBorderStyle = FormBorderStyle.None;
                        _surface.Location = new Point(0, 0);
                        _surface.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                        IsFullscreen = true;
                    }
                    else
                    {
                        _surface.FormBorderStyle = FormBorderStyle.Sizable;
                        _surface.ClientSize = new Size(SGL.GraphicsDevice.BackBuffer.Width,
                            SGL.GraphicsDevice.BackBuffer.Height);
                        _surface.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - _surface.Width)/2,
                            (Screen.PrimaryScreen.WorkingArea.Height - _surface.Height)/2);
                        IsFullscreen = false;
                    }
                };
                _surface.Invoke(br);

                _surfaceStyle = value;

                FixWindow();
            }
        }

        /// <summary>
        ///     Gets or sets the SurfaceLayout.
        /// </summary>
        public SurfaceLayout SurfaceLayout
        {
            get { return _surfaceLayout; }
            set
            {
                MethodInvoker br = delegate
                {
                    _surface.MaximizeBox = value.CanMaximize;
                    _surface.MinimizeBox = value.CanMinimize;
                    _surface.ControlBox = value.CanClose;
                };
                _surface.Invoke(br);

                _surfaceLayout = value;
            }
        }

        /// <summary>
        ///     A value indicating whether the window is fullscreened.
        /// </summary>
        public bool IsFullscreen { private set; get; }

        /// <summary>
        ///     A value indicating whether the window is active.
        /// </summary>
        public bool IsActive
        {
            get
            {
                bool flag = true;
                MethodInvoker br = delegate { flag = _surface.Focused; };
                _surface.Invoke(br);

                return flag;
            }
        }

        /// <summary>
        ///     Sets the CursorIcon.
        /// </summary>
        /// <param name="path">The Path.</param>
        public void SetCursorIcon(string path)
        {
            MethodInvoker br = delegate { Cursor.Current = new Cursor(path); };
            _surface.Invoke(br);
        }

        /// <summary>
        ///     Fixes the size of the window.
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
        ///     Frees the window size.
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
        ///     Deactivate Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _surface_Deactivate(object sender, EventArgs e)
        {
            SGL.GameInstance.OnDeactivation();
        }

        /// <summary>
        ///     Activate Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _surface_Activated(object sender, EventArgs e)
        {
            SGL.GameInstance.OnActivation();
        }

        /// <summary>
        ///     FormClosing Event.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void _surface_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            SGL.Shutdown();
        }
    }
}