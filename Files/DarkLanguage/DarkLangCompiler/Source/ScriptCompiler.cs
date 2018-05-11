using System;
using DarkLanguage.DarkLangCompiler.Classes;
using System.IO;
using DarkLanguage.Bases.Source;

namespace DarkLanguage.DarkLangCompiler.Source
{
	public class ScriptCompiler
	{
		public static CompilationOutput Output = null;
		public static CompiledScript CompileScript(string script,string name = "")
		{
			Output = new CompilationOutput();
			script = script.Replace("\r\n","");
			var compiled = new CompiledScript(script);
			compiled.Name = name;
			if(!CODE_INFO.Scripts.Contains(compiled))
			{
				CODE_INFO.Scripts.Add(compiled);
			}

			return compiled;
		}
		public static string GetScriptCode(string fileName)
		{
			fileName += ".dls";
			StreamReader read = new StreamReader(fileName);
			return read.ReadToEnd();
		}
	}
}
