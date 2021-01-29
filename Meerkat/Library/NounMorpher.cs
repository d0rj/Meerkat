﻿using System.Collections.Generic;
using LingvoNET;
using Meerkat.Library.Converters;
using Meerkat.Library.Exceptions;


namespace Meerkat.Library
{
	public sealed class NounMorpher : Caching<Noun>
	{
		private readonly Dictionary<string, IFormConverter<Noun>> formConverters 
			= new Dictionary<string, IFormConverter<Noun>>() 
			{
				{"about", new AboutConverter() },
				{"обо", new AboutConverter() },

				{"about+", new AboutPluralConverter() },
				{"обо+", new AboutPluralConverter() },
			};


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
