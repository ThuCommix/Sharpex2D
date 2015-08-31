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
using System.Collections.Generic;

namespace Sharpex2D.Framework.Input
{
    public class JoystickState
    {
        /// <summary>
        /// Initializes a new JoystickState class.
        /// </summary>
        /// <param name="x">The X.</param>
        /// <param name="y">The Y.</param>
        /// <param name="z">The Z.</param>
        /// <param name="r">The R.</param>
        /// <param name="u">The U.</param>
        /// <param name="v">The V.</param>
        /// <param name="pointOfView">The PointOfView.</param>
        /// <param name="buttonStates">The ButtonStates.</param>
        internal JoystickState(uint x, uint y, uint z, uint r, uint u, uint v, PointOfView pointOfView,
            Dictionary<int, bool> buttonStates)
        {
            X = x;
            Y = y;
            Z = z;
            R = r;
            U = u;
            V = v;
            PointOfView = pointOfView;

            if (buttonStates.Count != 32)
            {
                throw new ArgumentException("ButtonStates need 32 entries to be accepted.");
            }

            Button1 = new JoystickButton(buttonStates[0]);
            Button2 = new JoystickButton(buttonStates[1]);
            Button3 = new JoystickButton(buttonStates[2]);
            Button4 = new JoystickButton(buttonStates[3]);
            Button5 = new JoystickButton(buttonStates[4]);
            Button6 = new JoystickButton(buttonStates[5]);
            Button7 = new JoystickButton(buttonStates[6]);
            Button8 = new JoystickButton(buttonStates[7]);
            Button9 = new JoystickButton(buttonStates[8]);
            Button10 = new JoystickButton(buttonStates[9]);
            Button11 = new JoystickButton(buttonStates[10]);
            Button12 = new JoystickButton(buttonStates[11]);
            Button13 = new JoystickButton(buttonStates[12]);
            Button14 = new JoystickButton(buttonStates[13]);
            Button15 = new JoystickButton(buttonStates[14]);
            Button16 = new JoystickButton(buttonStates[15]);
            Button17 = new JoystickButton(buttonStates[16]);
            Button18 = new JoystickButton(buttonStates[17]);
            Button19 = new JoystickButton(buttonStates[18]);
            Button20 = new JoystickButton(buttonStates[19]);
            Button21 = new JoystickButton(buttonStates[20]);
            Button22 = new JoystickButton(buttonStates[21]);
            Button23 = new JoystickButton(buttonStates[22]);
            Button24 = new JoystickButton(buttonStates[23]);
            Button25 = new JoystickButton(buttonStates[24]);
            Button26 = new JoystickButton(buttonStates[25]);
            Button27 = new JoystickButton(buttonStates[26]);
            Button28 = new JoystickButton(buttonStates[27]);
            Button29 = new JoystickButton(buttonStates[28]);
            Button30 = new JoystickButton(buttonStates[29]);
            Button31 = new JoystickButton(buttonStates[30]);
            Button32 = new JoystickButton(buttonStates[31]);
        }

        /// <summary>
        /// Gets the JoystickButton1.
        /// </summary>
        public JoystickButton Button1 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton2.
        /// </summary>
        public JoystickButton Button2 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton3.
        /// </summary>
        public JoystickButton Button3 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton4.
        /// </summary>
        public JoystickButton Button4 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton5.
        /// </summary>
        public JoystickButton Button5 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton6.
        /// </summary>
        public JoystickButton Button6 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton7.
        /// </summary>
        public JoystickButton Button7 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton8.
        /// </summary>
        public JoystickButton Button8 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton9.
        /// </summary>
        public JoystickButton Button9 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton10.
        /// </summary>
        public JoystickButton Button10 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton11.
        /// </summary>
        public JoystickButton Button11 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton12.
        /// </summary>
        public JoystickButton Button12 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton13.
        /// </summary>
        public JoystickButton Button13 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton14.
        /// </summary>
        public JoystickButton Button14 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton15.
        /// </summary>
        public JoystickButton Button15 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton16.
        /// </summary>
        public JoystickButton Button16 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton17.
        /// </summary>
        public JoystickButton Button17 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton18.
        /// </summary>
        public JoystickButton Button18 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton19.
        /// </summary>
        public JoystickButton Button19 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton20.
        /// </summary>
        public JoystickButton Button20 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton21.
        /// </summary>
        public JoystickButton Button21 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton22.
        /// </summary>
        public JoystickButton Button22 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton23.
        /// </summary>
        public JoystickButton Button23 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton24.
        /// </summary>
        public JoystickButton Button24 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton25.
        /// </summary>
        public JoystickButton Button25 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton26.
        /// </summary>
        public JoystickButton Button26 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton27.
        /// </summary>
        public JoystickButton Button27 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton28.
        /// </summary>
        public JoystickButton Button28 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton29.
        /// </summary>
        public JoystickButton Button29 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton30.
        /// </summary>
        public JoystickButton Button30 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton31.
        /// </summary>
        public JoystickButton Button31 { private set; get; }

        /// <summary>
        /// Gets the JoystickButton32.
        /// </summary>
        public JoystickButton Button32 { private set; get; }

        /// <summary>
        /// The first axis.
        /// </summary>
        public uint X { private set; get; }

        /// <summary>
        /// The second axis.
        /// </summary>
        public uint Y { private set; get; }

        /// <summary>
        /// The third axis.
        /// </summary>
        public uint Z { private set; get; }

        /// <summary>
        /// The fourth axis.
        /// </summary>
        public uint R { private set; get; }

        /// <summary>
        /// The fifth axis.
        /// </summary>
        public uint U { private set; get; }

        /// <summary>
        /// The sixth axis.
        /// </summary>
        public uint V { private set; get; }

        /// <summary>
        /// Gets the PointOfView.
        /// </summary>
        public PointOfView PointOfView { private set; get; }
    }
}
