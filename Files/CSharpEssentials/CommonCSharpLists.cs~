using System;
using System.Collections.Generic;
namespace CSharpEssentials
{
	/// <summary>
	/// String list with implicit conversion from to string array.
	/// </summary>
	public class StringList : List<string>
	{
		public static implicit operator string[](StringList from)
		{
			return from.ToArray();
		}
		public static implicit operator StringList(string[] from)
		{
			StringList ret = new StringList();
			from.ForEach((obj) => ret.Add(obj));
			return ret;
		}
		public static StringList operator +(StringList a,string b)
		{
			a.Add(b);
			return a;
		}
		public static StringList operator -(StringList a,string b)
		{
			a.Remove(b);
			return a;
		}
	}
	/// <summary>
	/// Type list with implicit conversion from to type array.
	/// </summary>
	public class TypeList : List<Type>
	{
		public static implicit operator Type[](TypeList from)
		{
			return from.ToArray();
		}
		public static implicit operator TypeList(Type[] from)
		{
			TypeList ret = new TypeList();
			from.ForEach((obj) => ret.Add(obj));
			return ret;
		}
	}
	/// <summary>
	/// Object list with implicit conversion from to object array and +- operators to add and remove items,and 
	/// GetObject method receiving generic T type to cast it.
	/// </summary>
	public class ObjectList : List<object>
	{
		public T GetObject<T>(int index)
		{
			return (T)this[index];
		}

		public static ObjectList operator +(ObjectList from,object to)
		{
			from.Add(to);
			return from;
		}
		public static ObjectList operator -(ObjectList from,object to)
		{
			from.Remove(to);
			return from;
		}

		public static implicit operator object[](ObjectList from)
		{
			if(from == null){return new object[0];}
			return from.ToArray();
		}
		public static implicit operator ObjectList(object[] from)
		{
			ObjectList ret = new ObjectList();
			from.ForEach((obj) => ret.Add(obj));
			return from;
		}
		public static implicit operator ObjectList(defOL from)
		{
			return new ObjectList();
		}
		public static implicit operator ObjectList(DBNull from)
		{
			return new ObjectList();
		}
	}
	public class defOL
	{

	}
}
