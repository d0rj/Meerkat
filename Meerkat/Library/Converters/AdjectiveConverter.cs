using LingvoNET;
using Meerkat.Library.Converters.Utils;


namespace Meerkat.Library.Converters
{
	public class AdjectiveConverter : IFormConverter<Adjective>
	{
		private readonly Case form;
		private readonly Gender gender;


		public AdjectiveConverter(string form, string modifier = "M")
		{
			this.form = CaseParser.Parse(form); // throws

			if (modifier.Contains("+"))
			{
				modifier = modifier.Replace("+", "");
			}

			gender = GenderParser.Parse(modifier);
		}


		public string Convert(Adjective word)
		{
			return word[form, gender];
		}
	}
}
