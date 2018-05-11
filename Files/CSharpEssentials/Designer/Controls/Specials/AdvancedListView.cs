using System;
using System.Windows.Forms;
using CSharpEssentials.Designer.Controls.Specials.Classes;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CSharpEssentials.Designer.Controls.Bases;

namespace CSharpEssentials.Designer.Controls.Specials
{
	//[Serializable]
	public class AdvancedListView : NControl
	{
		#region Properties
		public new event OnItemDoubleClickHandler DoubleClick;
		public event OnBeginDragItems OnBeginDrag;
		public event OnEndDragItems OnEndDrag;

		bool multiselection = true;
		[Description("The user can selecte more than one items?")]
		public bool Multiselection
		{
			get{return multiselection;}
			set{multiselection = value;}
		}
		bool canDrag;
		[Description("The user can drag items of this list?")]
		public bool CanDragItems
		{
			get
			{
				return canDrag;
			}
			set
			{
				canDrag = value;
			}
		}
		Size spacing = new Size(1,1);
		[Description("The space between items,in X = width,and in Y = height")]
		public Size ItemSpacing;
		AdvancedListViewItemCollection items = new AdvancedListViewItemCollection();
		Size itemSize = new Size(30,30);
		[Description("The item size")]
		public Size ItemSize{get{return itemSize;}set{itemSize = value;Invalidate();}}
		Color itemColor = Color.Gray,selectedItemColor = Color.Blue;
		[Description("The item color")]
		public Color ItemColor{get{return itemColor;}set{itemColor = value;Invalidate();}}
		[Description("The item color when it is selected")]
		public Color SelectedItemColor{get{return selectedItemColor;}set{selectedItemColor = value;Invalidate();}}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Description("Do not modify it at design mode of any program,only use this list if you are to add in runtime")]
		public AdvancedListViewItemCollection Items
		{
			get
			{
				return items;
			}
			set
			{
				items = value;
				Invalidate();
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AdvancedListViewItem[] ItemArray
		{
			get
			{
				return Items;
			}
			set
			{
				Items = value;
			}
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public List<AdvancedListViewItem> ItemList
		{
			get
			{
				return Items;
			}
			set
			{
				Items = value;
			}
		}
		/// <summary>
		/// Gets or sets the selected items.
		/// </summary>
		/// <value>The selected items.</value>
		public AdvancedListViewItem[] SelectedItems
		{
			get
			{
				List<AdvancedListViewItem> _items = new List<AdvancedListViewItem>();
				foreach(var item in items)
				{
					if(item.Selected)
					{
						_items.Add(item);
					}
				}
				return _items.ToArray();
			}
			set
			{
				items.DeselectAll();
				foreach(var item in value)
				{
					item.Selected = true;
				}
			}
		}
		/// <summary>
		/// Gets the current in mouse item.
		/// </summary>
		/// <value>The mouse item.</value>
		public AdvancedListViewItem MouseItem
		{
			get
			{
				return GetItemAt(this.MousePos());
			}
		}
		#endregion

		#region Common
		public void CompleteInvalidate()
		{
			CreateGraphics().Clear(BackColor);
		}
		public void AddToNextDraw(AdvancedListViewItem[] _items)
		{
			_items.ForEach((obj) => items.Add(obj));
			Invalidate();
		}
		protected override void OnPaint (PaintEventArgs e)
		{
			//if(!inv)
			//{
				DrawItems(e.Graphics);
			//}
			//inv = false;
			//base.OnPaint (e);
		}
		protected Rectangle drawBounds;
		protected void DrawItems(Graphics g)
		{
			drawBounds = new Rectangle(new Point(),itemSize);
			if(Items == null){Items = new AdvancedListViewItemCollection();}
			foreach(var item in Items)
			{
				DrawItem(item,g);
			}
		}
		protected void DrawItem(AdvancedListViewItem item,Graphics g)
		{
			if(item.Visible)
			{
				item.Source = this;
				item.DrawItem(g,drawBounds,itemColor,ForeColor,Font,selectedItemColor);
				if(drawBounds.X >= ClientSize.Width)
				{
					drawBounds.X = 0;
					drawBounds.Y += itemSize.Height;
					drawBounds.Y += spacing.Height;
				}
				drawBounds.X += itemSize.Width;
				drawBounds.X += spacing.Width;
			}
		}
		public AdvancedListViewItem GetItemAt(Point point)
		{
			foreach(var it in Items)
			{
				if(it.Bounds.Contains(point))
				{
					return it;
				}
			}
			return null;
		}
		public AdvancedListViewItem GetItemWith(Rectangle rect)
		{
			foreach(var it in Items)
			{
				if(it.Bounds == rect)
				{
					return it;
				}
			}
			return null;
		}
		protected override void OnClick (EventArgs e)
		{
			if(!isDragging)
			{
				var item = GetItemAt(this.MousePos());
				if(item != null)
				{
					SelectItem(item);
					//item.Selected = !item.Selected;
				}
			}
			base.OnClick (e);
		}
		protected override void OnDoubleClick (EventArgs e)
		{
			if(MouseItem != null)
			{
				DoubleClick(MouseItem);
			}
		}
		public void SelectItem(AdvancedListViewItem item)
		{
			if(!dontSelect)
			{
				if(ModifierKeys == Keys.Control)
				{
					if(Multiselection)
					{
						item.Selected = !item.Selected;
					}
				}
				else if(ModifierKeys == Keys.Shift)
				{
					if(Multiselection)
					{
						var selecteds = SelectedItems;
						if(selecteds.Length > 0)
						{
							var first = selecteds[0];
							if(item != first)
							{
								var fIndex = selecteds.IndexOf(first);
								var iIndex = this.ItemList.IndexOf(item);
								int subs = fIndex+iIndex;
								if(subs < -1)
								{
									subs *= -1;
								}
								if(subs > 0 && subs <= ItemList.Count)
								{
									for(int i=fIndex;i<fIndex+subs;i++)
									{
										ItemList[i].Selected = true;
									}
								}
							}
						}
					}
				}
				else
				{
					items.DeselectAll();
					item.Selected = true;
				}
			}else
			{
				dontSelect = false;
			}
		}
		/// <summary>
		/// Clear all items of this list.
		/// </summary>
		public void Clear()
		{
			Items.Clear();
		}
		//bool inv;
		//protected override void OnInvalidated (InvalidateEventArgs e)
		//{
		//	var item = GetItemWith(e.InvalidRect);
		//	if(item != null)
		//	{
		//		item.DrawItem(CreateGraphics(),item.Bounds,itemColor,ForeColor,Font,selectedItemColor);
		//		inv = true;
		//	}else
		//	{
		//		base.OnInvalidated (e);
		//	}
		//}

		public void GetObjectData (SerializationInfo info,StreamingContext context)
		{
			info.AddValue("Items",items);
			info.AddValue("ItemColor",this.itemColor);
			info.AddValue("ItemSize",itemSize);
		}
		AdvancedListView(SerializationInfo info,StreamingContext context)
		{
			//items = new AdvancedListViewItemCollection(info,context);
			items = (AdvancedListViewItemCollection)info.GetValue("Items");
			itemColor = (Color)info.GetValue("ItemColor");
			itemSize = (Size)info.GetValue("ItemSize");
		}
		public AdvancedListView()
		{

		}
		/// <summary>
		/// Filters the items,and hides/show items based on filter.
		/// </summary>
		/// <param name="filter">Filter.</param>
		public void FilterItems(Predicate<AdvancedListViewItem> filter)
		{
			bool complete = false;
			foreach(var item in items)
			{
				if(filter(item))
				{
					bool prev = item.Visible;
					item.Visible = true;
					if(prev != item.Visible)
					{
						complete = true;
					}
				}else
				{
					bool prev = item.Visible;
					item.Visible = false;
					if(prev != item.Visible)
					{
						complete = true;
					}
				}
			}
			if(complete)
			{
				Invalidate();
				//CompleteInvalidate();
			}
		}
		#endregion

		#region Special
		bool CanMouseDown
		{
			get
			{
				return !(MouseItem == null)&&MouseButtons == MouseButtons.Left;
			}
		}
		AdvancedListViewItem firstD;
		AdvancedListViewItem[] dragging;
		bool isDragging;
		protected override void OnMouseDown (MouseEventArgs e)
		{
			if(CanMouseDown)
			{
				if(CanDragItems)
				{
					firstD = MouseItem;
					dragging = SelectedItems;
				}
			}
			base.OnMouseDown (e);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			//if(panel != null){dontSelect = true;}
			Cursor = DefaultCursor;
			isDragging = false;
			if(OnEndDrag != null && beginDragged)
			{
				OnEndDrag(dragging);
			}
			RegisterHandling(false);
			dragging = null;
			if(images != null)
			{
				images.Close();
				images = null;
			}
			beginDragged = false;
			base.OnMouseUp (e);
		}
		bool beginDragged;
		FloatingImageGrid images;
		protected override void OnMouseMove (MouseEventArgs e)
		{
			if(dragging != null)
			{
				Cursor = Cursors.Cross;
				if(images == null && MouseItem != firstD)
				{
					beginDragged = true;
					if(OnBeginDrag != null)
					{
						OnBeginDrag.Invoke(dragging);
					}
					RegisterHandling(true);
					isDragging = true;
					var imgs = GetAllImages(SelectedItems);
					//imgs = imgs.SetSizeOfAll(new Size(30,30));
					images = FloatingImageGrid.GetImage(this,imgs,Color.Gray,null);
					images.KeepInMousePos(true,new Point(3,3),Arithmetics.GetImagesSize(imgs,10));
					images.Enabled = false;
					images.AddExecutor(mouseCondition,mouseAction);
					this.FindForm().Focus();
					this.FindForm().Select();
				}
			}else
			{
				if(MouseItem == null && dragging == null && e.Button == MouseButtons.Left)
				{
					if(panel == null)
					{
						startPoint = MousePosition;
						panel = FloatingControl.DisplayFloatingControl<FloatingControl>(this,1,1,false,MousePosition);
						panel.Location = startPoint;
						panel.AllowTransparency = true;
						panel.Enabled = false;
						Bitmap bmp = new Bitmap(1,1);
						bmp.SetPixel(0,0,Color.FromArgb(100,Color.Gray));
						panel.BackgroundImage = bmp;
						panel.TransparencyKey = Color.FromArgb(100,Color.Gray);
						this.FindForm().Focus();
						this.FindForm().Select();
					}
				}
				if(panel != null && e.Button == MouseButtons.Left)
				{
					var size = new Size(MousePosition.X-startPoint.X,MousePosition.Y-startPoint.Y);
					var loc = panel.Location;
					if(size.Width < 0)
					{
						size.Width *= -1;
						loc.X = startPoint.X-size.Width;
					}
					if(size.Height < 0)
					{
						size.Height *= -1;
						loc.Y = startPoint.Y-size.Height;
					}
					panel.Size = size;
					panel.Location = loc;
					SelectItemsAt(new Rectangle(loc,size));
				}else
				{
					if(panel != null)
					{
						panel.Close();
						panel = null;
						//dontSelect = true;
					}
				}
			}
			base.OnMouseMove (e);
		}
		bool mouseCondition()
		{
			var mouseControl = ControlUtils.GetMouseControl(this.Parent);
			//ControlUtils.MouseControl.Select();
			if(MouseButtons == MouseButtons.None)
			{
				return true;
			}else
			{
				return false;
			}
		}
		void mouseAction()
		{
			OnMouseUp(new MouseEventArgs(MouseButtons,1,MousePosition.X,MousePosition.Y,1));
		}
		void RegisterHandling(bool register)
		{
			var form = this.FindForm();
			if(register)
			{
				form.DoActionOnAllControls((obj) => obj.MouseUp += Obj_MouseUp);
			}else
			{
				form.DoActionOnAllControls((obj) => obj.MouseUp -= Obj_MouseUp);
			}
		}

		void Obj_MouseUp (object sender,MouseEventArgs e)
		{
			if(!this.Focused)
			{
				OnMouseUp(e);
			}
		}

		bool dontSelect;
		public void SelectItemsAt(Rectangle at)
		{
			foreach(var item in ItemList)
			{
				if(item.Bounds.IntersectsWith(this.RectangleToClient(at)))//if(at.Contains(item.Bounds))
				{
					item.Selected = true;
				}else
				{
					item.Selected = false;
				}
				//if(at.Contains(item.Bounds))
				//{
				//	item.Selected = true;
				//}
			}
		}
		protected override void OnMouseLeave (EventArgs e)
		{
			if(panel != null)
			{
				//if(OnEndDrag != null)
				//{
				//	OnEndDrag(dragging);
				//}
				//panel.Close();
				//panel = null;
			}
			base.OnMouseLeave (e);
		}
		Point startPoint;
		FloatingControl panel;
		public Image[] GetAllImages(AdvancedListViewItemCollection items)
		{
			List<Image> ret = new List<Image>();
			items.ForEach((obj) => ret.Add(obj.Image));
			return ret.ToArray();
		}

		#endregion
	}
	public delegate void OnItemDoubleClickHandler(AdvancedListViewItem item);
	public delegate void OnBeginDragItems(AdvancedListViewItem[] dragging);
	public delegate void OnEndDragItems(AdvancedListViewItem[] dragged,bool cancelDrag = false);
}
