using System;

namespace CSharpEssentials
{
	public static class Clipboard
	{
		static object data;
		public static void Copy(object copy)
		{
			data = copy;
		}
		public static object Paste()
		{
			return data;
		}
	}
}
