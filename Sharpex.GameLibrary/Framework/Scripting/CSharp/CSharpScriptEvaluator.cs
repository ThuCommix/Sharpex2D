using System.Reflection;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Debug.Logging;

namespace SharpexGL.Framework.Scripting.CSharp
{
    public class CSharpScriptEvaluator : IScriptEvaluator
    {
        private readonly ScriptStorageBuffer _storageBuffer;

        /// <summary>
        /// A value indicating whether the compiled scripts gets buffered.
        /// </summary>
        public bool Buffering { set; get; }

        /// <summary>
        /// Initializes a new SharpScriptEvaluator.
        /// </summary>
        public CSharpScriptEvaluator()
        {
            //Register loading technique

            SGL.Components.Get<ContentManager>().Extend(new CSharpScriptLoader());
            _storageBuffer = new ScriptStorageBuffer();
        }

        /// <summary>
        /// Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        public void Evaluate(IScript script, params object[] objects)
        {
            if (script.GetType() != typeof(CSharpScript))
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
                    Log.Next("Used previously compiled script.", LogLevel.Info, LogMode.StandardOut);
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

            var fType = assembly.GetTypes()[0];
            var iType = fType.GetInterface("IScriptEntry");

            if (iType != null)
            {
                var scriptbase = (IScriptEntry)assembly.CreateInstance(fType.FullName);
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
