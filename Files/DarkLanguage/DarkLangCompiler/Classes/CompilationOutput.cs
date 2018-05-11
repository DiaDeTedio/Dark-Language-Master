using System;
using System.Collections.Generic;

namespace DarkLanguage.DarkLangCompiler.Classes
{
	public class CompilationOutput
	{
		public List<CompilationLog> Logs = new List<CompilationLog>();

		public void AddLog(CompilationLog log)
		{
			Logs.Add(log);
		}
		public IEnumerator<CompilationLog> GetEnumerator()
		{
			foreach(var log in Logs)
			{
				yield return log;
			}
		}
	}
	public class CompilationLog
	{
		public readonly string Log;
		public readonly int Level;
		public readonly logType Type;

		public CompilationLog(string log = "None",logType type = logType.Info,int level = 0)
		{
			Log = log;
			Level = level;
			Type = type;
		}
		public override string ToString ()
		{
			return $"{Log}|{Type}|{Level}";
		}
		public static CompilationLog Parse(string from)
		{
			var l = from.Split('|');
			string logMessage = l[0];
			string logTypeS = l[1];
			string logLevelS = l[2];

			logType logType = new logType();
			int logLevel = 0;
			int tI = -1;
			if(int.TryParse(logTypeS,out tI))
			{
				logType = (logType)tI;
			}
			var lgT = new logType();
			if(Enum.TryParse<logType>(logTypeS,true,out lgT))
			{
				logType = lgT;
			}
			int.TryParse(logLevelS,out logLevel);
			return new CompilationLog(logMessage,logType,logLevel);
		}
		public static implicit operator string(CompilationLog log)
		{
			return log.ToString();
		}
		public static implicit operator CompilationLog(string from)
		{
			return Parse(from);
		}
	}
	public enum logType : byte
	{
		Info = 0,Warning = 1,Error = 2
	}
}
