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

namespace Sharpex2D.Framework.Audio.OpenAL
{
    internal class OpenALSourcePool : IDisposable
    {
        private readonly OpenALContext _context;
        private readonly List<OpenALSource> _sources;

        /// <summary>
        /// Initializes a new OpenALSourcePool class.
        /// </summary>
        /// <param name="context">The OpenALContext.</param>
        public OpenALSourcePool(OpenALContext context)
        {
            _context = context;
            _sources = new List<OpenALSource>();
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
        /// Requests a Source from the source pool.
        /// </summary>
        /// <returns>OpenALSource.</returns>
        public OpenALSource RequestSource()
        {
            _context.MakeCurrent();
            var sources = new uint[1];
            OpenALInterops.alGenSources(1, sources);

            var source = new OpenALSource(sources[0]);
            _sources.Add(source);
            return source;
        }

        /// <summary>
        /// Frees a source.
        /// </summary>
        /// <param name="source">The OpenALSource.</param>
        public void FreeSource(OpenALSource source)
        {
            if (_sources.Contains(source))
            {
                _sources.Remove(source);
                if (source.SourceId != 0)
                {
                    _context.MakeCurrent();
                    var sources = new uint[1];
                    sources[0] = source.SourceId;
                    OpenALInterops.alDeleteSources(1, sources);
                    source.Reset();
                }
            }
        }

        /// <summary>
        /// Deconstructs the OpenALSourcePool class.
        /// </summary>
        ~OpenALSourcePool()
        {
            Dispose(false);
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        /// <param name="disposing">The disposing state.</param>
        protected virtual void Dispose(bool disposing)
        {
            _context.MakeCurrent();

            foreach (OpenALSource source in _sources)
            {
                if (source != null && source.SourceId != 0)
                {
                    var sources = new uint[1];
                    sources[0] = source.SourceId;
                    OpenALInterops.alDeleteSources(1, sources);
                    source.Reset();
                }
#if DEBUG
                Logger.Instance.Debug("Source automatically disposed.");
#endif
            }

            if (disposing)
            {
                _sources.Clear();
            }
        }
    }
}
