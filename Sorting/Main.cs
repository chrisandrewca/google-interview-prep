using System;
using System.Collections.Generic;
using Utilities;

namespace Sorting
{
	public class SortingMain
	{
		private static Dictionary<string, IRunnable> sorts = new Dictionary<string, IRunnable>()
		{
			{ "q", new QuickSort() },
			{ "m", new MergeSort() }
		};

		public static void Main(string[] args)
		{
			if (args.Length == 0)
			{
				Console.WriteLine("Choose a sort to run.");
				return;
			}

			var sort = args[0];
			if (!sorts.ContainsKey(sort))
			{
				Console.WriteLine("Invalid choice.");
				return;
			}

			sorts[sort].Run();
		}
	}
}