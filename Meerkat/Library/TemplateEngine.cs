using Meerkat.Library.Exceptions;

using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	public sealed class TemplateEngine
	{
		private readonly WordMorpher wordMorpher = new WordMorpher();


		public readonly string GeneralRegex = @"\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*(\w*)[\t ]*(\+){0,1}\]";

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

				if (Variables.ContainsKey(parsedVar))
				{
					var processedWord = wordMorpher.Morph(
						Variables[parsedVar], 
						form, 
						count ?? string.Empty);
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
