using System;
using System.Collections.Generic;
using DarkLanguage.Classes.ScriptClasses;
using DarkLanguage.Versioning.Source;
using System.IO;
using System.Diagnostics;

namespace DarkLanguage.Bases.Source
{
	public static class CODE_EXEC
	{
		public static ScriptFieldCollection Fields = new ScriptFieldCollection();
		public static ScriptFieldCollection ScriptFields = new ScriptFieldCollection();
		public static ScriptFieldCollection StaticFields = new ScriptFieldCollection();
		public static ScriptFieldCollection EnvironmentFields = new ScriptFieldCollection();

		public static int TotalFieldCount
		{
			get
			{
				return Fields.Count+ScriptFields.Count+StaticFields.Count+EnvironmentFields.Count;
			}
		}

		public static Exec Start = sta;
		public static Exec End = end;
		public static Exec Move = mov;
		public static Exec Delete = del;
		public static Exec Invoke = inv;

		static object sta(params object[] args)
		{
			if(args.Length == 1)
			{
				string original = (string)args[0];
				original = original.Replace("\"","");

				processes.Add(Process.Start(original));
			}
			return processes[processes.Count-1].Id;
		}
		static List<Process> processes = new List<Process>();

		static object end(params object[] args)
		{
			if(args.Length == 1)
			{
				string original = args[0] as string;
				if(original != null)
				{
					original = original.Replace("\"","");
				}
				if(original != null && original == "this")
				{
					System.Threading.Thread.CurrentThread.Abort();
				}
				if(original != null)
				{
					foreach(var process in processes)
					{
						if(process.ProcessName == original)
						{
							process.Kill();
							processes.Remove(process);
							break;
						}
						Console.WriteLine(process.ProcessName);
					}
				}
			}
			return null;
		}

		static object mov(params object[] args)
		{
			if(args.Length == 2)
			{
				string original = (string)args[0];
				string next = (string)args[1];
				original = original.Replace("\"","");
				next = next.Replace("\"","");

				File.Move(original,next);
			}
			return null;
		}

		static object del(params object[] args)
		{
			if(args.Length == 1)
			{
				string original = (string)args[0];
				original = original.Replace("\"","");

				File.Delete(original);
			}
			return null;
		}

		static object inv(params object[] args)
		{

			return null;
		}

		public static void ExecuteCode(string code,params object[] args)
		{
			var type = typeof(CODE_EXEC);
			var fields = type.GetFields();
			foreach(var field in fields)
			{
				if(field.Name == code)
				{
					var exec = (Exec)field.GetValue(null);
					exec.Invoke(args);
				}
			}
			foreach(var method in CODE_INFO.Methods)
			{
				if(code == method.Name)
				{
					method.Invoke(args);
				}
			}
		}
		public static void AutoExecuteCode(string code)
		{
			code = CodeTranslator.TranslateCode(code);
			CODE_READER.CheckLanguageRoutines(code,0);
			var name = CODE_READER.GetCodeDeclaration(code);
			var args = CODE_READER.GetReads(code);
			args = CorrectArgs(name,args);
			ExecuteCode(name,args);
		}
		public static object[] CorrectArgs(string name,object[] args)
		{
			var m = GetMethod(name);
			if(m != null)
			{
				List<object> pars = new List<object>();
				int i = 0;
				if(m.Parameters != null)
				{
					foreach(var par in m.Parameters)
					{
						if(i < args.Length)
						{
							if(par.MatchParameter(args[i]))
							{
								pars.Add(args[i]);
							}
						}
						i++;
					}
				}
				return pars.ToArray();
			}
			return args;
		}
		public static ScriptMethodHandler GetMethod(string name)
		{
			return CODE_INFO.Methods.Find((obj) => obj.Name == name);
		}
		public static ScriptField GetField(string name,ScriptRoutineCheckContext context=0)
		{
			if(context == ScriptRoutineCheckContext.Fields)
			{
				foreach(var field in Fields)
				{
					if(field.Name == name)
					{
						return field;
					}
				}
				foreach(var field in EnvironmentFields)
				{
					if(field.Name == name)
					{
						return field;
					}
				}
			}
			if(context == ScriptRoutineCheckContext.ScriptFields)
			{
				foreach(var field in ScriptFields)
				{
					if(field.Name == name)
					{
						return field;
					}
				}
			}
			if(context == ScriptRoutineCheckContext.StaticFields)
			{
				foreach(var field in StaticFields)
				{
					if(field.Name == name)
					{
						return field;
					}
				}
				foreach(var field in EnvironmentFields)
				{
					if(field.Name == name)
					{
						return field;
					}
				}
			}
			return null;
		}
		public static void SetField(string name,object value,ScriptRoutineCheckContext context=0)
		{
			if(context == ScriptRoutineCheckContext.Fields)
			{
				for (int i = 0; i < Fields.Count; i++) 
				{
					var field = Fields [i];
					if (field.Name == name) 
					{
						field = CODE_READER.SetFieldValue(field,value);
					}
					Fields[i] = field;
				}

				for (int i = 0; i < EnvironmentFields.Count; i++) 
				{
					var field = EnvironmentFields [i];
					if(field.Name == name)
					{
						field = CODE_READER.SetFieldValue(field,value);
					}
					EnvironmentFields[i] = field;
				}
			}
			if(context == ScriptRoutineCheckContext.ScriptFields)
			{
				for (int i = 0; i < ScriptFields.Count; i++) 
				{
					var field = ScriptFields [i];
					if(field.Name == name)
					{
						field = CODE_READER.SetFieldValue(field,value);
					}
					ScriptFields[i] = field;
				}
			}
			if(context == ScriptRoutineCheckContext.StaticFields)
			{
				for (int i = 0; i < StaticFields.Count; i++) 
				{
					var field = StaticFields [i];
					if(field.Name == name)
					{
						field = CODE_READER.SetFieldValue(field,value);
					}
					StaticFields[i] = field;
				}
				for (int i = 0; i < EnvironmentFields.Count; i++) 
				{
					var field = EnvironmentFields [i];
					if(field.Name == name)
					{
						field = CODE_READER.SetFieldValue(field,value);
					}
					EnvironmentFields[i] = field;
				}
			}
		}
		public static void RegisterField(ScriptField field,ScriptRoutineCheckContext context=ScriptRoutineCheckContext.Fields)
		{
			if(context == ScriptRoutineCheckContext.Fields)
			{
				Fields.Add(field);
			}
			if(context == ScriptRoutineCheckContext.ScriptFields)
			{
				ScriptFields.Add(field);
			}
			if(context == ScriptRoutineCheckContext.StaticFields)
			{
				StaticFields.Add(field);
			}
		}
		public static void UnregisterField(ScriptField field,ScriptRoutineCheckContext context)
		{
			if(context == ScriptRoutineCheckContext.Fields)
			{
				Fields.Remove(field);
			}
			if(context == ScriptRoutineCheckContext.ScriptFields)
			{
				ScriptFields.Remove(field);
			}
			if(context == ScriptRoutineCheckContext.StaticFields)
			{
				StaticFields.Remove(field);
			}
		}
	}
	public delegate object Exec(params object[] args);
}
