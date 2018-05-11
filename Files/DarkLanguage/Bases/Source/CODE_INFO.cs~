using System;
using System.Collections.Generic;
using DarkLanguage.DarkLangCompiler.Classes;
using DarkLanguage.Classes.ScriptClasses;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using DarkLanguage.Classes;
using System.Reflection;
using System.Collections;

namespace DarkLanguage.Bases.Source
{
	public static class CODE_INFO
	{
		public const string Commands = "Start,End,Move(fromPath toPath),Delete(path),Invoke";
		public const string Specials = "(,),[,],{,}";
		public const string Keywords = "Declare,While,When,Cast,As,Var,var,variable";
		public const string Common = "Object,Int,Float,String,Bool";
		public const string EventWords = "stop";
		public const char LineSeparator = ';';

		public static DarkScriptAssemblyCollection Assemblies = new DarkScriptAssemblyCollection();
		public static CompiledScriptCollection Scripts = new CompiledScriptCollection();
		public static CompiledScriptCollection AllScripts
		{
			get
			{
				CompiledScriptCollection ret = new CompiledScriptCollection();
				foreach(var assembly in Assemblies)
				{
					foreach(var script in assembly.Scripts)
					{
						ret.Add(script);
					}
				}
				return ret;
			}
		}
		static List<ScriptMethodHandler> methods = new List<ScriptMethodHandler>();
		public static List<ScriptMethodHandler> Methods
		{
			get
			{
				return methods;
			}
		}
		public static ScriptFieldCollection AllCodeFields
		{
			get
			{
				List<ScriptField> ret = new List<ScriptField>();
				CODE_EXEC.Fields.ForEach((obj) => ret.Add(obj));
				CODE_EXEC.EnvironmentFields.ForEach((obj) => ret.Add(obj));
				CODE_EXEC.ScriptFields.ForEach((obj) => ret.Add(obj));
				CODE_EXEC.StaticFields.ForEach((obj) => ret.Add(obj));
				return new ScriptFieldCollection(ret);
			}
		}
		static List<ScriptObjectType> possibles = new List<ScriptObjectType>();
		/// <summary>
		/// The possible objects that the code can know.
		/// </summary>
		public static List<ScriptObjectType> Possibles
		{
			get
			{
				return possibles;
			}
		}

		public static DarkScriptAssembly CurrentAssembly 
		{
			get
			{
				if(Assemblies.Count > 0)
				{
					return Assemblies[0];
				}else
				{
					return null;
				}
			}
		}

		public static void AddPossible(ScriptObjectType possible)
		{
			possibles.Add(possible);
		}
		public static void AddMethod(ScriptMethodHandler method)
		{
			methods.Add(method);
		}

		public static string[] GetInfo(string from)
		{
			List<string> its = new List<string>();
			var l = from.Split(',');
			foreach(var it in l)
			{
				its.Add(it);
			}
			return its.ToArray();
		}
		public static int IsOf(string command)
		{
			if(Commands.Contains(command)){return 0;}
			if(Specials.Contains(command)){return 1;}
			if(Keywords.Contains(command)){return 2;}
			if(Common.Contains(command)){return 3;}

			return -1;
		}
		public static void SaveAssembly(string path)
		{
			path = GetCorrectPath(path);
			BinaryFormatter bin = new BinaryFormatter();
			Scripts.ScriptFields = CODE_EXEC.ScriptFields;
			var file = File.Create(path);
			var assembly = new DarkScriptAssembly(Path.GetFileNameWithoutExtension(path),Scripts);
			bin.Serialize(file,assembly);
			//file.Dispose();
			file.Close();
			System.Diagnostics.Process.Start(Path.GetDirectoryName(path));
		}
		public static void LoadAssembly(string path)
		{
			path = GetCorrectPath(path);
			BinaryFormatter bin = new BinaryFormatter();
			if(File.Exists(path))
			{
				var file = File.OpenRead(path);
				var assembly = (DarkScriptAssembly)bin.Deserialize(file);
				Scripts = assembly.Scripts;
				foreach(var field in Scripts.ScriptFields)
				{
					CODE_EXEC.ScriptFields.Add(field);
				}
				//file.Dispose();
				file.Close();
				Assemblies.Add(assembly);
			}
		}
		public static DarkScriptAssembly LoadAssemblyFile(string path)
		{
			path = GetCorrectPath(path);
			BinaryFormatter bin = new BinaryFormatter();
			if(File.Exists(path))
			{
				var file = File.OpenRead(path);
				var assembly = (DarkScriptAssembly)bin.Deserialize(file);
				Scripts = assembly.Scripts;
				foreach(var field in Scripts.ScriptFields)
				{
					CODE_EXEC.ScriptFields.Add(field);
				}
				//file.Dispose();
				file.Close();
				Assemblies.Add(assembly);
				return assembly;
			}
			return null;
		}
		static string GetCorrectPath(string path)
		{
			path = path.Replace(@"\","/");
			if(!Path.IsPathRooted(path))
			{
				//path = Path.GetFullPath(path);
			}
			if(Path.HasExtension(path))
			{
				if(!Path.GetExtension(path).Contains("dla"))
				{
					path = Path.ChangeExtension(path,"dla");
				}
			}else
			{
				path += ".dla";
			}

			return path;
		}
	}

