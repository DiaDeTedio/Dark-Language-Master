using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSharpEssentials.Source
{
	public class BinaryComparer
	{
		public object First;
		public object Second;
		byte[] data0;
		byte[] data1;

		BinaryComparer(object f,object s)
		{
			First = f;
			Second = s;
		}
		public bool IsEqual()
		{
			//bool ret = false;

			if(data0.Length != data1.Length)
			{
				return false;
			}else
			{
				int equals = 0;
				for(int i=0;i<data0.Length;i++)
				{
					var b0 = data0[i];
					var b1 = data1[i];
					if(b0 == b1)
					{
						equals++;
					}
				}
				if(equals >= data0.Length)
				{
					return true;
				}else
				{
					return false;
				}
			}

			//return ret;
		}
		public static BinaryComparer Create(object first,object second)
		{
			BinaryComparer bc = new BinaryComparer(first,second);
			BinaryFormatter bin = new BinaryFormatter();
			MemoryStream mem = new MemoryStream();
			bin.Serialize(mem,bc.First);
			bc.data0 = mem.ToArray();
			mem.Dispose();
			mem.Close();
			mem = new MemoryStream();
			bin.Serialize(mem,bc.Second);
			bc.data1 = mem.ToArray();
			mem.Dispose();
			mem.Close();
			return bc;
		}
	}
}
