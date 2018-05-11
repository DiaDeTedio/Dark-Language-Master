/*
 * Criado por SharpDevelop.
 * Usuário: Computador Pessoal
 * Data: 21/07/2017
 * Hora: 21:13
 * 
 * Para alterar este modelo use Ferramentas | Opções | Codificação | Editar Cabeçalhos Padrão.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DarkLanguage.Bases.Source;
using System.Collections.Generic;
using CSharpEssentials;
using DarkLanguage.Versioning.Source;
using DarkLanguage.Classes;
using DarkLanguage;

namespace DarkLanguageScriptEditor.Designer.Controls
{
	/// <summary>
	/// Description of DarkLanguageCoder.
	/// </summary>
	public partial class DarkLanguageCoder : UserControl
	{
		public string CurrentCode
		{
			get
			{
				return codeBox.Text;
			}
			set
			{
				codeBox.Text = value;
			}
		}
		public bool Saved
		{
			get{return codeBox.Saved;}
		}
		public string Path
		{
			get
			{
				return codeBox.Path;
			}
		}
		public void SaveCode(string path)
		{
			codeBox.Save(path);
		}
		public void LoadCode(string path)
		{
			codeBox.Load(path);
		}

		public Color ObjectColor = Color.Green;
		public Color MethodColor = Color.Gray;
		public Color FieldsColor = Color.Goldenrod;
		public Color KeywordColor = Color.DarkBlue;
		public Color EventWordsColor = Color.Blue;
		public Color CommonColor = Color.Aqua;
		public Color CommandsColor = Color.Coral;
		public Color SpecialColor = Color.Crimson;
		public DarkLanguageCoder()
		{
			InitializeComponent();
		}
		protected override void OnCreateControl ()
		{
			if(!DesignMode)
			{
				Interpreter.Initialize();
				InitializeCodeEditor();
			}
			base.OnCreateControl ();
		}
		public void InitializeCodeEditor()
		{
			List<WordCompletion> source = new List<WordCompletion>();
			var objects = CODE_INFO.Possibles;
			var methods = CODE_INFO.Methods;
			var fields = CODE_INFO.AllCodeFields;
			var keywords = CODE_INFO.GetInfo(CODE_INFO.Keywords);
			var eventWords = CODE_INFO.GetInfo(CODE_INFO.EventWords);
			var common = CODE_INFO.GetInfo(CODE_INFO.Common);
			var commands = CODE_INFO.GetInfo(CODE_INFO.Commands);

			objects.ForEach((obj) => source.Add(new WordCompletion(obj.Name)));
			methods.ForEach((obj) => source.Add(new WordCompletion(obj.Name,$"({obj.ParametersRepresentation()})")));
			fields.ForEach((obj) => source.Add(new WordCompletion($"{obj.Name} = {obj.Value+""}")));

			keywords.ForEach((obj) => source.Add(new WordCompletion(obj)));
			eventWords.ForEach((obj) => source.Add(new WordCompletion(obj)));
			common.ForEach((obj) => source.Add(new WordCompletion(obj)));
			commands.ForEach((obj) => source.Add(new WordCompletion(obj)));

			codeBox.AutoCompletionSource = source.ToArray();
			for (var i = 0; i < codeBox.WordInfos.Count;i++) 
			{
				var info = codeBox.WordInfos[i];
				if(objects.Exists((obj) => obj.Name != info.Word))
				{
					codeBox.WordInfos.Remove(info);
				}
				if(fields.Exists((obj) => obj.Name != info.Word))
				{
					codeBox.WordInfos.Remove(info);
				}
			}
			codeBox.WordInfos.Add(new WordInfo("DECLARE",Color.DodgerBlue));
			codeBox.WordInfos.Add(new WordInfo("EVENT",Color.DodgerBlue));
			objects.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj.Name,ObjectColor)));
			methods.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj.Name,MethodColor)));
			fields.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj.Name,FieldsColor)));
			GetAllStrings().ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,Color.Gold)));

			keywords.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,KeywordColor)));
			eventWords.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,EventWordsColor)));
			common.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,CommonColor)));
			commands.ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,CommandsColor)));
			CODE_INFO.GetInfo(CODE_INFO.Specials).ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,SpecialColor)));
			GetAllStrings().ForEach((obj) => codeBox.WordInfos.Add(new WordInfo(obj,KeywordColor)));
			List<WordDescription> descs = new List<WordDescription>();
			descs.Add(new WordDescription("Declare","Declares a new variable in this method or script"));
			descs.Add(new WordDescription("var",descs[0].Description));
			descs.Add(new WordDescription("String","A characted literal array,it can be declared typing a text"));

			AutoCompletionBox.Descriptions.AddRange(descs);
		}
		string[] GetAllStrings()
		{
			List<string> strings = new List<string>();
			foreach(var field in CODE_EXEC.Fields)
			{
				if(field.Value is string)
				{
					var str = (string)field.Value;
					if(!string.IsNullOrEmpty(str))
					{
						strings.Add(str);
					}
				}
			}
			return strings.ToArray();
		}
		long changes = 0;
		void CodeBoxTextChanged(object sender, EventArgs e)
		{
			if(changes >= 1)
			{
				AnalyzeCode();
				InitializeCodeEditor();
				changes = 0;
			}
			changes++;
		}
		public void AnalyzeCode()
		{
			CODE_EXEC.Fields.Clear();
			var lines = CODE_READER.GetScriptLines(CurrentCode);
			foreach(var line in lines)
			{
				var lineCode = CodeTranslator.TranslateCode(line);
				CODE_READER.CheckLanguageRoutines(lineCode,0);
			}
		}
	}
}
