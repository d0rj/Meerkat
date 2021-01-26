using LingvoNET;


namespace Meerkat.Library.Converters
{
	public sealed class AboutPluralConverter : AboutConverter
	{
		protected override string MorphWord(Noun word)
		{
			return word[Case.Locative, Number.Plural];
		}
	}
}
