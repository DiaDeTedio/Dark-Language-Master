using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpEssentials.Utils
{
	/// <summary>
	/// Protected list class,to make your own protected lists.
	/// </summary>
	public class ProtectedList<T> : IList<T>,ICollection<T>,IEnumerable<T>
	{
		List<T> items = new List<T>();
		string password = "";
		string acess = "";
		/// <summary>
		/// Sets the acess of this list.
		/// </summary>
		/// <param name="prevAcess">Previous acess.</param>
		/// <param name="newAcess">New acess.</param>
		public void SetAcess(string prevAcess,string newAcess)
		{
			if(prevAcess == acess)
			{
				acess = newAcess;
			}
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:CSharpEssentials.Utils.ProtectedList`1"/> is protected.
		/// </summary>
		/// <value><c>true</c> if is protected; otherwise, <c>false</c>.</value>
		public bool IsProtected
		{
			get
			{
				return @protected;
			}
		}

		bool @protected;
		/// <summary>
		/// Protect the specified list by typping a new password and previous acess to confirm.
		/// </summary>
		/// <returns>The protect.</returns>
		/// <param name="password">Password.</param>
		/// <param name="acess">Acess.</param>
		public void Protect(string password,string acess)
		{
			if(this.acess == acess)
			{
				this.password = password;
				@protected = true;
			}
		}
		/// <summary>
		/// Unprotect the list by typping the specified password.
		/// </summary>
		/// <returns>The unprotect.</returns>
		/// <param name="password">Password.</param>
		public void Unprotect(string password)
		{
			if(this.password == password)
			{
				@protected = false;
			}
		}

		public T this [int index]
		{
			get
			{
				return ((IList<T>)items) [index];
			}

			set
			{
				if(!@protected)
				((IList<T>)items) [index] = value;
			}
		}

		public int Count
		{
			get
			{
				return ((IList<T>)items).Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return ((IList<T>)items).IsReadOnly;
			}
		}

		public void Add (T item)
		{
			if(!@protected)
			((IList<T>)items).Add (item);
		}

		public void Clear ()
		{
			if(!@protected)
			((IList<T>)items).Clear ();
		}

		public bool Contains (T item)
		{
			return ((IList<T>)items).Contains (item);
		}

		public void CopyTo (T [] array,int arrayIndex)
		{
			if(!@protected)
			((IList<T>)items).CopyTo (array,arrayIndex);
		}

		public IEnumerator<T> GetEnumerator ()
		{
			if(!@protected)
			return ((IList<T>)items).GetEnumerator ();
			else
				return prtEnumer();
		}
		IEnumerator<T> prtEnumer()
		{
			yield return default(T);
		}
		public int IndexOf (T item)
		{
			return ((IList<T>)items).IndexOf (item);
		}

		public void Insert (int index,T item)
		{
			if(!@protected)
			((IList<T>)items).Insert (index,item);
		}

		public bool Remove (T item)
		{
			if(!@protected)
				return ((IList<T>)items).Remove (item);
			else
				return false;
		}

		public void RemoveAt (int index)
		{
			if(!@protected)
			((IList<T>)items).RemoveAt (index);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IList<T>)items).GetEnumerator ();
		}
		/// <summary>
		/// To use this list correctly,first,make it, and call SetAcess("","NEW"), 
		/// after, call Protect("EXAMPLE","NEW");
		/// bye.
		/// </summary>
		public const bool Info = false;
	}
}
