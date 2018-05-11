/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 21:13
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
namespace DarkLanguageScriptEditor.Designer.Controls
{
	partial class DarkLanguageCoder
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DarkLanguageScriptEditor.Designer.Controls.CustomRichTextBox codeBox;
		
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
			this.codeBox = new DarkLanguageScriptEditor.Designer.Controls.CustomRichTextBox();
			this.SuspendLayout();
			// 
			// codeBox
			// 
			this.codeBox.AutoCompletionSource = new WordCompletion[0];
			this.codeBox.CurrentText = null;
			this.codeBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.codeBox.Location = new System.Drawing.Point(0, 0);
			this.codeBox.Name = "codeBox";
			this.codeBox.Size = new System.Drawing.Size(491, 472);
			this.codeBox.TabIndex = 0;
			this.codeBox.Text = "";
			this.codeBox.UseAutoCompletion = true;
			this.codeBox.TextChanged += this.CodeBoxTextChanged;
			// 
			// DarkLanguageCoder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.codeBox);
			this.Name = "DarkLanguageCoder";
			this.Size = new System.Drawing.Size(491, 472);
			this.ResumeLayout(false);

		}
	}
}
