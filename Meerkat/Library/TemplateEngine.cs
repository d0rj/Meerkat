using Meerkat.Library.Exceptions;
using Meerkat.Library.Extensions;
using Meerkat.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private readonly List<IWordMorpher> wordMorphers;
		private readonly string GeneralRegex = @"[\^]{0,2}\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*([a-zа-я]*)[\t ]*(\+){0,1}[\t ]*([FMN]){0,1}\]";

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


		/// <summary> Processes all units of the template in given string </summary>
		/// <param name="template"> Template to process </param>
		/// <exception cref="UnknownWordException"></exception>
		/// <exception cref="UnknownFormException"></exception>
		/// <exception cref="UnknownVariableException"> When <see cref="IgnoreUnknown"/> setted to <code><see langword="false"/></code> </exception>
		public string ProcessTemplate(string template)
		{
			string result = template;

			MatchCollection matchList = Regex.Matches(template, GeneralRegex);
			HashSet<string> allFinded = matchList
											.Cast<Match>()
											.Select(x => x.Value)
											.ToHashSet();

			foreach (var finded in allFinded)
			{
				result = result.Replace(finded, ProcessSingle(finded));
			}

			return result;
		}


		/// <summary> Processes one unit of the template based on internal settings </summary>
		/// <param name="template"> Unit in square brackets to process </param>
		/// <exception cref="UnknownWordException"> Throws when there are no workers 
		/// to change the word and the <seealso cref="IgnoreUnknown"/> flag is not set </exception>
		/// <exception cref="UnknownFormException"></exception>
		/// <exception cref="UnknownVariableException"> When <see cref="IgnoreUnknown"/> setted to <code><see langword="false"/></code> </exception>
		/// <returns> Processed string </returns>
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

				if (exception == null)
				{
					processedWord = HandleUppercase(template, processedWord);
					return processedWord;
				}
				else
					throw exception;
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


		private string HandleUppercase(string template, string processed)
		{
			if (template.StartsWith("^^"))
				return processed.ToUpper();
			else if (template.StartsWith("^"))
				return processed.FirstCharToUpper();

			return processed;
		} 
	}
}
