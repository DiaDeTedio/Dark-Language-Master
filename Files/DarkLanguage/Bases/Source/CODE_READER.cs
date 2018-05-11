using System;
using DarkLanguage.Classes;
using System.Collections.Generic;
using DarkLanguage.Classes.ScriptClasses;
using DarkLanguage.Source;
using DarkLanguage.DarkLangCompiler.Source;

namespace DarkLanguage.Bases.Source
{
	public static class CODE_READER
	{
		public static object[] GetReads(string code,string sep = "(,)",bool requireSemicolon = false)
		{
			if(!code.Contains(";") && requireSemicolon)
			{
				throw new Exception("This code has no ';' at the end");
			}
			var possibles = new string[0];
			if(sep != null)
			{
				string[] seps = StringUtils.Split(sep,",");
				var after = StringUtils.ReadAfter(code,seps[0]);
				var inside = StringUtils.GetInside(code,seps[0],seps[1]);
				var before = StringUtils.ReadBefore(code,seps[1]);
				possibles = inside.Split(' ');
			}else
			{
				possibles = code.Split(',');
			}
			List<object> vals = new List<object>();
			foreach(var possible in possibles)
			{
				//Type tp;
				var add = FindValue(possible);
				vals.Add(add);
			}
			object[] calls = vals.ToArray();
			int index = 0;
			foreach(var call in calls)
			{
				string str = call+"";
				if(str != null)
				{
					var field = CODE_EXEC.GetField(str);
					if(field != null)
					{
						vals[index] = field.Value;
					}
				}
				var add = calls[index];
				if(add is string)
				{
					object prev = add;
					//add = CODE_MATH.DoMath(add.ToString());
					add = null;
					if(add == null)
					{
						add = prev;
					}else
					{
						calls[index] = add;
					}
				}
				index++;
			}
			return calls;
		}
		/// <summary>
		/// Checks the language routines.
		/// </summary>
		/// <param name="code">Code.</param>
		/// <param name="routineContext">Context Of This Routine,Script,Compilation or Static.</param>
		public static void CheckLanguageRoutines(string code,int routineContext = 0)
		{
			ScriptRoutineCheckContext context = (ScriptRoutineCheckContext)routineContext;
			if(code.Contains(CODE_INFO.LineSeparator.ToString()))
			{
				code = code.Replace(CODE_INFO.LineSeparator.ToString(),"");
			}
			if(code.Contains("Declare"))
			{
				if(code.Contains("var") && routineContext == 1)
				{
					ScriptCompiler.Output.AddLog("This var declaration is invalid|2|10");
				}
				//var l = code.Split(' ');
				var l = StringUtils.CustomSplit(code,' ','"');
				if(l.Length >= 3)
				{
					string nameOf = l[1];
					string valueOf = l[2];
					ScriptField field = new ScriptField(nameOf);
					var reads = GetReads(valueOf,null);
					field = SetFieldValue(field,reads);
					var possibleField = CODE_EXEC.GetField(nameOf,context);
					if(possibleField == null) 
					{
						CODE_EXEC.RegisterField(field,context);
					}
				}
			}
			if(code.Contains("Set"))
			{
				if(code.Contains("=") && routineContext == 1)
				{
					ScriptCompiler.Output.AddLog("= in a wrong local|2|10");
				}
				var l = StringUtils.CustomSplit(code,' ','"');
				if(l.Length >= 4)
				{
					string nameOf = l[1];
					string valueOf = l[3];
					var reads = GetReads(valueOf,null);
					CODE_EXEC.SetField(nameOf,reads);
				}
			}
		}
		public static ScriptField SetFieldValue(ScriptField field,params object[] values)
		{
			List<object> relact = new List<object>();
			foreach(var val in values)
			{
				var enumer = val as System.Collections.IEnumerable;
				if(enumer != null)
				{
					foreach(var item in enumer)
					{
						relact.Add(item);
					}
				}else
				{
					relact.Add(val);
				}
			}
			values = relact.ToArray();
			field = new ScriptField(field.Name);
			int index = 0;
			foreach(var val in values)
			{
				if(values.Length > 1)
				{
					var fieldItem = new ScriptField("");
					fieldItem.Value = val;
					field.AddItem(fieldItem);
				}else
				{
					field.Value = val;
				}
				index++;
			}
			return field;
		}
		/// <summary>
		/// Gets the code declaration.
		/// If it is a variable,the declaration is Declare here=NAME blablabla.
		/// If it is a method,the declaration is here=NAME(blablabla).
		/// WIP-If it is a Keyword,the declaration is WIP Keyword(here)WIP.
		/// </summary>
		/// <returns>The code declaration.</returns>
		/// <param name="code">Code.</param>
		public static string GetCodeDeclaration(string code)
		{
			string declaration = StringUtils.ReadAfter(code,"(");
			if(string.IsNullOrEmpty(declaration) && code.Contains(" "))
			{
				declaration = code.Split(' ')[0];
			}
			return declaration;
		}
		public static object FindValue(string from)
		{
			Type tp;
			var value = StringUtils.ToPossibleValue(from,out tp);
			if(tp == typeof(string))
			{
				string fieldName = from;
				string[] fieldAcess = null;
				if(fieldName.Contains("."))
				{
					var ac = fieldName.Split('.');
					fieldAcess = new string[ac.Length-1];
					for(int i=1;i<ac.Length;i++)
					{
						fieldAcess[i-1] = ac[i];
					}
				}
				var field = CODE_EXEC.GetField(fieldName);
				if(field != null)
				{
					value = field.Value;
					if(fieldAcess != null)
					{
						var vt = value.GetType();
						foreach(var acess in fieldAcess)
						{
							if(!vt.IsPrimitive && vt != typeof(string))
							{
								var fld = vt.GetField(acess);
								if(fld != null && !fld.FieldType.IsPrimitive&&fld.FieldType != typeof(string))
								{
									vt = fld.FieldType;
								}else
								{
									value = fld.GetValue(value);
								}
							}else
							{
								break;
							}
						}
					}
				}
			}
			return value;
		}
		public static string[] GetScriptLines(string script)
		{
			List<string> ret = new List<string>();
			//var lines = script.Split(CODE_INFO.LineSeparator);
			var lines = StringUtils.SplitAllKeeping(script,CODE_INFO.LineSeparator);
			string statement = null;
			string loopment = null;
			foreach(var line in lines)
			{
				if(line.Contains("?"))
				{
					statement = string.Empty;
				}
				if(line.Contains("loop("))
				{
					loopment = string.Empty;
				}
				if(statement != null || loopment != null)
				{
					if(statement != null)
					{
						statement += line;
					}
					if(loopment != null)
					{
						loopment += line;
					}
				}else
				{
					ret.Add(line);
				}
				if(StringUtils.ContainsAnyOf(line,"end when") && statement != null)
				{
					ret.Add(statement);
					statement = null;
				}
				if(StringUtils.ContainsAnyOf(line,"stop loop") && loopment != null)
				{
					ret.Add(loopment);
					loopment = null;
				}
			}
			return ret.ToArray();
		}
	}
	/// <summary>
	/// Script routine check context.
	/// </summary>
	public enum ScriptRoutineCheckContext
	{
		/// <summary>
		/// The normal fields context.
		/// </summary>
		Fields=0,
		/// <summary>
		/// The script fields,for script assembly serialization without losting of data,and need of recompilation.
		/// </summary>
		ScriptFields=1,
		/// <summary>
		/// The static fields context,For Static Variables!!!.
		/// </summary>
		StaticFields=2
	}
}
