using System.Reflection;
using SharpexGL.Framework.Content;
using SharpexGL.Framework.Debug.Logging;

namespace SharpexGL.Framework.Scripting.SharpScript
{
    public class SharpScriptEvaluator : IScriptEvaluator
    {
        private readonly ScriptStorageBuffer _storageBuffer;

        /// <summary>
        /// Initializes a new SharpScriptEvaluator.
        /// </summary>
        public SharpScriptEvaluator()
        {
            //Register loading technique

            SGL.Components.Get<ContentManager>().Extend(new SharpScriptLoader());
            _storageBuffer = new ScriptStorageBuffer();
        }

        /// <summary>
        /// Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        public void Evaluate(IScript script, params object[] objects)
        {
            if (script.GetType() != typeof(SharpScript))
            {
                throw new ScriptException("The given script does not match the SharpScript sheme.");
            }

            var sharpScript = script as SharpScript;

            //check if the script was compiled previously

            Assembly assembly;

            if (_storageBuffer.Exists(script.Guid))
            {
                assembly = _storageBuffer[script.Guid];
                Log.Next("Used previously compiled script.", LogLevel.Info, LogMode.StandardOut);
            }
            else
            {
                assembly = ScriptCompiler.CompileToAssembly(sharpScript);
                if (!_storageBuffer.Exists(script.Guid))
                {
                    _storageBuffer.Add(script.Guid, assembly);
                }
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
