using System.Collections.Generic;
using LingvoNET;
using Meerkat.Library.Converters;


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
			};


		private Noun GetCachedNoun(string word)
		{
			if (!cachedNouns.ContainsKey(word))
				cachedNouns.Add(word, Nouns.FindOne(word));

			return cachedNouns[word];
		}


		public string Morph(string word, string command)
		{
			var noun = GetCachedNoun(word);

			if (formConverters.ContainsKey(command))
			{
				return formConverters[command].Convert(noun);
			}
			else
				return new DefaultConverter(command).Convert(noun);
		}
	}
}
