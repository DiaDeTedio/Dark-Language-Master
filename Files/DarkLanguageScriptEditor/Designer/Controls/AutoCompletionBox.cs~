/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 20:25
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using CSharpEssentials.Designer.Controls;

namespace DarkLanguageScriptEditor.Designer.Controls
{
	/// <summary>
	/// Description of AutoCompletionBox.
	/// </summary>
	public partial class AutoCompletionBox : UserControl
	{
		public static List<WordDescription> Descriptions = new List<WordDescription>();
		public event OnItemSelectionEvent OnItemSelected;
		public List<WordCompletion> AutoCompletionSource = new List<WordCompletion>();
		public AutoCompletionBox()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		//DescriptionShower desc = new DescriptionShower();
		void CompletionListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if(CompletionListBox.SelectedItem != null)
			{
				var word = (WordCompletion)CompletionListBox.SelectedItem;
				var sel = word.Word;
				var d = "";
				foreach(var des in Descriptions)
				{
					if(sel == des.Word)
					{
						d = des.Description;
					}
				}
				var crb = Parent as CustomRichTextBox;
				var cB = Bounds;
				cB.X += Width;
				if(crb != null && !string.IsNullOrEmpty(d))
				{
					//if(desc == null)
					//{
					//	desc = FloatingText.GetText(crb,d,Color.White,Color.Black,null);
					//	desc.Enabled = false;
					//	desc.CausesValidation = false;
					//	cB = crb.RectangleToScreen(cB);
					//	desc.Bounds = cB;
					//}
					//crb.Select();
					//crb.FindForm().Select();
					//crb.FindForm().Focus();
					crb.DrawBox(cB,Color.Black,d,Color.White);
				}else
				{
					//if(desc != null)
					//{
					//	desc.Close();
					//	desc = null;
					//}
					crb.Invalidate(cB);
				}
			}
			//desc.Text = d;
			//desc.Visible = true;
		}
		//FloatingText desc;

		public void Add(WordCompletion item)
		{
			AutoCompletionSource.Add(item);
			UpdateCompletionSource();
		}
		protected override void OnCreateControl ()
		{
			this.Cursor = Cursors.Default;
			base.OnCreateControl ();
		}
		protected override void OnParentChanged (EventArgs e)
		{
			if(Parent != null)
			{
				//desc.Parent = Parent;
			}else
			{
				//desc.Parent = this;
			}
			base.OnParentChanged (e);
		}
		protected override void OnVisibleChanged (EventArgs e)
		{
			//desc.Location = new Point(Location.X-500,Location.Y);
			//desc.Visible = Visible;
			if(Visible)
			{
				Select();
				if(CompletionListBox.SelectedIndex == -1 && CompletionListBox.Items.Count > 0)
				{
					CompletionListBox.SelectedIndex = 0;
				}
			}
			base.OnVisibleChanged (e);
		}
		public void UpdateCompletionSource()
		{
			CompletionListBox.Items.Clear();
			List<object> items = new List<object>();
			foreach(var item in AutoCompletionSource)
			{
				items.Add(item);
			}
			CompletionListBox.Items.AddRange(items.ToArray());
		}
		public int SelectedItemIndex
		{
			get{return CompletionListBox.SelectedIndex;}
			set{CompletionListBox.SelectedIndex = value;}
		}
		public void MoveSelection(int offset)
		{
			if(SelectedItemIndex+offset < CompletionListBox.Items.Count && SelectedItemIndex+offset > 0)
			{
				SelectedItemIndex += offset;
			}
		}
		public void FilterCompletionSource(string filter)
		{
			if(string.IsNullOrEmpty(filter))
			{
				this.Visible = false;
			}
			var prevSelected = CompletionListBox.SelectedIndex;
			for(int i=0;i<AutoCompletionSource.Count;i++)
			{
				var item = AutoCompletionSource[i];
				if(filter == null||!item.ToString().Contains(filter))
				{
					CompletionListBox.Items.Remove(item);
				}else
				{
					if(!CompletionListBox.Items.Contains(item))
					{
						CompletionListBox.Items.Add(item);
					}
				}
			}
			if(prevSelected != -1 && prevSelected < CompletionListBox.Items.Count)
			{
				CompletionListBox.SelectedIndex = prevSelected;
			}else
			{
				if(CompletionListBox.Items.Count > 0)
				{
					CompletionListBox.SelectedIndex = 0;
				}
			}
		}
		void CompletionListBoxDoubleClick(object sender, EventArgs e)
		{
			CheckItemSelection();
		}
		void CompletionListBoxKeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode != (Keys.Up|Keys.Down|Keys.Enter))
			{
				//Parent.Select();
				//TextBoxBase txt = Parent as TextBoxBase;
				//if(txt != null)
				//{
				//	var key = e.KeyCode.ToString();
				//	if(key.Length == 1)
				//	{
				//		if(!e.Shift)
				//		{
				//			key = key.ToLower();
				//		}
				//		var chs = key[0];
				//		txt.AppendText(chs.ToString());
				//	}
				//	if(e.KeyCode == Keys.Back)
				//	{
				//		if(txt.SelectionStart -1 >= 0 && txt.SelectionStart -1 < txt.TextLength)
				//		{
				//			int sls = txt.SelectionStart;
				//			txt.Text = txt.Text.Remove(txt.SelectionStart-1,1);
				//			txt.SelectionStart = sls-1;
				//		}
				//	}
				//	if(e.KeyCode == Keys.Space)
				//	{
				//		txt.AppendText(" ");
				//	}
				//}
			}
			if(e.KeyCode == Keys.Enter)
			{
				CheckItemSelection();
				Visible = false;
			}
		}
		public void CheckItemSelection()
		{
			var selected = CompletionListBox.SelectedItem as WordCompletion;
			if(selected != null && OnItemSelected != null)
			{
				OnItemSelected.Invoke(selected.ToString());
			}
		}
	}
	public class WordCompletion
	{
		public string Word;
		public object Tag;

		public WordCompletion(string word,object tag = null)
		{
			Word = word;
			Tag = tag;
		}
		public override string ToString ()
		{
			return $"{Word}{Tag}";
		}
	}
	public class WordDescription
	{
		public string Word;
		public string Description;

		public WordDescription(string word,string desc)
		{
			Word = word;
			Description = desc;
		}
	}
	public delegate void OnItemSelectionEvent(string item);
}
