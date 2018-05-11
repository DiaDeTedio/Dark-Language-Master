using System;
using System.Drawing;
using CSharpEssentials.Designer.Controls.Bases;
using System.Windows.Forms;
namespace CSharpEssentials.Designer.Controls
{
	public class FloatingImage : DrawArea
	{
		Image DrawImage;
		decimal? DisplayTime;
		public FloatingImage (Image image,Color backColor,decimal? displayTime = null)
		{
			BackColor = backColor;
			DrawImage = image;
			DisplayTime = displayTime;
			this.ToDoOnPaint += DrawTheImage;
		}
		public FloatingImage() : base()
		{
			this.ToDoOnPaint += DrawTheImage;
		}
		void DrawTheImage(Graphics g)
		{
			g.DrawImage(DrawImage,ClientRectangle);
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

		public static FloatingImage GetImage(IWin32Window own,Image image,Color backColor,decimal? displayTime=1)
		{
			var ft = DisplayFloatingControl<FloatingImage>(own,300,300,false);
			ft.BackColor = backColor;
			ft.DrawImage = image;
			ft.DisplayTime = displayTime;
			ft.Refresh();
			return ft;
		}
	}
}
