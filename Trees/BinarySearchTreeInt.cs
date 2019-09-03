using System;
using Utilities;

namespace Trees
{
	public class BinarySearchTreeInt : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("BinarySearchTreeInt");

			var tree = new BinarySearchTreeInt(10);
			tree.Insert(5);
			tree.Insert(15);
			tree.Insert(8);

			Console.WriteLine($"Tree contains 5: {tree.Contains(5)}");
			Console.WriteLine($"Tree contains 15: {tree.Contains(15)}");
			Console.WriteLine($"Tree contains 8: {tree.Contains(8)}");

			tree.PrintInOrder();
			Console.WriteLine();
			tree.PrintPreOrder();
			Console.WriteLine();
			tree.PrintPostOrder();
			Console.WriteLine();
		}

		private BinarySearchTreeInt left;
		private BinarySearchTreeInt right;
		private int data;

		public BinarySearchTreeInt() {} // for Main.cs
		public BinarySearchTreeInt(int data)
		{
			this.data = data;
		}

		public void Insert(int value)
		{
			if (value <= data)
			{
				if (left == null)
				{
					left = new BinarySearchTreeInt(value);
				}
				else
				{
					left.Insert(value);
				}
			}
			else
			{
				if (right == null)
				{
					right = new BinarySearchTreeInt(value);
				}
				else
				{
					right.Insert(value);
				}
			}
		}

		public bool Contains(int value)
		{
			if (value == data)
			{
				return true;
			}

			if (value < data)
			{
				if (left != null)
				{
					return left.Contains(value);
				}
			}

			if (value > data)
			{
				if (right != null)
				{
					return right.Contains(value);
				}
			}

			return false;
		}

		public void PrintInOrder()
		{
			if (left != null)
			{
				left.PrintInOrder();
			}

			Console.Write($"{data},");

			if (right != null)
			{
				right.PrintInOrder();
			}
		}

		public void PrintPreOrder()
		{
			Console.Write($"{data},");

			if (left != null)
			{
				left.PrintInOrder();
			}

			if (right != null)
			{
				right.PrintInOrder();
			}
		}

		public void PrintPostOrder()
		{
			if (left != null)
			{
				left.PrintInOrder();
			}

			if (right != null)
			{
				right.PrintInOrder();
			}

			Console.Write($"{data},");
		}
	}
}