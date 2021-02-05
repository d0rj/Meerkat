using Meerkat.Library.Extensions;
using Meerkat.Library.Interfaces;


namespace Meerkat.Library.Converters
{
	internal sealed class UppercaseHandler : IWordMorpher
	{
		public bool CanMorph(string word) => word.Length > 0;


		/// <summary> Processes word uppercase based on template start </summary>
		/// <param name="word"> Word to process </param>
		/// <param name="template"> Template, where word was processed </param>
		/// <returns> Uppercase or lowercase word </returns>
		public string Morph(string word, string template, string modifier = "")
		{
			if (template.StartsWith("^^"))
				return word.ToUpper();
			else if (template.StartsWith("^"))
				return word.FirstCharToUpper();

			return word;
		}
	}
}
