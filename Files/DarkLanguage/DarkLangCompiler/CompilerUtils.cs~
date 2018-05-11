using System;
using DarkLanguage.DarkLangCompiler.Source;
using DarkLanguage.Classes;
using DarkLanguage.Bases.Source;
using System.IO;

namespace DarkLanguage.DarkLangCompiler
{
	public static class CompilerUtils
	{
		public static void Compile(params string[] scripts)
		{
			foreach(var script in scripts)
			{
				string name = script.GetHashCode()+"";
				var fl = script.Split(new string[]{"\r\n"},StringSplitOptions.None)[0];
				if(!string.IsNullOrEmpty(fl))
				{
					name = StringUtils.GetInside(fl,"{","}");
				}
				ScriptCompiler.CompileScript(script,name);
			}
		}
		public static void SaveAssembly(string assemblyName = null)
		{
			if(string.IsNullOrEmpty(assemblyName))
			{
				assemblyName = Path.GetRandomFileName();
			}
			string path = $"../Assembly/{assemblyName}";
			if(File.Exists(path))
			{
				File.Delete(path);
			}
			CODE_INFO.SaveAssembly(path);
		}
		public static void CustomSaveAssembly(string path)
		{
			if(File.Exists(path))
			{
				File.Delete(path);
			}
			CODE_INFO.SaveAssembly(path);
		}
		public static void SaveToApplication(string path)
		{

		}
	}
}
