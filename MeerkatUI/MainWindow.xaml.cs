using Meerkat.Library;
using MeerkatUI.Utils;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace MeerkatUI
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private readonly TemplateEngine engine;


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		private ObservableCollection<Variable> variables = new ObservableCollection<Variable>()
		{
			new Variable("Y", "утка"),
		};
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


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			foreach (var variable in Variables)
				engine.Variables.Add(variable.Name, variable.Value);

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
	}
}
