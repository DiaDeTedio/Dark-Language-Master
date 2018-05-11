using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using CSharpEssentials.Designer.Controls;

namespace CSharpEssentials.Xml
{
	public static class XmlUtils
	{
		public static string GetXml(object obj,bool showProgress = false)
		{
			NProgressBar pb = null;
			if(showProgress)
			{
				pb = GetProgressBar();
				prevBar = pb;
			}
			var xml = new XmlSerializer(obj.GetType());
			if(pb != null)
			{
				pb.OnTimerTick += onPbInv;
				pb.Increment(20);
			}
			string tempPath = Path.GetTempFileName();
			if(pb != null)
			{
				pb.Increment(20);
			}
			StreamWriter mem = new StreamWriter(tempPath);
			mem.AutoFlush = true;
			xml.Serialize(mem,obj);
			mem.Close();
			if(pb != null)
			{
				pb.Increment(20);
			}
			var reader = new StreamReader(tempPath);
			var str = reader.ReadToEnd();
			reader.Close();
			if(pb != null)
			{
				pb.Increment(20);
			}
			File.Delete(tempPath);
			if(pb != null)
			{
				pb.Increment(20);
			}
			//if(pb != null)
			//{
			//	pb.FindForm().Close();
			//}
			return str;
		}
		static NProgressBar prevBar;
		static void onPbInv(object sender,EventArgs e)
		{
			if(prevBar.Value >= 90)
			{
				prevBar.FindForm().Close();
			}
		}
		public static object FromXml<T>(string xml)
		{
			var xmls = new XmlSerializer(typeof(T));
			MemoryStream mem = new MemoryStream();
			new BinaryFormatter().Serialize(mem,xml);
			var obj = (T)xmls.Deserialize(mem);
			mem.Close();
			return obj;
		}
		public static void SaveXml<T>(object obj,string path)
		{
			StreamWriter file = new StreamWriter(path);
			var xmls = new XmlSerializer(typeof(T));
			xmls.Serialize(file,obj);
			file.Close();
		}
		public static T LoadFromXml<T>(string path)
		{
			StreamWriter file = new StreamWriter(path);
			var xmls = new XmlSerializer(typeof(T));
			var fl = (T)xmls.Deserialize(file.BaseStream);
			file.Close();
			return fl;
		}
		static NProgressBar GetProgressBar()
		{
			Form f = new Form();
			f.FormBorderStyle = FormBorderStyle.None;
			f.Size = new Size(300,50);
			f.Text = "Progress...";
			NProgressBar pb = new NProgressBar();
			pb.Parent = f;
			pb.Dock = DockStyle.Fill;
			pb.Show();
			pb.Minimum = 0;
			pb.Maximum = 100;
			f.Show(Form.ActiveForm);
			return pb;
		}
	}
}
