using System;


namespace MeerkatUI.Utils
{
	public sealed class Settings : ICloneable
	{
		public int EditorFontSize { get; set; } = 12;
		public int OutputFontSize { get; set; } = 12;


		/// <summary> Deep copy </summary>
		public object Clone()
		{
			return new Settings
			{
				EditorFontSize = EditorFontSize,
				OutputFontSize = OutputFontSize,
			};
		}


		/// <summary> Deep copy but with same type </summary>
		public Settings Copy() => (Settings)Clone();
	}
}
