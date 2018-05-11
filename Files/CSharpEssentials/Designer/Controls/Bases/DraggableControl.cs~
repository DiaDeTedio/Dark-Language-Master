using System;
using System.Windows.Forms;
using System.Drawing;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class DraggableControl : Control
	{
		bool beginDragged;
		protected override void OnMouseDown (MouseEventArgs e)
		{
			beginDragged = true;
			base.OnMouseDown (e);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			beginDragged = false;
			image.Close();
			image = null;
			base.OnMouseUp (e);
		}
		FloatingImage image;
		protected override void OnMouseMove (MouseEventArgs e)
		{
			if(beginDragged)
			{
				if(image == null)
				{
					image = FloatingImage.GetImage(this,this.BackgroundImage,Color.Gray,null);
					image.KeepInMousePos(true,new Point(5,5),this.Size);
				}
			}
			base.OnMouseMove (e);
		}
	}
}
