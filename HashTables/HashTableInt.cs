using System;
using Utilities;

namespace HashTables
{
	public class HashTableInt : IRunnable
	{
		private const int ACTIVE = 1;
		private const int INACTIVE = 0;

		private int[] storage = new int[6151];
		private int[] active = new int[6151];

		public void Run()
		{
			Console.WriteLine("HashTableInt");

			var keys = Utils.GenInts(-10, 10, 5);

			Console.WriteLine("Inserting...");
			foreach (var key in keys)
			{
				var value = Utils.GenInt(-1000, 1000);
				Console.WriteLine($"k: {key}, v: {value}");
				Insert(key, value);
			}
			Console.WriteLine();

			Console.WriteLine("Retrieving...");
			foreach (var key in keys)
			{
				var value = Get(key);
				Console.WriteLine($"k: {key}, v: {value}");
			}
			Console.WriteLine();

			Console.WriteLine("Deleting...");
			Insert(1, 1);
			Insert(1, 1);
			Delete(1);
			Insert(1, 2);
			Console.WriteLine(Get(1));
		}
/*
	public object this[int i]
	{
	    get { return InnerList[i]; }
	    set { InnerList[i] = value; }
	}
*/
		public void Insert(int key, int value)
		{
			var index = GetHash(key);

			/*
			 * TODO
			 * How to check if location is empty?
			 * How to handle collision?
			 * How to grow a nearly full or full storage space?
			 * Will index ever be out of bounds?
			 */
			if (active[index] == INACTIVE)
			{
				storage[index] = value;
				active[index] = ACTIVE;
			}
			else
			{
				Console.WriteLine("::Insert - Collision, skipping for now.");
			}
		}

		public int Get(int key)
		{
			var index = GetHash(key);

			/*
			 * TODO
			 * How to check if there's a valid value at location?
			 * Will index ever be out of bounds?
			 */
			var value = storage[index];
			return value;
		}

		public void Delete(int key)
		{
			var index = GetHash(key);

			/*
			 * TODO
			 * How to delete?
			 */
		 	active[index] = INACTIVE;
		}

		/*
		 * We hash because our keys come from a universe of all integers.
		 * Using a universe key directly means we'd need storage that is the size of the universe.
		 * Hashing produces a subset of the key space.
		 * The key space is then further reduced when mapped to the storage space.
		 */
		private int GetHash(int key)
		{
			var index = Math.Abs(key) % storage.Length;
			//Console.WriteLine($"::GetHash - storage.Length {storage.Length}");
			Console.WriteLine($"::GetHash - index {index}");
			return index;
		}
	}
}