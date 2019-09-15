using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GPrep
{
	class Program
	{
		static Dictionary<string, Action> runners = new Dictionary<string, Action>();

		static void Main(string[] args)
		{
			Console.WriteLine($"Hello, world! {string.Join(", ", args)}");
			Init();

			if (args.Length == 0 || !runners.ContainsKey(args[0]))
			{
				Console.WriteLine("Invalid option.");
				return;
			}

			runners[args[0]]();
		}

		static void Init()
		{
			runners["is"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				Console.WriteLine(string.Join(", ", numbers) + "\n");

				var _is = new InsertionSort();
				_is.Sort(numbers);
				Console.WriteLine(string.Join(", ", numbers));

				for (var i = 0; i < numbers.Length - 1; i++)
				{
					Debug.Assert(numbers[i] <= numbers[i + 1]);
				}
			};

			runners["qs"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				Console.WriteLine(string.Join(", ", numbers) + "\n");

				var qs = new QuickSort();
				qs.Sort(numbers);
				Console.WriteLine(string.Join(", ", numbers));

				for (var i = 0; i < numbers.Length - 1; i++)
				{
					Debug.Assert(numbers[i] <= numbers[i + 1]);
				}
			};

			runners["s"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				//Console.WriteLine(string.Join(", ", numbers) + "\n");

				var s = new GPrep.Stack<int>();
				foreach (var n in numbers)
				{
					s.Push(n);
				}

				Debug.Assert(numbers.Length == s.Count);

				for (var i = numbers.Length - 1; i >= 0; i--)
				{
					var n = numbers[i];
					var sn = s.Pop();
					Debug.Assert(n == sn);
				}

				s.Pop();
				Debug.Assert(s.Count == 0);

				s.Push(2);
				Debug.Assert(s.Count == 1 && s.Pop() == 2);
			};
		}
	}
}