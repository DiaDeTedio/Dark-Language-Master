using System;

namespace DarkLanguage.Classes
{
	public class PossibleObjectHandler
	{
		public readonly string Name;
		public readonly Type Type;

		public readonly TryParseTo ParseMethod;

		public PossibleObjectHandler(string name,Type type,TryParseTo parseMethod)
		{
			Name = name;
			Type = type;
			ParseMethod = parseMethod;
		}
		public bool TryParse(string from,out object value)
		{
			value = null;
			if(ParseMethod != null)
			{
				return ParseMethod.Invoke(from,out value);
			}
			return false;
		}
	}
	public delegate bool TryParseTo(string from,out object to);
}
