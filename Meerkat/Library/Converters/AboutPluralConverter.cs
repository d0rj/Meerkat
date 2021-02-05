using LingvoNET;


namespace Meerkat.Library.Converters
{
	internal sealed class AboutPluralConverter : AboutConverter
	{
		protected override string MorphWord(Noun word)
		{
			return word[Case.Locative, Number.Plural];
		}
	}
}
