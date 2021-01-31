using Meerkat.Library.Exceptions;
using Meerkat.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private readonly List<IWordMorpher> wordMorphers;


		public readonly string GeneralRegex = @"\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*([a-zа-я]*)[\t ]*(\+){0,1}([FMN]){0,1}\]";

		public bool IgnoreUnknown { get; set; }
		public Dictionary<string, string> Variables { get; set; }


		public TemplateEngine()
		{
			Variables = new Dictionary<string, string>();

			wordMorphers = new List<IWordMorpher> 
			{
				new NounMorpher(),
				new AdjectiveMorpher()
			};
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

				var modifier = (count ?? string.Empty) + (gender ?? string.Empty);

				if (Variables.ContainsKey(parsedVar))
				{
					string processedWord = "";
					Exception exception = null;
					foreach (var morpher in wordMorphers)
					{
						try
						{
							processedWord = morpher.Morph(
								Variables[parsedVar],
								form,
								modifier);
							exception = null;

							break;
						}
						catch (UnknownWordException e)
						{
							exception = e;
						}
					}

					if (exception != null)
						throw exception;
					else
					{
						result = result.Replace(matched, processedWord);
					}
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
