using System.Collections.Generic;
using LingvoNET;
using Meerkat.Library.Converters;
using Meerkat.Library.Exceptions;


namespace Meerkat.Library
{
	public sealed class WordMorpher
	{
		private Dictionary<string, Noun> cachedNouns = new Dictionary<string, Noun>();
		private Dictionary<string, IFormConverter<Noun>> formConverters 
			= new Dictionary<string, IFormConverter<Noun>>() 
			{
				{"about", new AboutConverter() },
				{"обо", new AboutConverter() },

				{"about+", new AboutPluralConverter() },
				{"обо+", new AboutPluralConverter() },
			};


		private Noun GetCachedNoun(string word)
		{
			if (!cachedNouns.ContainsKey(word))
			{
				var noun = Nouns.FindOne(word);
				if (noun == null)
					throw new UnknownWordException(word);

				cachedNouns.Add(word, noun);
			}

			return cachedNouns[word];
		}


		public string Morph(string word, string command, string number = "")
		{
			var noun = GetCachedNoun(word);

			if (formConverters.ContainsKey(command))
			{
				return formConverters[command + number].Convert(noun);
			}
			else
				return new DefaultConverter(command, number).Convert(noun);
		}
	}
}
