using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private WordMorpher wordMorpher = new WordMorpher();


		public readonly string GeneralRegex = @"\[[\ ]*([A-Z_-]+)[\ ]*[|]{0,1}[\ ]*(\w*)[\ ]*\]";

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

				if (Variables.ContainsKey(parsedVar))
				{
					var processedWord = wordMorpher.Morph(Variables[parsedVar], form);
					result = result.Replace(matched, processedWord);
				}
				else
				{
					if (IgnoreUnknown)
						continue;
					else
						throw new Exception("No such variable: " + parsedVar);
				}
			}

			return result;
		}
	}
}
