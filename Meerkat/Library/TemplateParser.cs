using System.Text.RegularExpressions;


namespace Meerkat.Library
{
	static class TemplateParser
	{
		private static readonly string GeneralRegex = 
		@"[\^]{0,2}\[[\t ]*([A-Z_-]+)[\t ]*[|]{0,1}[\t ]*([a-zа-я]*)[\t ]*(\+){0,1}[\t ]*([FMN]|FA|MA|NA|PA|MAFA){0,1}\]";


		public static Match ParseSingle(string template)
		{
			return Regex.Match(template, GeneralRegex);
		}


		public static MatchCollection ParseAll(string template)
		{
			return Regex.Matches(template, GeneralRegex);
		}


		public static TemplateVars Split(Match match)
		{
			return new TemplateVars
			{
				VariableName = match.Groups[1].Value,
				Command = match.Groups[2].Value,
				Modifier = (match.Groups[3].Value ?? string.Empty)
							+ (match.Groups[4].Value ?? string.Empty)
			};
		}
	}
}
