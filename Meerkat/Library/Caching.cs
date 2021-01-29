using System.Collections.Generic;


namespace Meerkat.Library
{
	public abstract class Caching<T>
	{
		protected Dictionary<string, T> cached = new Dictionary<string, T>();


		public abstract void Cache(string key);


		public T GetCached(string key)
		{
			if (!cached.ContainsKey(key))
				Cache(key);

			return cached[key];
		}
	}
}
