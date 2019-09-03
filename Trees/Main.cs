using System;
using System.Collections.Generic;
using Utilities;

namespace Trees
{
	public class TressMain
	{
		private static Dictionary<string, IRunnable> trees = new Dictionary<string, IRunnable>()
		{
			{ "b", new BinarySearchTreeInt() }
		};

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Choose a tree to run.");
				return;
			}

			var tree = args[0];
			if (!trees.ContainsKey(tree))
			{
				Console.WriteLine("Invalid choice.");
				return;
			}

			trees[tree].Run();
		}
	}
}