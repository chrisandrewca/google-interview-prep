using System;

namespace GPrep
{
	public class MergeSort
	{
		public void Sort(int[] arr)
		{
			var temp = new int[arr.Length];
			Sort(arr, temp, 0, arr.Length - 1);
		}

		public void Sort(int[] arr, int[] temp, int start, int end)
		{
			if (start < end)
			{
				var p = (start + end) / 2;
				Sort(arr, temp, start, p);
				Sort(arr, temp, p + 1, end);
				Merge(arr, temp, start, end);
			}
		}

		public void Merge(int[] arr, int[] temp, int start, int end)
		{
			var left = start;
			var middle = (start + end) / 2;
			var right = middle + 1;
			var i = start;

			while (left <= middle && right <= end)
			{
				if (arr[left] <= arr[right])
				{
					temp[i] = arr[left];
					left++;
				}
				else
				{
					temp[i] = arr[right];
					right++;
				}
				i++;
			}

			for (; left <= middle; left++, i++)
			{
				temp[i] = arr[left];
			}

			for (; right <= end; right++, i++)
			{
				temp[i] = arr[right];
			}

			for (i = start; i <= end; i++)
			{
				arr[i] = temp[i];
			}
		}
	}
}