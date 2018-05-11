using System;
using System.Collections.Generic;

namespace DarkLanguage.Classes.ScriptClasses
{
	[Serializable]
	public class ScriptFieldCollection
	{
		List<ScriptField> Items = new List<ScriptField>();

		public ScriptField this[int index]
		{
			get
			{
				return Items[index];
			}
			set
			{
				Items[index] = value;
			}
		}
		public ScriptField this[string name]
		{
			get
			{
				return GetItem(name);
			}
			set
			{
				SetItem(name,value);
			}
		}
		public int Count
		{
			get
			{
				return Items.Count;
			}
		}

		public void Add(ScriptField item){Items.Add(item);}
		public void Remove(ScriptField item){Items.Remove(item);}
		public void RemoveAt(int index){Items.RemoveAt(index);}
		public void IndexOf(ScriptField item){Items.IndexOf(item);}
		public ScriptField Find(Predicate<ScriptField> predicate){return Items.Find(predicate);}

		public ScriptField GetItem(string name)
		{
			foreach(var item in Items)
			{
				if(item.Name == name)
				{
					return item;
				}
			}
			return null;
		}
		public void SetItem(string name,ScriptField value)
		{
			for(int i=0;i<Items.Count;i++)
			{
				if(Items[i].Name == name)
				{
					Items[i] = value;
				}
			}
		}
		public void Clear()
		{
			Items.Clear();
		}
		public ScriptFieldCollection()
		{

		}
		public ScriptFieldCollection(IEnumerable<ScriptField> fields)
		{
			foreach(var field in fields)
			{
				Add(field);
			}
		}

		public void ForEach(Action<ScriptField> action)
		{
			Items.ForEach(action);
		}

		public IEnumerator<ScriptField> GetEnumerator()
		{
			foreach(var item in Items)
			{
				yield return item;
			}
		}
		public override string ToString ()
		{
			string ret = "";
			foreach(var item in Items)
			{
				ret += item+"\r\n";
			}
			return ret;
		}
		public bool Exists(Predicate<ScriptField> predicate)
		{
			foreach(var item in this)
			{
				if(predicate.Invoke(item))
				{
					return true;
				}
			}
			return false;
		}
	}
}
