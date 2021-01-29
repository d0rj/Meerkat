using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace MeerkatUI
{
	public partial class SettingsWindow : Window, INotifyPropertyChanged
	{
		private int editorFontSize = 12;
		public int EditorFontSize
		{
			get => editorFontSize;
			set
			{
				editorFontSize = value;
				OnPropertyChanged("EditorFontSize");
			}
		}


		public SettingsWindow()
		{
			InitializeComponent();

			DataContext = this;
		}


		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		private void Window_Closing(object sender, CancelEventArgs e)
		{
			var mainWindow = Owner as MainWindow;

			mainWindow.EditorFontSize = EditorFontSize;

			mainWindow.Focus();
		}
	}
}
