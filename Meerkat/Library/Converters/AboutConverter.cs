using LingvoNET;
using System.Collections.Generic;


namespace Meerkat.Library.Converters
{
	public class AboutConverter : IFormConverter<Noun>
	{
		private readonly List<char> vowels 
			= new List<char>() { 'у', 'ы', 'а', 'о', 'э', 'и' };


		public string Convert(Noun word)
		{
			string result = MorphWord(word);

			if (vowels.Contains(result[0]))
				result = "об " + result;
			else
				result = "о " + result;

			return result;
		}


		protected virtual string MorphWord(Noun word)
		{
			return word[Case.Locative];
		}
	}
}
