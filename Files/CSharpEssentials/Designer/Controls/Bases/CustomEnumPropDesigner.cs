using System;
using CSharpEssentials.Bases;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing;
using System.ComponentModel;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class CustomEnumPropDesigner : BasePropDesigner<CustomEnum>
	{
		public string Value
		{
			get
			{
				return Target.SelectedItem;
			}
			set
			{
				foreach(var item in Target.Items)
				{
					if(value == item)
					{
						Target.SelectedItem = item;
						PropertyGrid.Invalidate();
						return;
					}
				}
				ContextMenu menu = new ContextMenu();
				foreach(var item in Target.Items)
				{
					menu.MenuItems.Add(item,onCheck);
				}
				menu.Show(PropertyGrid,PropertyGrid.MousePos);
			}
		}
		void onCheck(object sender,EventArgs e)
		{
			Target.SelectedItem = ((MenuItem)sender).Text;
			PropertyGrid.SelectedObject = Target;
		}
		public override bool UseForInheritedTypes
		{
			get
			{
				return true;
			}
		}

		public CustomEnumPropDesigner(object target) : base(target)
		{

		}
	}
}
