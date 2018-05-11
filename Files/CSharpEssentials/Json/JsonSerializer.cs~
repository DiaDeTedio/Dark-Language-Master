using System;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;

namespace CSharpEssentials.Json
{
	public static class JsonSerializer
	{
		public static string Serialize(object obj)
		{
			string ret = "";
			//var type = obj.GetType();
			var ser = Newtonsoft.Json.JsonSerializer.Create();
			string path = Path.GetTempFileName();
			StreamWriter writer = new StreamWriter(path);
			ser.Serialize(writer,obj);
			writer.Close();
			StreamReader reader = new StreamReader(path);
			ret = reader.ReadToEnd();
			reader.Close();
			return ret;
		}
		public static object Deserialize(string from,Type type)
		{
			from = from.Replace("\r\n","");
			var ser = Newtonsoft.Json.JsonSerializer.Create();
			MemoryStream mem = new MemoryStream();
			StreamWriter writer = new StreamWriter(mem);
			writer.Write(from);
			writer.Flush();
			StreamReader reader = new StreamReader(mem);
			var ret = ser.Deserialize(reader,type);
			writer.Close();
			reader.Close();
			mem.Close();
			return ret;
		}
		public static void SaveIt(object obj,string path)
		{
			StreamWriter wrr = new StreamWriter(path);
			DataContractJsonSerializerSettings stt = new DataContractJsonSerializerSettings();
			stt.RootName = "root";
			stt.MaxItemsInObjectGraph = int.MaxValue;
			DataContractJsonSerializer ser = new DataContractJsonSerializer(obj.GetType(),stt);
			ser.WriteObject(wrr.BaseStream,obj);
			wrr.Close();
		}
		public static T Deserialize<T>(string from)
		{
			return (T)Deserialize(from,typeof(T));
		}

		public static void SaveJson(string json,string path)
		{
			File.WriteAllText(path,json);
		}
		public static string LoadJson(string path)
		{
			return File.ReadAllText(path);
		}
		public static object FromJson(string path,Type type)
		{
			var json = LoadJson(path);
			return Deserialize(json,type);
		}
		public static T FromJson<T>(string path)
		{
			var json = LoadJson(path);
			return Deserialize<T>(json);
		}
		public static string LayoutJson(string json)
		{
			json = json.Replace("{","{\r\n");
			json = json.Replace("}","\r\n}");
			json = json.Replace(",",",\r\n");
			return json;
		}
	}
}
