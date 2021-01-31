using LingvoNET;
using Meerkat.Library.Converters.Utils;


namespace Meerkat.Library.Converters
{
	public class AdjectiveConverter : IFormConverter<Adjective>
	{
		private readonly Case form;
		private readonly Gender gender;
		private readonly Number number = Number.Singular; // TODO: adj number support


		public AdjectiveConverter(string form, string modifier = "M")
		{
			this.form = CaseParser.Parse(form); // throws

			if (modifier.Contains("+"))
			{
				number = Number.Plural;
				modifier = modifier.Replace("+", "").Replace(" ", "").Replace("\t", "");
			}

			gender = GenderParser.Parse(modifier);
		}


		public string Convert(Adjective word)
		{
			return word[form, gender];
		}
	}
}
