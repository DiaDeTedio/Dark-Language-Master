using System;
using System.Drawing;

namespace CSharpEssentials
{
	/// <summary>
	/// The Arithmetics Class,to do util calculations in your code.
	/// </summary>
	public class Arithmetics
	{
		/// <summary>
		/// Gets the size of the images by max in XPosition to down a line.
		/// </summary>
		/// <returns>The images size.</returns>
		/// <param name="images">Images.</param>
		/// <param name="maxX">Max x.</param>
		public static Size GetImagesSize(Image[] images,int maxX)
		{
			if(images.Length > 0)
			{
				Size ret = new Size(0,images[0].Height);
				int curX = 0;
				int index = 0;
				foreach(var image in images)
				{
					if(index <= maxX)
					{
						ret.Width += image.Width;
					}
					if(curX >= maxX)
					{
						ret.Height += image.Height;
						curX = 0;
					}

					index++;
					curX++;
				}
				return ret;
			}else
			{
				return new Size(1,1);
			}
		}
		/// <summary>
		/// Gets the size of the images.
		/// </summary>
		/// <returns>The images size.</returns>
		/// <param name="images">Images.</param>
		public static Size GetImagesSize(Image[] images)
		{
			Size ret = new Size();
			foreach(var image in images)
			{
				ret.Width += image.Width;
				ret.Height += image.Height;
			}
			return ret;
		}
		public static Point Plus(Point a,Point b)
		{
			return new Point(a.X+b.X,a.Y+b.Y);
		}
		public static bool IsMultiple(int a,int b)
		{
			float res = a/b;
			if(res.ToString().Split('.').Length > 10)
			{
				return false;
			}else
			{
				return true;
			}
		}
	}
	public static class ArthUtilsExt
	{
		public static bool LessThan(this Point a,Point b)
		{
			if(a.X < b.X || a.Y < b.Y)
			{
				return true;
			}else
			{
				return false;
			}
		}
		public static bool LessThan(this PointF a,PointF b)
		{
			if(a.X < b.X || a.Y < b.Y)
			{
				return true;
			}else
			{
				return false;
			}
		}
		public static bool LessThan(this Size a,Size b)
		{
			if(a.Width < b.Width || a.Height < b.Height)
			{
				return true;
			}else
			{
				return false;
			}
		}

		public static bool LessThanOrEqualTo(this Size a,Size b)
		{
			if(a.Width <= b.Width || a.Height <= b.Height)
			{
				return true;
			}else
			{
				return false;
			}
		}
	}
}
