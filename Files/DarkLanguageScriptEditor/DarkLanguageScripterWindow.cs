/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 15:46
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DarkLanguageScriptEditor.Designer.Controls;
using System.IO;
using DarkLanguageCompiler.Source;
using System.Runtime;
using CSharpEssentials;

namespace DarkLanguageScriptEditor
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class DarkLanguageScripterWindow : Form
	{
		public DarkLanguageCoder[] Coders
		{
			get
			{
				List<DarkLanguageCoder> coders = new List<DarkLanguageCoder>();
				foreach(TabPage tab in scriptsTabControl.TabPages)
				{
					var coder = (DarkLanguageCoder)tab.Controls[0];
					coders.Add(coder);
				}
				return coders.ToArray();
			}
		}
		public DarkLanguageScripterWindow()
		{
			InitializeComponent();
		}
		void NewToolStripMenuItemClick(object sender, EventArgs e)
		{
			AddTabPage();
		}
		void OpenToolStripMenuItemClick(object sender, EventArgs e)
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Multiselect = true;
			open.Filter = "Dark Language Script(.dls)|*.dls";
			open.Title = "Select Your Dark Language Script";
			open.ShowDialog();
			foreach(var finish in open.FileNames)
			{
				if(!string.IsNullOrEmpty(finish))
				{
					var title = Path.GetFileNameWithoutExtension(finish);
					AddTabPage(finish,title);
				}
			}
		}
		public void OpenScript(string path)
		{
			foreach(var coder in Coders)
			{
				if(coder.Path == path)
				{
					this.Focus();
					return;
				}
			}
			var title = Path.GetFileNameWithoutExtension(path);
			AddTabPage(path,title);
		}
		void SaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			var coder = (DarkLanguageCoder)scriptsTabControl.SelectedTab.Controls[0];
			if(coder.Path == null && !File.Exists(coder.Path))
			{
				CustomSave();
			}else
			{
				coder.SaveCode(coder.Path);
			}
		}
		void SaveAsToolStripMenuItemClick(object sender, EventArgs e)
		{
			var coder = (DarkLanguageCoder)scriptsTabControl.SelectedTab.Controls[0];
			CustomSave();
		}
		public void CustomSave()
		{
			string name = scriptsTabControl.SelectedTab.Text;
			SaveFileDialog open = new SaveFileDialog();
			open.Filter = "Dark Language Script(.dls)|*.dls";
			open.Title = "Select Location Of Your Dark Language Script";
			open.FileName = name;
			open.ShowDialog();
			var finish = open.FileName;
			if(!string.IsNullOrEmpty(finish))
			{
				var coder = (DarkLanguageCoder)scriptsTabControl.SelectedTab.Controls[0];
				coder.SaveCode(finish);
			}
		}
		void CompileToolStripMenuItemClick(object sender, EventArgs e)
		{
			List<string> scripts = new List<string>();
			foreach(TabPage tab in scriptsTabControl.TabPages)
			{
				var coder = (DarkLanguageCoder)tab.Controls[0];
				scripts.Add(coder.CurrentCode);
			}
			Compiler.Compile(scripts.ToArray());
			SaveFileDialog open = new SaveFileDialog();
			open.Filter = "Dark Language Assembly(.dla)|*.dla";
			open.Title = "Select Location Of Your Dark Language Script";
			open.ShowDialog();
			if(!string.IsNullOrEmpty(open.FileName))
			{
				Compiler.CustomSaveAssembly(open.FileName);
			}
		}
		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show("A program to make scripts in .dls format","About Us");
		}
		public void AddTabPage()
		{
			TabPage page = new TabPage("Script "+scriptsTabControl.TabPages.Count);
			DarkLanguageCoder coder = new DarkLanguageCoder();
			coder.Dock = DockStyle.Fill;
			coder.Parent = page;
			coder.Show();
			scriptsTabControl.TabPages.Add(page);
			scriptsTabControl.SelectTab(page);
		}
		public void AddTabPage(string path,string title = "Script")
		{
			TabPage page = new TabPage(title);
			DarkLanguageCoder coder = new DarkLanguageCoder();
			coder.Dock = DockStyle.Fill;
			coder.LoadCode(path);
			//coder.CurrentCode = code;
			coder.Parent = page;
			coder.Show();
			scriptsTabControl.TabPages.Add(page);
			scriptsTabControl.SelectTab(page);
		}
		void ScriptsTabControlMouseDown(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				var mpos = PointToClient(MousePosition);
				var mTab = this.scriptsTabControl.GetTabAt(this.scriptsTabControl.MousePos());
				this.scriptsTabControl.SelectedTab = mTab;
				ContextMenu menu = new ContextMenu();
				menu.MenuItems.Add("Close",CloseCurrentTab);
				menu.Show(this,this.PointToClient(MousePosition));
			}
		}
		public void CloseCurrentTab(object sender,EventArgs e)
		{
			CloseTab(scriptsTabControl.SelectedTab);
		}
		public void CloseTab(TabPage tab)
		{
			//var r = MessageBox.Show("You are sure?",$"Close {tab.Text}.dls",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
			var r = ConfirmScriptClosing(tab.Text,(DarkLanguageCoder)tab.Controls[0]);
			if(r == DialogResult.Yes || r == DialogResult.No)
			{
				tab.Controls.ForEach<Control>((obj) => obj.Dispose());
				tab.Dispose();
				scriptsTabControl.TabPages.Remove(tab);
			}
		}
		public DialogResult ConfirmScriptClosing(string title,DarkLanguageCoder coder)
		{
			if(!coder.Saved)
			{
				var message = $"{title}.dls Has No Saved,you want to save?";
				var r = MessageBox.Show(message,"Script not Saved",MessageBoxButtons.YesNoCancel);
				if(r == DialogResult.Yes)
				{
					SaveToolStripMenuItemClick(null,null);
				}
				return r;
			}
			return DialogResult.Yes;
		}
		protected override void OnClosing (System.ComponentModel.CancelEventArgs e)
		{
			foreach(TabPage tab in scriptsTabControl.TabPages)
			{
				var coder = (DarkLanguageCoder)tab.Controls[0];
				if(!coder.Saved)
				{
					var message = $"{tab.Text}.dls Has No Saved,you want to save?";
					var r = ConfirmScriptClosing(tab.Text,coder);
					if(r == DialogResult.No)
					{
						continue;
					}
					if(r == DialogResult.Cancel)
					{
						e.Cancel = true;
						break;
					}
				}
			}
			base.OnClosing (e);
		}
	}
}
