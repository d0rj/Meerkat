using Meerkat.Library;
using System.Collections.Generic;
using System.Windows;


namespace MeerkatUI
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			var variables = new Dictionary<string, string>()
			{
				{"X", "коммунизм" },
			};

			var engine = new TemplateEngine(variables);
			var text = engine.ProcessTemplate("Скажи нет [X|дательный]!");

			testText.Text = text;
		}
	}
}
