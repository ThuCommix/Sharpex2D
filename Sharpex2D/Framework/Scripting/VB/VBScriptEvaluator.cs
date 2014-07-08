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
using System.Reflection;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Scripting.VB
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [TestState(TestState.Untested)]
    public class VBScriptEvaluator : IScriptEvaluator<VBScript>
    {
        private readonly ScriptStorageBuffer _storageBuffer;

        /// <summary>
        ///     Initializes a new SharpScriptEvaluator.
        /// </summary>
        public VBScriptEvaluator()
        {
            //Register loading technique

            _storageBuffer = new ScriptStorageBuffer();
        }

        /// <summary>
        ///     A value indicating whether the compiled scripts gets buffered.
        /// </summary>
        public bool Buffering { set; get; }

        /// <summary>
        ///     Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        public void Evaluate(VBScript script, params object[] objects)
        {
            //check if the script was compiled previously

            Assembly assembly;

            if (Buffering)
            {
                if (_storageBuffer.Exists(script.Guid))
                {
                    assembly = _storageBuffer[script.Guid];
                    LogManager.GetClassLogger().Info("Used previously compiled script.");
                }
                else
                {
                    assembly = VBScriptCompiler.CompileToAssembly(script);
                    if (!_storageBuffer.Exists(script.Guid))
                    {
                        _storageBuffer.Add(script.Guid, assembly);
                    }
                }
            }
            else
            {
                assembly = VBScriptCompiler.CompileToAssembly(script);
            }

            Type fType = assembly.GetTypes()[0];
            Type iType = fType.GetInterface("IScriptEntry");

            if (iType != null)
            {
                var scriptbase = (IScriptEntry) assembly.CreateInstance(fType.FullName);
                if (scriptbase != null)
                {
                    scriptbase.Main(objects);
                }
                else
                {
                    throw new ScriptException("IScriptEntry interface not found.");
                }
            }
            else
            {
                throw new ScriptException("IScriptEntry interface not found.");
            }
        }
    }
}