#pragma warning disable IDE0002 // Simplify member access '{0}'
#pragma warning disable RECS0030 // Suggests using the class declaring a static function when calling it
using System;
using DarkLanguage.Classes;
using DarkLanguage.Bases.Source;
using DarkLanguage.Classes.ScriptClasses;
using DarkLanguage.DarkLangCompiler.Classes;

namespace DarkLanguage
{
	/// <summary>
	/// Interpreter,with this class,you can manage all of your code very easy and fully.
	/// </summary>
	public class Interpreter
	{
		/// <summary>
		/// Adds the type of the object.
		/// </summary>
		/// <param name="name">Name of the object.</param>
		/// <param name="type">Type of the object.</param>
		/// <param name="parseMethod">Parse method for string to object conversion.</param>
		/// <param name="defaultValue">Default value for this object.</param>
		public static void AddObjectType(string name,Type type,TryParseTo parseMethod,object defaultValue = null)
		{
			var objType = new ScriptObjectType(name,type,parseMethod,defaultValue);
			CODE_INFO.AddPossible(objType);
		}
		/// <summary>
		/// Adds the script method.
		/// </summary>
		/// <param name="methodHandler">Method handler to add to code information class.</param>
		public static void AddScriptMethod(ScriptMethodHandler methodHandler)
		{
			CODE_INFO.AddMethod(methodHandler);
		}
		public static void AddEnvironmentVariable(ScriptField variable)
		{
			CODE_EXEC.EnvironmentFields.Add(variable);
		}
		/// <summary>
		/// Initialize this instance and make then to add the first Object reference to the code,the null type.
		/// </summary>
		public static void Initialize()
		{
			ScriptObjectType nullV = new ScriptObjectType("null",typeof(object),TryParseNull);
			CODE_INFO.AddPossible(nullV);
		}
		static Interpreter()
		{

		}
		public static void SaveLanguageInfo(DarkLanguageInfo info,string path)
		{
			if(!path.Contains(".dli")){path += ".dli";}
			DarkLanguageInfo.Save(info,path);
		}
		public static DarkLanguageInfo LoadLanguageInfo(string path)
		{
			if(!path.Contains(".dli")){path += ".dli";}
			return DarkLanguageInfo.Load<DarkLanguageInfo>(path);
		}
		public static void ReadLanguageInfo(DarkLanguageInfo info)
		{
			foreach(var method in info.MethodHandlers)
			{
				CODE_INFO.AddMethod(method);
			}
			foreach(var possible in info.ObjectTypes)
			{
				CODE_INFO.AddPossible(possible);
			}
		}
		public static bool TryParseNull(string from,out object value)
		{
			value = null;
			if(from == "null")
			{
				return true;
			}else
			{
				return false;
			}
		}
	}
}
