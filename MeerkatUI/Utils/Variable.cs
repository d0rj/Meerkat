﻿


namespace MeerkatUI.Utils
{
	public sealed class Variable
	{
		public string Name { get; set; }
		public string Value { get; set; }


		public Variable(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}
}
