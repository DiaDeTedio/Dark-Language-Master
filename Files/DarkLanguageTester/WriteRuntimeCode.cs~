using System;
using DarkLanguage.Bases.Source;
using DarkLanguage.DarkLangCompiler.Classes;
using DarkLanguage.DarkLangCompiler.Source;
using System.Threading;
using System.IO;
using DarkLanguage.Versioning.Source;

namespace DarkLanguageTester
{
	public class WriteRuntimeCode
	{
		public void Run()
		{
			CODE_INFO.LoadAssembly("DarkLanguageAssembly");
			ScriptExecutor.StartAutoExecutor();
			while(true)
			{
				var read = Console.ReadLine();
				//CODE_EXEC.AutoExecuteCode(read);
				//if(CODE_EXEC.Fields.Count > 0)
				//{
				//	Console.WriteLine("/////////After//////////");
				//	Console.WriteLine(CODE_EXEC.Fields[0].Value);
				//	var trl = CodeTranslator.TranslateCode(read);
				//	Console.WriteLine(trl);
				//	Console.WriteLine("/////////Before/////////");
				//	CODE_EXEC.AutoExecuteCode(trl);
				//	Console.WriteLine(CODE_EXEC.Fields[0].Value);
				//}
				CheckScript(read);
			}
		}
		public void CheckScript(string read)
		{
			if(read == "Clear")
			{
				Console.Clear();
				return;
			}
			if(read == "Save")
			{
				CODE_INFO.SaveAssembly("DarkLanguageAssembly");
				Console.WriteLine("This Assembly Was Saved!!!");
				Console.ReadKey();
				return;
			}
			var scr = ScriptCompiler.GetScriptCode("../Script");
			CompiledScript script = ScriptCompiler.CompileScript(scr);
			Console.WriteLine("///////////////Script////////////////");
			foreach(var method in script.Methods)
			{
				foreach(var e in method.Events)
				{
					foreach(var condition in e.Conditions)
					{
						foreach(var action in condition.Actions)
						{
							Console.WriteLine(action.Code);
						}
						Console.WriteLine(condition.Condition);
					}
					Console.WriteLine(e.Type);
				}
				Console.WriteLine(method.Type);
			}
			Console.ReadKey(true);

			//CODE_EXEC.AutoExecuteCode(read);

			//Console.WriteLine("///////////FIELDS///////////////");
			//foreach(var field in CODE_EXEC.Fields)
			//{
			//	Console.WriteLine(field);
			//}
			//Console.WriteLine("///////////METHOD//////////////");
			//CODE_READER.CheckLanguageRoutines(read);
			//var rs = CODE_READER.GetReads(read);
			//foreach(var r in rs)
			//{
			//	Console.WriteLine(r);
			//}
		}
	}
}
