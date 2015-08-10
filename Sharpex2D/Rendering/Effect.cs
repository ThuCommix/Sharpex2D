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
using Sharpex2D.Framework.Content;

namespace Sharpex2D.Framework.Rendering
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class Effect : IContent, IDisposable
    {
        internal readonly IEffect EffectInstance;

        /// <summary>
        /// Initializes a new Effect class.
        /// </summary>
        /// <param name="effect">The Effect.</param>
        internal Effect(IEffect effect)
        {
            EffectInstance = effect;
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <typeparam name="T">The DataType.</typeparam>
        /// <param name="identifier">The Identifier.</param>
        /// <param name="value">The Value.</param>
        public void SetData<T>(string identifier, T value) where T : struct
        {
            EffectInstance.SetData(identifier, value);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <typeparam name="T">The DataType.</typeparam>
        /// <param name="identifier">The Identifier.</param>
        /// <returns>Value.</returns>
        public T GetData<T>(string identifier) where T : struct
        {
            return EffectInstance.GetData<T>(identifier);
        }

        /// <summary>
        /// Compiles the effect.
        /// </summary>
        internal void Compile()
        {
            EffectInstance.Compile();
        }

        /// <summary>
        /// Deconstructs the Effect class.
        /// </summary>
        ~Effect()
        {
            Dispose(false);
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
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
                EffectInstance.Dispose();
        }
    }
}
