using LingvoNET;
using Meerkat.Library.Converters.Utils;


namespace Meerkat.Library.Converters
{
	internal class AdjectiveConverter : IFormConverter<Adjective>
	{
		private readonly Case form;
		private readonly Gender gender;


		public AdjectiveConverter(string form, string modifier = "M")
		{
			this.form = CaseParser.Parse(form); // throws

			if (modifier.Contains("+"))
			{
				gender = Gender.P;
			}
			else
			{
				gender = GenderParser.Parse(modifier);
			}
		}


		public string Convert(Adjective word)
		{
			return word[form, gender];
		}
	}
}
