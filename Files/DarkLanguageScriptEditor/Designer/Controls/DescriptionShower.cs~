/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 22/07/2017
 * Hora: 00:26
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DarkLanguageScriptEditor.Designer.Controls
{
	/// <summary>
	/// Description of DescriptionShower.
	/// </summary>
	public partial class DescriptionShower : UserControl
	{
		[Browsable(true)]
		public override string Text {
			get 
			{
				return base.Text;
			}
			set 
			{
				base.Text = value;
			}
		}
		public DescriptionShower()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		protected override void OnPaint (PaintEventArgs e)
		{
			e.Graphics.DrawString(Text,Font,new SolidBrush(ForeColor),Location);
			base.OnPaint (e);
		}
	}
}
