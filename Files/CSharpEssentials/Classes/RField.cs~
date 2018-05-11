using System;
using CSharpEssentials.Bases;

namespace CSharpEssentials.Classes
{
	/// <summary>
	/// Runtime Field Handler to a Generic type of value,you can set the "T" object to this implicitly,
	/// and set this value to new by typping + in this,example:::
	/// public "T" FieldName;
	/// RField "T" field = new RField(this,"FieldName");
	/// FieldName = field;
	/// FieldName = newValue of "T";
	/// field = FieldName:WRONG:::field += FieldName:CORRECT,or field.Value = FieldName;
	/// Bye.
	/// </summary>
	public class RField<T> : RuntimeField
	{
		public new T Value
		{
			get
			{
				return (T)GetValue();
			}
			set
			{
				SetValue(value);
			}
		}
		public RField (object instance,string fieldName) : base(instance,fieldName)
		{
			
		}
		public static implicit operator T(RField<T> from)
		{
			return from.Value;
		}
		public static RField<T> operator +(RField<T> from,T to)
		{
			from.Value = to;
			return from;
		}
	}
}
