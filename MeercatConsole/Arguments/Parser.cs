using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MeercatConsole.Arguments
{
	static class Parser
	{
		public static Dictionary<string, string> Parse(string[] args)
		{
			var variables = new Dictionary<string, string>();

			args = args[1..];
			variables = args.Select(a => a.Split(new[] { '=' }, 2))
					 .GroupBy(a => a[0], a => a.Length == 2 ? a[1] : null)
					 .ToDictionary(g => g.Key, g => g.FirstOrDefault());

			return variables;
		}
	}
}
