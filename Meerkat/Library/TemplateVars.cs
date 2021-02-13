using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Meerkat.Library
{
	public class TemplateVars
	{
		public string VariableName { get; set; }
		public string Command { get; set; }
		public string Modifier { get; set; }


		public void Deconstruct(out string varName, out string command, out string modifier)
		{
			varName = VariableName;
			command = Command;
			modifier = Modifier;
		}
	}
}
