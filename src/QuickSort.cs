using System;

namespace GPrep
{
	public class QuickSort
	{
		public void Sort(int[] arr)
		{
			Sort(arr, 0, arr.Length - 1);
		}

		private void Sort(int[] arr, int start, int end)
		{
			if (start < end)
			{
				var p = Partition(arr, start, end);
				Sort(arr, start, p - 1);
				Sort(arr, p + 1, end);
			}
		}

		private int Partition(int[] arr, int start, int end)
		{
			var pv = arr[end];
			var i = start;

			for (int j = start; j < end; j++)
			{
				var n = arr[j];
				if (n <= pv)
				{
					arr[j] = arr[i];
					arr[i] = n;
					i++;
				}
			}

			arr[end] = arr[i];
			arr[i] = pv;
			return i;
		}
	}
}