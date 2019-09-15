using System;
using System.Collections.Generic;

namespace GPrep
{
	public class Utility
	{
		private static Random rng = new Random();

		public static int[] GenInts(int lower, int upper, int size)
		{
			var ints = new int[size];
			for (var i = 0; i < size; i++)
			{
				ints[i] = rng.Next(lower, upper);
			}
			return ints;
		}

		public static int GenInt(int lower, int upper)
		{
			return rng.Next(lower, upper + 1);
		}
	}
}