/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 20:25
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
namespace DarkLanguageScriptEditor.Designer.Controls
{
	partial class AutoCompletionBox
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListBox CompletionListBox;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.CompletionListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// CompletionListBox
			// 
			this.CompletionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CompletionListBox.FormattingEnabled = true;
			this.CompletionListBox.Location = new System.Drawing.Point(0, 0);
			this.CompletionListBox.Name = "CompletionListBox";
			this.CompletionListBox.Size = new System.Drawing.Size(399, 309);
			this.CompletionListBox.TabIndex = 0;
			this.CompletionListBox.SelectedIndexChanged += new System.EventHandler(this.CompletionListBoxSelectedIndexChanged);
			this.CompletionListBox.DoubleClick += new System.EventHandler(this.CompletionListBoxDoubleClick);
			this.CompletionListBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompletionListBoxKeyDown);
			// 
			// AutoCompletionBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CompletionListBox);
			this.Name = "AutoCompletionBox";
			this.Size = new System.Drawing.Size(399, 309);
			this.ResumeLayout(false);

		}
	}
}
