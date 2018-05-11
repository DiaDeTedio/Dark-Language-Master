using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using CSharpEssentials.Designer.Controls.Bases;
using CSharpEssentials;
using System.ComponentModel;
using System.Collections.Generic;

namespace CSharpEssentials.Designer.Controls
{
	[System.ComponentModel.DefaultEvent("OnEndDragNode")]
	public class NTreeView : TreeView
	{
		bool clwf2;
		public bool ChangeLabelWithF2
		{
			get
			{
				return clwf2;
			}
			set
			{
				if(!LabelEdit)
				{
					clwf2 = false;
				}
				clwf2 = value;
			}
		}
		public TreeNode CurrentMouseNode
		{
			get
			{
				return GetNodeAt(this.MousePos());
			}
		}
		bool canDN;
		[Category("NTreeNode")]
		[Description("With this active,you can change nodes index")]
		public bool CanDragNodes
		{
			get
			{
				return canDN;
			}
			set
			{
				canDN = value;
			}
		}

		public event OnBeginDragNodeHandler OnBeginDragNode;
		public event OnEndDragNodeHandler OnEndDragNode;
		TreeNode dragging;
		protected override void OnMouseDown (MouseEventArgs e)
		{
			if(canDN)
			{
				this.SelectedNode = this.GetNodeAt(this.MousePos());
				dragging = this.SelectedNode;
			}

			base.OnMouseDown (e);
		}
		protected override void OnMouseUp (MouseEventArgs e)
		{
			if(beginDragged)
			{
				text.Close();
				text = null;
				var mNode = GetNodeAt(this.MousePos());
				bool canDo = true;
				if(OnEndDragNode != null)
				{
					canDo = OnEndDragNode(this,dragging,mNode);
				}
				if(mNode != null && canDo)
				{
					var list = this.Nodes.ToList<TreeNode>();
					list.Switch(dragging,mNode);
					this.Nodes.Clear();
					this.Nodes.AddRange(list.ToArray());
					this.SelectedNode = dragging;
					//this.Nodes.Switch(dragging,mNode);
					this.Invalidate();
				}
			}
			Cursor = DefaultCursor;
			beginDragged = false;
			dragging = null;
			base.OnMouseUp (e);
		}
		bool beginDragged;
		FloatingText text;
		protected override void OnMouseMove (MouseEventArgs e)
		{
			if(dragging != null && dragging != CurrentMouseNode)
			{
				beginDragged = true;
				Cursor = Cursors.Cross;
				if(text == null)
				{
					text = FloatingText.GetText(this,dragging.Text,Color.Black,Color.Gray,null);
					text.KeepInMousePos(true,new Point(3,3),new Size((int)(dragging.Text.Length*Font.Size),20));
				}
				if(OnBeginDragNode != null)
				{
					OnBeginDragNode(dragging);
				}
			}
			base.OnMouseMove (e);
		}
		protected override void OnKeyDown (KeyEventArgs e)
		{
			if(dragging != null && beginDragged)
			{
				if(e.KeyCode == Keys.Escape)
				{
					dragging = null;
					beginDragged = false;
					Cursor = DefaultCursor;
				}
			}
			if(ChangeLabelWithF2)
			{
				if(e.KeyCode == Keys.F2)
				{
					f2 = true;
					var node = SelectedNode;
					if(node != null && LabelEdit)
					{
						node.BeginEdit();
					}
				}
			}
			base.OnKeyDown (e);
		}
		bool f2;
		protected override void OnBeforeLabelEdit (NodeLabelEditEventArgs e)
		{
			if(!f2)
			{
				e.CancelEdit = true;
			}
			f2 = false;
			base.OnBeforeLabelEdit (e);
		}
		protected override void OnAfterLabelEdit (NodeLabelEditEventArgs e)
		{
			
			base.OnAfterLabelEdit (e);
		}
	}
	/// <summary>
	/// On begin drag node.
	/// </summary>
	public delegate void OnBeginDragNodeHandler(TreeNode dragged);
	/// <summary>
	/// On end drag node,if true,the drag is made by control,if false,the user make the drag event.
	/// </summary>
	public delegate bool OnEndDragNodeHandler(NTreeView tree,TreeNode dragged,TreeNode toNode);

	public class TreeNodeSchema
	{
		public List<TreeNodeSchemaItem> Items = new List<TreeNodeSchemaItem>();

		public void GenerateSchema(TreeView tree)
		{
			Items.Clear();
			var nodes = tree.GetAllNodes();
			foreach(var node in nodes)
			{
				Items.Add(new TreeNodeSchemaItem(node.IsExpanded,node.Checked,node));
			}
		}
		public void LoadSchema(TreeView tree)
		{
			foreach(var item in Items)
			{
				var node = tree.FindNode((obj) => obj.Handle == item.Handle);
				if(node != null)
				{
					if(item.Expanded){node.Expand();}
					if(!item.Expanded){node.Collapse();}
					node.Checked = item.Checked;
				}
			}
		}
	}
	public class TreeNodeSchemaItem
	{
		public bool Expanded;
		public bool Checked;
		public TreeNode Node;
		public IntPtr Handle;

		public TreeNodeSchemaItem (bool isExpanded,bool @checked,TreeNode node)
		{
			Expanded = isExpanded;
			Checked = @checked;
			Node = node;
			Handle = node.Handle;
		}
	}
}
