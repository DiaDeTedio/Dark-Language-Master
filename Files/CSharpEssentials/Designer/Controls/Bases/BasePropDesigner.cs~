using System;
using System.ComponentModel;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class BasePropDesigner<T> : BasePropDesigner
	{
		public override Type Type
		{
			get
			{
				return typeof(T);
			}
		}
		public override bool UseForInheritedTypes
		{
			get
			{
				return false;
			}
		}
		public T Target;
		public BasePropDesigner(object target)
		{
			if(target is T)
			{
				Target = (T)target;
			}
		}
		public static implicit operator T(BasePropDesigner<T> des)
		{
			return des.Target;
		}
		public static implicit operator BasePropDesigner<T>(T from)
		{
			return new BasePropDesigner<T>(from);
		}
	}
	public abstract class BasePropDesigner
	{
		[Browsable(false)]
		public abstract bool UseForInheritedTypes{get;}
		[Browsable(false)]
		public abstract Type Type{get;}
		NPropertyGrid stastsstagrid = new NPropertyGrid();
		[Browsable(false)]
		public NPropertyGrid PropertyGrid{get{return stastsstagrid;}}
		protected BasePropDesigner()
		{

		}
	}
}
