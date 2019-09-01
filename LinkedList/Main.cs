using System;
using System.Collections.Generic;
using Utilities;

namespace LinkedLists
{
	public class HashTableMain
	{
		private static Dictionary<string, IRunnable> linkedLists = new Dictionary<string, IRunnable>()
		{
			{ "i", new LinkedListInt() }
		};

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Choose a linked list to run.");
				return;
			}

			var linkedList = args[0];
			if (!linkedLists.ContainsKey(linkedList))
			{
				Console.WriteLine("Invalid choice.");
				return;
			}

			linkedLists[linkedList].Run();
		}
	}
}