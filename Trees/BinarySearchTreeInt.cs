using System;
using Utilities;

namespace Trees
{
	public class BinarySearchTreeInt : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("BinarySearchTreeInt");

/*
			var tree = new BinarySearchTreeInt(8);
			tree.Insert(15);
			tree.Insert(5);
			var ten = tree.Insert(10);

			Console.WriteLine($"Tree contains 10: {tree.Contains(10)}");
			Console.WriteLine($"Tree contains 5: {tree.Contains(5)}");
			Console.WriteLine($"Tree contains 15: {tree.Contains(15)}");
			Console.WriteLine($"Tree contains 8: {tree.Contains(8)}");

			tree.Insert(9);
			tree.PrintInOrder();
			Console.WriteLine();

			
			var s = tree.Successor(tree);
			Console.WriteLine(s.data);

			s = tree.Successor(ten);
			Console.WriteLine(s.data);
*/
			/*
			tree.PrintPreOrder();
			Console.WriteLine();
			tree.PrintPostOrder();
			Console.WriteLine();
			*/

			var tree = new BinarySearchTreeInt(2);
			tree.Insert(4);
			tree.Insert(3);
			tree.Insert(9);
			tree.Insert(7);
			var tt = tree.Insert(13);
			tree.Insert(6);
			tree.Insert(20);
			tree.Insert(17);
			var ft = tree.Insert(15);
			tree.Insert(18);

			tree.PrintInOrder();
			Console.WriteLine();

			var s = tree.Successor(ft);
			Console.WriteLine(s.data);

			s = tree.Successor(tt);
			Console.WriteLine(s.data);
		}

		private BinarySearchTreeInt left;
		private BinarySearchTreeInt right;
		private BinarySearchTreeInt predecessor;
		public int data;

		public BinarySearchTreeInt() {} // for Main.cs
		public BinarySearchTreeInt(int data)
		{
			this.data = data;
		}

		public BinarySearchTreeInt Insert(int value)
		{
			/*
			if (value <= data)
			{
				if (left == null)
				{
					left = new BinarySearchTreeInt(value); 
					left.predecessor = this;
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
					right.predecessor = this;
				}
				else
				{
					right.Insert(value);
				}
			}*/

			BinarySearchTreeInt lastNode = null;
			var node = this;
			while (node != null)
			{
				lastNode = node;
				if (value <= node.data)
				{
					node = node.left;
				}
				else
				{
					node = node.right;
				}
			}

			var newNode = new BinarySearchTreeInt(value);
			newNode.predecessor = lastNode;
			if (value <= lastNode.data)
			{
				lastNode.left = newNode;
			}
			else
			{
				lastNode.right = newNode;
			}

			return newNode;
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

		public BinarySearchTreeInt Minimum(BinarySearchTreeInt node)
		{
			while (node.left != null)
			{
				node = node.left;
			}
			return node;
		}

		public BinarySearchTreeInt Maximum(BinarySearchTreeInt node)
		{
			while (node.right != null)
			{
				node = node.right;
			}
			return node;
		}

		public BinarySearchTreeInt Successor(BinarySearchTreeInt node)
		{
			if (node.right != null)
			{
				return Minimum(node.right);
			}

			var p = node.predecessor;
			while (p != null && p.right == node)
			{
				node = p;
				p = p.predecessor;
			}

			return p;
		}
	}
}