using System;
using Utils;

namespace Sorting
{
	public class QuickSort : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("QuickSort");

			var numbers = Utilities.GenInts(-10, 10, 20).ToArray();

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();

			QSort(numbers, 0, numbers.Length - 1);

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();
		}

		private void QSort(int[] numbers, int start, int length)
		{
			if (start < length)
			{
				var partition = Partition(numbers, start, length);
				QSort(numbers, start, partition - 1);
				QSort(numbers, partition + 1, length);
			}
		}

		private int Partition(int[] numbers, int start, int length)
		{
			var pivot = numbers[length];
			var partition = start;
			for (var lead = start; lead < length; lead++)
			{
				var n = numbers[lead];
				if (n <= pivot)
				{
					numbers[lead] = numbers[partition];
					numbers[partition] = n;
					partition++;
				}
			}

			numbers[length] = numbers[partition];
			numbers[partition] = pivot;
			return partition;
		}

		private void RQSort(int[] numbers, int start, int length)
		{
		}
	}
}