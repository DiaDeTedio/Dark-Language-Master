using System;
using DarkLanguageExamples.ApplicationExamples;
using DarkLanguageApplication;
using DarkLanguage.Bases.Source;

namespace DarkLanguageExamples
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			DLWindowsFormsApplication.Initialize();
			Application.RunApplication("WindowsFormsApplication");
			Console.WriteLine(CODE_INFO.Scripts[0].ToCSharpCode());
		}
	}
}
