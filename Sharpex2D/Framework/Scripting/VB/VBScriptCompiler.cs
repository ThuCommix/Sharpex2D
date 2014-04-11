using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Sharpex2D.Framework.Debug.Logging;

namespace Sharpex2D.Framework.Scripting.VB
{
    internal static class VBScriptCompiler
    {
        /// <summary>
        /// Compiles the source to assembly.
        /// </summary>
        /// <param name="script">The SharpScript.</param>
        /// <returns>Assembly.</returns>
        public static Assembly CompileToAssembly(VBScript script)
        {
            var cdProvider = new VBCodeProvider();
            var param = new CompilerParameters();
            param.ReferencedAssemblies.Add("System.dll");
            param.ReferencedAssemblies.Add("Sharpex.GameLibrary.dll");
            param.ReferencedAssemblies.Add(Application.ExecutablePath);
            param.GenerateExecutable = false;

            var result = cdProvider.CompileAssemblyFromSource(param, script.Content);

            var flag = false;

            foreach (CompilerError error in result.Errors)
            {
                if (error.IsWarning)
                {
                    Log.Next("VBScript [" + script.Guid + "] -> " + error.ErrorText + "(Line " + error.Line + ")", LogLevel.Warning,
                        LogMode.StandardOut);
                }
                else
                {
                    Log.Next("VBScript [" + script.Guid + "] -> " + error.ErrorText + "(Line " + error.Line + ")", LogLevel.Critical, LogMode.StandardOut);
                    flag = true;
                }

            }

            if (flag)
            {
                throw new ScriptException("Critical error while compiling script.");
            }

            return result.CompiledAssembly;
        }
    }
}
