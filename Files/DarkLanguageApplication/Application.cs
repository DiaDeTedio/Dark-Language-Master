using System;
using DarkLanguage.Bases.Source;
using DarkLanguage.DarkLangCompiler.Source;
using System.Threading;
using CSharpEssentials;
using DarkLanguage;

namespace DarkLanguageApplication
{
	public class Application
	{
		public static Application CurrentApplication;
		public readonly string Name;
		public readonly DarkScriptAssembly Assembly;
		public event ApplicationRunEvent OnApplicationRun = ExecuteScriptsTime;
		public static string StartupPath
		{
			get
			{
				return System.Windows.Forms.Application.StartupPath;
			}
		}
		public static string ExecutablePath
		{
			get
			{
				return System.Windows.Forms.Application.ExecutablePath;
			}
		}
		public static string ExecutableName
		{
			get
			{
				return System.IO.Path.GetFileNameWithoutExtension(ExecutablePath);
			}
		}
		Application (string name = "Application")
		{
			CurrentApplication = this;
			Assembly = CODE_INFO.CurrentAssembly;
			Name = name;
			thread = new Thread(Run);
			thread.Start();
		}
		public static Application RunApplication(string name = "Application")
		{
			GetCurrentAssembly();
			GetAllInfo();
			return new Application(name);
		}
		static void GetCurrentAssembly()
		{
			var path = Application.StartupPath;
			var files = CSEMainClass.SearchForFiles(path,extension:"dla");
			foreach(var file in files)
			{
				CODE_INFO.LoadAssembly(file);
			}
		}
		static void GetAllInfo()
		{
			var path = Application.StartupPath;
			var files = CSEMainClass.SearchForFiles(path,extension:"dli");
			foreach(var file in files)
			{
				var info = Interpreter.LoadLanguageInfo(file);
				Interpreter.ReadLanguageInfo(info);
			}
		}
		Thread thread;
		public void Run()
		{
			while(true)
			{
				thread.Join(100);
				OnApplicationRun.Invoke();
			}
		}
		public static void ExecuteScriptsTime()
		{
			ScriptExecutor.ExecuteScripts();
		}
		public void Close()
		{

		}
	}
	public delegate void ApplicationRunEvent();
}
