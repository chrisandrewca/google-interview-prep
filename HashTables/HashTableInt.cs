using System;
using LinkedLists;
using Utilities;

namespace HashTables
{
	public class HashTableInt : IRunnable
	{
		private const int ACTIVE = 1;
		private const int INACTIVE = 0;

		//private int[] storage = new int[193];
		//private int[] active = new int[193];
		private LinkedListInt[] storage = new LinkedListInt[12289];

		public void Run()
		{
			Console.WriteLine("HashTableInt");
/*
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
			Console.WriteLine(Get(1));*/

			for (var i = -6100; i <= 6100; i++)
			{
				Insert(i, i);
			}

			for (var i = -6100; i <= 6100; i++)
			{
				Console.WriteLine(Get(i));
			}
		}
/*
	public object this[int i]
	{
	    get { return InnerList[i]; }
	    set { InnerList[i] = value; }
	}
*/
		// TODO hash strings, maybe objs
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
			if (storage[index] == null)
			{
				storage[index] = new LinkedListInt();
			}

			var node = storage[index].Find(key);
			if (node == null)
			{
				storage[index].Add(new NodeInt()
				{
					Key = key,
					Value = value
				});
			}
			else
			{
				Console.WriteLine("::Insert - key already exists");
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
			if (storage[index] != null)
			{
				var node = storage[index].Find(key);
				if (node != null)
				{
					Console.WriteLine($"::Get - Length: {storage[index].Length}");
					return node.Value;
				}
				else
				{
					Console.WriteLine("::Get - key does not exist");
					throw new ArgumentException();
				}
			}
			throw new ArgumentException();
		}

		public void Delete(int key)
		{
			var index = GetHash(key);

			/*
			 * TODO
			 * How to delete?
			 */
		 	if (storage[index] != null)
			{
			 	storage[index].Delete(key);
	 		}
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

		// @Insert
		private void Grow()
		{
			/*
			if (insertedCount >= storage.Length / 2)
			{
				var nextPrime = primeList[++primeIndex];
				var newStorage = new int[nextPrime];
				for (var i = 0; i < storage.Length; i++)
				{
					var list = storage[i];
					if (list != null)
					{
						var key = list.Head.key;
						var hash = GetHash(key);
						newStorage[hash] = list;
					}
				}

				storage = newStorage;
			}
			*/
		}
	}
}