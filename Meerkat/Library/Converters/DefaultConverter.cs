using LingvoNET;

using Meerkat.Library.Exceptions;

using System.Collections.Generic;


namespace Meerkat.Library.Converters
{
	public class DefaultConverter : IFormConverter<Noun>
	{
		protected readonly Case convertCase;
		protected readonly Number number;


		protected static Dictionary<string, Case> commands = 
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


		public DefaultConverter(string command, string number = "")
		{
			if (commands.ContainsKey(command))
				convertCase = commands[command];
			else
				throw new UnknownFormException(command, "for nouns");

			if (number == string.Empty)
				this.number = Number.Singular;
			else if (number == "+")
				this.number = Number.Plural;
			else
				throw new UnknownFormException(command + number, "for nouns");
		}


		public string Convert(Noun word)
		{
			return word[convertCase, number];
		}
	}
}
