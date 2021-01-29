using MeerkatUI.Utils;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace MeerkatUI
{
	public partial class SettingsWindow : Window, INotifyPropertyChanged
	{
		private Settings settings = new Settings();
		public Settings Settings
		{
			get => settings;
			set
			{
				settings = value;
				OnPropertyChanged(nameof(Settings));
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		public SettingsWindow()
		{
			InitializeComponent();

			DataContext = Settings;
		}


		private void Apply()
		{
			var mainWindow = Owner as MainWindow;

			mainWindow.Settings = Settings;
		}


		private void Apply_Click(object sender, RoutedEventArgs e)
		{
			Apply();
		}


		private void Close_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}


		private void Ok_Click(object sender, RoutedEventArgs e)
		{
			Apply();
			Close();
		}
	}
}
