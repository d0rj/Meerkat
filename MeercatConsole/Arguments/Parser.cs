using System.Collections.Generic;
using System.Linq;


namespace MeercatConsole.Arguments
{
	static class Parser
	{
		public class ParsingResult
		{
			public Dictionary<string, string> Variables { get; set; }

			public Dictionary<string, string> Commands { get; set; }

			public List<string> Flags { get; set; }


			private Dictionary<string, string> keyValues;


			protected internal ParsingResult(Dictionary<string, string> keyValues)
			{
				this.keyValues = keyValues;
				Variables = new Dictionary<string, string>();
				Commands = new Dictionary<string, string>();
				Flags = new List<string>();

				foreach (var pair in this.keyValues)
				{
					if (char.ToLower(pair.Key[0]) == pair.Key[0] && pair.Value == null)
						Flags.Add(pair.Key);
					else if (char.ToLower(pair.Key[0]) != pair.Key[0])
						Variables.Add(pair.Key, pair.Value);
					else
						Commands.Add(pair.Key, pair.Value);
				}
			}


			public static explicit operator Dictionary<string, string>(ParsingResult result)
			{
				return result.keyValues;
			}
		}


		public static ParsingResult Parse(string[] args)
		{
			var variables = new Dictionary<string, string>();

			args = args[1..];
			variables = args.Select(a => a.Split(new[] { '=' }, 2))
					 .GroupBy(a => a[0], a => a.Length == 2 ? a[1] : null)
					 .ToDictionary(g => g.Key, g => g.FirstOrDefault());

			return new ParsingResult(variables ?? new Dictionary<string, string>());
		}
	}
}
