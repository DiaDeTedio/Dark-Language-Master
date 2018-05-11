using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace CSharpEssentials
{
	public static class CSEMainClass
	{
		public const BindingFlags AllBindingFlags = BindingFlags.CreateInstance|
		BindingFlags.DeclaredOnly|BindingFlags.Default|
		BindingFlags.ExactBinding|BindingFlags.FlattenHierarchy|BindingFlags.GetField|BindingFlags.GetProperty|
		BindingFlags.IgnoreCase|BindingFlags.IgnoreReturn|BindingFlags.Instance|BindingFlags.InvokeMethod|
		BindingFlags.NonPublic|BindingFlags.OptionalParamBinding|BindingFlags.Public|
		BindingFlags.PutDispProperty|BindingFlags.PutRefDispProperty|BindingFlags.SetField|
		BindingFlags.SetProperty|BindingFlags.Static|BindingFlags.SuppressChangeType;

		#region Methods
		public static byte[] GetByteData(object from)
		{
			MemoryStream mem = new MemoryStream();
			BinaryFormatter bin = new BinaryFormatter();
			bin.Serialize(mem,from);
			var bytes = mem.ToArray();
			mem.Close();
			return bytes;
		}
		public static object GetObjectFromBytes(byte[] from)
		{
			BinaryFormatter bin = new BinaryFormatter();
			MemoryStream mem = new MemoryStream(from);
			var obj = bin.Deserialize(mem);
			mem.Close();
			return obj;
		}
		public static T GetObjectFromBytes<T>(byte[] from)
		{
			return (T)GetObjectFromBytes(from);
		}
		public static string ToStringB(this byte[] bytes)
		{
			string ret = "";
			int index = 0;
			foreach(var @byte in bytes)
			{
				ret += @byte;
				if(index <= bytes.Length-1)
				{
					ret += ".";
				}
				index++;
			}
			return ret;
		}
		public static byte[] FromString(string str)
		{
			List<byte> bytes = new List<byte>();
			var l = str.Split('.');
			foreach(var i in l)
			{
				byte f = byte.Parse(i);
				bytes.Add(f);
			}
			return bytes.ToArray();
		}

		public static Type[] GetAllTypes()
		{
			if(AppDomain.CurrentDomain == null)
			{
				return new Type[0];
			}
			List<Type> types = new List<Type>();
			foreach(var ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				types.AddRange(ass.GetExportedTypes());
			}
			return types.ToArray();
		}
		public static Type[] GetAllTypesOfBaseType(Type baseType)
		{
			if(AppDomain.CurrentDomain == null)
			{
				return new Type[0];
			}
			List<Type> types = new List<Type>();
			foreach(var ass in AppDomain.CurrentDomain.GetAssemblies())
			{
				types.AddRange(ass.GetFilteredTypes(baseType));
			}
			return types.ToArray();
		}
		public static T GetObject<T>(Type type,params object[] args)
		{
			var obj = Activator.CreateInstance(type,args);
			return (T)obj;
		}
		public static FieldInfo FindField(object target,string fieldName)
		{
			if(target == null){return null;}
			FieldInfo _field = null;
			var type = target.GetType();
			while(type != null)
			{
				var fields = type.GetRuntimeFields();
				foreach(var field in fields)
				{
					if(field.Name == fieldName)
					{
						_field = field;
					}
				}
				type = type.BaseType;
			}
			return _field;
		}
		public static MethodInfo FindMethod(Type target,string methodName)
		{
			if(target == null){return null;}
			MethodInfo _method = null;
			var type = target;
			while(type != null)
			{
				var methods = type.GetRuntimeMethods();
				foreach(var method in methods)
				{
					if(method.Name == methodName)
					{
						_method = method;
					}
				}
				type = type.BaseType;
			}
			return _method;
		}
		public static FieldInfo[] GetAllFields(this Type type)
		{
			if(type == null){return null;}
			FieldInfo[] fields = type.GetRuntimeFields().ToArray();
			return fields;
		}
		public static PropertyInfo FindProperty(object target,string propertyName)
		{
			if(target == null){return null;}
			PropertyInfo _property = null;
			var type = target.GetType();
			while(type != null)
			{
				var fields = type.GetRuntimeProperties();
				foreach(var property in fields)
				{
					if(property.Name == propertyName)
					{
						_property = property;
					}
				}
				type = type.BaseType;
			}
			return _property;
		}
		#endregion

		#region Extensions
		/// <summary>
		/// Cast the specified enumerable and return this result.
		/// </summary>
		/// <returns>The cast.</returns>
		/// <param name="enumer">Enumer.</param>
		/// <typeparam name="TResult">The 1st type parameter.</typeparam>
		public static IEnumerable<TResult> Cast<TResult>(this IEnumerable enumer)
		{
			foreach(var item in enumer)
			{
				var cast = (TResult)item;
				yield return cast;
			}
		}
		/// <summary>
		/// Count the specified enumer items that match the specified condition.
		/// </summary>
		/// <returns>The count.</returns>
		/// <param name="enumer">Enumer.</param>
		/// <param name="condition">Condition.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static int Count<T>(this IEnumerable<T> enumer,Predicate<T> condition)
		{
			int count = 0;
			foreach(var item in enumer)
			{
				if(condition(item))
				{
					count++;
				}
			}
			return count;
		}
		/// <summary>
		/// Tries the remove at the list item at the specified index.
		/// </summary>
		/// <param name="list">List.</param>
		/// <param name="index">Index.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void TryRemoveAt<T>(this IList<T> list,int index)
		{
			if(index >= 0 && index < list.Count)
				list.RemoveAt(index);
		}

		/// <summary>
		/// Creates the instance of the specified type and returns it.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="type">Type.</param>
		public static object CreateInstance(Type type)
		{
			var ctor = type.GetConstructor();
			var pars = ctor.GetDefaultParameters();
			var obj = ctor.Invoke(pars);
			return obj;
		}
		/// <summary>
		/// Gets the type name of this type,this include the namespace and the type name,ex:
		/// NAMESPACE.TYPENAME,ASSEMBLY
		/// </summary>
		/// <returns>The name.</returns>
		/// <param name="type">Type.</param>
		public static string TypeName(this Type type)
		{
			string name = "";
			name = type.Namespace;
			name += $".{type.Name}";
			name += $",{type.Assembly.GetName().Name}";
			return name;
		}
		/// <summary>
		/// Creates the instance of the specified T type and returns it.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CreateInstance<T>()
		{
			var type = typeof(T);
			var obj = CreateInstance(type);
			return (T)obj;
		}
		/// <summary>
		/// Creates the instance of the specified T type without initializing then.
		/// </summary>
		/// <returns>The instance of.</returns>
		/// <param name="type">Type.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CreateInstanceOf<T>(this Type type)
		{
			var obj = FormatterServices.GetSafeUninitializedObject(type);
			return (T)obj;
		}
		/// <summary>
		/// Gets the default parameters of the specified method base.
		/// </summary>
		/// <returns>The default parameters.</returns>
		/// <param name="method">Method.</param>
		public static object[] GetDefaultParameters(this MethodBase method)
		{
			List<object> pars = new List<object>();
			var parameters = method.GetParameters();
			foreach(var parameter in parameters)
			{
				object par = null;
				if(parameter.HasDefaultValue)
				{
					par = parameter.DefaultValue;
				}else
				{
					var tp = parameter.ParameterType;
					if(tp.IsValueType)
					{
						par = tp.GetConstructor(new Type[0]).Invoke(null);
					}
				}
				pars.Add(par);
			}
			return pars.ToArray();
		}

		/// <summary>
		/// Chars is any of from argument.
		/// </summary>
		/// <returns><c>true</c>, if is was chared, <c>false</c> otherwise.</returns>
		/// <param name="ch">Ch.</param>
		/// <param name="from">From.</param>
		public static bool CharIs(this char ch,string from)
		{
			foreach(var @char in from){if(ch == @char){return true;}}return false;
		}
		public static bool CharIs(this char ch,params char[] from)
		{
			foreach(var @char in from){if(ch == @char){return true;}}return false;
		}
		public static char ToUpper(this char ch)
		{
			return char.ToUpper(ch);
		}
		public static string SetChar(this string str,int index,char set)
		{
			var chs = str.ToCharArray();
			chs[index] = set;
			return new string(chs);
		}
		/// <summary>
		/// Finds the same item of the specified condition excepting the items in the specified list.
		/// </summary>
		/// <returns>The same item.</returns>
		/// <param name="list">List.</param>
		/// <param name="sameof">Sameof.</param>
		/// <param name="except">Except.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T FindSameItem<T>(this IList<T> list,Predicate<T> sameof,List<T> except)
		{
			foreach(var sames in list)
			{
				if(sameof(sames) && !except.Contains(sames))
				{
					return sames;
				}
			}
			return default(T);
		}

		public static Image[] SetSizeOfAll(this IEnumerable<Image> images,Size size)
		{
			foreach(Bitmap image in images)
			{
				image.SetResolution(size.Width,size.Height);
			}
			return images.ToArray();
		}
		public static Image SetSize(this Image from,Size size)
		{
			Bitmap bmp = (Bitmap)from;
			while(bmp.Size.LessThanOrEqualTo(size))
			{
				bmp.SetResolution(size.Width,size.Height);
				size.Width--;
				size.Height--;
			}
			return bmp;
		}
		public static Image GetThumb(this Image image,int width,int height)
		{
			Image img = image.GetThumbnailImage(width,height,null,IntPtr.Zero);
			return img;
		}
		/// <summary>
		/// Searchs the field and return the results.
		/// </summary>
		/// <returns>The field.</returns>
		/// <param name="obj">Object.</param>
		/// <param name="fieldName">Field name.</param>
		public static FieldInfo SearchField(this object obj,string fieldName)
		{
			var field = FindField(obj,fieldName);
			return field;
		}
		/// <summary>
		/// Searchs the method and return the results.
		/// </summary>
		/// <returns>The method.</returns>
		/// <param name="type">Type.</param>
		/// <param name="methodName">Method name.</param>
		public static MethodInfo SearchMethod(this Type type,string methodName)
		{
			var method = FindMethod(type,methodName);
			return method;
		}
		public static FieldInfo SearchField(this object obj,params string[] attempts)
		{
			FieldInfo ret = null;
			foreach(var attempt in attempts)
			{
				var field = FindField(obj,attempt);
				if(field != null)
				{
					ret = field;
					break;
				}
			}
			return ret;
		}
		public static FieldInfo SearchField(this object obj,Type fieldType)
		{
			var fields = obj.GetType().GetAllFields();
			foreach(var field in fields)
			{
				if(field.FieldType == fieldType)
				{
					return field;
				}
			}
			return null;
		}
		public static FieldInfo SearchField<T>(this object obj)
		{
			return obj.SearchField(typeof(T));
		}
		/// <summary>
		/// Searchs the property and return the results.
		/// </summary>
		/// <returns>The property.</returns>
		/// <param name="obj">Object.</param>
		/// <param name="propertyName">Property name.</param>
		public static PropertyInfo SearchProperty(this object obj,string propertyName)
		{
			var prop = FindProperty(obj,propertyName);
			return prop;
		}
		/// <summary>
		/// Gets the constructor of the specified type.
		/// </summary>
		/// <returns>The constructor.</returns>
		/// <param name="type">Type.</param>
		public static ConstructorInfo GetConstructor(this Type type)
		{
			var ctors = type.GetConstructors();
			ConstructorInfo ctor = null;
			if(ctors.Length >= 1)
			{
				ctor = ctors[0];
			}
			return ctor;
		}
		public static object GetValue(this object obj,string fieldName,bool includeProperties = false)
		{
			var field = obj.SearchField(fieldName);
			if(field != null)
			{
				return field.GetValue(obj);
			}else
			{
				if(!includeProperties)
				{
					return null;
				}else
				{
					var prop = obj.SearchProperty(fieldName);
					if(prop != null)
					{
						return prop.GetValue(obj);
					}else
					{
						return null;
					}
				}
			}
		}
		public static int IndexOf<T>(this IEnumerable<T> enumer,T item)
		{
			int index = 0;
			foreach(var _item in enumer)
			{
				if(_item.Equals(item))
				{
					return index;
				}
				index++;
			}
			return -1;
		}
		public static T[] ToArray<T>(this IEnumerable<T> enumer)
		{
			List<T> l = new List<T>();
			foreach(var item in enumer){l.Add(item);}
			return l.ToArray();
		}
		public static T[] ToArray<T>(this IEnumerable enumer)
		{
			List<T> l = new List<T>();
			foreach(var item in enumer){l.Add((T)item);}
			return l.ToArray();
		}
		public static T[] As<T>(this IEnumerable enumer)
		{
			List<T> ret = new List<T>();
			foreach(var item in enumer)
			{
				var n = (T)item;
				if(n != null)
				{
					ret.Add(n);
				}
			}
			return ret.ToArray();
		}
		public static T GetValue<T>(this object obj,string fieldName,bool includeProperties = false)
		{
			var field = obj.SearchField(fieldName);
			if(field != null)
			{
				return (T)field.GetValue(obj);
			}else
			{
				if(!includeProperties)
				{
					return default(T);
				}else
				{
					var prop = obj.SearchProperty(fieldName);
					if(prop != null)
					{
						return (T)prop.GetValue(obj);
					}else
					{
						return default(T);
					}
				}
			}
		}
		public static void SetValue(this object obj,string fieldName,object value)
		{
			var field = obj.SearchField(fieldName);
			if(field != null)
			{
				field.SetValue(obj,value);
			}
		}

		public static void Switch(this System.Collections.IList list,object from,object to)
		{
			int fromInd = list.IndexOf(from);
			int toInd = list.IndexOf(to);
			if(fromInd != -1 && toInd != -1)
			{
				list[fromInd] = to;
				list[toInd] = from;
			}
		}
		public static void SwitchClonning(this System.Collections.IList list,object from,object to)
		{
			int fromInd = list.IndexOf(from);
			int toInd = list.IndexOf(to);
			if(fromInd != -1 && toInd != -1)
			{
				object fromC = ((ICloneable)from).Clone();
				object toC = ((ICloneable)to).Clone();
				list[fromInd] = toC;
				list[toInd] = fromC;
			}
		}
		public static void Switch<T>(this IList<T> list,T from,T to)
		{
			int fromInd = list.IndexOf(from);
			int toInd = list.IndexOf(to);
			if(fromInd != -1 && toInd != -1)
			{
				list[fromInd] = to;
				list[toInd] = from;
			}
		}
		public static T[] AddItems<T>(this T[] array,IList<T> addIt)
		{
			T[] ret = new T[array.Length+addIt.Count];
			int arrayIndex = 0;
			int addItIndex = 0;
			for(int i=0;i<ret.Length;i++)
			{
				if(arrayIndex < array.Length)
				{
					ret[i] = array[arrayIndex];
					arrayIndex++;
				}else
				{
					ret[i] = addIt[addItIndex];
					addItIndex++;
				}
			}
			return ret;
		}
		//public static T[] AddItems<T>(this T[] array,IList addIt)
		//{
		//	T[] ret = new T[array.Length+addIt.Count];
		//	int arrayIndex = 0;
		//	int addItIndex = array.Length-1;
		//	for(int i=0;i<ret.Length;i++)
		//	{
		//		if(arrayIndex < array.Length)
		//		{
		//			ret[i] = array[arrayIndex];
		//			arrayIndex++;
		//		}else
		//		{
		//			ret[i] = (T)addIt[addItIndex];
		//			addItIndex++;
		//		}
		//	}
		//	return ret;
		//}
		public static object[] AddItems(this object[] array,IList addIt)
		{
			object[] ret = new object[array.Length+addIt.Count];
			int arrayIndex = 0;
			int addItIndex = array.Length-1;
			for(int i=0;i<ret.Length;i++)
			{
				if(arrayIndex < array.Length)
				{
					ret[i] = array[arrayIndex];
					arrayIndex++;
				}else
				{
					ret[i] = addIt[addItIndex];
					addItIndex++;
				}
			}
			return ret;
		}
		public static bool ContainsAnyOf(this string text,params string[] anyOf)
		{
			foreach(var any in anyOf)
			{
				if(text.Contains(any))
				{
					return true;
				}
			}
			return false;
		}
		public static bool ContainsAnyOf(this string text,params char[] anyOf)
		{
			foreach(var any in anyOf)
			{
				if(text.Contains(any))
				{
					return true;
				}
			}
			return false;
		}
		public static bool Contains<T>(this IEnumerable<T> enumer,T item)
		{
			foreach(var _item in enumer)
			{
				if(_item != null && _item.Equals(item))
				{
					return true;
				}
			}
			return false;
		}

		public static void ForEach<T>(this IEnumerable<T> array,Action<T> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static void ForEach<T>(this ICollection<T> array,Action<T> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static void ForEach<T>(this ICollection<T> collection,Predicate<T> condition,Action<T> action)
		{
			foreach(var item in collection)
			{
				if(condition(item))
				{
					action(item);
				}
			}
		}
		//public static void ForEach(this System.Collections.ICollection array,Action<object> action)
		//{
		//	foreach(var item in array)
		//	{
		//		action.Invoke(item);
		//	}
		//}
		public static void ForEach(this System.Collections.IEnumerable array,Action<object> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static void ForEach<T>(this System.Collections.IEnumerable array,Action<T> action)
		{
			foreach(var item in array)
			{
				var t = (T)item;
				action.Invoke(t);
			}
		}
		public static void ForEach<T>(this T[,] array,Action<T> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static void ForEach<T>(this T[,,] array,Action<T> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static void ForEach<T>(this T[,,,] array,Action<T> action)
		{
			foreach(var item in array)
			{
				action.Invoke(item);
			}
		}
		public static List<T> ToList<T>(this IEnumerable<T> enumer)
		{
			List<T> l = new List<T>();
			foreach(var item in enumer)
			{
				l.Add(item);
			}
			return l;
		}
		public static List<object> ToList(this System.Collections.IEnumerator enumer)
		{
			List<object> l = new List<object>();
			while(enumer.MoveNext())
			{
				l.Add(enumer.Current);
			}
			return l;
		}
		public static List<T> ToList<T>(this IEnumerator<T> enumer)
		{
			List<T> l = new List<T>();
			while(enumer.MoveNext())
			{
				l.Add(enumer.Current);
			}
			return l;
		}
		public static List<T> ToList<T>(this IEnumerable enumer)
		{
			List<T> l = new List<T>();
			foreach(var item in enumer){l.Add((T)item);}
			return l;
		}
		public static List<object> ToList(this IEnumerable enumer)
		{
			List<object> l = new List<object>();
			foreach(var item in enumer){l.Add(item);}
			return l;
		}
		public static T GetLastItem<T>(this IEnumerable<T> enumer)
		{
			int index = 0;
			foreach(var item in enumer)
			{
				if(index >= enumer.Count()-1)
				{
					return item;
				}
				index++;
			}
			return default(T);
		}
		public static int Count<T>(this IEnumerable<T> enumer)
		{
			int index = 0;
			foreach(var item in enumer)
			{
				index++;
			}
			return index;
		}
		public static bool Exists<T>(this IEnumerable<T> enumer,Predicate<T> exists)
		{
			foreach(var item in enumer)
			{
				if(exists(item))
				{
					return true;
				}
			}
			return false;
		}
		public static bool ContainsInAnyItem(this IEnumerable<string> enumer,string contains)
		{
			foreach(var item in enumer)
			{
				if(item.Contains(contains))
				{
					return true;
				}
			}
			return false;
		}
		public static bool Exists(this IEnumerable enumer,Predicate<object> exists)
		{
			foreach(var item in enumer)
			{
				if(exists(item))
				{
					return true;
				}
			}
			return false;
		}
		public static bool Exists<T>(this IEnumerable enumer,Predicate<T> exists)
		{
			foreach(var item in enumer)
			{
				if(exists((T)item))
				{
					return true;
				}
			}
			return false;
		}
		public static bool ExistsInItems<T>(this IEnumerable<T> enumer,Predicate<T> exists)
		{
			int c = 0;
			foreach(var item in enumer)
			{
				if(exists(item))
				{
					c++;
				}
				if(c >= 1)
				{
					return true;
				}
			}
			return false;
		}
		public static bool ExistsInItems(this IEnumerable enumer,Predicate<object> exists)
		{
			int c = 0;
			foreach(var item in enumer)
			{
				if(exists(item))
				{
					c++;
				}
				if(c >= 2)
				{
					return true;
				}
			}
			return false;
		}
		public static void RemoveLast<T>(this IList<T> list)
		{
			list.RemoveAt(list.Count-1);
		}
		public static void RemoveFirst<T>(this IList<T> list)
		{
			list.RemoveAt(0);
		}
		public static T[] OpenAcess<T>(this T[] array,params Action<List<T>>[] actions)
		{
			List<T> l = new List<T>();
			array.ForEach((obj) => l.Add(obj));
			foreach(var action in actions)
			{
				action.Invoke(l);
			}
			array = l.ToArray();
			return array;
		}
		public static string ToStringList<T>(this IEnumerable<T> enumer,string lastCharOnItem = "")
		{
			string ret = "";
			foreach(var item in enumer)
			{
				ret += item+lastCharOnItem;
			}
			return ret;
		}
		public static Size GetTotalSize(this IEnumerable<Image> images)
		{
			int w = 0;
			int h = 0;
			foreach(var image in images)
			{
				w += image.Width;
				h += image.Height;
			}
			return new Size(w,h);
		}
		public static Size GetTotalSize(this IEnumerable<Image> images,int maxX)
		{
			int w = 0;
			int h = 0;
			int curX = 0;
			int index = 0;
			foreach(var image in images)
			{
				if(index == 0)
				{
					h = image.Height;
				}
				if(curX >= maxX)
				{
					h += image.Height;
					curX = 0;
				}
				if(index <= maxX)
				{
					w += image.Width;
				}

				curX++;
				index++;
			}
			return new Size(w,h);
		}
		public static T[] Filter<T>(this IEnumerable<T> enumer,Predicate<T> filter)
		{
			List<T> ret = new List<T>();
			foreach(var item in enumer)
			{
				if(filter(item))
				{
					ret.Add(item);
				}
			}
			return ret.ToArray();
		}
		public static T[] Filter<T>(this IEnumerable enumer,Predicate<T> filter)
		{
			List<T> ret = new List<T>();
			foreach(var item in enumer)
			{
				if(filter((T)item))
				{
					ret.Add((T)item);
				}
			}
			return ret.ToArray();
		}
		public static T Find<T>(this IEnumerable<T> enumer,Predicate<T> filter)
		{
			foreach(var item in enumer)
			{
				if(filter(item))
				{
					return item;
				}
			}
			return default(T);
		}
		public static void SortList<T>(this IList<T> iList,params Predicate<T>[] predicates)
		{
			List<T> list = new List<T>();
			List<T> current = (List<T>)iList;
			foreach(var predicate in predicates)
			{
				foreach(var c in current)
				{
					if(predicate.Invoke(c) && !list.Contains(c))
					{
						list.Add(c);
					}
				}
			}
			foreach(var c in current)
			{
				if(!list.Contains(c))
				{
					list.Add(c);
				}
			}
			iList.Clear();
			foreach(var item in list)
			{
				iList.Add(item);
			}
		}
		public static void KeepOnly<T>(this List<T> list,params Predicate<T>[] conditions)
		{
			List<T> ret = new List<T>();
			foreach(var item in list)
			{
				int match = 0;
				foreach(var condition in conditions)
				{
					if(condition.Invoke(item))
					{
						match++;
					}
				}
				if(match >= conditions.Length)
				{
					ret.Add(item);
				}
			}
			list.Clear();
			list.AddRange(ret);
		}
		public static void CustomAdd<T>(this IList<T> list,params Action<T>[] customs)
		{
			T item = Activator.CreateInstance<T>();
			customs.ForEach((Action<T> action) => action(item));
			list.Add(item);
		}
		public static Type[] GetFilteredTypes(this System.Reflection.Assembly assembly,Type baseType)
		{
			List<Type> ret = new List<Type>();
			foreach(var type in assembly.GetExportedTypes())
			{
				if(type.BaseType == baseType)
				{
					ret.Add(type);
					continue;
				}
				if(type.BaseType != null)
				{
					if(type.BaseType.BaseType == baseType)
					{
						ret.Add(type);
					}
				}
			}
			return ret.ToArray();
		}
		public static Type[] GetDerivedTypes(this System.Reflection.Assembly assembly,Type baseType)
		{
			List<Type> ret = new List<Type>();
			foreach(var type in assembly.GetExportedTypes())
			{
				if(type.Is(baseType))
				{
					ret.Add(type);
					continue;
				}
			}
			return ret.ToArray();
		}
		public static Type[] GetDerivedTypes<T>(this System.Reflection.Assembly assembly)
		{
			return assembly.GetDerivedTypes(typeof(T));
		}
		public static bool Is(this Type from,Type @is,bool useInterfaces = true)
		{
			Type cur = from;
			while(cur != null)
			{
				if(cur == @is)
				{
					return true;
				}
				cur = cur.BaseType;
			}
			if(useInterfaces)
			{
				if(from.IsInterface(@is))
				{
					return true;
				}
			}
			return false;
		}
		public static bool IsInterface(this Type from,Type @is)
		{
			var ins = from.GetInterfaces();
			foreach(var inter in ins)
			{
				if(inter == @is)
				{
					return true;
				}
			}
			return false;
		}
		public static bool Is<T>(this Type from)
		{
			return from.Is(typeof(T));
		}
		public static string[] GetNames(this Enum from)
		{
			return Enum.GetNames(from.GetType());
		}
		public static Enum FromName(this Enum from,string text)
		{
			Enum ret = (Enum)Enum.Parse(from.GetType(),text);
			return ret;
		}
		public static T FromName<T>(this Enum from,string text)
		{
			object ret = (Enum)Enum.Parse(from.GetType(),text);
			return (T)ret;
		}

		/// <summary>
		/// Creates the instance of this type.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="from">From.</param>
		/// <param name="args">Arguments.</param>
		public static object CreateInstance(this Type from,params object[] args)
		{
			return Activator.CreateInstance(from,args);
		}
		/// <summary>
		/// Creates the instance of this type.
		/// </summary>
		/// <returns>The instance.</returns>
		/// <param name="from">From.</param>
		/// <param name="args">Arguments.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CreateInstance<T>(this Type from,params object[] args)
		{
			return (T)from.CreateInstance(args);
		}
		public static Attribute FindAttribute(this Type from,Type attributeType)
		{
			Attribute ret = null;
			ret = from.GetCustomAttribute(attributeType);
			return ret;
		}
		public static T FindAttribute<T>(this Type from) where T : Attribute
		{
			return (T)from.FindAttribute(typeof(T));
		}

		/// <summary>
		/// Returns the Mouses position normalized to the current control.
		/// </summary>
		/// <returns>The position.</returns>
		/// <param name="control">Control.</param>
		public static Point MousePos(this Control control)
		{
			return control.PointToClient(Control.MousePosition);
		}

		#region Control_Extensions
		public static bool IsMouseOn(this Control control)
		{
			var form = control.FindForm();
			if(control.Bounds.Contains(form.MousePos()))
			{
				return true;
			}else
			{
				return false;
			}
		}
		public static Control[] GetAllControls(this Control main)
		{
			List<Control> controls = new List<Control>();
			foreach(Control control in main.Controls)
			{
				controls.Add(control);
				controls.AddRange(control.GetAllControls());
			}
			return controls.ToArray();
		}
		public static void DoActionOnAllControls(this Control main,Action<Control> action)
		{
			foreach(var control in main.GetAllControls())
			{
				action(control);
			}
		}
		public static TreeNode[] GetAllNodes(this TreeView tree,TreeNode _node = null)
		{
			List<TreeNode> nodes = new List<TreeNode>();
			if(tree != null)
			{
				foreach(TreeNode node in tree.Nodes)
				{
					nodes.Add(node);
					nodes.AddRange(GetAllNodes(null,node));
				}
			}
			if(_node != null)
			{
				foreach(TreeNode node in _node.Nodes)
				{
					nodes.Add(node);
					nodes.AddRange(GetAllNodes(null,node));
				}
			}
			return nodes.ToArray();
		}
		public static TreeNode FindNode(this TreeView tree,TreeNode _node)
		{
			var nodes = tree.GetAllNodes();
			foreach(var node in nodes)
			{
				if(node == _node)
				{
					return node;
				}
			}
			return null;
		}
		public static TreeNode FindNode(this TreeView tree,Predicate<TreeNode> condition)
		{
			var nodes = tree.GetAllNodes();
			foreach(var node in nodes)
			{
				if(condition.Invoke(node))
				{
					return node;
				}
			}
			return null;
		}
		public static object GetItemAt(this ListBox list,Point at)
		{
			int index = 0;
			foreach(var item in list.Items)
			{
				var rect = list.GetItemRectangle(index);
				if(rect.Contains(at))
				{
					return item;
				}
				index++;
			}
			return null;
		}
		public static TabPage GetTabAt(this TabControl tabC,Point at)
		{
			int index = 0;
			foreach(TabPage tab in tabC.TabPages)
			{
				var rect = tabC.GetTabRect(index);
				if(rect.Contains(at))
				{
					return tab;
				}
				index++;
			}
			return null;
		}
		public static void SelectItems(this ListView list,ListViewItem[] items,bool select = true)
		{
			foreach(var item in items)
			{
				if(item != null)
				{
					item.Selected = select;
				}
			}
			foreach(ListViewItem item in list.Items)
			{
				if(!items.Contains(item))
				{
					item.Selected = false;
				}
			}
		}
		public static ToolStripMenuItem ToTSMenuItem(this MenuItem item,EventHandler onClick = null)
		{
			item.Click += utilEH;
			ToolStripMenuItem ret = new ToolStripMenuItem(item.Text,null,onClick);
			foreach(MenuItem child in item.MenuItems)
			{
				var conC = child.ToTSMenuItem();
				ret.DropDownItems.Add(conC);
			}

			return ret;
		}
		static void utilEH(object sender,EventArgs e)
		{

		}
		public static MenuItem[] FindAllMenuItems(this MenuItem source)
		{
			List<MenuItem> ret = new List<MenuItem>();
			foreach(MenuItem item in source.MenuItems)
			{
				ret.Add(item);
				ret.AddRange(item.FindAllMenuItems());
			}
			return ret.ToArray();
		}
		public static MenuItem[] GetAllItems(this Menu menu)
		{
			List<MenuItem> ret = new List<MenuItem>();
			foreach(MenuItem item in menu.MenuItems)
			{
				ret.AddRange(item.FindAllMenuItems());
			}
			return ret.ToArray();
		}
		public static void RemoveDuplicates(this IList list,object item)
		{
			int matches = 0;
			for(int i=0;i<list.Count;i++)
			{
				var _item = list[i];
				if(_item == item)
				{
					matches++;
				}
				if(matches >= 2)
				{
					list.RemoveAt(i);
				}
			}
		}
		public static void RemoveDuplicatesWith(this IList list,Predicate<object> condition)
		{
			int matches = 0;
			for(int i=0;i<list.Count;i++)
			{
				var item = list[i];
				if(condition(item))
				{
					matches++;
				}
				if(matches >= 2)
				{
					list.RemoveAt(i);
				}
			}
		}
		#endregion
		#endregion

		#region Common
		public static string SearchForFile(string path,string name = null,string extension = null,bool caseSensitive = false)
		{
			var files = Directory.GetFiles(path);
			foreach(var filePath in files)
			{
				var file = filePath;
				if(!caseSensitive)
				{
					if(name != null){name = name.ToLower();}
					if(extension != null){extension = extension.ToLower();}
					file = file.ToLower();
				}
				var fileName = Path.GetFileNameWithoutExtension(file);
				var fileExtension = Path.GetExtension(file);
				if(name != null)
				{
					if(fileName.Contains(name))
					{
						return file;
					}
				}
				if(extension != null)
				{
					if(fileExtension.Contains(extension))
					{
						return file;
					}
				}
			}
			return null;
		}
		public static string[] SearchForFiles(string path,string name = null,string extension = null,bool caseSensitive = false)
		{
			List<string> ret = new List<string>();
			var files = Directory.GetFiles(path);
			foreach(var filePath in files)
			{
				var file = filePath;
				if(!caseSensitive)
				{
					if(name != null){name = name.ToLower();}
					if(extension != null){extension = extension.ToLower();}
					file = file.ToLower();
				}
				var fileName = Path.GetFileNameWithoutExtension(file);
				var fileExtension = Path.GetExtension(file);
				if(name != null)
				{
					if(fileName.Contains(name))
					{
						if(!ret.Contains(file))
						{
							ret.Add(file);
						}
					}
				}
				if(extension != null)
				{
					if(fileExtension.Contains(extension))
					{
						if(!ret.Contains(file))
						{
							ret.Add(file);
						}
					}
				}
			}
			return ret.ToArray();
		}
		public static void Iterate(Delegate method,long iterations = 1)
		{
			for(int i=0;i<=iterations;i++)
			{
				method.DynamicInvoke(i);
			}
		}
		public static void Iterate(Action<int> action,long iterations)
		{
			for(int i=0;i<=iterations;i++)
			{
				action.Invoke(i);
			}
		}
		public static void CMethod(Delegate method)
		{
			method.DynamicInvoke();
		}
		#endregion
	}
}
namespace CSharpEssentials.Extends
{
	public static class CSEMainClass
	{
		/// <summary>
		/// Finds the name in this object.
		/// </summary>
		/// <returns>The name.</returns>
		/// <param name="obj">Object.</param>
		public static string FindName(this object obj)
		{
			string name = "";
			var fn = obj.SearchField("name","Name","ID","identity","Identiry");
			if(fn != null)
			{
				name = fn.GetValue(obj)+"";
			}else
			{
				fn = obj.SearchField<string>();
				if(fn != null)
				{
					name = (string)fn.GetValue(obj);
				}
			}
			return name;
		}
	}
}
