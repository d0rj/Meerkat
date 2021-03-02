using MeercatConsole.Arguments;

using System;
using System.Linq;


namespace MeercatConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			args = new string[] { "MeerkatConsole.exe", "read", "file='hi.txt'", "A=привет" };

			var result = Parser.Parse(args);
			foreach (var key in result.Keys)
				Console.WriteLine(key + "=" + result[key]);
		}
	}
}
