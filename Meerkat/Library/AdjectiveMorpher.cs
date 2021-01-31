using LingvoNET;

using Meerkat.Library.Converters;
using Meerkat.Library.Exceptions;
using Meerkat.Library.Interfaces;


namespace Meerkat.Library
{
	public sealed class AdjectiveMorpher : Caching<Adjective>, IWordMorpher
	{
		public string Morph(string word, string form, string modifier = "M")
		{
			var adj = GetCached(word);

			AdjectiveConverter converter;
			if (modifier == string.Empty)
				converter = new AdjectiveConverter(form);
			else
				converter = new AdjectiveConverter(form, modifier);

			return converter.Convert(adj);
		}


		public override void Cache(string key)
		{
			var adj = Adjectives.FindOne(key);
			if (adj == null)
				throw new UnknownWordException(key);

			cached.Add(key, adj);
		}
	}
}
