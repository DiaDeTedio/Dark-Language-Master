using System;
using System.Reflection;

namespace CSharpEssentials.Bases
{
	/// <summary>
	/// Runtime field that handles a field in a specific instance.
	/// </summary>
	public class RuntimeField
	{
		/// <summary>
		/// Gets or sets the value of this field.
		/// </summary>
		/// <value>The value.</value>
		public object Value
		{
			get
			{
				return GetValue();
			}
			set
			{
				SetValue(value);
			}
		}
		/// <summary>
		/// Gets the type of the handled field.
		/// </summary>
		/// <value>The type.</value>
		public Type Type
		{
			get
			{
				return Field.FieldType;
			}
		}
		/// <summary>
		/// Gets the name of the field.
		/// </summary>
		/// <value>The name.</value>
		public string Name{get{return Field.Name;}}
		/// <summary>
		/// The field that are being handled.
		/// </summary>
		public readonly FieldInfo Field;
		/// <summary>
		/// The instance that contains the field.
		/// </summary>
		public object Instance;

		public RuntimeField(object instance,string fieldName)
		{
			Instance = instance;
			var field = CSEMainClass.FindField(instance,fieldName);
			if(field != null)
			{
				Field = field;
			}else
			{
				//throw new Exception($"The Field {fieldName},in {instance},has not found!!!");
			}
		}
		public object GetValue()
		{
			return Field.GetValue(Instance);
		}
		public void SetValue(object value)
		{
			Field.SetValue(Instance,value);
		}
		public override string ToString ()
		{
			//return "";
			return Name+" = "+Value+" in "+Instance;
			//return $"{Name} = {Value} in {Instance}";
		}
	}
}
