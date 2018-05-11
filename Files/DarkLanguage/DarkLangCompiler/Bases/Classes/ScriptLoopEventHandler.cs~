using System;
using DarkLanguage.Classes;
using DarkLanguage.Bases.Source;

namespace DarkLanguage.DarkLangCompiler.Bases.Classes
{
	[Serializable]
	public class ScriptLoopEventHandler : ScriptEventHandler
	{
		public int CurrentIndex;
		public int MaxIndex;
		public loopType LoopType;
		public object Cond0;
		public object Cond1;
		public bool CanPass
		{
			get
			{
				if(Cond0 == Cond1)
				{
					return true;
				}
				return false;
			}
		}

		public override eventType Type 
		{
			get 
			{
				return eventType.Loop;
			}
		}

		public override bool CanExecute 
		{
			get 
			{
				if(LoopType == loopType.Iteration)
				{
					if(CurrentIndex <= MaxIndex)
					{
						CurrentIndex++;
						return true;
					}else
					{
						return false;
					}
				}
				if(LoopType == loopType.Condition)
				{
					if(CanPass)
					{
						return true;
					}else
					{
						return false;
					}
				}
				if(LoopType == loopType.Infinity)
				{
					return true;
				}
				return false;
			}
		}

		ScriptLoopEventHandler ()
		{

		}
		public static ScriptLoopEventHandler GetLoopHandler(string code)
		{
			ScriptLoopEventHandler loop = new ScriptLoopEventHandler();
			var inside = StringUtils.GetInside(code,"(",")");
			if(inside.Contains("infinity") || inside.Contains("Infinity"))
			{
				loop.LoopType = loopType.Infinity;
				loop.CurrentIndex = -1;
				loop.MaxIndex = -1;
			}else
			{
				if(inside.Contains(" ") && !inside.Contains("stop loop"))
				{
					loop.LoopType = loopType.Condition;
					var l = inside.Split(' ');
					object arg0 = CODE_READER.FindValue(l[0]);
					object arg1 = CODE_READER.FindValue(l[2]);
					loop.Cond0 = arg0;
					loop.Cond1 = arg1;
				}else
				{
					loop.LoopType = loopType.Iteration;
					object arg = CODE_READER.FindValue(inside);
					if(!(arg is int) && arg is string)
					{
						arg = StringUtils.FilterInteger((string)arg,true);
					}
					if(arg is int)
					{
						loop.MaxIndex = (int)arg;
					}
				}
			}
			return loop;
		}
	}
	public enum loopType
	{
		Iteration,Condition,Infinity
	}
}
