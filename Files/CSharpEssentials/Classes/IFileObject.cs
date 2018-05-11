using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace CSharpEssentials.Classes
{
	/// <summary>
	/// IFile object is object that can be saved and loaded.
	/// </summary>
	[Serializable]
	public class IFileObject
	{
		public static void Save(IFileObject fileObject,string path)
		{
			BinaryFormatter bin = new BinaryFormatter();
			var file = File.Create(path);
			bin.Serialize(file,fileObject);
			file.Close();
		}
		public static IFileObject Load(string path)
		{
			BinaryFormatter bin = new BinaryFormatter();
			if(File.Exists(path))
			{
				var file = File.OpenRead(path);
				var ret = (IFileObject)bin.Deserialize(file);
				file.Close();
				return ret;
			}
			return null;
		}
		public static T Load<T>(string path) where T : IFileObject
		{
			BinaryFormatter bin = new BinaryFormatter();
			if(File.Exists(path))
			{
				var file = File.OpenRead(path);
				var ret = (T)bin.Deserialize(file);
				file.Close();
				return ret;
			}
			return null;
		}
	}
}
