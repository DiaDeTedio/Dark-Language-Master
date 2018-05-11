using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace CSharpEssentials.Designer.Controls.Specials.Classes
{
	[Serializable]
	public class AdvancedListViewItem //: ISerializable
	{
		/// <summary>
		/// The source of this item,that contains,draw, and holds it.
		/// </summary>
		public AdvancedListView Source;
		string text;
		/// <summary>
		/// The text of this item.
		/// </summary>
		[Description("The text of this item")]
		public string Text
		{
			get{return text;}
			set{text = value;Invalidate();}
		}
		Image image;
		/// <summary>
		/// The image of this item.
		/// </summary>
		[Description("The image of this item")]
		public Image Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;
				image = image.GetThumb(50,50);
				//image = image.SetSize(new Size(50,50));
				//var bmp = (Bitmap)image;
				//bmp.SetResolution(50,50);
				//image = bmp;
				Invalidate();
			}
		}
		bool visible = true;
		/// <summary>
		/// This item is visible?.
		/// </summary>
		[Description("This item is visible?")]
		public bool Visible{get{return visible;}set{visible = value;Invalidate();}}
		bool selected;
		/// <summary>
		/// This item is selected?.
		/// </summary>
		[Description("This item is selected?")]
		public bool Selected
		{
			get
			{
				return selected;
			}
			set
			{
				var prev = selected;
				selected = value;
				if(prev != selected)
				{
					Invalidate();
				}
			}
		}
		object tag;
		[Description("The optional value of this object")]
		public object Tag
		{
			get{return tag;}
			set{tag = value;}
		}
		/// <summary>
		/// Gets the bounds of this item.
		/// </summary>
		/// <value>The bounds.</value>
		[Description("The local that this item is drawed")]
		public Rectangle Bounds
		{
			get
			{
				return bounds;
			}
		}
		Rectangle bounds;

		public void DrawItem(Graphics g,Rectangle rect,Color backColor,Color textColor,Font textFont,Color selectedColor)
		{
			Rectangle imageR = GetImageRect(rect);
			Rectangle textR = GetTextRect(rect);
			bounds = rect;
			SolidBrush backBrush = new SolidBrush(backColor);
			SolidBrush textBrush = new SolidBrush(textColor);
			if(Selected)
			{
				backBrush.Color = selectedColor;
			}
			g.FillRectangle(backBrush,rect);
			if(Image != null)
			{
				g.DrawImage(Image,imageR);
			}
			//rect.Y -= rect.Height-5;
			g.DrawString(Text,textFont,textBrush,textR);

			backBrush.Dispose();
			textBrush.Dispose();
		}
		Rectangle GetImageRect(Rectangle rect)
		{
			rect.Width -= 5;
			rect.Height -= 5;
			rect.X += 2;
			rect.Y += 2;
			return rect;
		}
		Rectangle GetTextRect(Rectangle rect)
		{
			rect.Y += rect.Height-5;
			return rect;
		}
		public void Invalidate()
		{
			if(Source != null)
			{
				Source.Invalidate(Bounds);
			}
		}

		//void ISerializable.GetObjectData (SerializationInfo info,StreamingContext context)
		//{
		//	info.AddValue("Text",Text);
		//	info.AddValue("Image",Image);
		//	info.AddValue("Selected",Selected);
		//	info.AddValue("Visible",Visible);
		//	info.AddValue("Bounds",bounds);
		//}
		//AdvancedListViewItem(SerializationInfo info,StreamingContext context)
		//{
		//	Text = (string)info.GetValue("Text");
		//	Image = (Image)info.GetValue("Image");
		//	Selected = info.GetBoolean("Selected");
		//	Visible = info.GetBoolean("Visible");
		//	bounds = (Rectangle)info.GetValue("Bounds");
		//}
		public AdvancedListViewItem()
		{
			
		}
	}
	[Serializable]
	public class AdvancedListViewItemCollection : ICollection<AdvancedListViewItem>,IList<AdvancedListViewItem>,IEnumerable<AdvancedListViewItem>//,ISerializable
	{
		List<AdvancedListViewItem> items = new List<AdvancedListViewItem>();

		public AdvancedListViewItem this [int index]
		{
			get
			{
				return ((IList<AdvancedListViewItem>)items) [index];
			}

			set
			{
				((IList<AdvancedListViewItem>)items) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<AdvancedListViewItem>)items).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<AdvancedListViewItem>)items).IsReadOnly;
			}
		}

		public void Add (AdvancedListViewItem item)
		{
			((ICollection<AdvancedListViewItem>)items).Add (item);
		}
		public void Add(string text,Image img = null,object tag = null)
		{
			AdvancedListViewItem item = new AdvancedListViewItem();
			item.Text = text;
			item.Image = img;
			item.Tag = tag;
			items.Add(item);
		}

		public void Clear ()
		{
			((ICollection<AdvancedListViewItem>)items).Clear ();
		}

		public bool Contains (AdvancedListViewItem item)
		{
			return ((ICollection<AdvancedListViewItem>)items).Contains (item);
		}

		public void CopyTo (AdvancedListViewItem [] array,int arrayIndex)
		{
			((ICollection<AdvancedListViewItem>)items).CopyTo (array,arrayIndex);
		}

		public IEnumerator<AdvancedListViewItem> GetEnumerator ()
		{
			return ((ICollection<AdvancedListViewItem>)items).GetEnumerator ();
		}

		public int IndexOf (AdvancedListViewItem item)
		{
			return ((IList<AdvancedListViewItem>)items).IndexOf (item);
		}

		public void Insert (int index,AdvancedListViewItem item)
		{
			((IList<AdvancedListViewItem>)items).Insert (index,item);
		}

		public bool Remove (AdvancedListViewItem item)
		{
			return ((ICollection<AdvancedListViewItem>)items).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<AdvancedListViewItem>)items).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<AdvancedListViewItem>)items).GetEnumerator ();
		}
		public void ForEach(Action<AdvancedListViewItem> action)
		{
			items.ForEach(action);
		}
		public void SelectItems(IEnumerable<AdvancedListViewItem> _items,bool selected)
		{
			foreach(var item in _items)
			{
				item.Selected = selected;
			}
		}
		public void DeselectAll()
		{
			items.ForEach((obj) => obj.Selected = false);
		}

		//void ISerializable.GetObjectData (SerializationInfo info,StreamingContext context)
		//{
		//	byte[] Ubytes = CSEMainClass.GetByteData(items);
		//	var bytes = Ubytes.ToStringB();
		//	info.AddValue("Items",bytes);
		//	//int index = 0;
		//	//foreach(var item in items)
		//	//{
		//	//	info.AddValue(index.ToString(),item,typeof(AdvancedListViewItem));
		//	//	index++;
		//	//}
		//}
		//AdvancedListViewItemCollection(SerializationInfo info,StreamingContext context)
		//{
		//	//string Sbytes = info.GetString("Items");
		//	//var bytes = CSEMainClass.FromString(Sbytes);
		//	//items = CSEMainClass.GetObjectFromBytes<List<AdvancedListViewItem>>(bytes);
		//	//items = (List<AdvancedListViewItem>)info.GetValue("Items");
		//	//int max = info.MemberCount;
		//	//for(int index=0;index<max;index++)
		//	//{
		//	//	var item = (AdvancedListViewItem)info.GetValue(index.ToString(),typeof(AdvancedListViewItem));
		//	//	items.Add(item);
		//	//}
		//}
		public AdvancedListViewItemCollection()
		{
			items = new List<AdvancedListViewItem>();
		}

		public static implicit operator AdvancedListViewItemCollection(AdvancedListViewItem[] array)
		{
			var ret = new AdvancedListViewItemCollection();
			array.ForEach((obj) => ret.Add(obj));
			return ret;
		}
		public static implicit operator AdvancedListViewItemCollection(List<AdvancedListViewItem> list)
		{
			var ret = new AdvancedListViewItemCollection();
			list.ForEach((obj) => ret.Add(obj));
			return ret;
		}
		public static implicit operator AdvancedListViewItem[](AdvancedListViewItemCollection collection)
		{
			return collection.ToArray();
		}
		public static implicit operator List<AdvancedListViewItem>(AdvancedListViewItemCollection collection)
		{
			return collection.items;
		}
	}
}
