using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using CSharpEssentials;
using System.Collections;

namespace DarkLanguageScriptEditor.Designer.Controls
{
	public class CustomRichTextBox : RichTextBox
	{
		bool saved;
		string path;
		public bool Saved
		{
			get
			{
				return saved;
			}
		}
		public string Path
		{
			get{return path;}
		}

		public void Save(string path)
		{
			File.WriteAllText(path,Text);
			this.path = path;
			saved = true;
		}
		public void Load(string path)
		{
			Text = File.ReadAllText(path);
			this.path = path;
			saved = true;
		}

		AutoCompletionBox autoCompletion = new AutoCompletionBox();
		[Description("Auto completion source,with this,you can make your own auto completion")]
		[Category("Text Auto Completion")]
		public WordCompletion[] AutoCompletionSource
		{
			get
			{
				if(autoCompletion == null)
				{
					return new WordCompletion[0];
				}
				return autoCompletion.AutoCompletionSource.ToArray();
			}
			set
			{
				autoCompletion.AutoCompletionSource = value.ToList();
				autoCompletion.UpdateCompletionSource();
			}
		}
		bool useAutoCompletion;
		[Description("If true,display's auto completion box on this control")]
		[Category("Text Auto Completion")]
		public bool UseAutoCompletion
		{
			get
			{
				return useAutoCompletion;
			}
			set
			{ 
				useAutoCompletion = value;
			}
		}
		public CustomRichTextBox() : base()
		{
			
		}
		public WordInfoCollection WordInfos = new WordInfoCollection();
		string prevText;
		public string CurrentText
		{
			get
			{
				return prevText;
			}
			set
			{
				if(prevText == null){prevText = string.Empty;}
				prevText = value;
				if(string.IsNullOrEmpty(prevText))
				{
					prevText = value;
				}
				if(string.IsNullOrEmpty(value))
				{
					prevText = value;
					return;
				}
				string val = this.Text.Replace(prevText,value);
				prevText = val;
			}
		}
		public string LocalizedText
		{
			get
			{
				return LocalizeText(this.SelectionStart);
			}
		}
		public string LocalizeText(int startChar)
		{
			startChar--;
			string ret = "";
			if(startChar < 0||startChar >= Text.Length)
			{
				return ret;
			}
			char? current = Text[startChar];
			int index = startChar;
			if(current == null)
			{
				return ret;
			}
			while(char.IsLetterOrDigit(current.Value) && !char.IsSeparator(current.Value) || current != null)
			{
				if(index < 0 || index >= Text.Length)
				{
					current = null;
					break;
				}
				current = Text[index];
				if(char.IsLetterOrDigit(current.Value) && !char.IsSeparator(current.Value))
				{
					ret = ret.Insert(0,current.ToString());
				}else
				{
					break;
				}
				index--;

			}
			if(current == null)
			{
				return ret;
			}
			while(char.IsLetterOrDigit(current.Value) && !char.IsSeparator(current.Value) || current != null)
			{
				if(index < 0 || index >= Text.Length)
				{
					current = null;
					break;
				}
				current = Text[index];
				if(char.IsLetterOrDigit(current.Value) && !char.IsSeparator(current.Value))
				{
					ret += current;
				}else
				{
					break;
				}
				index++;
			}
			return ret;
		}
		public void DrawBox(Rectangle rect,Color rectColor,string text,Color textColor)
		{
			var g = CreateGraphics();
			g.FillRectangle(new SolidBrush(rectColor),rect);
			g.DrawString(text,Font,new SolidBrush(textColor),rect);
		}
		public void OnItemSelected(string item)
		{
			string result = item;
			if(!string.IsNullOrEmpty(this.LocalizedText))
			{
				result = item.Replace(this.LocalizedText,"");
			}
			this.AppendText(result);
		}
		protected override void OnCreateControl ()
		{
			if(!DesignMode)
			{
				autoCompletion = new AutoCompletionBox();
				autoCompletion.OnItemSelected += OnItemSelected;
				autoCompletion.Parent = this;
				autoCompletion.Show();
				autoCompletion.Visible = false;
			}
			base.OnCreateControl ();
		}
		protected override void OnTextChanged (EventArgs e)
		{
			saved = false;
			base.OnTextChanged (e);
			//CurrentText = Text;
			MakeAutoCompletion(true);
			CheckWords();
			Memory.Add(Text);
			//autoCompletion.Focus();
		}
		public void CheckWords()
		{
			foreach(var word in WordInfos)
			{
				CheckKeyword(word.Word,word.Color,0);
			} 
		}
		int selection;
		public List<string> Memory = new List<string>();
		protected override void OnKeyDown (KeyEventArgs e)
		{
			this.ClearUndo();
			selection = SelectionStart;
			if(e.KeyCode == Keys.Escape)
			{
				MakeAutoCompletion(false);
			}else
			{
				//if(e.KeyCode == Keys.Enter)
				//{
				//	if(!e.Control)
				//	{
				//		Text = this.Text.Remove(SelectionStart-1);
				//	}else
				//	{
				//		autoCompletion.Select();
				//		autoCompletion.CheckItemSelection();
				//	}
				//}
				if(e.KeyCode == Keys.Space)
				{
					//CurrentText = "";
				}
				if(e.KeyCode == Keys.Up)
				{
					autoCompletion.Focus();
					autoCompletion.MoveSelection(-1);
				}
				if(e.KeyCode == Keys.Down)
				{
					autoCompletion.Focus();
					autoCompletion.MoveSelection(1);
				}
			}
			if(e.Control && e.KeyCode == Keys.Z)
			{
				UndoCode();
			}
			base.OnKeyDown (e);
		}
		public void UndoCode()
		{
			int index = this.SelectionStart;
			if(Memory.Count > 1)
			{
				string current = Memory.GetLastItem();
				Memory.RemoveLast();
				string prevTxt = Text;
				this.ResetText();
				AppendText(current);
				index -= prevTxt.Length-current.Length;
			}
			this.SelectionStart = index;
		}
		public void RedoCode()
		{

		}
		private void CheckKeyword(string word, Color color, int startIndex)
		{
			this.SelectionColor = Parent.ForeColor;
			if (this.Text.Contains(word))
			{
				int index = -1;
				int selectStart = this.SelectionStart;

				while ((index = this.Text.IndexOf(word, (index + 1),StringComparison.CurrentCulture)) != -1)
				{
					this.Select((index + startIndex), word.Length);
					this.SelectionColor = color;
					this.Select(selectStart, 0);
					this.SelectionColor = Parent.ForeColor;
				}
			}
		}
		char GetChar(string text,int index)
		{
			if(index < 0 || index >= text.Length)
			{
				return '"';
			}else
			{
				return text[index];
			}
		}
		protected override void OnMouseDown (MouseEventArgs e)
		{
			MakeAutoCompletion(false);
			base.OnMouseDown (e);
		}
		public void MakeAutoCompletion(bool showhide)
		{
			var pos = GetPositionFromCharIndex(this.SelectionStart);
			pos.Y += 20;
			autoCompletion.Location = pos;
			autoCompletion.FilterCompletionSource(this.LocalizedText);
			autoCompletion.Visible = showhide;
		}
	}
	public struct WordInfo
	{
		public string Word;
		public Color Color;

