using Meerkat.Library;
using MeerkatUI.Utils;

using Microsoft.Win32;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace MeerkatUI
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private readonly TemplateEngine engine;
		private string openedTemplatePath = string.Empty;


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		private ObservableCollection<Variable> variables = new ObservableCollection<Variable>();
		public ObservableCollection<Variable> Variables 
		{
			get => variables;
			set 
			{
				variables = value;
				OnPropertyChanged("Variables");
			}
		} 


		public MainWindow()
		{
			InitializeComponent();

			engine = new TemplateEngine();

			DataContext = this;
		}


		private void RefreshVariables()
		{
			engine.Variables.Clear();
			foreach (var variable in Variables)
			{
				if (engine.Variables.ContainsKey(variable.Name))
					engine.Variables[variable.Name] = variable.Value;
				else
					engine.Variables.Add(variable.Name, variable.Value);
			}
		}


		private void ProcessButton_Click(object sender, RoutedEventArgs e)
		{
			RefreshVariables();
			var text = engine.ProcessTemplate(InputTextbox.Text);

			OutputTextbox.Text = text;
		}


		private void IgnoreUnknown_Checked(object sender, RoutedEventArgs e)
		{
			engine.IgnoreUnknown = true;
		}


		private void IgnoreUnknown_Unchecked(object sender, RoutedEventArgs e)
		{
			engine.IgnoreUnknown = false;
		}


		private void AddVariable(object sender, RoutedEventArgs e)
		{
			Variables.Add(new Variable("A", "яблоко"));
		}


		private void DeleteVariable(object sender, RoutedEventArgs e)
		{
			var variable = ((Button)sender).DataContext as Variable;

			if (variable != null)
				Variables.Remove(variable);
		}


		private void OpenFileMenu_Click(object sender, RoutedEventArgs e)
		{
			var openFileDialogue = new OpenFileDialog()
			{
				Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
			};

			if (openFileDialogue.ShowDialog() == true)
			{
				openedTemplatePath = openFileDialogue.FileName;
				InputTextbox.Text = File.ReadAllText(openedTemplatePath);
			}
		}


		private void SaveTemplate_Click(object sender, RoutedEventArgs e)
		{
			if (openedTemplatePath != string.Empty)
			{
				SaveToFile(InputTextbox.Text, openedTemplatePath);
			}
			else
			{
				SaveToFile(InputTextbox.Text);
			}
		}


		private void SaveTemplateAs_Click(object sender, RoutedEventArgs e)
		{
			SaveToFile(InputTextbox.Text);
		}


		private void SaveResult_Click(object sender, RoutedEventArgs e)
		{
			SaveToFile(OutputTextbox.Text);
		}


		private void SaveToFile(string text, string filepath)
		{
			File.WriteAllText(filepath, text);
		}


		private void SaveToFile(string text)
		{
			var saveTemplateDialog = new SaveFileDialog()
			{
				Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
			};

			if (saveTemplateDialog.ShowDialog() == true)
			{
				SaveToFile(text, saveTemplateDialog.FileName);
			}
		}


		private void ExitMenu_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
