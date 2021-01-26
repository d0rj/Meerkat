using System;


namespace Meerkat.Library.Exceptions
{
	public sealed class UnknownFormException : Exception
	{
		public string Form { get; private set; }


		public UnknownFormException(string form, string additional = null)
			: base($"Unknown form '{form}' " + additional ?? "" + ".")
		{
			Form = form;
		}
	}
}
