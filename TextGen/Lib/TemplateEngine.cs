using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace TextGen.Lib
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
				var matches = match.Groups[0].Value;
				var parsedVars = match.Groups[1].Value;
				var forms = match.Groups[2].Value;

				if (Variables.ContainsKey(parsedVars))
				{
					var processedWord = wordMorpher.Morph(Variables[parsedVars], forms);
					result = result.Replace(matches, processedWord);
				}
				else
				{
					if (IgnoreUnknown)
						continue;
					else
						throw new Exception("No such variable: " + parsedVars);
				}
			}

			return result;
		}
	}
}
