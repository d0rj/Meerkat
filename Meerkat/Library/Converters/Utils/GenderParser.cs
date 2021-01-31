using LingvoNET;


namespace Meerkat.Library.Converters.Utils
{
	public static class GenderParser
	{
		public static Gender Parse(string input)
		{
			if (input == "M")
				return Gender.M;
			if (input == "F")
				return Gender.F;
			if (input == "N")
				return Gender.N;

			return Gender.Undefined;
		}
	}
}
