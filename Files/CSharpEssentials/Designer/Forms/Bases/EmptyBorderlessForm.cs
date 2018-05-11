using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace CSharpEssentials.Designer.Forms.Bases
{
	/// <summary>
	/// Empty borderless form to make your controls.
	/// </summary>
	public class EmptyBorderlessForm : Form
	{
		Keys? CloseKey = null;
		public EmptyBorderlessForm ()
		{
			Size = new System.Drawing.Size(100,100);
			this.FormBorderStyle = FormBorderStyle.None;
			this.ShowInTaskbar = false;
			this.Capture = false;
			AllowTransparency = true;
		}
		public static EmptyBorderlessForm DisplayNewForm(IWin32Window owner = null,int width = 100,int height = 100,bool hasDialog = false,Point? pos = null,Keys? closeKey = null)
		{
			EmptyBorderlessForm form = new EmptyBorderlessForm();
			form.Size = new System.Drawing.Size(width,height);
			form.CloseKey = closeKey;
			if(pos != null)
			{
				form.Location = pos.Value;
			}
			if(hasDialog)
			{
				form.ShowDialog(owner);
			}else
			{
				form.Show(owner);
			}
			return form;
		}
		public static T DisplayNewForm<T>(IWin32Window owner = null,int width = 100,int height = 100,bool hasDialog = false,Point? pos = null,Keys? closeKey = null) where T : EmptyBorderlessForm
		{
			//T form = (T)new EmptyBorderlessForm ();
			T form = Activator.CreateInstance<T>();
			form.Size = new System.Drawing.Size(width,height);
			form.CloseKey = closeKey;
			if(pos != null)
			{
				form.Location = pos.Value;
			}
			if(hasDialog)
			{
				form.ShowDialog(owner);
			}else
			{
				form.Show(owner);
			}
			return form;
		}
		protected override void OnKeyDown (KeyEventArgs e)
		{
			if(CloseKey != null)
			{
				if(e.KeyCode == CloseKey.Value)
				{
					Close();
				}
			}
			base.OnKeyDown (e);
		}
		public void ChangePos(int? x = null,int? y = null)
		{
			var loc = this.Location;
			if(x != null){loc.X = x.Value;}
			if(y != null){loc.Y = y.Value;}
			this.Location = loc;
		}
	}
}
