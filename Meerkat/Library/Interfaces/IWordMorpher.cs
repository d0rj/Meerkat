


namespace Meerkat.Library.Interfaces
{
	public interface IWordMorpher
	{
		string Morph(string word, string command, string modifier);
		bool CanMorph(string word);
	}
}
