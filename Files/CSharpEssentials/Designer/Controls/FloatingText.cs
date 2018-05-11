using System;
using System.Drawing;
using CSharpEssentials.Designer.Controls.Bases;
using System.Windows.Forms;
namespace CSharpEssentials.Designer.Controls
{
	public class FloatingText : DrawArea
	{
		string DrawText;
		Color DrawColor;
		decimal? DisplayTime;
		public FloatingText (string text,Color textColor,Color backColor,decimal? displayTime = null)
		{
			BackColor = backColor;
			DrawText = text;
			DrawColor = textColor;
			DisplayTime = displayTime;
			this.ToDoOnPaint += DrawTheText;
		}
		public FloatingText() : base()
		{
			this.ToDoOnPaint += DrawTheText;
		}
		void DrawTheText(Graphics g)
		{
			g.DrawString(DrawText,Font,new SolidBrush(DrawColor),ClientRectangle);
		}
		//protected override void OnPaint (PaintEventArgs e)
		//{
		//	DrawTheText(e.Graphics);
		//	base.OnPaint (e);
		//}
		decimal currentTime;
		protected override void OnTimerTick ()
		{
			if(DisplayTime != null && currentTime >= DisplayTime)
			{
				Close();
			}
			this.Enabled = false;
			currentTime++;
			//Refresh();
			base.OnTimerTick ();
		}

		public static FloatingText GetText(IWin32Window own,string text,Color textColor,Color backColor,decimal? displayTime=1)
		{
			var ft = DisplayFloatingControl<FloatingText>(own,300,300,false);
			ft.BackColor = backColor;
			ft.DrawText = text;
			ft.DrawColor = textColor;
			ft.DisplayTime = displayTime;
			ft.Refresh();
			return ft;
		}
	}
}
