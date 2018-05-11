using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using CSharpEssentials.Bases;
using System.Windows.Forms.Design;

namespace CSharpEssentials.Bases
{
	public abstract class CustomEnum
	{
		public abstract string Name
		{
			get;
		}
		public string[] Items
		{
			get
			{
				List<string> ret = new List<string>();
				_items.ForEach((obj) => ret.Add(obj));
				return ret.ToArray();
			}
		}
		long _selectedIndex;
		public long SelectedIndex
		{
			get{return _selectedIndex;}
			set
			{
				long val = value;
				if(val < 0 || val >= _items.Count)
				{
					val = -1;
				}
				_selectedIndex = val;
			}
		}
		public string SelectedItem
		{
			get
			{
				return GetItem(SelectedIndex);
			}
			set
			{
				SetItem(value);
			}
		}
		List<CustomEnumItem> _items = new List<CustomEnumItem>();
		protected CustomEnum (params string[] items)
		{
			items.ForEach((string item) => _items.Add(new CustomEnumItem(item)));
			InitializeEnum();
		}
		protected CustomEnum()
		{
			InitializeEnum();
		}
		public void AddItem(string item)
		{
			_items.Add(item);
		}
		public void RemoveItem(string item)
		{
			_items.Remove(item);
		}
		public void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}
		public override string ToString ()
		{
			return string.Format ("[CustomEnum: Items={0}, SelectedItem={1}]",Items,SelectedItem);
		}
		public void SetItem(string value)
		{
			for(int i=0;i<_items.Count;i++)
			{
				var item = _items[i];
				if(item == value)
				{
					SelectedIndex = i;
					return;
				}
			}
			SelectedIndex = -1;
		}
		public string GetItem(long index)
		{
			if(index != -1)
			{
				return Items[index];
			}else
			{
				return "(null)";
			}
		}


		/// <summary>
		/// Initializes the enum,in this method,you will do all of your enum items.
		/// </summary>
		protected abstract void InitializeEnum();
	}
	public class ExampleCustomEnum : CustomEnum
	{
		public override string Name
		{
			get
			{
				return "ExampleCustomEnum";
			}
		}

		protected override void InitializeEnum ()
		{
			AddItem("Hello");
			AddItem("Holla");
			AddItem("Hollow");
		}
		public ExampleCustomEnum() : base()
		{

		}
	}

	public class CustomEnumItem
	{
		public string Name;
		public object Data;
		public bool Selected;

		public CustomEnumItem(string name,object data = null,bool selected = false)
		{
			Name = name;
			Data = data;
			Selected = selected;
		}
		/// <summary>
		/// Parse the specified from a string,example NAME(string)|DATA(int)|Selected(bool).
		/// </summary>
		/// <returns>The parse.</returns>
		/// <param name="from">From.</param>
		public static CustomEnumItem Parse(string from)
		{
			CustomEnumItem item = new CustomEnumItem("");
			string[] l = from.Split('|');
			if(l.Length >= 1)
			{
				item.Name = l[0];
			}
			if(l.Length == 2)
			{
				item.Selected = bool.Parse(l[1]);
			}
			if(l.Length == 3)
			{
				item.Data = int.Parse(l[1]);
				item.Selected = bool.Parse(l[2]);
			}
			return item;
		}
		public static implicit operator CustomEnumItem(string from){return CustomEnumItem.Parse(from);}
		public static implicit operator string(CustomEnumItem from){return from.Name;}
	}
}