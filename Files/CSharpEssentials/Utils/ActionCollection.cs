using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpEssentials.Utils
{
	public class ActionCollection : ICollection<Action>,IList<Action>,IEnumerable<Action>
	{
		List<Action> Actions = new List<Action>();

		public void Invoke()
		{
			foreach(var action in Actions)
			{
				action.Invoke();
			}
		}

		public static ActionCollection operator +(ActionCollection collection,Action action)
		{
			collection.Add(action);
			return collection;
		}
		public static ActionCollection operator -(ActionCollection collection,Action action)
		{
			collection.Remove(action);
			return collection;
		}
		public static ActionCollection operator -(ActionCollection collection,int index)
		{
			collection.RemoveAt(index);
			return collection;
		}

		public Action this [int index]
		{
			get
			{
				return ((IList<Action>)Actions) [index];
			}

			set
			{
				((IList<Action>)Actions) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<Action>)Actions).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<Action>)Actions).IsReadOnly;
			}
		}

		public void Add (Action item)
		{
			((ICollection<Action>)Actions).Add (item);
		}

		public void Clear ()
		{
			((ICollection<Action>)Actions).Clear ();
		}

		public bool Contains (Action item)
		{
			return ((ICollection<Action>)Actions).Contains (item);
		}

		public void CopyTo (Action [] array,int arrayIndex)
		{
			((ICollection<Action>)Actions).CopyTo (array,arrayIndex);
		}

		public IEnumerator<Action> GetEnumerator ()
		{
			return ((ICollection<Action>)Actions).GetEnumerator ();
		}

		public int IndexOf (Action item)
		{
			return ((IList<Action>)Actions).IndexOf (item);
		}

		public void Insert (int index,Action item)
		{
			((IList<Action>)Actions).Insert (index,item);
		}

		public bool Remove (Action item)
		{
			return ((ICollection<Action>)Actions).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<Action>)Actions).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<Action>)Actions).GetEnumerator ();
		}
	}
	public class ActionCollection<T> : ICollection<Action<T>>,IList<Action<T>>,IEnumerable<Action<T>>
	{
		List<Action<T>> Actions = new List<Action<T>>();

		public void Invoke(T arg)
		{
			foreach(var action in Actions)
			{
				action.Invoke(arg);
			}
		}

		public static ActionCollection<T> operator +(ActionCollection<T> collection,Action<T> action)
		{
			collection.Add(action);
			return collection;
		}
		public static ActionCollection<T> operator -(ActionCollection<T> collection,Action<T> action)
		{
			collection.Remove(action);
			return collection;
		}
		public static ActionCollection<T> operator -(ActionCollection<T> collection,int index)
		{
			collection.RemoveAt(index);
			return collection;
		}

		public Action<T> this [int index]
		{
			get
			{
				return ((IList<Action<T>>)Actions) [index];
			}

			set
			{
				((IList<Action<T>>)Actions) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<Action<T>>)Actions).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<Action<T>>)Actions).IsReadOnly;
			}
		}

		public void Add (Action<T> item)
		{
			((ICollection<Action<T>>)Actions).Add (item);
		}

		public void Clear ()
		{
			((ICollection<Action<T>>)Actions).Clear ();
		}

		public bool Contains (Action<T> item)
		{
			return ((ICollection<Action<T>>)Actions).Contains (item);
		}

		public void CopyTo (Action<T> [] array,int arrayIndex)
		{
			((ICollection<Action<T>>)Actions).CopyTo (array,arrayIndex);
		}

		public IEnumerator<Action<T>> GetEnumerator ()
		{
			return ((ICollection<Action<T>>)Actions).GetEnumerator ();
		}

		public int IndexOf (Action<T> item)
		{
			return ((IList<Action<T>>)Actions).IndexOf (item);
		}

		public void Insert (int index,Action<T> item)
		{
			((IList<Action<T>>)Actions).Insert (index,item);
		}

		public bool Remove (Action<T> item)
		{
			return ((ICollection<Action<T>>)Actions).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<Action<T>>)Actions).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<Action<T>>)Actions).GetEnumerator ();
		}
	}
	public class ActionCollection<T,T0> : ICollection<Action<T,T0>>,IList<Action<T,T0>>,IEnumerable<Action<T,T0>>
	{
		List<Action<T,T0>> Actions = new List<Action<T,T0>>();

		public void Invoke(T arg,T0 arg0)
		{
			foreach(var action in Actions)
			{
				action.Invoke(arg,arg0);
			}
		}

		public static ActionCollection<T,T0> operator +(ActionCollection<T,T0> collection,Action<T,T0> action)
		{
			collection.Add(action);
			return collection;
		}
		public static ActionCollection<T,T0> operator -(ActionCollection<T,T0> collection,Action<T,T0> action)
		{
			collection.Remove(action);
			return collection;
		}
		public static ActionCollection<T,T0> operator -(ActionCollection<T,T0> collection,int index)
		{
			collection.RemoveAt(index);
			return collection;
		}

		public Action<T,T0> this [int index]
		{
			get
			{
				return ((IList<Action<T,T0>>)Actions) [index];
			}

			set
			{
				((IList<Action<T,T0>>)Actions) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<Action<T,T0>>)Actions).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<Action<T,T0>>)Actions).IsReadOnly;
			}
		}

		public void Add (Action<T,T0> item)
		{
			((ICollection<Action<T,T0>>)Actions).Add (item);
		}

		public void Clear ()
		{
			((ICollection<Action<T,T0>>)Actions).Clear ();
		}

		public bool Contains (Action<T,T0> item)
		{
			return ((ICollection<Action<T,T0>>)Actions).Contains (item);
		}

		public void CopyTo (Action<T,T0> [] array,int arrayIndex)
		{
			((ICollection<Action<T,T0>>)Actions).CopyTo (array,arrayIndex);
		}

		public IEnumerator<Action<T,T0>> GetEnumerator ()
		{
			return ((ICollection<Action<T,T0>>)Actions).GetEnumerator ();
		}

		public int IndexOf (Action<T,T0> item)
		{
			return ((IList<Action<T,T0>>)Actions).IndexOf (item);
		}

		public void Insert (int index,Action<T,T0> item)
		{
			((IList<Action<T,T0>>)Actions).Insert (index,item);
		}

		public bool Remove (Action<T,T0> item)
		{
			return ((ICollection<Action<T,T0>>)Actions).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<Action<T,T0>>)Actions).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<Action<T,T0>>)Actions).GetEnumerator ();
		}
	}
	public class ActionCollection<T,T0,T1> : ICollection<Action<T,T0,T1>>,IList<Action<T,T0,T1>>,IEnumerable<Action<T,T0,T1>>
	{
		List<Action<T,T0,T1>> Actions = new List<Action<T,T0,T1>>();

		public void Invoke(T arg,T0 arg0,T1 arg1)
		{
			foreach(var action in Actions)
			{
				action.Invoke(arg,arg0,arg1);
			}
		}

		public static ActionCollection<T,T0,T1> operator +(ActionCollection<T,T0,T1> collection,Action<T,T0,T1> action)
		{
			collection.Add(action);
			return collection;
		}
		public static ActionCollection<T,T0,T1> operator -(ActionCollection<T,T0,T1> collection,Action<T,T0,T1> action)
		{
			collection.Remove(action);
			return collection;
		}
		public static ActionCollection<T,T0,T1> operator -(ActionCollection<T,T0,T1> collection,int index)
		{
			collection.RemoveAt(index);
			return collection;
		}

		public Action<T,T0,T1> this [int index]
		{
			get
			{
				return ((IList<Action<T,T0,T1>>)Actions) [index];
			}

			set
			{
				((IList<Action<T,T0,T1>>)Actions) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((ICollection<Action<T,T0,T1>>)Actions).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<Action<T,T0,T1>>)Actions).IsReadOnly;
			}
		}

		public void Add (Action<T,T0,T1> item)
		{
			((ICollection<Action<T,T0,T1>>)Actions).Add (item);
		}

		public void Clear ()
		{
			((ICollection<Action<T,T0,T1>>)Actions).Clear ();
		}

		public bool Contains (Action<T,T0,T1> item)
		{
			return ((ICollection<Action<T,T0,T1>>)Actions).Contains (item);
		}

		public void CopyTo (Action<T,T0,T1> [] array,int arrayIndex)
		{
			((ICollection<Action<T,T0,T1>>)Actions).CopyTo (array,arrayIndex);
		}

		public IEnumerator<Action<T,T0,T1>> GetEnumerator ()
		{
			return ((ICollection<Action<T,T0,T1>>)Actions).GetEnumerator ();
		}

		public int IndexOf (Action<T,T0,T1> item)
		{
			return ((IList<Action<T,T0,T1>>)Actions).IndexOf (item);
		}

		public void Insert (int index,Action<T,T0,T1> item)
		{
			((IList<Action<T,T0,T1>>)Actions).Insert (index,item);
		}

		public bool Remove (Action<T,T0,T1> item)
		{
			return ((ICollection<Action<T,T0,T1>>)Actions).Remove (item);
		}

		public void RemoveAt (int index)
		{
			((IList<Action<T,T0,T1>>)Actions).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<Action<T,T0,T1>>)Actions).GetEnumerator ();
		}
	}
}
