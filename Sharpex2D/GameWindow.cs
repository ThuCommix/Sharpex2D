// Copyright (c) 2012-2015 Sharpex2D - Kevin Scholz (ThuCommix)
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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Sharpex2D.Framework
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class GameWindow : IComponent, IDisposable
    {
        [Developer("ThuCommix", "developer@sharpex2d.de")]
        [TestState(TestState.Tested)]
        public enum Appearance
        {
            /// <summary>
            /// Windowed.
            /// </summary>
            Windowed,

            /// <summary>
            /// Fullscreen.
            /// </summary>
            Fullscreen
        }

        private readonly Form _systemWindow;
        private bool _allowUserResize;
        private bool _beginDeviceChange;
        private bool _isCursorVisible;
        private Appearance _targetAppearance;

        /// <summary>
        /// Initializes a new GameWindow class.
        /// </summary>
        /// <param name="hwnd">The WindowHandle.</param>
        private GameWindow(IntPtr hwnd)
        {
            _systemWindow = (Form) Control.FromHandle(hwnd);
            Handle = hwnd;
            _systemWindow.FormClosing += Closing;
            _systemWindow.Activated += Activated;
            _systemWindow.Deactivate += Deactivate;
            _systemWindow.LocationChanged += LocationChanged;
            _systemWindow.ClientSizeChanged += ClientSizeChangedHandler;
            Application.Idle += ApplicationIdleEvent;
            WindowAppearance = Appearance.Windowed;
            Title = Application.ProductName;
            MethodInvoker br = delegate
            {
                ScreenDeviceName = Screen.FromControl(_systemWindow).DeviceName;
                _systemWindow.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            };
            _systemWindow.Invoke(br);
            AllowUserResizing = false;
            ScreenDeviceManager = new ScreenDeviceManager();
        }

        /// <summary>
        /// A value indicating whether the cursor is visible.
        /// </summary>
        public bool IsCursorVisible
        {
            set
            {
                _isCursorVisible = value;
                if (value)
                    Cursor.Show();
                else
                    Cursor.Hide();
            }
            get { return _isCursorVisible; }
        }

        /// <summary>
        /// Gets the screen device manager.
        /// </summary>
        public ScreenDeviceManager ScreenDeviceManager { get; }

        /// <summary>
        /// A value indicating whether to allow the user to resize the game window.
        /// </summary>
        public bool AllowUserResizing
        {
            get { return _allowUserResize; }
            set
            {
                MethodInvoker br = delegate
                {
                    if (value)
                    {
                        _systemWindow.FormBorderStyle = FormBorderStyle.Sizable;
                        _systemWindow.MaximizeBox = true;
                    }
                    else
                    {
                        _systemWindow.FormBorderStyle = FormBorderStyle.FixedSingle;
                        _systemWindow.MaximizeBox = false;
                    }
                };
                _systemWindow.Invoke(br);
                _allowUserResize = value;
            }
        }

        /// <summary>
        /// Gets the screen dimensions of the game window's client rectangle.
        /// </summary>
        public Rectangle ClientBounds
        {
            get
            {
                Rectangle result = Rectangle.Empty;
                MethodInvoker br = delegate
                {
                    result = new Rectangle(_systemWindow.ClientRectangle.X, _systemWindow.ClientRectangle.Y,
                        _systemWindow.ClientRectangle.Width, _systemWindow.ClientRectangle.Height);
                };
                _systemWindow.Invoke(br);
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the size of the game window.
        /// </summary>
        public Vector2 ClientSize
        {
            get
            {
                var size = new Vector2();
                MethodInvoker br =
                    delegate { size = new Vector2(_systemWindow.ClientSize.Width, _systemWindow.ClientSize.Height); };
                _systemWindow.Invoke(br);
                return size;
            }
            set
            {
                MethodInvoker br = delegate { _systemWindow.ClientSize = new Size((int) value.X, (int) value.Y); };
                _systemWindow.Invoke(br);
            }
        }

        /// <summary>
        /// Gets the handle to the underlaying system window.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Gets the device name of the screen the window is currently in.
        /// </summary>
        public string ScreenDeviceName { get; private set; }

        /// <summary>
        /// Gets or sets the title of the window.
        /// </summary>
        public string Title
        {
            set
            {
                MethodInvoker br = delegate { _systemWindow.Text = value; };
                _systemWindow.Invoke(br);
            }
            get
            {
                string title = "";

                MethodInvoker br = delegate { title = _systemWindow.Text; };
                _systemWindow.Invoke(br);

                return title;
            }
        }

        /// <summary>
        /// A value indicating whether the window is focused.
        /// </summary>
        public bool IsFocused
        {
            get
            {
                bool flag = true;
                MethodInvoker br = delegate { flag = _systemWindow.Focused; };
                _systemWindow.Invoke(br);

                return flag;
            }
        }

        /// <summary>
        /// Gets the window appearance.
        /// </summary>
        public Appearance WindowAppearance { get; private set; }

        /// <summary>
        /// Gets the default window of this proccess.
        /// </summary>
        public static GameWindow Default
        {
            get
            {
                IntPtr hwnd = Process.GetCurrentProcess().MainWindowHandle;

                if (Control.FromHandle(hwnd) is Form)
                {
                    return new GameWindow(hwnd);
                }

                throw new InvalidOperationException("Could not get the window associated with the current process.");
            }
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Raises when the application is in idle mode.
        /// </summary>
        internal event EventHandler<EventArgs> ApplicationIdle;

        /// <summary>
        /// Raises when the application is idle.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void ApplicationIdleEvent(object sender, EventArgs e)
        {
            if (ApplicationIdle != null)
                ApplicationIdle(this, EventArgs.Empty);
        }

        /// <summary>
        /// Raised when the size of the window changed.
        /// </summary>
        public event EventHandler<EventArgs> ClientSizeChanged;

        /// <summary>
        /// Raised when the screen device changed.
        /// </summary>
        /// <remarks>Occurs when the window is moved from one display to another.</remarks>
        public event EventHandler<EventArgs> ScreenDeviceChanged;

        /// <summary>
        /// Raised when the window appearance changed.
        /// </summary>
        public event EventHandler<EventArgs> WindowAppearanceChanged;

        /// <summary>
        /// Raised when the window position changed.
        /// </summary>
        public event EventHandler<EventArgs> PositionChanged;

        /// <summary>
        /// Starts a device transition.
        /// </summary>
        /// <param name="apparance">The Appearance.</param>
        public void BeginScreenDeviceChange(Appearance apparance)
        {
            _beginDeviceChange = true;
            _targetAppearance = apparance;
        }

        /// <summary>
        /// Completes a device transition.
        /// </summary>
        public void EndScreenDeviceChange()
        {
            EndScreenDeviceChange(ScreenDeviceManager.Default);
        }

        /// <summary>
        /// Completes a device transition.
        /// </summary>
        /// <param name="screenDevice">The ScreenDevice.</param>
        public void EndScreenDeviceChange(ScreenDevice screenDevice)
        {
            EndScreenDeviceChange(screenDevice, (int) ClientSize.X, (int) ClientSize.Y);
        }

        /// <summary>
        /// Completes a device transition.
        /// </summary>
        /// <param name="screenDevice">The ScreenDevice.</param>
        /// <param name="width">The Width.</param>
        /// <param name="height">The Height.</param>
        public void EndScreenDeviceChange(ScreenDevice screenDevice, int width, int height)
        {
            if (!_beginDeviceChange)
            {
                throw new InvalidOperationException("BeginScreenDeviceChange must be called first in order to end it.");
            }

            Screen target = Screen.AllScreens.FirstOrDefault(screen => screenDevice.Name == screen.DeviceName);

            if (target == null)
            {
                throw new ArgumentException("Invalid screen device specified.");
            }

            MethodInvoker br = delegate
            {
                if (_targetAppearance == Appearance.Windowed)
                {
                    _systemWindow.WindowState = FormWindowState.Normal;
                    _systemWindow.FormBorderStyle = _allowUserResize
                        ? FormBorderStyle.Sizable
                        : FormBorderStyle.FixedSingle;
                    _systemWindow.ClientSize = new Size(width, height);

                    _systemWindow.Location = new Point(target.WorkingArea.Width/2 - width/2,
                        target.WorkingArea.Height/2 - height/2);
                }
                else
                {
                    _systemWindow.Location = new Point(target.Bounds.Left, target.Bounds.Top);
                    _systemWindow.FormBorderStyle = FormBorderStyle.None;
                    _systemWindow.WindowState = FormWindowState.Maximized;
                }

                if (WindowAppearance != _targetAppearance)
                {
                    WindowAppearance = _targetAppearance;
                    if (WindowAppearanceChanged != null)
                    {
                        WindowAppearanceChanged(this, EventArgs.Empty);
                    }
                }
            };
            _systemWindow.Invoke(br);
        }

        /// <summary>
        /// Raises if the system window size has changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void ClientSizeChangedHandler(object sender, EventArgs e)
        {
            if (ClientSizeChanged != null)
            {
                ClientSizeChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises if the system window position has changed.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void LocationChanged(object sender, EventArgs e)
        {
            string sdn = "";
            MethodInvoker br = delegate { sdn = Screen.FromControl(_systemWindow).DeviceName; };
            _systemWindow.Invoke(br);

            if (sdn != ScreenDeviceName)
            {
                ScreenDeviceName = sdn;
                if (ScreenDeviceChanged != null)
                {
                    ScreenDeviceChanged(this, EventArgs.Empty);
                }
            }

            if (PositionChanged != null)
            {
                PositionChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Raises if the system window is deactivated.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void Deactivate(object sender, EventArgs e)
        {
            GameHost.GameInstance.OnDeactivation();
        }

        /// <summary>
        /// Raises if the system window is activated.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void Activated(object sender, EventArgs e)
        {
            GameHost.GameInstance.OnActivation();
        }

        /// <summary>
        /// Raised if the system window is closing.
        /// </summary>
        /// <param name="sender">The Sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            GameHost.Shutdown();
        }

        /// <summary>
        /// Deconstructs the GameWindow class.
        /// </summary>
        ~GameWindow()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                MethodInvoker br = () => _systemWindow.Dispose();
                try
                {
                    _systemWindow.Invoke(br);
                }
                catch
                {
                    Logger.Instance.Warn("Already disposed.");
                }
            }
        }

        /// <summary>
        /// Creates a new game window based on the specified window handle.
        /// </summary>
        /// <param name="hwnd">The WindowHandle.</param>
        /// <returns>GameWindow.</returns>
        public static GameWindow FromHandle(IntPtr hwnd)
        {
            if (Control.FromHandle(hwnd) is Form)
            {
                return new GameWindow(hwnd);
            }

            throw new InvalidOperationException("The handle must be pointing to a valid window.");
        }

        /// <summary>
        /// Creates the game window underlaying resources.
        /// </summary>
        /// <returns>GameWindow.</returns>
        public static GameWindow CreateNew()
        {
            var surface = new Form();

            new Thread(() => Application.Run(surface)).Start();

            while (!surface.IsHandleCreated)
            {
            }

            IntPtr handle = IntPtr.Zero;

            MethodInvoker br = delegate { handle = surface.Handle; };
            surface.Invoke(br);

            return new GameWindow(handle);
        }
    }
}