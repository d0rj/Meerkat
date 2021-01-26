using System;


namespace Meerkat.Library.Exceptions
{
	public sealed class UnknownVariableException : Exception
	{
		public string Varname { get; private set; }


		public UnknownVariableException(string varname) 
			: base($"Unknown variable: '{varname}'.")
		{
			Varname = varname;
		}
	}
}
