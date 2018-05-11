using System;
using DarkLanguage.DarkLangCompiler.Bases.Base;
using System.Collections.Generic;
using DarkLanguage.Classes;
using DarkLanguage.Bases.Source;
using System.Collections;

namespace DarkLanguage.DarkLangCompiler.Bases
{
	[Serializable]
	public class ScriptCondition : ScriptHandler
	{
		public List<ScriptAction> Actions = new List<ScriptAction>();
		public bool Passed
		{
			get
			{
				return CheckPassCondition();
			}
		}
		public conditionType Condition;

		public ScriptCondition(string code,params object[] args):base(code,args)
		{

		}
		public void ExecuteActions()
		{
			if(Passed)
			{
				foreach(var action in Actions)
				{
					action.ExecuteAction();
				}
			}
		}

		bool CheckPassCondition()
		{
			bool ret = false;
			if(Condition == conditionType.None)
			{
				ret = true;
			}
			if(Condition == conditionType.Equal)
			{
				if(Args[0].Equals(Args[1]))
				{
					ret = true;
				}
			}
			if(Condition == conditionType.NotEqual)
			{
				if(!Args[0].Equals(Args[1]))
				{
					ret = true;
				}
			}
			if(Condition == conditionType.LessThan)
			{
				int comp = Comparer.Default.Compare(Args[0],Args[1]);
				if(comp == 0)
				{
					ret = true;
				}
			}
			if(Condition == conditionType.LargerThan)
			{
				int comp = Comparer.Default.Compare(Args[0],Args[1]);
				if(comp == 2)
				{
					ret = true;
				}
			}
			return ret;
		}
		public static object[] GetArgs(string code,out conditionType condition)
		{
			List<object> args = new List<object>();
			condition = conditionType.None;
			if(code.Length > 0 && code[code.Length-1] != '?')
			{
				var ls = code.Split('?');
				code = ls[0];
			}
			var l = code.Split(' ');
			//if(code.Contains(":")){code = code.Replace(":","");}
			if(code.Contains("When") && l.Length >= 4)
			{
				string first = l[1];
				string second = l[3];
				object obj0 = CODE_READER.FindValue(first);
				object obj1 = CODE_READER.FindValue(second);
				args.Add(obj0);
				args.Add(obj1);
				string lc = l[2];
				if(lc == "equals"){condition = conditionType.Equal;}
				if(lc == "lessThan"){condition = conditionType.LessThan;}
				if(lc == "largerThan"){condition = conditionType.LargerThan;}
				if(lc == "notEqual"){condition = conditionType.NotEqual;}
			}
			return args.ToArray();
		}
		public static ScriptCondition GetCondition(string code)
		{
			conditionType type = conditionType.None;
			object[] args = GetArgs(code,out type);
			var cond = new ScriptCondition(code,args);
			cond.Condition = type;
			if(type != conditionType.None)
			{
				var condCode = StringUtils.GetInside(code,"?","end when");
				var lines = condCode.Split(CODE_INFO.LineSeparator);
				foreach(var line in lines)
				{
					ScriptAction action = ScriptAction.GetAction(line);
					if(!string.IsNullOrEmpty(action.Code))
					{
						cond.Actions.Add(action);
					}
				}
			}else
			{
				ScriptAction action = ScriptAction.GetAction(code);
				if(action != null)
				{
					cond.Actions.Add(action);
				}
			}

			return cond;
		}
	}
	public enum conditionType
	{
		None,Equal,NotEqual,LessThan,LargerThan
	}
}
