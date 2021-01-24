using Meerkat.Library;
using System.Collections.Generic;
using System.Windows;


namespace MeerkatUI
{
	public partial class MainWindow : Window
	{
		private TemplateEngine engine;


		public MainWindow()
		{
			InitializeComponent();

			var variables = new Dictionary<string, string>()
			{
				{"X", "коммунизм" },
			};

			engine = new TemplateEngine(variables);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var text = engine.ProcessTemplate(InputTextbox.Text);

			OutputTextbox.Text = text;
		}
	}
}
