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

			MSort(numbers);

			foreach (var n in numbers)
			{
				Console.Write($"{n}, ");
			}
			Console.WriteLine();
		}

		public void MSort(int[] array)
		{
			var temp = new int[array.Length]; // talk about space complexity O(n)
			MSort(array, temp, 0, array.Length - 1);
		}

		public void MSort(int[] array, int[] temp, int start, int end)
		{
			if (start < end)
			{
				var middle = (start + end) / 2;
				MSort(array, temp, start, middle);
				MSort(array, temp, middle + 1, end);
				Merge(array, temp, start, end);
			}
		}

		public void Merge(int[] array, int[] temp, int start, int end)
		{
			var left = start;
			var middle = (start + end) / 2;
			var right = middle + 1;
			var i = start;

			while (left <= middle && right <= end)
			{
				if (array[left] <= array[right])
				{
					temp[i] = array[left];
					left++;
				}
				else
				{
					temp[i] = array[right];
					right++;
				}
				i++;
			}

			for (; left <= middle; left++, i++)
			{
				temp[i] = array[left];
			}

			for (; right <= end; right++, i++)
			{
				temp[i] = array[right];
			}

			for (i = start; i <= end; i++)
			{
				array[i] = temp[i];
			}
		}
	}
}