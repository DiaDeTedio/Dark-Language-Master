using System;
using DarkLanguage.DarkLangCompiler.Classes;
using DarkLanguage.DarkLangCompiler.Bases;

namespace DarkLanguage.DarkLangCompiler.Source
{
	public class ScriptType
	{
		public readonly string Name;
		public readonly string Namespace;
		public readonly methodType Type;

		ScriptType (string name,string _namespace,methodType type)
		{
			Name = name;
			Namespace = _namespace;
			Type = type;
		}

		public static ScriptType FromScript(CompiledScript script)
		{
			var name = script.Name;
			var _namespace = $"{script.Name}.{script.Methods[0].Name}";
			var type = script.Methods[0].Type;
			ScriptType tpc = new ScriptType(name,_namespace,type);
			return tpc;
		}
	}
}
