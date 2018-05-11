using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace CSharpEssentials.Designer.Controls
{
	/// <summary>
	/// Extensor control,to extend and retract a control.
	/// </summary>
	[Description("To extend and retract a control and it childs")]
	public class ExtensorControl : Control
	{
		bool expanded;
		[Description("This is expanded?")]
		public bool Expanded
		{
			get
			{
				return expanded;
			}
			set
			{
				expanded = value;
				CheckExpansion();
			}
		}
		Control expansible;
		[Description("The control to expand/contract")]
		public Control Expansible
		{
			get
			{
				return expansible;
			}
			set
			{
				expansible = value;
			}
		}
		Size expandSize;
		[Description("The size of main parent when it has expanded")]
		public Size ExpandSize
		{
			get{return expandSize;}
			set{expandSize = value;}
		}
		Size originalSize;
		public ExtensorControl ()
		{
			Size = new Size(30,30);
		}
		protected override void OnParentChanged (EventArgs e)
		{
			if(Parent != null)
			{
				originalSize = Parent.Size;
			}
			base.OnParentChanged (e);
		}
		Color color = Color.Black;
		protected override void OnPaint (PaintEventArgs e)
		{
			string charactere = "▲";
			if(Expanded)
			{
				charactere = "▼";
			}
			e.Graphics.DrawString(charactere,Font,new SolidBrush(color),Bounds);
			base.OnPaint (e);
		}
		protected override void OnMouseEnter (EventArgs e)
		{
			color = Color.Gray;
			Invalidate();
			base.OnMouseEnter (e);
		}
		protected override void OnMouseLeave (EventArgs e)
		{
			color = Color.Black;
			Invalidate();
			base.OnMouseLeave (e);
		}
		/// <summary>
		/// Expand this instance.
		/// </summary>
		public void Expand()
		{
			Expanded = true;
		}
		/// <summary>
		/// Contract this instance.
		/// </summary>
		public void Contract()
		{
			Expanded = false;
		}
		public void CheckExpansion()
		{
			if(Expansible != null)
			{
				if(Expanded)
				{
					if(Parent != null)
					{
						originalSize = Parent.Size;
						Parent.Size = ExpandSize;
					}
					Expansible.Visible = true;
				}else
				{
					if(Parent != null)
					{
						Parent.Size = originalSize;
					}
					Expansible.Visible = false;
				}
			}

			Invalidate();
		}
		protected override void OnClick (EventArgs e)
		{
			Expanded = !Expanded;
			base.OnClick (e);
		}
	}
}
