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

		public readonly string GeneralRegex = @"\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*([a-zа-я]*)[\t ]*(\+){0,1}[\t ]*([FMN]){0,1}\]";

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
			: this()
		{
			Variables = variables;
			IgnoreUnknown = ignoreUnknown;
		}


		public string ProcessTemplate(string template)
		{
			string result = template;

			foreach (Match match in Regex.Matches(template, GeneralRegex))
			{
				var finded = match.Value;
				result = result.Replace(finded, ProcessSingle(finded));
			}

			return result;
		}


		public string ProcessSingle(string template)
		{
			var match = Regex.Match(template, GeneralRegex);
			var (parsedVar, command, modifier) = SplitMatch(match);

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
							command,
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
					return processedWord;
				}
			}
			else 
			{
				if (IgnoreUnknown)
					return template;
				else
					throw new UnknownVariableException(parsedVar);
			}
		}


		private (string parsedVar, string command, string modifier) SplitMatch(Match match)
		{
			var parsedVar = match.Groups[1].Value;
			var command = match.Groups[2].Value;
			var modifier = (match.Groups[3].Value ?? string.Empty) 
							+ (match.Groups[4].Value ?? string.Empty);

			return (parsedVar, command, modifier);
		}
	}
}
