using System;
using System.Windows.Forms;
using System.Collections.Generic;
using CSharpEssentials.Designer.Controls.Bases;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class NMenuItem : MenuItem
	{
		public List<NCondition<NMenuItem>> OnDrawCheck = new List<NCondition<NMenuItem>>();

		protected override void OnDrawItem (DrawItemEventArgs e)
		{
			foreach(var check in OnDrawCheck)
			{
				check.Invoke();
			}
			base.OnDrawItem (e);
		}
		protected override void OnPopup (EventArgs e)
		{
			base.OnPopup (e);
		}

		public NMenuItem(string caption,EventHandler click)
		{
			this.Text = caption;
			this.Click += click;
		}
	}
	
}
