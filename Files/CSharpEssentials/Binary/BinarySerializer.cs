using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CSharpEssentials.Binary
{
	/// <summary>
	/// Binary serializer class to help making serializations and deserializations in CSharp.
	/// </summary>
	public static class BinarySerializer
	{
		static BinaryFormatter ___bin___ = new BinaryFormatter();
		/// <summary>
		/// Gets the binary serializer of this class.
		/// </summary>
		/// <value>The bin.</value>
		public static BinaryFormatter Bin
		{
			get{return ___bin___;}
		}
		/// <summary>
		/// Serialize the specified object and return his byte data.
		/// </summary>
		/// <returns>The serialize.</returns>
		/// <param name="obj">Object.</param>
		public static byte[] Serialize(object obj)
		{
			var mem = new MemoryStream();
			Bin.Serialize(mem,obj);
			var bytes = mem.ToArray();
			mem.Close();
			return bytes;
		}
		/// <summary>
		/// Deserialize the specified data and return his object.
		/// </summary>
		/// <returns>The deserialize.</returns>
		/// <param name="data">Data.</param>
		public static object Deserialize(byte[] data)
		{
			var mem = new MemoryStream(data);
			var obj = Bin.Deserialize(mem);
			mem.Close();
			return obj;
		}
		/// <summary>
		/// Deserialize the specified data and return his generic object.
		/// </summary>
		/// <returns>The deserialize.</returns>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Deserialize<T>(byte[] data)
		{
			return (T)Deserialize(data);
		}
	}
}