	public class DarkScriptAssemblyCollection : IList<DarkScriptAssembly>,ICollection<DarkScriptAssembly>
	{
		List<DarkScriptAssembly> Items = new List<DarkScriptAssembly>();

		public DarkScriptAssembly this [int index] {
			get {
				return ((IList<DarkScriptAssembly>)Items) [index];
			}

			set {
				((IList<DarkScriptAssembly>)Items) [index] = value;
			}
		}

		public int Count {
			get {
				return ((ICollection<DarkScriptAssembly>)Items).Count;
			}
		}

		public bool IsReadOnly {
			get {
				return ((ICollection<DarkScriptAssembly>)Items).IsReadOnly;
			}
		}

		public void Add (DarkScriptAssembly item)
		{
			((ICollection<DarkScriptAssembly>)Items).Add (item);
		}

		public void Clear ()
		{
			((ICollection<DarkScriptAssembly>)Items).Clear ();
		}

		public bool Contains (DarkScriptAssembly item)
		{
			return ((ICollection<DarkScriptAssembly>)Items).Contains (item);
		}

		public void CopyTo (DarkScriptAssembly [] array, int arrayIndex)
		{
			((ICollection<DarkScriptAssembly>)Items).CopyTo (array, arrayIndex);
		}

		public IEnumerator<DarkScriptAssembly> GetEnumerator ()
		{
			return ((ICollection<DarkScriptAssembly>)Items).GetEnumerator ();
		}

		public int IndexOf (DarkScriptAssembly item)
		{
			return ((IList<DarkScriptAssembly>)Items).IndexOf (item);
		}

		public void Insert (int index, DarkScriptAssembly item)
		{
			((IList<DarkScriptAssembly>)Items).Insert (index, item);
		}

