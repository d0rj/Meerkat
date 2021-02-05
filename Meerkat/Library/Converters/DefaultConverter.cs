using LingvoNET;

using Meerkat.Library.Converters.Utils;
using Meerkat.Library.Exceptions;


namespace Meerkat.Library.Converters
{
	internal class DefaultConverter : IFormConverter<Noun>
	{
		protected readonly Case convertCase;
		protected readonly Number number;


		public DefaultConverter(string command, string number = "")
		{
			if (CaseParser.IsValid(command))
				convertCase = CaseParser.Parse(command);
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
