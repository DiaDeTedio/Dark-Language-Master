using System;
using CSharpEssentials.Designer.Controls.Bases;
using System.Drawing;
using CSharpEssentials.Utils;

namespace CSharpEssentials.Designer.Controls
{
	public class DrawArea : FloatingControl
	{
		/// <summary>
		/// To do on paint event of this control.
		/// </summary>
		public ActionCollection<Graphics> ToDoOnPaint = new ActionCollection<Graphics>();
		/// <summary>
		/// The graphics of this control.
		/// </summary>
		public Graphics graphics;

		public DrawArea()
		{
			graphics = CreateGraphics();
			this.DoubleBuffered = true;
		}
		protected override void OnPaint (System.Windows.Forms.PaintEventArgs e)
		{
			foreach(var todo in ToDoOnPaint)
			{
				todo.Invoke(e.Graphics);
			}
			base.OnPaint (e);
		}
	}
}
