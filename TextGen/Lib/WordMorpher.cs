using System.Collections.Generic;
using LingvoNET;
using TextGen.Lib.Converters;


namespace TextGen.Lib
{
	public sealed class WordMorpher
	{
		private Dictionary<string, Noun> cachedNouns = new Dictionary<string, Noun>();
		private Dictionary<string, IFormConverter<Noun>> formConverters 
			= new Dictionary<string, IFormConverter<Noun>>() 
			{
				{"about", new AboutConverter() },
				{"о-об", new AboutConverter() },
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
