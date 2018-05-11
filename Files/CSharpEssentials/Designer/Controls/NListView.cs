using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;

namespace CSharpEssentials.Designer.Controls
{
	public class NListView : ListView
	{
		bool canDragItems;
		[Description("The user can drag items of this list")]
		public bool CanDragItems
		{
			get
			{
				return canDragItems;
			}
			set
			{
				canDragItems = value;
			}
		}
		public ListViewItem CurrentMouseItem
		{
			get
			{
				var m = MousePos;
				var item = this.GetItemAt(m.X,m.Y);
				return item;
			}
		}
		public Point MousePos
		{
			get
			{
				return this.MousePos();
			}
		}
		public NListView()
		{
			this.OwnerDraw = true;
		}
		List<ListViewItem> Hided = new List<ListViewItem>();
		protected override void OnDrawItem (DrawListViewItemEventArgs e)
		{
			if(!Hided.Contains(e.Item))
			{
				e.DrawBackground();
				e.DrawFocusRectangle();
				e.DrawText();
			}
			base.OnDrawItem (e);
		}

		ListViewItem[] dragging;
		protected override void OnMouseDown (MouseEventArgs e)
		{
			if(canDragItems)
			{
				var mPos = MousePos;
				if(this.SelectedItems.Count == 0)
				{
					dragging = new ListViewItem[]{GetItemAt(mPos.X,mPos.Y)};
					this.SelectItems(dragging);
				}else
				{
					dragging = SelectedItems.ToArray<ListViewItem>();
				}
			}
			base.OnMouseDown (e);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			if(beginDragged)
			{
				Cursor = DefaultCursor;
				if(image != null)
				{
					image.Close();
					image = null;
				}
				dragging = null;
			}
			base.OnMouseUp (e);
		}
		bool beginDragged;
		FloatingImageGrid image;
		protected override void OnMouseMove (MouseEventArgs e)
		{
			if(dragging != null && dragging[0] != CurrentMouseItem)
			{
				beginDragged = true;
				Cursor = Cursors.Cross;
				if(image == null)
				{
					//var img = LargeImageList.Images[dragging[0].ImageIndex];
					var imgs = GetAllImagesOf(dragging);
					image = FloatingImageGrid.GetImage(this,imgs,Color.Gray,null);
					image.KeepInMousePos(true,new Point(3,3),imgs.GetTotalSize(5));
				}
			}
			base.OnMouseMove (e);
		}
		bool EqualTo(ListViewItem[] items,ListViewItem item)
		{
			int equal = 0;
			foreach(var _item in items)
			{
				if(_item == item)
				{
					equal++;
				}
			}
			if(equal >= items.Length-1)
			{
				return true;
			}else
			{
				return false;
			}
		}
		Image[] GetAllImagesOf(ListViewItem[] items)
		{
			List<Image> imgs = new List<Image>();
			foreach(var item in items)
			{
				if(item != null)
				{
					int imgIndex = item.ImageIndex;
					var img = this.LargeImageList.Images[imgIndex];
					imgs.Add(img);
				}
			}
			return imgs.ToArray();
		}
		/// <summary>
		/// Filters the items and removes not matching items.
		/// </summary>
		/// <param name="filter">Filter.</param>
		public void FilterItems(Predicate<ListViewItem> filter)
		{
			foreach(ListViewItem item in Items)
			{
				if(!filter(item))
				{
					if(!Hided.Contains(item)){Hided.Add(item);}
					//item.Remove();
				}else
				{
					Hided.Remove(item);
				}
			}
		}
	}
}
