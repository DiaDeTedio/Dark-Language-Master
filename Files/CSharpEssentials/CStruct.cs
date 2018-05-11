using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSharpEssentials
{
	/// <summary>
	/// Custom Struct From A Reference Type.
	/// </summary>
	public struct CStruct<T> where T : class
	{
		/// <summary>
		/// Gets the new created instance of this Custom Struct.
		/// </summary>
		/// <value>The instance.</value>
		public T Instance
		{
			get
			{
				return ctor.Construct<T>(0,fields);
			}
		}
		List<StructField> fields;
		CStructCtor ctor;
		public TField FindField<TField>(string name)
		{
			var fld = fields.Find((obj) => obj.Name == name);
			if(fld != null){return (TField)fld.Value;}
			return default(TField);
		}
		public CStruct (T instance)
		{
			fields = new List<StructField>();
			var fI = fields;
			var flds = instance.GetType().GetRuntimeFields();
			flds.ForEach((FieldInfo obj) => fI.Add(new StructField(obj.Name,obj.GetValue(instance))));
			fields = fI;
			ctor = new CStructCtor(typeof(T));
		}
	}
	public class StructField
	{
		public string Name;
		public object Value;

		public StructField(string name,object value)
		{
			Name = name;
			Value = value;
		}
	}
	/// <summary>
	/// Custom Struct Constructor.
	/// </summary>
	public class CStructCtor
	{
		public List<CtorInfo> Constructors = new List<CtorInfo>();
		public Type Type;

		public CStructCtor(Type type)
		{
			Type = type;
			GetCtor(type);
		}
		void GetCtor(Type type)
		{
			var ctors = type.GetConstructors();
			foreach(var ctor in ctors)
			{
				var constructor = new CtorInfo(ctor);
				Constructors.Add(constructor);
			}
		}
		public T Construct<T>(int ctor,List<StructField> fields)
		{
			return Constructors[0].Ctor<T>(fields);
		}
	}
	public class CtorInfo
	{
		public List<StructField> Fields = new List<StructField>();
		public ConstructorInfo Ctori;

		public CtorInfo(ConstructorInfo constructor)
		{
			Ctori = constructor;
			var pars = constructor.GetParameters();
			foreach(var par in pars)
			{
				string name = par.Name;
				name.SetChar(0,name[0].ToUpper());
				var field = new StructField(name,par.DefaultValue);
				Fields.Add(field);
			}
		}
		public T Ctor<T>(List<StructField> fields)
		{
			var vals = new List<object>();
			fields.ForEach((StructField obj) => vals.Add(obj.Value));
			return (T)Ctori.Invoke(vals.ToArray());
		}
	}
}
