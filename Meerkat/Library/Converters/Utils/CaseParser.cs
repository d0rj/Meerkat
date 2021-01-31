using LingvoNET;
using Meerkat.Library.Exceptions;
using System.Collections.Generic;


namespace Meerkat.Library.Converters.Utils
{
	public static class CaseParser
	{
		private static Dictionary<string, Case> commands =
			new Dictionary<string, Case>()
			{
				{"имен", Case.Nominative },
				{"родит", Case.Genitive },
				{"дате",  Case.Dative},
				{"вини", Case.Accusative },
				{"твор", Case.Instrumental },
				{"предл", Case.Locative },

				{string.Empty, Case.Nominative },

				{"именительный", Case.Nominative },
				{"родительный", Case.Genitive },
				{"дательный",  Case.Dative},
				{"винительный", Case.Accusative },
				{"творительный", Case.Instrumental },
				{"предложный", Case.Locative },
			};


		public static bool IsValid(string command) => commands.ContainsKey(command);


		public static Case Parse(string command)
		{
			if (commands.ContainsKey(command))
				return commands[command];

			throw new UnknownFormException(command);
		}
	}
}