		public WordInfo(string word,Color color)
		{
			Word = word;
			Color = color;
		}
	}
	public class WordInfoCollection : ICollection<WordInfo>
	{
		public List<WordInfo> Items = new List<WordInfo>();

		public WordInfo this[int index]
		{
			get
			{
				return Items[index];
			}
			set
			{
				Items[index] = value;
			}
		}

		public int Count {
			get {
				return ((ICollection<WordInfo>)Items).Count;
			}
		}

		public bool IsReadOnly {
			get {
				return ((ICollection<WordInfo>)Items).IsReadOnly;
			}
		}

		public void Add (WordInfo item)
		{
			if(!Items.Contains(item))
			{
				((ICollection<WordInfo>)Items).Add (item);
			}
		}

		public void Clear ()
		{
			((ICollection<WordInfo>)Items).Clear ();
		}

		public bool Contains (WordInfo item)
		{
			return ((ICollection<WordInfo>)Items).Contains (item);
		}

		public void CopyTo (WordInfo [] array, int arrayIndex)
		{
			((ICollection<WordInfo>)Items).CopyTo (array, arrayIndex);
		}

		public IEnumerator<WordInfo> GetEnumerator ()
		{
			return ((ICollection<WordInfo>)Items).GetEnumerator ();
		}

		public bool Remove (WordInfo item)
		{
			return ((ICollection<WordInfo>)Items).Remove (item);
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((ICollection<WordInfo>)Items).GetEnumerator ();
		}
	}
}
