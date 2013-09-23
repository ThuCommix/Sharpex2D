using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SharpexGL.Framework.Components;

namespace SharpexGL.Framework.Window
{
    public class GameWindowProvider : IComponent
    {
        /// <summary>
        /// Creates a new GameWindow.
        /// </summary>
        public void Create()
        {
            var threadCreate = new Thread(InternalCreate) {IsBackground = true};
            _flag = true;
            threadCreate.Start();
        }
        /// <summary>
        /// Represents the GameWindow.
        /// </summary>
        public IGameWindow GameWindow { private set; get; }

        /// <summary>
        /// Destroys the GameWindow.
        /// </summary>
        public void Destroy()
        {
            _flag = false;
            IsCreated = false;
        }

        ~GameWindowProvider()
        {
            Destroy();
        }

        private bool _flag;
        /// <summary>
        /// Determines if the GameWindow is created.
        /// </summary>
        public bool IsCreated { private set; get; }

        private void InternalCreate()
        {
            var surface = new Form
                {
                    Text = Application.ProductName,
                    Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox =false,
                    MinimizeBox=false
                };
            Cursor.Hide();
            GameWindow = new GameWindow(surface.Handle);
            surface.Show();
            surface.Activate();
            surface.Focus();
            SetActiveWindow(surface.Handle);
            IsCreated = true;
            while (_flag)
            {
                GameWindow.Update();
                Application.DoEvents();
            }
            GameWindow = null;
            IsCreated = false;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetActiveWindow(IntPtr hWnd);
    }
}
