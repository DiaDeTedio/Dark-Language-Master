using System;

namespace DarkLanguage.DarkLangCompiler.Bases.Base
{
	[Serializable]
	public class ScriptHandler
	{
		public string Code;
		public object[] Args;

		public ScriptHandler(string code,params object[] args)
		{
			Code = code;
			Args = args;
		}
		public ScriptHandler()
		{

		}
	}
}
