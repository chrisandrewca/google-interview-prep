using System;
using System.Collections.Generic;

namespace Utilities
{
	public class Utils
	{
		private static Random rng = new Random();

		public static List<int> GenInts(int lower, int upper, int size)
		{
			var ints = new List<int>();
			for (var i = 0; i < size; i++)
			{
				ints.Add(rng.Next(lower, upper));
			}
			return ints;
		}

		public static int GenInt(int lower, int upper)
		{
			return rng.Next(lower, upper + 1);
		}
	}
}