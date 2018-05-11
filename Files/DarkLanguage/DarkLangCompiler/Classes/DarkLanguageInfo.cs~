using System;
using System.Collections.Generic;
using DarkLanguage.Bases.Source;
using CSharpEssentials.Classes;

namespace DarkLanguage.DarkLangCompiler.Classes
{
	/// <summary>
	/// Dark language info,to load in DarkLanguageApplication or other.
	/// </summary>
	[Serializable]
	public class DarkLanguageInfo : IFileObject
	{
		public readonly string Name;

		public List<ScriptObjectType> ObjectTypes = new List<ScriptObjectType>();
		public List<ScriptMethodHandler> MethodHandlers = new List<ScriptMethodHandler>();

		public void AddObjectType(ScriptObjectType objectType)
		{
			ObjectTypes.Add(objectType);
		}
		public void AddMethodHandler(ScriptMethodHandler methodHandler)
		{
			MethodHandlers.Add(methodHandler);
		}

		public DarkLanguageInfo(string name)
		{
			Name = name;
		}
	}
}
