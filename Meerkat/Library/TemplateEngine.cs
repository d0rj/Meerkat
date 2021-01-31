﻿using Meerkat.Library.Exceptions;

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private readonly NounMorpher nounMorpher = new NounMorpher();
		private readonly AdjectiveMorpher adjectiveMorpher = new AdjectiveMorpher();


		public readonly string GeneralRegex = @"\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*([a-zа-я]*)[\t ]*(\+){0,1}([FMN]){0,1}\]";

		public bool IgnoreUnknown { get; set; }
		public Dictionary<string, string> Variables { get; set; }


		public TemplateEngine()
		{
			Variables = new Dictionary<string, string>();
		}


		public TemplateEngine(Dictionary<string, string> variables, bool ignoreUnknown = false)
		{
			Variables = variables;
			IgnoreUnknown = ignoreUnknown;
		}


		public string ProcessTemplate(string template)
		{
			string result = template;

			foreach (Match match in Regex.Matches(template, GeneralRegex))
			{
				var matched = match.Groups[0].Value;
				var parsedVar = match.Groups[1].Value;
				var form = match.Groups[2].Value;
				var count = match.Groups[3].Value;
				var gender = match.Groups[4].Value;

				if (Variables.ContainsKey(parsedVar))
				{
					string processedWord = "";
					try
					{
						processedWord = nounMorpher.Morph(
							Variables[parsedVar], 
							form, 
							count ?? string.Empty);
					}
					catch (UnknownWordException _)
					{
						processedWord = adjectiveMorpher.Morph(
							Variables[parsedVar],
							form,
							(count ?? string.Empty) + (gender ?? string.Empty));
					}

					result = result.Replace(matched, processedWord);
				}
				else
				{
					if (IgnoreUnknown)
						continue;
					else
						throw new UnknownVariableException(parsedVar);
				}
			}

			return result;
		}
	}
}
