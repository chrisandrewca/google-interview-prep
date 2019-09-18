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
				var numbers = Utility.GenInts(-100, 100, 100);
				//Console.WriteLine(string.Join(", ", numbers) + "\n");

				var rbt = new RedBlackTree<int>();
				foreach (var n in numbers)
				{
					rbt.Insert(n);
				}

				Debug.Assert(rbt.Count == numbers.Length);

				var inOrderValues = rbt.GetInOrder();
				Debug.Assert(inOrderValues.Length == numbers.Length);

				new QuickSort().Sort(numbers);
				for (var i = 0; i < numbers.Length; i++)
				{
					var rbtv = inOrderValues[i];
					var n = numbers[i];
					Debug.Assert(rbtv == n);

					var node = rbt.Find(n);
					Debug.Assert(node != null && node.Value == n);

					rbt.Remove(node.Value);
				}

				rbt.Remove(0);
				Debug.Assert(rbt.Count == 0);

				rbt.Insert(2);
				Debug.Assert(rbt.Count == 1 && rbt.Find(2) != null && rbt.Find(2).Value == 2);
			};

			// Graph, adjacency list
			runners["g"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				//Console.WriteLine(string.Join(", ", numbers) + "\n");

				var graph = new Graph<int>();
				var _nodes = new List<GraphNode<int>>();
				foreach (var n in numbers)
				{
					Console.WriteLine("Add");
					var node = graph.AddNode(n, n);
					_nodes.Add(node);
				}

				Debug.Assert(graph.Nodes.Count == numbers.Length);

				foreach (var node in _nodes)
				{
					// Utility.GenInt impl is weird

					// single edge, multi edge, self reference, ...
					var a = _nodes[Utility.GenInt(0, _nodes.Count - 2)];
					var b = _nodes[Utility.GenInt(0, _nodes.Count - 2)];

					var cost = Utility.GenInt(0, 99);
					Console.WriteLine("AddUndirectedEdge");
					graph.AddUndirectedEdge(a, b, cost);

					Console.WriteLine("a.Neighbors.Find(b)");
					Debug.Assert(a.Neighbors.Find(b).Value == b);
					Console.WriteLine("b.Neighbors.Find(a)");
					Debug.Assert(b.Neighbors.Find(a).Value == a);

					Console.WriteLine("a.Costs.Find(cost)");
					Debug.Assert(a.Costs.Find(cost).Value == cost);
					Console.WriteLine("b.Costs.Find(cost)");
					Debug.Assert(b.Costs.Find(cost).Value == cost);
				}

				foreach (var node in _nodes)
				{
					Console.WriteLine("RemoveNode");
					graph.RemoveNode(node);
				}

				graph.RemoveNode(_nodes[0]);
				graph.RemoveNode(null);
				Debug.Assert(graph.Nodes.Count == 0);
			};

			// Depth First Search, O(V + E), when used vs DFS?
			runners["ga"] = () =>
			{
				Console.WriteLine("DFS");
				var graph = new Graph<int>();
				var _1 = graph.AddNode(0, 1);
				var _2 = graph.AddNode(1, 2);
				var _3 = graph.AddNode(2, 3);

				graph.AddUndirectedEdge(_1, _2, 1);
				graph.AddUndirectedEdge(_2, _3, 1);
				graph.AddUndirectedEdge(_3, _1, 1);

				var nodes = graph.DFS(_1);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				nodes = graph.DFS(_2);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				nodes = graph.DFS(_3);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				var _4 = graph.AddNode(3, 4);
				var _5 = graph.AddNode(4, 5);
				var _6 = graph.AddNode(5, 6);

				graph.AddDirectedEdge(_4, _5, 1);
				graph.AddDirectedEdge(_5, _6, 1);
				graph.AddDirectedEdge(_6, _4, 1);

				var components = graph.FindComponents();
				Debug.Assert(components.Count == 2);

				Console.WriteLine("Components");
				PrintList<int>(components.Head.Value);
				PrintList<int>(components.Tail.Value);

				Console.WriteLine("BFS");
				nodes = graph.BFS(_1);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				nodes = graph.BFS(_4);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				Console.WriteLine("DFS 2");
				nodes = _DFS<int>(_1);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);

				Console.WriteLine("BFS 2");
				nodes = _DFS<int>(_4);
				Debug.Assert(nodes.Count == 3);
				PrintList<int>(nodes);
			};

			// Hash Table
			runners["h"] = () =>
			{
				var table = new HashTable<int, GraphNode<double>>();
				for (var i = -10; i <= 10; i++)
				{
					table.Add(i, new GraphNode<double>(i, i + 0.3));
				}

				Debug.Assert(table.Count == 21);

				for (var i = 10; i >= -10; i--)
				{
					var node = table.Find(i);
					Debug.Assert((i + 0.3) == node.Value);
					Console.WriteLine($"K: {i} V: {node.Value}");
				}
			};

			// Trie Tree
			runners["t"] = () =>
			{
				var trie = new TrieTree();
				trie.Insert("Tomato");
				trie.Insert("Potato");
				trie.Insert("Tom");
				trie.Insert("Tomboy");
				trie.Insert("Tommy");
				trie.Insert("Tomato2");

				Debug.Assert(trie.Find("Tomato"));
				Debug.Assert(trie.Find("Potato"));
				Debug.Assert(trie.Find("Tom"));
				Debug.Assert(trie.Find("Tomboy"));
				Debug.Assert(trie.Find("Tommy"));
				Debug.Assert(trie.Find("Tomato2"));
				Debug.Assert(trie.Find("To") == false);
				Debug.Assert(trie.Find("Tomat") == false);
				Debug.Assert(trie.Find("") == false);
			};

			// Merge Sort O(nlgn)
			runners["m"] = () =>
			{
				var numbers = Utility.GenInts(-100, 100, 100);
				Console.WriteLine(string.Join(", ", numbers) + "\n");

				var ms = new MergeSort();
				ms.Sort(numbers);
				Console.WriteLine(string.Join(", ", numbers));

				for (var i = 0; i < numbers.Length - 1; i++)
				{
					Debug.Assert(numbers[i] <= numbers[i + 1]);
				}
			};
		}

		static void PrintList<T>(LinkedList<GraphNode<T>> list)
		{
			var node = list.Head;
			while (node != null)
			{
				Console.Write($"{node.Value.Value}, ");
				node = node.Next;
			}
			Console.WriteLine();
		}

		static LinkedList<GraphNode<T>> _DFS<T>(GraphNode<T> start)
		{
			var visited = new HashTable<int, GraphNode<T>>();
			__DFS(start, visited);
			return visited.GetValues();
		}

		static void __DFS<T>(GraphNode<T> start, HashTable<int, GraphNode<T>> visited)
		{
			while (start != null && visited.Find(start.Key) == null)
			{
				visited.Add(start.Key, start);
				var neighbor = start.Neighbors.Head;
				while (neighbor != null)
				{
					__DFS(neighbor.Value, visited);
					neighbor = neighbor.Next;
				}
			}
		}

		static LinkedList<GraphNode<T>> _BFS<T>(LinkedList<GraphNode<T>> nodes)
		{
			var visited = new HashTable<int, GraphNode<T>>();
			var searching = new Queue<GraphNode<T>>();

			searching.Enqueue(nodes.Head.Value);

			while (searching.Count > 0)
			{
				var node = searching.Dequeue();
				var neighbor = node.Neighbors.Head;
				while (neighbor != null)
				{
					if (visited.Find(neighbor.Value.Key) == null)
					{
						searching.Enqueue(neighbor.Value);
					}
					neighbor = neighbor.Next;
				}
				visited.Add(node.Key, node);
			}
			return visited.GetValues();
		}
	}
}