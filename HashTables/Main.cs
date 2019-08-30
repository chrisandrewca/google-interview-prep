using System;
using System.Collections.Generic;
using Utilities;

namespace HashTables
{
	public class HashTableMain
	{
		private static Dictionary<string, IRunnable> hashTables = new Dictionary<string, IRunnable>()
		{
			{ "h", new HashTable() }
		};

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Choose a hash table to run.");
				return;
			}

			var hashTable = args[0];
			if (!hashTables.ContainsKey(hashTable))
			{
				Console.WriteLine("Invalid choice.");
				return;
			}

			hashTables[hashTable].Run();
		}
	}
}