using System;
using System.Collections.Generic;
using System.Reflection;

namespace DarkLanguage.Classes
{
	public static class StringUtils
	{
		public static string[] commonSeparators =
		{
			" ",",",".",";","?","=","-","_","+","-","*","/",@"\","|","^","~","]","[","(",")",":","%","@","!","<",">"
		};
		public const string line = "\r\n";
		public const string tab = "\t";
		public const string space = " ";
		public static List<PossibleObjectHandler> Possibles = new List<PossibleObjectHandler>();

		public static string GetInside(string txt,string open,string close)
		{
			string a = ReadAfter(txt,open);
			string b = ReadBefore(txt,close);
			string ret = txt.Replace(open,"");
			ret = ret.Replace(close,"");
			if(!string.IsNullOrEmpty(a))
			{
				ret = ret.Replace(a,"");
			}
			if(!string.IsNullOrEmpty(b))
			{
				ret = ret.Replace(b,"");
			}
			if(ret.Contains(";"))
			{
				ret = ret.Replace(";","");
			}
			return ret;
		}
		public static int FilterInteger(string txt,bool stopAfterNonDigit)
		{
			List<char> chs = new List<char>();
			var chars = txt.ToCharArray();
			foreach(var ch in chars)
			{
				if(char.IsDigit(ch))
				{
					chs.Add(ch);
				}else
				{
					if(stopAfterNonDigit)
					{
						break;
					}
				}
			}
			string ret = new string(chs.ToArray());
			return int.Parse(ret);
		}
		public static string ReadAfter(string txt,string w)
		{
			string s = txt;
			int ind = s.IndexOf(w,StringComparison.CurrentCulture);
			string res = null;
			if(ind != -1)
			{
				res = s.Substring(0,ind);
			}
			return res;
		}
		public static string ReadBefore(string txt,string w)
		{
			string s = txt;
			int ind = s.IndexOf(w,StringComparison.CurrentCulture);
			string res = null;
			if(ind != -1)
			{
				res = s.Substring(ind,s.Length-ind);
			}
			return res;
		}
		public static string[] Split(string txt,params string[] from)
		{
			List<string> splits = new List<string>();
			foreach(var f in from)
			{
				var l = txt.Split(new string[]{f},StringSplitOptions.None);
				splits.AddRange(l);
			}
			return splits.ToArray();
		}
		public static object ToPossibleValue(string val,out Type typeIs)
		{
			int r = 0;
			float rf = 0f;
			bool rb = false;
			string rs = val;
			object customParse = null;
			if(int.TryParse(val,out r))
			{
				typeIs = typeof(int);
				return r;
			}
			if(float.TryParse(val,out rf))
			{
				typeIs = typeof(float);
				return rf;
			}
			if(bool.TryParse(val,out rb))
			{
				typeIs = typeof(bool);
				return rb;
			}
			foreach(var possible in Possibles)
			{
				if(possible.TryParse(val,out customParse))
				{
					typeIs = possible.Type;
					return customParse;
				}
			}
			if(val.Contains(""))
			{
				typeIs = typeof(string);
				return rs;
			}
			typeIs = typeof(object);
			return null;
		}
		public static string Mount(string txt,object from)
		{
			string ret = txt;
			var bind = BindingFlags.Default|BindingFlags.Public|BindingFlags.Instance|BindingFlags.Static;
			var fields = from.GetType().GetFields(bind);
			foreach(var field in fields)
			{
				var value = field.GetValue(from);
				ret = ret.Replace(field.Name,value+"");
			}
			return ret;
		}
		public static string[] CustomSplit(string txt,char separator,char exception)
		{
			var l = txt.Split(separator);
			List<string> list = new List<string>();
			string contained = null;
			foreach(var i in l)
			{
				if(i.Contains(exception.ToString()))
				{
					if(contained == null)
					{
						contained = i;
						continue;
					}else
					{
						contained += separator+i;
						list.Add(contained);
						contained = null;
					}
				}
				if(contained == null)
				{
					list.Add(i);
				}else
				{
					contained += separator+i;
				}
			}

			return list.ToArray();
		}
		public static string[] OnlySplit(string txt,char separator,params char[] exclude)
		{
			var l = txt.Split(separator);
			if(l.Length == 1)
			{
				l = new string[0];
			}
			List<string> ret = new List<string>();
			foreach(var i in l)
			{
				bool contains = false;
				foreach(var exc in exclude)
				{
					if(i.Contains(exc.ToString()))
					{
						contains = true;
					}
				}
				if(!contains)
				{
					ret.Add(i);
				}
			}
			return ret.ToArray();
		}
		public static string[][] SplitInGroups(string txt,params char[] separators)
		{
			List<string[]> groups = new List<string[]>();
			foreach(var separator in separators)
			{
				groups.Add(txt.Split(separator));
			}
			return groups.ToArray();
		}
		public static string[] SplitAll(string txt,params char[] separators)
		{
			return txt.Split(separators);
		}
		public static string[] SplitAllKeeping(string txt,params char[] separators)
		{
			List<string> split = new List<string>();
			string[] prev = null;
			foreach(var separator in separators)
			{
				var seps = txt.Split(separator);
				for(int i=0;i<seps.Length;i++)
				{
					seps[i] += separator;
				}
				prev = seps;
				split.AddRange(seps);
				string from = GetStringFromEnumerable(prev);
				txt = txt.Replace(from,"");
			}
			return split.ToArray();
		}
		public static string GetStringFromEnumerable(IEnumerable<string> strings)
		{
			string ret = "";
			foreach(var str in strings)
			{
				ret += str;
			}
			return ret;
		}
		public static int GetLess(int? exclude,params int[] array)
		{
			int less = int.MaxValue;
			foreach(var item in array)
			{
				if(item < less)
				{
					if(exclude == null)
					{
						less = item;
					}else
					{
						if(item != exclude.Value)
						{
							less = item;
						}
					}
				}
			}
			return less;
		}
		public static string RemoveFirstChar(string txt,params char[] filter)
		{
			if(filter == null || filter.Length == 0)
			{
				return txt.Remove(0,1);
			}else
			{
				char[] chars = txt.ToCharArray();
				List<char> ret = new List<char>();
				foreach(var ch in chars)
				{
					bool c = false;
					foreach(var item in filter)
					{
						if(ch == item)
						{
							c = true;
						}
					}
					if(!c)
					{
						ret.Add(ch);
					}
				}
				return new string(ret.ToArray());
			}
		}
		public static bool IsAnyOf(char character,params char[] of)
		{
			foreach(var i in of)
			{
				if(character == i)
				{
					return true;
				}
			}
			return false;
		}
		public static string CustomSubstring(string txt,int startIndex,params char[] stopAt)
		{
			List<char> ret = new List<char>();
			var chars = txt.ToCharArray();
			for(int i=startIndex;i<chars.Length;i++)
			{
				var ch = chars[i];
				if(!IsAnyOf(ch,stopAt))
				{
					ret.Add(ch);
				}else
				{
					break;
				}
			}
			return new string(ret.ToArray());
		}
		public static string ReplaceFirst(string txt,string replace)
		{
			List<char> ret = new List<char>();

			int index = 0;
			char prev = char.MinValue;
			foreach(var ch in txt)
			{
				if(ch == replace[index] && (replace.Contains(prev.ToString()) || prev == char.MinValue))
				{

					index++;
				}
			}

			return new string(ret.ToArray());
		}
		public static bool ContainsAnyOf(string txt,params string[] of)
		{
			foreach(var i in of)
			{
				if(txt.Contains(i))
				{
					return true;
				}
			}
			return false;
		}
	}
}
