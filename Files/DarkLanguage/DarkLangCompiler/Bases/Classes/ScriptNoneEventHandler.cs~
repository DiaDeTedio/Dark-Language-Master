using System;

namespace DarkLanguage.DarkLangCompiler.Bases.Classes
{
	[Serializable]
	public class ScriptNoneEventHandler : ScriptEventHandler
	{
		public bool Executed;
		public override bool CanExecute 
		{
			get 
			{
				var ret = !Executed;
				Executed = true;
				return ret;
			}
		}

		public override eventType Type 
		{
			get 
			{
				return eventType.None;
			}
		}
	}
}
