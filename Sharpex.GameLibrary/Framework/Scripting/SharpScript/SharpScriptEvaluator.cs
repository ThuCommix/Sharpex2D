using SharpexGL.Framework.Content;

namespace SharpexGL.Framework.Scripting.SharpScript
{
    public class SharpScriptEvaluator : IScriptEvaluator
    {
        /// <summary>
        /// Evaluate the script content.
        /// </summary>
        /// <param name="script">The Script.</param>
        /// <param name="objects">The Objects.</param>
        public void Evaluate(IScript script, params object[] objects)
        {
            if (script.GetType() != typeof (SharpScript))
            {
                throw new ScriptException("The given script does not match the SharpScript sheme.");
            }

            var sharpScript = script as SharpScript;

            var assembly = ScriptCompiler.CompileToAssembly(sharpScript);

            var fType = assembly.GetTypes()[0];
            var iType = fType.GetInterface("IScriptEntry");

            if (iType != null)
            {
                var scriptbase = (IScriptEntry) assembly.CreateInstance(fType.FullName);
                if (scriptbase != null) scriptbase.Main(objects);
            }
            else
            {
                throw new ScriptException("IScriptEntry interface not found.");
            }
        }
        /// <summary>
        /// Initializes a new SharpScriptEvaluator.
        /// </summary>
        public SharpScriptEvaluator()
        {
            //Register loading technique

            SGL.Components.Get<ContentManager>().Extend(new SharpScriptLoader());
        }
    }
}
