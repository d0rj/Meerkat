﻿using Meerkat.Library;
using MeerkatUI.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


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


		private ObservableDictionary<string, string> variables;
		public ObservableDictionary<string, string> Variables 
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

			Variables = new ObservableDictionary<string, string>()
			{
				{"X", "коммунизм" },
			};

			engine = new TemplateEngine(Variables.ToDictionary());

			DataContext = this;
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
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
	}
}
