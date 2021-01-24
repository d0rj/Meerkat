using LingvoNET;
using System.Collections.Generic;


namespace Meerkat.Library.Converters
{
	public sealed class AboutConverter : IFormConverter<Noun>
	{
		private readonly List<char> vowels 
			= new List<char>() { 'ё', 'у', 'е', 'ы', 'а', 'о', 'э', 'я', 'и', 'ю' };


		public string Convert(Noun word)
		{
			string result = word[Case.Locative];

			if (vowels.Contains(result[0]))
				result = "об " + result;
			else
				result = "о " + result;

			return result;
		}
	}
}
