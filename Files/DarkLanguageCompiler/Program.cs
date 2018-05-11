using System;
using System.IO;
using System.Collections.Generic;
using DarkLanguageCompiler.Source;
using DarkLanguage.Bases.Source;
using DarkLanguage.DarkLangCompiler.Source;

namespace DarkLanguageCompiler
{
	public static class MainClass
	{
		public static string[] GetScripts(string[] files)
		{
			List<string> scripts = new List<string>();
			foreach(var arg in files)
			{
				if(File.Exists(arg))
				{
					var script = File.ReadAllText(arg);
					scripts.Add(script);
				}
				if(Directory.Exists(arg))
				{
					//Console.WriteLine(arg);
					//Console.ReadLine();
					var fls = Directory.GetFiles(arg,".",SearchOption.AllDirectories);
					foreach(var file in fls)
					{
						var script = File.ReadAllText(file);
						scripts.Add(script);
					}
				}
			}
			return scripts.ToArray();
		}
		public static void Main (string [] args)
		{
			var scripts = GetScripts(args);
			if(args.Length > 0)
			{
				//Console.WriteLine("Digit The Name Of Your Assembly");
				//Console.WriteLine("If null,a random assembly name has to be generated");
				//string assemblyName = Console.ReadLine();
				string assemblyName = Path.GetFileNameWithoutExtension(args[0]);
				Console.WriteLine ($"Compiling...[{args.Length}]");
				Compiler.Compile(scripts);
				Console.WriteLine($"{DarkLanguage.Bases.Source.CODE_EXEC.TotalFieldCount} Fields,");
				Console.WriteLine($"{DarkLanguage.Bases.Source.CODE_INFO.Scripts.Count} Scripts.");
				Console.WriteLine("//////////////ERRORS//////////////");
				//foreach(var log in ScriptCompiler.Output)
				//{
				//	Console.WriteLine(log);
				//}
				Console.WriteLine("Compiled!!!");
				Console.ReadLine();
				Console.WriteLine("Saving Generated Assembly...");
				Compiler.SaveAssembly(assemblyName);
				Console.WriteLine("Saved!!!");
				Console.WriteLine(CODE_INFO.Scripts[0].Name);
				Console.ReadKey();
			}else
			{
				Console.WriteLine("Nothing Scripts Found.");
				Console.ReadKey();
			}
		}
	}
}
