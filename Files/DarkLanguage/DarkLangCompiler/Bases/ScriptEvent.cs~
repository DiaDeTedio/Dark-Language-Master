using System;
using DarkLanguage.DarkLangCompiler.Bases.Base;
using System.Collections.Generic;
using DarkLanguage.DarkLangCompiler.Bases.Classes;
using DarkLanguage.Classes;
using DarkLanguage.Bases.Source;

namespace DarkLanguage.DarkLangCompiler.Bases
{
	[Serializable]
	public class ScriptEvent : ScriptHandler
	{
		public List<ScriptCondition> Conditions = new List<ScriptCondition>();
		public ScriptEventHandler Handler;
		public eventType Type{get{return Handler.Type;}}

		public ScriptEvent(string code,params object[] args):base(code,args)
		{

		}
		public void ExecuteConditions()
		{
			if(Type == eventType.None && Handler.CanExecute)
			{
				foreach(var condition in Conditions)
				{
					condition.ExecuteActions();
				}
			}
			if(Type == eventType.Loop)
			{
				ScriptLoopEventHandler loop = (ScriptLoopEventHandler)Handler;
				if(loop.LoopType != loopType.Infinity)
				{
					while(loop.CanExecute)
					{
						Conditions.ForEach((obj) => obj.ExecuteActions());
					}
				}
			}
		}
		public void ExecuteInfinityLoops()
		{
			if(Type == eventType.Loop)
			{
				ScriptLoopEventHandler loop = (ScriptLoopEventHandler)Handler;
				if(loop.LoopType == loopType.Infinity)
				{
					if(loop.CanExecute)
					{
						Conditions.ForEach((obj) => obj.ExecuteActions());
					}
				}
			}
		}
		public static ScriptEvent GetEvent(string code)
		{
			ScriptEvent e = new ScriptEvent(code);
			string[] lines = new string[0];
			if(code.Contains("loop"))
			{
				var loop = ScriptLoopEventHandler.GetLoopHandler(code);
				string loopCode = StringUtils.GetInside(code,")","stop loop");
				lines = loopCode.Split(CODE_INFO.LineSeparator);
				e.Handler = loop;
			}else
			{
				e.Handler = new ScriptNoneEventHandler();
				lines = new string[]{code};
			}
			foreach(var line in lines)
			{
				ScriptCondition cond = ScriptCondition.GetCondition(line);
				if(cond != null && cond.Actions.Count > 0)
				{
					e.Conditions.Add(cond);
				}
			}
			return e;
		}
	}
	public enum eventType
	{
		None,Loop
	}
}
