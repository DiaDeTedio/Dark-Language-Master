using System;

namespace DarkLanguage.Classes.ScriptClasses
{
	[Serializable]
	public class ScriptField
	{
		public string Name;
		object value;
		public object Value
		{
			get
			{
				if(IsArray)
				{
					return Values;
				}else
				{
					return value;
				}
			}
			set
			{
				this.value = value;
			}
		}
		public ScriptFieldCollection Values;
		public bool IsArray
		{
			get
			{
				if(Values != null)
				{
					return true;
				}
				return false;
			}
		}
		object defaultValue;
		public object DefaultValue
		{
			get
			{
				return defaultValue;
			}
		}

		public ScriptField (string name,object value = null)
		{
			Name = name;
			Value = value;
			defaultValue = value;
		}
		public void AddItem(object item)
		{
			if(Values == null){Values = new ScriptFieldCollection();}
			Values.Add(new ScriptField($"{Name}[{Values.Count-1}]",item));
		}
		public void AddItem(ScriptField item)
		{
			if(Values == null){Values = new ScriptFieldCollection();}
			Values.Add(item);
			Values[Values.Count-1].Name = $"{Name}[{Values.Count-1}]";
		}
		public override string ToString ()
		{
			if(IsArray)
			{
				return Values.ToString();
			}else
			{
				return string.Format ("[{0}: Value={1}, IsArray={2}, DefaultValue={3}]",Name,Value,IsArray,DefaultValue);
			}
		}
	}
}
