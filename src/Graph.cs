using System;
using System.Collections.Generic;

namespace GPrep
{
	public class GraphNode<T> : IComparable<GraphNode<T>>
	{
		public LinkedList<int> Costs { get; set; }
		// what if tree tho?
		public LinkedList<GraphNode<T>> Neighbors { get; set; }

		public int Key { get; private set; }
		public T Value { get; set; }

		public GraphNode(int key, T value)
		{
			Costs = new LinkedList<int>();
			Neighbors = new LinkedList<GraphNode<T>>();
			Key = key;
			Value = value;
		}

		public int CompareTo(GraphNode<T> other)
		{
			return Comparer<int>.Default.Compare(Key, other.Key);
		}

		public override bool Equals(object other)
		{
			var o = (GraphNode<T>)other;
			return Key.Equals(o.Key);
		}

		public override int GetHashCode()
		{
			return Key.GetHashCode();
		}
	}

	public class Graph<T>
	{
		public LinkedList<GraphNode<T>> Nodes { get; set; }

		public Graph()
		{
			Nodes = new LinkedList<GraphNode<T>>();
		}

		public GraphNode<T> AddNode(int key, T value)
		{
			var node = new GraphNode<T>(key, value);
			Nodes.InsertBack(node);
			return node;
		}

		public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
		{
			from.Neighbors.InsertBack(to);
			from.Costs.InsertBack(cost);
		}

		public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
		{
			from.Neighbors.InsertBack(to);
			from.Costs.InsertBack(cost);

			to.Neighbors.InsertBack(from);
			to.Costs.InsertBack(cost);
		}

		public LinkedList<GraphNode<T>> BFS(GraphNode<T> start)
		{
			var visited = new LinkedList<GraphNode<T>>();
			BFS(start, visited);
			return visited;
		}

		public LinkedList<GraphNode<T>> DFS(GraphNode<T> start)
		{
			// TODO make HashTable?
			var visited = new LinkedList<GraphNode<T>>();
			DFS(start, visited);
			return visited;
		}

		public LinkedList<LinkedList<GraphNode<T>>> FindComponents()
		{
			var components = new LinkedList<LinkedList<GraphNode<T>>>();
			var component = new LinkedList<GraphNode<T>>();
			var visited = new LinkedList<GraphNode<T>>();

			var node = Nodes.Head;
			while (node != null)
			{
				if (visited.Find(node.Value) == null)
				{
					DFS(node.Value, component);

					var cnode = component.Head;
					while (cnode != null)
					{
						visited.InsertBack(cnode.Value);
						cnode = cnode.Next;
					}

					components.InsertBack(component);
					component = new LinkedList<GraphNode<T>>();
				}
				node = node.Next;
			}

			return components;
		}

		public void RemoveNode(GraphNode<T> node)
		{
			Nodes.Remove(node);

			var _node = Nodes.Head;
			while (_node != null)
			{
				var neighbor = _node.Value.Neighbors.Head;
				var cost = _node.Value.Costs.Head;

				// assert Neighbors and Costs length?
				while (neighbor != null)
				{
					if (neighbor.Value.Equals(node))
					{
						// for multiple edges between the same nodes
						var _neighbor = neighbor;
						var _cost = cost;

						neighbor = neighbor.Next;
						cost = cost.Next;

						_node.Value.Neighbors.Remove(_neighbor);
						_node.Value.Costs.Remove(_cost);
					}
					else
					{
						neighbor = neighbor.Next;
						cost = cost.Next;
					}
				}

				_node = _node.Next;
			}
		}

		private void BFS(GraphNode<T> start, LinkedList<GraphNode<T>> visited)
		{
			var searching = new GPrep.Queue<GraphNode<T>>();

			searching.Enqueue(start);
			while (searching.Count > 0)
			{
				var node = searching.Dequeue();
				var neighbor = node.Neighbors.Head;
				var cost = node.Costs.Head;
				while (neighbor != null)
				{
					if (visited.Find(neighbor.Value) == null) // TODO could be faster
					{
						searching.Enqueue(neighbor.Value);
					}
					neighbor = neighbor.Next;
				}

				if (visited.Find(node) == null) // TODO improve, last node is repeated w/o guard
				{
					visited.InsertBack(node);
				}
			}
		}

		private void DFS(GraphNode<T> start, LinkedList<GraphNode<T>> visited)
		{
			if (start != null && visited.Find(start) == null) // TODO O(n), can be improved
			{
				visited.InsertBack(start);

				var neighbor = start.Neighbors.Head;
				while (neighbor != null)
				{
					DFS(neighbor.Value, visited);
					neighbor = neighbor.Next;
				}
			}
		}
	}
}