/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 15:46
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.Windows.Forms;
using System.IO;

namespace DarkLanguageScriptEditor
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DarkLanguageScripterWindow());
		}
		
	}
	public static class ProgramExecutor
	{
		static DarkLanguageScripterWindow Instance;
		public static DarkLanguageScripterWindow Execute(string scriptPath = null)
		{
			if(Instance == null || Instance.IsDisposed)
			{
				var coder = new DarkLanguageScripterWindow();
				coder.Show(Form.ActiveForm);
				Instance = coder;
				CheckScriptPath(scriptPath);
			}else
			{
				Instance.Focus();
				Instance.Select();
				CheckScriptPath(scriptPath);
			}
			return Instance;
		}
		static void CheckScriptPath(string path)
		{
			if(File.Exists(path))
			{
				Instance.OpenScript(path);
			}
		}
	}
}
