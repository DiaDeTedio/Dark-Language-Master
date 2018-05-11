using System;

namespace DarkLanguage.DarkLangCompiler.Bases.Classes
{
	[Serializable]
	public abstract class ScriptEventHandler
	{
		public abstract eventType Type{get;}
		public abstract bool CanExecute{get;}
	}
}
