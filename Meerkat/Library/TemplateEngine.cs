﻿using Meerkat.Library.Converters;
using Meerkat.Library.Exceptions;
using Meerkat.Library.Interfaces;

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private readonly List<IWordMorpher> wordMorphers;
		private readonly UppercaseHandler uppercaseHandler = new UppercaseHandler();

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

			MatchCollection matchList = TemplateParser.ParseAll(template);
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
			var match = TemplateParser.ParseSingle(template);
			var (parsedVar, command, modifier) = TemplateParser.Split(match);

			if (Variables.ContainsKey(parsedVar))
			{
				var processedWord = string.Empty;
				string word = Variables[parsedVar];

				foreach (var morpher in wordMorphers)
				{
					if (morpher.CanMorph(word))
					{
						processedWord = morpher.Morph(word, command, modifier);
						break;
					}
				}

				if (uppercaseHandler.CanMorph(processedWord))
				{
					processedWord = uppercaseHandler.Morph(processedWord, template);
					return processedWord;
				}
				else
					throw new UnknownWordException(word);
			}
			else 
			{
				if (IgnoreUnknown)
					return template;
				else
					throw new UnknownVariableException(parsedVar);
			}
		}
	}
}
