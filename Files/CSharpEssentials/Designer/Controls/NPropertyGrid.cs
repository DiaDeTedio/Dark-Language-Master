using System;
using System.Windows.Forms;
using System.Collections.Generic;
using CSharpEssentials.Designer.Controls.Bases;
using CSharpEssentials.Bases;
using System.Reflection;
using System.Drawing;

namespace CSharpEssentials.Designer.Controls
{
	public class NPropertyGrid : PropertyGrid
	{
		public Point MousePos
		{
			get
			{
				return PointToClient(MousePosition);
			}
		}
		static Type[] AvaliableEditors = null;
		protected override void OnCreateControl ()
		{
			base.OnCreateControl ();
		}
		public void InitializeNPropGrid()
		{
			if(AvaliableEditors == null)
			{
				AvaliableEditors = CSEMainClass.GetAllTypesOfBaseType(typeof(BasePropDesigner));
			}
		}
		static BasePropDesigner GetPropDesigner(Type type,object target,NPropertyGrid grid)
		{
			foreach(var avaliable in AvaliableEditors)
			{
				if(avaliable.Name != "BasePropDesigner`1")
				{
					var ins = (BasePropDesigner)Activator.CreateInstance(avaliable,new object[]{target});
					var ___grid = CSEMainClass.FindField(ins,"stastsstagrid");
					___grid.SetValue(ins,grid);
					if(ins.Type == type)
					{
						return ins;
					}
					if(ins.UseForInheritedTypes)
					{
						Type bas = type.BaseType;
						while(bas != null)
						{
							if(ins.Type == bas)
							{
								return ins;
							}
							bas = bas.BaseType;
						}
					}
				}
			}
			return null;
		}
		protected override void OnSelectedObjectsChanged (EventArgs e)
		{
			if(!DesignMode && SelectedObject != null)
			{
				var obj = SelectedObject;
				var des = GetPropDesigner(obj.GetType(),obj,this);
				if(des != null)
				{
					obj = des;
					SelectedObject = obj;
				}
			}
			base.OnSelectedObjectsChanged (e);
		}
	}
}
