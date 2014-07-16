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

namespace Sharpex2D.Content
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Tested)]
    public class BatchProgressEventArgs : EventArgs
    {
        /// <summary>
        ///     Gets the ProgressPercentage.
        /// </summary>
        public int ProgressPercentage { internal set; get; }

        /// <summary>
        ///     Gets the amount of progrProcessedessed batches.
        /// </summary>
        public int Processed { internal set; get; }

        /// <summary>
        ///     Gets the Count.
        /// </summary>
        public int Count { internal set; get; }

        /// <summary>
        ///     A value indicating whether the batches are completed.
        /// </summary>
        public bool Completed { internal set; get; }

        /// <summary>
        ///     Gets the current batch.
        /// </summary>
        public IBatch Current { internal set; get; }

        /// <summary>
        ///     Gets the TotalBytes.
        /// </summary>
        public long TotalBytes { internal set; get; }

        /// <summary>
        ///     Gets the ProcessedBytes.
        /// </summary>
        public long ProcessedBytes { internal set; get; }
    }
}