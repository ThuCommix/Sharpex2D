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

using Sharpex2D.Input.JoystickApi;

namespace Sharpex2D.Input
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class JoystickState : IInputState
    {
        /// <summary>
        ///     Initializes a new JoystickState class.
        /// </summary>
        /// <param name="joyInfoEx">The JoyInfoEx.</param>
        internal JoystickState(JoyInfoEx joyInfoEx)
        {
            PressedButtons = joyInfoEx.dwButtonNumber;
            PointOfView = new PointOfView(joyInfoEx.dwPOV);
            X = joyInfoEx.dwXpos;
            Y = joyInfoEx.dwYpos;
            Z = joyInfoEx.dwZpos;
            R = joyInfoEx.dwRpos;
            U = joyInfoEx.dwUpos;
            V = joyInfoEx.dwVpos;
            Button1 = JoystickButton.FromDwButtons(1, joyInfoEx.dwButtons);
            Button2 = JoystickButton.FromDwButtons(2, joyInfoEx.dwButtons);
            Button3 = JoystickButton.FromDwButtons(3, joyInfoEx.dwButtons);
            Button4 = JoystickButton.FromDwButtons(4, joyInfoEx.dwButtons);
            Button5 = JoystickButton.FromDwButtons(5, joyInfoEx.dwButtons);
            Button6 = JoystickButton.FromDwButtons(6, joyInfoEx.dwButtons);
            Button7 = JoystickButton.FromDwButtons(7, joyInfoEx.dwButtons);
            Button8 = JoystickButton.FromDwButtons(8, joyInfoEx.dwButtons);
            Button9 = JoystickButton.FromDwButtons(9, joyInfoEx.dwButtons);
            Button10 = JoystickButton.FromDwButtons(10, joyInfoEx.dwButtons);
            Button11 = JoystickButton.FromDwButtons(11, joyInfoEx.dwButtons);
            Button12 = JoystickButton.FromDwButtons(12, joyInfoEx.dwButtons);
            Button13 = JoystickButton.FromDwButtons(13, joyInfoEx.dwButtons);
            Button14 = JoystickButton.FromDwButtons(14, joyInfoEx.dwButtons);
            Button15 = JoystickButton.FromDwButtons(15, joyInfoEx.dwButtons);
            Button16 = JoystickButton.FromDwButtons(16, joyInfoEx.dwButtons);
            Button17 = JoystickButton.FromDwButtons(17, joyInfoEx.dwButtons);
            Button18 = JoystickButton.FromDwButtons(18, joyInfoEx.dwButtons);
            Button19 = JoystickButton.FromDwButtons(19, joyInfoEx.dwButtons);
            Button20 = JoystickButton.FromDwButtons(20, joyInfoEx.dwButtons);
            Button21 = JoystickButton.FromDwButtons(21, joyInfoEx.dwButtons);
            Button22 = JoystickButton.FromDwButtons(22, joyInfoEx.dwButtons);
            Button23 = JoystickButton.FromDwButtons(23, joyInfoEx.dwButtons);
            Button24 = JoystickButton.FromDwButtons(24, joyInfoEx.dwButtons);
            Button25 = JoystickButton.FromDwButtons(25, joyInfoEx.dwButtons);
            Button26 = JoystickButton.FromDwButtons(26, joyInfoEx.dwButtons);
            Button27 = JoystickButton.FromDwButtons(27, joyInfoEx.dwButtons);
            Button28 = JoystickButton.FromDwButtons(28, joyInfoEx.dwButtons);
            Button29 = JoystickButton.FromDwButtons(29, joyInfoEx.dwButtons);
            Button30 = JoystickButton.FromDwButtons(30, joyInfoEx.dwButtons);
            Button31 = JoystickButton.FromDwButtons(31, joyInfoEx.dwButtons);
            Button32 = JoystickButton.FromDwButtons(32, joyInfoEx.dwButtons);
        }

        /// <summary>
        ///     Gets the JoystickButton1.
        /// </summary>
        public JoystickButton Button1 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton2.
        /// </summary>
        public JoystickButton Button2 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton3.
        /// </summary>
        public JoystickButton Button3 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton4.
        /// </summary>
        public JoystickButton Button4 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton5.
        /// </summary>
        public JoystickButton Button5 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton6.
        /// </summary>
        public JoystickButton Button6 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton7.
        /// </summary>
        public JoystickButton Button7 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton8.
        /// </summary>
        public JoystickButton Button8 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton9.
        /// </summary>
        public JoystickButton Button9 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton10.
        /// </summary>
        public JoystickButton Button10 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton11.
        /// </summary>
        public JoystickButton Button11 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton12.
        /// </summary>
        public JoystickButton Button12 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton13.
        /// </summary>
        public JoystickButton Button13 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton14.
        /// </summary>
        public JoystickButton Button14 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton15.
        /// </summary>
        public JoystickButton Button15 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton16.
        /// </summary>
        public JoystickButton Button16 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton17.
        /// </summary>
        public JoystickButton Button17 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton18.
        /// </summary>
        public JoystickButton Button18 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton19.
        /// </summary>
        public JoystickButton Button19 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton20.
        /// </summary>
        public JoystickButton Button20 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton21.
        /// </summary>
        public JoystickButton Button21 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton22.
        /// </summary>
        public JoystickButton Button22 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton23.
        /// </summary>
        public JoystickButton Button23 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton24.
        /// </summary>
        public JoystickButton Button24 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton25.
        /// </summary>
        public JoystickButton Button25 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton26.
        /// </summary>
        public JoystickButton Button26 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton27.
        /// </summary>
        public JoystickButton Button27 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton28.
        /// </summary>
        public JoystickButton Button28 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton29.
        /// </summary>
        public JoystickButton Button29 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton30.
        /// </summary>
        public JoystickButton Button30 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton31.
        /// </summary>
        public JoystickButton Button31 { private set; get; }

        /// <summary>
        ///     Gets the JoystickButton32.
        /// </summary>
        public JoystickButton Button32 { private set; get; }

        /// <summary>
        ///     The first axis.
        /// </summary>
        public uint X { private set; get; }

        /// <summary>
        ///     The second axis.
        /// </summary>
        public uint Y { private set; get; }

        /// <summary>
        ///     The third axis.
        /// </summary>
        public uint Z { private set; get; }

        /// <summary>
        ///     The fourth axis.
        /// </summary>
        public uint R { private set; get; }

        /// <summary>
        ///     The fifth axis.
        /// </summary>
        public uint U { private set; get; }

        /// <summary>
        ///     The sixth axis.
        /// </summary>
        public uint V { private set; get; }

        /// <summary>
        ///     Gets the PointOfView.
        /// </summary>
        public PointOfView PointOfView { private set; get; }

        /// <summary>
        ///     Gets the number of pressed buttons.
        /// </summary>
        public uint PressedButtons { private set; get; }

        /// <summary>
        ///     Gets an empty JoystickState.
        /// </summary>
        internal static JoystickState Empty
        {
            get { return new JoystickState(new JoyInfoEx()); }
        }
    }
}