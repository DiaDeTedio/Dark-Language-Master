using System;
using DarkLanguage.Bases.Source;

namespace DarkLanguage.Versioning.Source
{
	public static class CodeTranslator
	{
		public static string TranslateScript(string script)
		{
			script = script.Replace("\r\n","");
			var lines = CODE_READER.GetScriptLines(script);
			int index = 0;
			foreach(var line in lines)
			{
				if(line.Contains("="))
				{
					lines[index] = TranslateCode(lines[index]);
				}
				index++;
			}
			return script;
		}
		public static string TranslateCode(string code)
		{
			code = ReplaceCommonKeyWords(code);
			if(code.Contains("var"))
			{
				code = code.Replace("var","Declare");
				code = code.Replace(" =","");
				return code;
			}
			if(code.Contains("=="))
			{
				code = code.Replace("==","equals");
			}
			if(code.Contains("<="))
			{
				code = code.Replace("<=","lessThan");
			}
			if(code.Contains(">="))
			{
				code = code.Replace(">=","largerThan");
			}
			if(code.Contains("!="))
			{
				code = code.Replace("!=","notEqual");
			}
			if(code.Contains("="))
			{
				code = code.Replace("=","to");
				code = code.Insert(0,"Set ");
			}
			return code;
		}
		static string ReplaceCommonKeyWords(string code)
		{
			code = code.Replace("\n","");
			code = code.Replace("declare","Declare");
			code = code.Replace("variable","var");
			code = code.Replace("when","When");
			code = code.Replace("Loop","loop");
			code = code.Replace("for","loop");
			code = code.Replace("set","Set");
			return code;
		}
	}
}
