using System;
using Utilities;

namespace Sorting
{
	public class QuickSort : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("QuickSort");

			var numbers = Utils.GenInts(-10, 10, 20).ToArray();

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();

			// QSort(numbers, 0, numbers.Length - 1);
			RQSort(numbers, 0, numbers.Length - 1);

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
			if (start < length)
			{
				var partition = RPartition(numbers, start, length);
				RQSort(numbers, start, partition - 1);
				RQSort(numbers, partition + 1, length);
			}
		}

		private int RPartition(int[] numbers, int start, int length)
		{
			// single random qsort
			/*
			var i = Utils.GenInt(start, length);
			var p = numbers[i];
			numbers[i] = numbers[length];
			numbers[length] = p;
			return Partition(numbers, start, length);*/

			// multiple random qsort
			var potentials = new int[3];
			potentials[0] = Utils.GenInt(start, length);
			potentials[1] = Utils.GenInt(start, length);
			potentials[2] = Utils.GenInt(start, length);
			QSort(potentials, 0, 2);

			var i = potentials[1];
			var p = numbers[i];
			numbers[i] = numbers[length];
			numbers[length] = p;
			return Partition(numbers, start, length);
		}
	}
}