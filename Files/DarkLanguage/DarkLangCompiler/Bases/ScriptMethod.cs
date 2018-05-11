using System;
using System.Collections.Generic;
using DarkLanguage.Bases.Source;
using DarkLanguage.Classes;

namespace DarkLanguage.DarkLangCompiler.Bases
{
	[Serializable]
	public class ScriptMethod
	{
		public string Name;
		public string Code;
		public List<ScriptEvent> Events = new List<ScriptEvent>();
		public methodType Type;

		ScriptMethod(string name,string code)
		{
			Name = name;
			Code = code;
		}
		public void ExecuteMethod()
		{
			foreach(var e in Events)
			{
				e.ExecuteConditions();
				e.ExecuteInfinityLoops();
			}
		}
		public static ScriptMethod GetScriptMethod(string name,string code)
		{
			ScriptMethod method = new ScriptMethod(name,code);
			if(string.IsNullOrEmpty(code))
			{
				method.Type = methodType.None;
			}
			if(code.Contains("DECLARE"))
			{
				method.Type = methodType.Declaration;
				code = code.Replace("DECLARE","");
			}
			if(code.Contains("EVENT"))
			{
				method.Type = methodType.Event;
				code = code.Replace("EVENT","");
			}

			//var lines = code.Split(CODE_INFO.LineSeparator);
			//var lines = StringUtils.CustomSplit(code,CODE_INFO.LineSeparator,')');
			var lines = CODE_READER.GetScriptLines(code);
			foreach(var line in lines)
			{
				CODE_READER.CheckLanguageRoutines(line,1);
				ScriptEvent e = ScriptEvent.GetEvent(line);
				if(e != null && e.Conditions.Count > 0)
				{
					method.Events.Add(e);
				}
			}
			return method;
		}
	}
	public enum methodType
	{
		/// <summary>
		/// The none type of a method,this method is basicly null.
		/// </summary>
		None,
		/// <summary>
		/// The script type of a method,this method is a normal script.
		/// </summary>
		Script,
		/// <summary>
		/// The declaration type of a method,this method is a static variable declaration.
		/// You can make one typping DECLARE on the method body.
		/// </summary>
		Declaration,
		/// <summary>
		/// The event type of a method,this method is a event method and will execute when the owner of program call it event.
		/// </summary>
		Event
	}
}
