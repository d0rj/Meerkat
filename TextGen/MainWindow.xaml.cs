using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LingvoNET;
using TextGen.Lib;


namespace TextGen
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			var variables = new Dictionary<string, string>() {
				{"X", "коммунизм" }
			};
			var engine = new TemplateEngine(variables, ignoreUnknown: true);
			var text = engine.ProcessTemplate("Скажи нет [X|дате]!");
			testText.Text = text;
		}
	}
}
