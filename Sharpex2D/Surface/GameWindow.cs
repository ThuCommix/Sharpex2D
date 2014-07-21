// Copyright (c) 2012-2014 Sharpex2D - Kevin Scholz (ThuCommix)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the 'Software'), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Drawing;
using System.Windows.Forms;
using Sharpex2D.Math;
using Sharpex2D.Rendering;

namespace Sharpex2D.Surface
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
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

            MethodInvoker br = delegate { _surface.KeyPreview = true; };
            _surface.Invoke(br);
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
                SGL.GraphicsDevice.BackBuffer = new BackBuffer(value);

                if (ScreenSizeChanged != null)
                {
                    ScreenSizeChanged(this, EventArgs.Empty);
                }
            }
            get
            {
                var vector = new Vector2(0);

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
                var vector = new Vector2(0);

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
                if (value == _surfaceStyle)
                {
                    return;
                }

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

                if (FullscreenChanged != null)
                {
                    FullscreenChanged(this, EventArgs.Empty);
                }
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
        ///     Centers the Window.
        /// </summary>
        public void CenterWindow()
        {
            MethodInvoker br = delegate
            {
                _surface.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width/2 - _surface.Width/2,
                    Screen.PrimaryScreen.WorkingArea.Height/2 - _surface.Height/2);
            };
            _surface.Invoke(br);
        }

        /// <summary>
        ///     ScreenSizeChanged event.
        /// </summary>
        public event ScreenSizeEventHandler ScreenSizeChanged;

        /// <summary>
        ///     FullscreenChanged event.
        /// </summary>
        public event ScreenSizeEventHandler FullscreenChanged;

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