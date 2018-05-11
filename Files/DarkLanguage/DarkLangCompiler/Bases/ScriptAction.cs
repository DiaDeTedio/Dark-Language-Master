using System;
using DarkLanguage.DarkLangCompiler.Bases.Base;
using DarkLanguage.Bases.Source;
using DarkLanguage.DarkLangCompiler.Source;

namespace DarkLanguage.DarkLangCompiler.Bases
{
	[Serializable]
	public class ScriptAction : ScriptHandler
	{
		public actionType Type;
		ScriptAction(string code,params object[] args):base(code,args)
		{
			
		}
		public void ExecuteAction()
		{
			CODE_EXEC.AutoExecuteCode(Code);
		}

		public static ScriptAction GetAction(string code)
		{
			if(code.Contains("\n"))
			{
				code = code.Replace("\n","");
			}
			ScriptAction action = new ScriptAction(code);
			if(!code.Contains("loop") && !code.Contains("When") && code.Contains("("))
			{
				action.Type = actionType.Invoke;
			}else
			if(code.Contains("Declare"))
			{
				action.Type = actionType.Declaration;
			}else
			{
				action.Type = actionType.Other;
			}
			if(string.IsNullOrEmpty(code))
			{
				action.Type = actionType.None;
			}
			if(code.Contains("§")||code.Contains("[")|code.Contains("]"))
			{
				ScriptCompiler.Output.AddLog("Invalid syntax error|2|10");
			}
			if(!code.Contains(";"))
			{
				ScriptCompiler.Output.AddLog("Line error,a line not contains \";\"|2|1000");
			}
			if(action.Code == "\n" || action.Code == ";" || string.IsNullOrEmpty(action.Code))
			{
				return null;
			}
			return action;
		}
	}
	public enum actionType
	{
		None,Declaration,Invoke,Other
	}
}
