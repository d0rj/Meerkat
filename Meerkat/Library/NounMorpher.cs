using System.Collections.Generic;
using LingvoNET;
using Meerkat.Library.Converters;
using Meerkat.Library.Exceptions;
using Meerkat.Library.Interfaces;


namespace Meerkat.Library
{
	public sealed class NounMorpher : Caching<Noun>, IWordMorpher
	{
		private readonly Dictionary<string, IFormConverter<Noun>> formConverters;


		public NounMorpher()
		{
			var about = new AboutConverter();
			var aboutPlural = new AboutPluralConverter();

			formConverters = new Dictionary<string, IFormConverter<Noun>> {
				{ "about", about },
				{ "обо", about },

				{ "about+", aboutPlural },
				{ "обо+", aboutPlural },
			};
		}


		public string Morph(string word, string command, string number = "")
		{
			var noun = GetCached(word);

			if (formConverters.ContainsKey(command))
			{
				return formConverters[command + number].Convert(noun);
			}
			else
				return new DefaultConverter(command, number).Convert(noun);
		}


		public override void Cache(string key)
		{
			var noun = Nouns.FindOne(key);
			if (noun == null)
				throw new UnknownWordException(key);

			cached.Add(key, noun);
		}
	}
}
