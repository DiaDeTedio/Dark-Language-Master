using System;
using DarkLanguage.DarkLangCompiler.Bases;
using System.Collections.Generic;
using DarkLanguage.Classes;

namespace DarkLanguage.DarkLangCompiler.Classes
{
	[Serializable]
	public class CompiledScript
	{
		public string Name;
		public List<ScriptMethod> Methods = new List<ScriptMethod>();

		public CompiledScript (string script)
		{
			if(script.Contains("METHODSTART"))
			{

			}else
			{
				ScriptMethod method = ScriptMethod.GetScriptMethod(Name,script);
				Methods.Add(method);
			}
		}
		public void Execute()
		{
			foreach(var method in Methods)
			{
				method.ExecuteMethod();
			}
		}
		public override string ToString ()
		{
			string ln = "\r\n";
			string ret = Name+ln;
			foreach(var method in Methods)
			{
				ret += method.Code+ln;
				foreach(var e in method.Events)
				{
					ret += e.Code+ln;
					foreach(var cond in e.Conditions)
					{
						ret += cond.Code+ln;
						foreach(var act in cond.Actions)
						{
							ret += act.Code+ln;
						}
					}
				}
			}
			return ret;
		}
		public string ToCSharpCode()
		{
			string rn = "\r\n";
			string c = "{";
			string en = "}";
			string code = $"public class {Name}{rn}{c}";
			foreach(var method in Methods)
			{
				code += $"public void {method.Name}{rn}{c}";
				foreach(var e in method.Events)
				{
					if(e.Type != eventType.None)
					{
						code += $"{e.Code}{c}";
					}else
					{
						foreach(var cond in e.Conditions)
						{
							if(cond.Condition == conditionType.None)
							{
								foreach(var action in cond.Actions)
								{
									code += action.Code+rn;
								}
							}
						}
					}
				}
				code += en;
			}
			code += en;
			return code;
		}
	}
}
