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
			// Insertion Sort O(n^2)
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

			// Quick Sort O(nlgn)
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

			// Stack with O(1) Push/Pop
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

				try
				{
					s.Pop();
				}
				catch (InvalidOperationException e)
				{
					Debug.Assert(e != null);
				}

				Debug.Assert(s.Count == 0);

				s.Push(2);
				Debug.Assert(s.Count == 1 && s.Pop() == 2);
			};

			// Queue with O(1) Enqueue/Dequeue
			runners["q"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				//Console.WriteLine(string.Join(", ", numbers) + "\n");

				var q = new GPrep.Queue<int>();
				foreach (var n in numbers)
				{
					q.Enqueue(n);
				}

				Debug.Assert(numbers.Length == q.Count);

				for (var i = 0; i < numbers.Length; i++)
				{
					var n = numbers[i];
					var sn = q.Dequeue();
					Debug.Assert(n == sn);
				}

				try
				{
					q.Dequeue();
				}
				catch (InvalidOperationException e)
				{
					Debug.Assert(e != null);
				}

				Debug.Assert(q.Count == 0);

				q.Enqueue(2);
				Debug.Assert(q.Count == 1 && q.Dequeue() == 2);
			};

			// Binary Search Tree, Search avg O(lgn)
			runners["bst"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				//Console.WriteLine(string.Join(", ", numbers) + "\n");

				var bst = new BinarySearchTree<int>();
				foreach (var n in numbers)
				{
					bst.Insert(n);
				}

				Debug.Assert(bst.Count == numbers.Length);

				var inOrderValues = bst.GetInOrder();
				Debug.Assert(inOrderValues.Length == numbers.Length);

				new QuickSort().Sort(numbers);
				for (var i = 0; i < numbers.Length; i++)
				{
					var bstv = inOrderValues[i];
					var n = numbers[i];
					Debug.Assert(bstv == n);

					var node = bst.Find(n);
					Debug.Assert(node != null && node.Value == n);

					bst.Remove(node.Value);
				}

				bst.Remove(0);
				Debug.Assert(bst.Count == 0);

				bst.Insert(2);
				Debug.Assert(bst.Count == 1 && bst.Find(2) != null && bst.Find(2).Value == 2);
			};

			// Red Black Tree, Search O(lgn)
			runners["rbt"] = () =>
			{
				var rbt = new RedBlackTree<int>();
			};
		}
	}
}