using MeercatConsole.Arguments;

using Meerkat.Library;

using System;
using System.Text;


namespace MeercatConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			// For 1251
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

			args = new string[] { "MeerkatConsole.exe", "read", "X=сова", "file='hi.txt'", "A=привет" };

			var variables = Parser.Parse(args).Variables;
			var engine = new TemplateEngine(variables);

			Console.WriteLine(engine.ProcessTemplate("Разговоры [X| about]."));
		}
	}
}
