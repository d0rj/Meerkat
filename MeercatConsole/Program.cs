using MeercatConsole.Arguments;

using System;


namespace MeercatConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			args = new string[] { "MeerkatConsole.exe", "read", "X=hello", "file='hi.txt'", "A=привет" };

			var result = Parser.Parse(args).Variables;
			foreach (var key in result.Keys)
				Console.WriteLine(key + "=" + result[key]);
		}
	}
}
