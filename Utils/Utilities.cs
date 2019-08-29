using System;
using System.Collections.Generic;

namespace Utils
{
	public class Utilities
	{
		public static List<int> GenInts(int lower, int upper, int size)
		{
			var rng = new Random();
			var ints = new List<int>();
			for (var i = 0; i < size; i++)
			{
				ints.Add(rng.Next(lower, upper));
			}
			return ints;
		}
	}
}