using System;
using System.Reflection;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Scripting.CSharp
{
    [Developer("ThuCommix", "developer@sharpex2d.de")]
    [Copyright("©Sharpex2D 2013 - 2014")]
    [TestState(TestState.Untested)]
    public class CSharpScriptEvaluator : IScriptEvaluator
    {
        private readonly ScriptStorageBuffer _storageBuffer;

        /// <summary>
        ///     Initializes a new SharpScriptEvaluator.
        /// </summary>
        public CSharpScriptEvaluator()
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
        public void Evaluate(IScript script, params object[] objects)
        {
            if (script.GetType() != typeof (CSharpScript))
            {
                throw new ScriptException("The given script does not match the CSharpScript sheme.");
            }

            var sharpScript = script as CSharpScript;

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
                    assembly = CSharpScriptCompiler.CompileToAssembly(sharpScript);
                    if (!_storageBuffer.Exists(script.Guid))
                    {
                        _storageBuffer.Add(script.Guid, assembly);
                    }
                }
            }
            else
            {
                assembly = CSharpScriptCompiler.CompileToAssembly(sharpScript);
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