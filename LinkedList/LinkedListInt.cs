using System;
using System.Linq;
using Utilities;

namespace LinkedLists
{
	public class NodeInt
	{
		public NodeInt Prev { get; set; }
		public NodeInt Next { get; set; }
		public int Key { get; set; }
	}

	public class LinkedListInt : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("LinkedList");
			var keys = Utils.GenInts(-10, 10, 10);

			foreach	(var k in keys)
			{
				var node = new NodeInt() { Key = k };
				Add(node);
			}

			foreach (var k in keys)
			{
				var node = Find(k);
				Console.Write($"{node.Key}, ");
			}
			Console.WriteLine();

			foreach (var k in keys.OrderByDescending(i => i))
			{
				var node = Find(k);
				Console.Write($"{node.Key}, ");
			}
			Console.WriteLine();

			foreach (var k in keys)
			{
				Delete(k);
			}

			foreach (var k in keys)
			{
				var node = Find(k);
				Console.Write($"{(node != null ? $"{node.Key}" : $"del-{k}")}, ");
			}
			Console.WriteLine();
		}

		private NodeInt head;

		public LinkedListInt()
		{
			head = null;
		}

		public void Add(NodeInt node)
		{
			node.Next = head;
			if (head != null)
			{
				head.Prev = node;
			}
			head = node;
		}

		public NodeInt Find(int key)
		{
			var node = head;
			while (node != null && node.Key != key)
			{
				node = node.Next;
			}
			return node;
		}

		public void Delete(int key)
		{
			var node = Find(key);

			if (node != null)
			{
				if (node.Prev != null)
				{
					node.Prev.Next = node.Next;
				}
				else
				{
					head = node.Next;
				}

				if (node.Next != null)
				{
					node.Next.Prev = node.Prev;
				}
			}
		}
	}
}