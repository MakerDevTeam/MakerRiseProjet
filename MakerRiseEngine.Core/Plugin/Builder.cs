﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection;
using Maker.RiseEngine.Core.Storage;

namespace Maker.RiseEngine.Core.Plugin
{

    public class BuildOutput
    {


        public BuildOutput(bool _Sucess, CompilerResults _Result)
        {
            Sucess = _Sucess;
            Result = _Result;
        }

        public bool Sucess;
        public CompilerResults Result;

    }

    public static class Builder
    {

        /// <summary>
        /// Compile an assembly from a source code file.
        /// </summary>
        /// <param name="SourcePath">Path of the source file.</param>
        /// <param name="OutputPath">Path of the output assemblie.</param>
        /// <param name="References">Liste of all references.</param>
        /// <returns>Compilation sucess.</returns>
        public static BuildOutput Build(string SourcePath, string OutputPath)
        {
            
            try
            {
                //Check if the file existe. return false went does't existe.
                if (!(System.IO.File.Exists(SourcePath)))
                {

                    return new BuildOutput(false, null);
                }

                //Reading the source file
                System.IO.StreamReader sr = new System.IO.StreamReader(SourcePath);
                string Code = sr.ReadToEnd().ToDosLineEnd();
                sr.Close();


                CodeDomProvider Provider = CodeDomProvider.CreateProvider("CSharp");

                //Setup compiler.

                #pragma warning disable CS0618 // Le type ou le membre est obsolète
                ICodeCompiler Compiler = Provider.CreateCompiler();
                #pragma warning restore CS0618 // Le type ou le membre est obsolète

                CompilerParameters Parameters = new CompilerParameters();
                Parameters.GenerateExecutable = false;
                Parameters.OutputAssembly = OutputPath;

                //getting references.
                foreach (Assembly Asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Parameters.ReferencedAssemblies.Add(Asm.Location);
                }

                //compiling assemblie.
                CompilerResults result = Compiler.CompileAssemblyFromSource(Parameters, Code);

                //Error handeling.
                if (result.Errors.Count == 0)
                {
                    //compilation sucess do nothing.
                    Debug.DebugLogs.WriteInLogs("[Plugin.Builder] Compilation success! '" + OutputPath + "'", Debug.LogType.Info);
                    return new BuildOutput(true, result);
                }
                else
                {
                    //Compilation failled. Catch and trow error to the user.
                    Debug.DebugLogs.WriteInLogs("Compilation failled! '" + OutputPath + "' with (" + result.Errors.Count + " ERROR!)", Debug.LogType.Error, "Plugin.Builder");

                    foreach (CompilerError Error in result.Errors)
                    {
                        if (Error.IsWarning)
                        {

                            Debug.DebugLogs.WriteInLogs("   Ln" + Error.Line + " '" + Error.ErrorText + "'", Debug.LogType.Warning);
                        }
                        else
                        {

                            Debug.DebugLogs.WriteInLogs("    Ln" + Error.Line + " '" + Error.ErrorText + "'", Debug.LogType.Error);
                        }


                    }

                    return new BuildOutput(false, result);
                }
            }
            catch (Exception ex)
            {

                //Catch exeption, write in logs.
                Debug.DebugLogs.WriteInLogs("Compilation failled!", Debug.LogType.Error, "Plugin.Builder");
                Debug.DebugLogs.WriteInLogs(ex.ToString(), Debug.LogType.Info, "Plugin.Builder");

                return new BuildOutput(false, null);
            }


        }

    }
}
