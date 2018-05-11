using System;
using DarkLanguage.Bases.Source;
using System.Threading;

namespace DarkLanguage.DarkLangCompiler.Source
{
	public static class ScriptExecutor
	{
		public static void ExecuteScripts()
		{
			foreach(var script in CODE_INFO.AllScripts)
			{
				script.Execute();
			}
		}
		static Thread thread;
		public static void StartAutoExecutor()
		{
			thread = new Thread(ExecuteScripts);
			thread.Start();
		}
		public static void StopAutoExecutor()
		{
			thread.Abort();
		}
	}
}
