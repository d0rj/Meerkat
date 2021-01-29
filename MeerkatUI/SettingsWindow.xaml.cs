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


		private void Window_Closing(object sender, CancelEventArgs e)
		{
			var mainWindow = Owner as MainWindow;

			mainWindow.Settings = Settings;

			mainWindow.Focus();
		}
	}
}
