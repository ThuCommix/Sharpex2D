using System.CodeDom.Compiler;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.CSharp;
using SharpexGL.Framework.Debug.Logging;

namespace SharpexGL.Framework.Scripting.SharpScript
{
    public static class ScriptCompiler
    {
        /// <summary>
        /// Compiles the source to assembly.
        /// </summary>
        /// <param name="script">The SharpScript.</param>
        /// <returns>Assembly.</returns>
        public static Assembly CompileToAssembly(SharpScript script)
        {
            var cdProvider = new CSharpCodeProvider();
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
                    Log.Next("SharpScript ["+ script.Guid +"] -> " + error.ErrorText + "(Line " + error.Line + ")", LogLevel.Warning,
                        LogMode.StandardOut);
                }
                else
                {
                    Log.Next("SharpScript [" + script.Guid + "] -> " + error.ErrorText + "(Line " + error.Line + ")", LogLevel.Critical, LogMode.StandardOut);
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
