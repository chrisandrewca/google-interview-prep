using System;
using Utilities;

namespace Sorting
{
	public class MergeSort : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("MergeSort");

			var numbers = Utils.GenInts(-10, 10, 20).ToArray();

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();

			MSort(numbers, 0, numbers.Length - 1);

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();
		}

		public MSort(int[] numbers, int start, int end)
		{
			if (start < end)
			{
				var partition = (start + end) / 2;
				MSort(numbers, start, partition - 1);
				MSort(numbers, partition - 1, end);
				Merge(numbers, start, partition, end);
			}
		}

		public void Merge(int[] numbers, int start, int partition, int end)
		{
			var lowHalf = new int[partition + 1];
			var highHalf = new int[end - partition + 1];

			var k = start;
			for (var i = 0; k <= partition; k++, i++)
			{
				lowHalf[i] = numbers[k];
			}

			for (var i = 0; k <= end; k++, i++)
			{
				highHalf[i] = numbers[k];
			}

			if (start >= end)
			{
			}
		}
	}
}