		public bool Remove (DarkScriptAssembly item)
		{
			return ((ICollection<DarkScriptAssembly>)Items).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<DarkScriptAssembly>)Items).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<DarkScriptAssembly>)Items).GetEnumerator ();
		}
	}

	[Serializable]
	public class CompiledScriptCollection
	{
		public List<CompiledScript> Scripts = new List<CompiledScript>();
		public ScriptFieldCollection ScriptFields = new ScriptFieldCollection();

		public int Count
		{
			get
			{
				return Scripts.Count;
			}
		}

		public CompiledScript this[int index]{get{return Scripts[index];}set{Scripts[index]=value;}}

		public void Add(CompiledScript script){Scripts.Add(script);}
		public void Remove(CompiledScript script){Scripts.Remove(script);}
		public void RemoveAt(int index){Scripts.RemoveAt(index);}

		public bool Contains(CompiledScript script){return Scripts.Contains(script);}

		public IEnumerator<CompiledScript> GetEnumerator()
		{
			foreach(var script in Scripts)
			{
				yield return script;
			}
		}
		public void ForEach(Action<CompiledScript> action)
		{
			foreach(var script in Scripts)
			{
				action.Invoke(script);
			}
		}
		public void ForEach(Action<ScriptField> action)
		{
			foreach(var field in ScriptFields)
			{
				action.Invoke(field);
			}
		}
	}
	[Serializable]
	public class DarkScriptAssembly
	{
		public readonly string Name;
		public readonly CompiledScriptCollection Scripts = new CompiledScriptCollection();

		public DarkScriptAssembly(string name = "Assembly",CompiledScriptCollection scripts = null)
		{
			Name = name;
			if(scripts != null)
			{
				Scripts = scripts;
			}
		}
	}
	[Serializable]
	public class ScriptObjectType
	{
		public string Name{get{return Handler.Name;}}
		public Type Type{get{return Handler.Type;}}
		public object DefaultValue;
		public TryParseTo TryParseMethod{get{return Handler.ParseMethod;}}
		public PossibleObjectHandler Handler;

		public ScriptObjectType(string name,Type typeofIt,TryParseTo method,object defaultValue = null)
		{
			Handler = new PossibleObjectHandler(name,typeofIt,method);
			if(!StringUtils.Possibles.Contains(Handler))
			{
				StringUtils.Possibles.Add(Handler);
			}
			DefaultValue = defaultValue;
		}
	}
	/// <summary>
	/// Script method handler,to say to the CODE_EXECUTOR to make it to be called when programmer types the
	/// name and arguments of the method.
	/// </summary>
	[Serializable]
	public class ScriptMethodHandler
	{
		public string Name;
		public object Instance;
		public Type StaticType;
		public MethodParameterHandler[] Parameters;
		public readonly CodeMethodHandler Handler;
		public CustomCodeMethodHandler CHandler;
		public int RequiredParametersCount
		{
			get
			{
				int c = 0;
				foreach(var par in Parameters)
				{
					if(par.Required)
					{
						c++;
					}
				}
				return c;
			}
		}

		public object Invoke(params object[] args)
		{
			if(Handler != null)
			{
				return Handler.Invoke(args);
			}
			if(CHandler != null)
			{
				return CHandler.Invoke(args);
			}
			return null;
		}

		public void SortParameters()
		{
			List<MethodParameterHandler> parameters = new List<MethodParameterHandler>();
			List<MethodParameterHandler> next = new List<MethodParameterHandler>();
			foreach(var parameter in Parameters)
			{
				if(parameter.Required)
				{
					parameters.Add(parameter);
				}else
				{
					next.Add(parameter);
				}
			}
			parameters.AddRange(next);
			Parameters = parameters.ToArray();
		}
		public ScriptMethodHandler(string name,object instance,params MethodParameterHandler[] parameters)
		{
			Name = name;
			Parameters = parameters;
			SortParameters();
			Instance = instance;
			var methodInfo = instance.GetType().GetMethod(Name);
			var ins = instance;
			CHandler = new CustomCodeMethodHandler(ins,methodInfo);
			Instance = ins;
		}
		public ScriptMethodHandler(CodeMethodHandler method)
		{
			Name = method.Method.Name;
			List<MethodParameterHandler> pars = new List<MethodParameterHandler>();
			foreach(var par in method.Method.GetParameters())
			{
				var name = par.Name;
				var type = par.ParameterType;
				var req = !par.IsOptional;
				var def = par.DefaultValue;
				MethodParameterHandler hand = new MethodParameterHandler(name,type,req,def);
			}
			Instance = method.Target;
			Handler = method;
		}
		public ScriptMethodHandler(string methodName,object from)
		{
			var method = from.GetType().GetMethod(methodName);
			Name = method.Name;
			List<MethodParameterHandler> pars = new List<MethodParameterHandler>();
			foreach(var par in method.GetParameters())
			{
				var name = par.Name;
				var type = par.ParameterType;
				var req = !par.IsOptional;
				var def = par.DefaultValue;
				MethodParameterHandler hand = new MethodParameterHandler(name,type,req,def);
				pars.Add(hand);
			}
			Parameters = pars.ToArray();
			Instance = from;
			CHandler = new CustomCodeMethodHandler(Instance,method);
		}
		public ScriptMethodHandler(string methodName,Type from)
		{
			var method = from.GetMethod(methodName);
			Name = method.Name;
			List<MethodParameterHandler> pars = new List<MethodParameterHandler>();
			foreach(var par in method.GetParameters())
			{
				var name = par.Name;
				var type = par.ParameterType;
				var req = !par.IsOptional;
				var def = par.DefaultValue;
				MethodParameterHandler hand = new MethodParameterHandler(name,type,req,def);
				pars.Add(hand);
			}
			Parameters = pars.ToArray();
			StaticType = from;
			CHandler = new CustomCodeMethodHandler(StaticType,method);
		}
		public bool MatchMethodParameters(params object[] args)
		{
			int count = RequiredParametersCount;
			int match = 0;
			if(args.Length < count)
			{
				return false;
			}

			for(int i=0;i<Parameters.Length;i++)
			{
				if(i < args.Length)
				{
					var next = args[i];
					if(Parameters[i].MatchParameter(next))
					{
						match++;
					}
				}
			}
			if(match >= count)
			{
				return true;
			}else
			{
				return false;
			}
		}

		public string ParametersRepresentation ()
		{
			string ret = "";
			int index = 0;
			foreach(var parameter in Parameters)
			{
				string completion = "";
				if(index < Parameters.Length-1)
				{
					completion = " ,";
				}
				ret += $"{parameter.Type} {parameter.Name}{completion}";
				index++;
			}
			return ret;
		}
		public override string ToString ()
		{
			string ret = Name+"(";
			ret += ParametersRepresentation()+")";
			return ret;
		}
	}
	[Serializable]
	public class MethodParameterHandler
	{
		public readonly string Name;
		public readonly Type Type;
		public readonly bool Required;
		public readonly object DefaultValue;

		public MethodParameterHandler(string name,Type type,bool required = true,object defaultValue = null)
		{
			Name = name;
			Type = type;
			Required = required;
			DefaultValue = defaultValue;
		}
		public bool MatchParameter(object value)
		{
			if(value.GetType() == Type)
			{
				return true;
			}else
			{
				return false;
			}
		}
		public override string ToString ()
		{
			return $"{Type.Name} {Name}";
		}
	}
	[Serializable]
	public class CustomCodeMethodHandler
	{
		public MethodInfo Method;
		public Object Instance;
		public Type StaticInstance;

		public CustomCodeMethodHandler (object instance, MethodInfo method)
		{
			Instance = instance;
			Method = method;
		}
		public CustomCodeMethodHandler (Type staticInstance, MethodInfo method)
		{
			StaticInstance = staticInstance;
			Method = method;
		}

		public object Invoke(params object[] arguments)
		{
			if(Instance != null)
			{
				return Method.Invoke(Instance,arguments);
			}else
			{
				return Method.Invoke(null,arguments);
			}
		}
	}
	public delegate object CodeMethodHandler(params object[] parameters);
}
