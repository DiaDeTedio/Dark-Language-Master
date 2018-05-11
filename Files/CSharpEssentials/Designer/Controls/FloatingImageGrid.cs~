using System;
using System.Drawing;
using CSharpEssentials.Designer.Controls.Bases;
using System.Windows.Forms;
namespace CSharpEssentials.Designer.Controls
{
	public class FloatingImageGrid : DrawArea
	{
		Image[] DrawImages;
		decimal? DisplayTime;
		public FloatingImageGrid (Image[] images,Color backColor,decimal? displayTime = null)
		{
			BackColor = backColor;
			DrawImages = images;
			DisplayTime = displayTime;
			this.ToDoOnPaint += DrawTheImages;
		}
		public FloatingImageGrid() : base()
		{
			this.ToDoOnPaint += DrawTheImages;
		}
		void DrawTheImages(Graphics g)
		{
			Rectangle max = ClientRectangle;
			Rectangle rect = new Rectangle();
			foreach(var image in DrawImages)
			{
				rect.Width = image.Width;
				rect.Height = image.Height;
				g.DrawImage(image,rect);
				rect.X += image.Width;
				if(rect.X > max.Width)
				{
					rect.Y += image.Height;
					rect.X = 0;
				}
			}
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

		public static FloatingImageGrid GetImage(IWin32Window own,Image[] images,Color backColor,decimal? displayTime=1)
		{
			var ft = DisplayFloatingControl<FloatingImageGrid>(own,300,300,false);
			ft.BackColor = backColor;
			ft.DrawImages = images;
			ft.DisplayTime = displayTime;
			ft.Refresh();
			return ft;
		}
	}
}
