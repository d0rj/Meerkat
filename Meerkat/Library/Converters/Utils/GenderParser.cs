using LingvoNET;


namespace Meerkat.Library.Converters.Utils
{
	internal static class GenderParser
	{
		public static Gender Parse(string input)
		{
			switch (input)
			{
				case "M": return Gender.M;
				case "F": return Gender.F;
				case "N": return Gender.N;
				case "P": return Gender.P;
				case "MA": return Gender.MA;
				case "FA": return Gender.FA;
				case "NA": return Gender.NA;
				case "PA": return Gender.PA;
				case "MAFA": return Gender.MAFA;
				default:
					return Gender.Undefined;
			}
		}
	}
}
