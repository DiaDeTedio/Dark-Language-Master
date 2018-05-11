using System;
using CSharpEssentials;
using DarkLanguage.Bases.Source;
using DarkLanguage;
using CSharpEssentials.Xml;
using System.IO;

namespace DarkLanguageApplication
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Tester t = new Tester();
			var xml = XmlUtils.GetXml(t);
			StreamWriter file = new StreamWriter("XML.xml");
			file.Write(xml);
			file.Close();
			Application.RunApplication();
		}
		public class Tester
		{
			public string Name = "Robson";
			public int Age = 60;
			public decimal Points = 0.3545455595959m;
		}
	}
}
