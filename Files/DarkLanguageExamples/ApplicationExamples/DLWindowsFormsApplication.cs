using System;
using DarkLanguage.DarkLangCompiler.Classes;
using DarkLanguage.Bases.Source;
using System.Windows.Forms;
using DarkLanguage;

namespace DarkLanguageExamples.ApplicationExamples
{
	public static class DLWindowsFormsApplication
	{
		public static void Initialize()
		{
			DarkLanguageInfo appInfo = new DarkLanguageInfo("WinFormsApplicationInfo");
			appInfo.AddMethodHandler(new ScriptMethodHandler("CreateForm",typeof(DLWindowsFormsApplication)));
			Interpreter.ReadLanguageInfo(appInfo);
			Interpreter.SaveLanguageInfo(appInfo,"WinFormsAppInfo");
		}
		static Form main;
		public static void CreateForm(string title = "Window")
		{
			var instance = new Form();
			instance.Text = title;
			if(main == null)
			{
				using(main = instance)
				{
					System.Windows.Forms.Application.Run(main);
				}
			}else
			{
				instance.Show();
			}
		}
	}
}
