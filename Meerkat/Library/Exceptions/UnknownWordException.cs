using System;


namespace Meerkat.Library.Exceptions
{
	public sealed class UnknownWordException : Exception
	{
		public string Word { get; private set; }


		public UnknownWordException(string word)
			: base($"Unknown word '{word}' given.")
		{

		}
	}
}
