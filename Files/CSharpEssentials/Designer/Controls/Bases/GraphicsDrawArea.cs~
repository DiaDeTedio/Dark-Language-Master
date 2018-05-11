using System;
using System.Windows.Forms;
using System.Drawing;

namespace CSharpEssentials.Designer.Controls.Bases
{
	public class GraphicsDrawArea : Control
	{
		public Graphics g;
		public GraphicsDrawArea ()
		{
			g = CreateGraphics();
			//this.BackColor = Color.FromArgb(10,0,0,0);
			this.Capture = false;
			this.Enabled = false;
			g.Clear(Color.Transparent);
		}
		public static GraphicsDrawArea CreateDrawArea(Control main,int width = 10,int height = 10)
		{
			GraphicsDrawArea draw = new GraphicsDrawArea();
			draw.Parent = main;
			draw.Size = new Size(width,height);
			return draw;
		}
	}
}
