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

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal enum ALSourceParameters
    {
        /// <summary>
        /// SourceState
        /// </summary>
        SourceState = 0x1010,

        /// <summary>
        /// BuffersQueued
        /// </summary>
        BuffersQueued = 0x1015,

        /// <summary>
        /// AllBuffersProcessed
        /// </summary>
        AllBuffersProcessed = 0x1016,

        /// <summary>
        /// Pitch
        /// </summary>
        Pitch = 0x1003,

        /// <summary>
        /// Position
        /// </summary>
        Position = 0x1004,

        /// <summary>
        /// Direction
        /// </summary>
        Direction = 0x1005,

        /// <summary>
        /// Velocity
        /// </summary>
        Velocity = 0x1006,

        /// <summary>
        /// Gain
        /// </summary>
        Gain = 0x100A,

        /// <summary>
        /// Min gain
        /// </summary>
        MinGain = 0x100D,

        /// <summary>
        /// Max gain
        /// </summary>
        MaxGain = 0x100E,

        /// <summary>
        /// Orientation
        /// </summary>
        Orientation = 0x100F,

        /// <summary>
        /// Max distance
        /// </summary>
        MaxDistance = 0x1023,

        /// <summary>
        /// Roll off factor
        /// </summary>
        RollOffFactor = 0x1021,

        /// <summary>
        /// Cone outer gain
        /// </summary>
        ConeOuterGain = 0x1022,

        /// <summary>
        /// Cone inner angle
        /// </summary>
        ConeInnerAngle = 0x1001,

        /// <summary>
        /// Cone outer angle
        /// </summary>
        ConeOuterAngle = 0x1002,

        /// <summary>
        /// Reference distance
        /// </summary>
        ReferenceDistance = 0x1020,

        /// <summary>
        /// Source relative
        /// </summary>
        SourceRelative = 514
    }
}
