using System;
using System.Collections.Generic;

namespace GPrep
{
	public class GraphNode<T>
	{
		public LinkedList<int> Costs { get; set; }
		// what if tree tho?
		public LinkedList<GraphNode<T>> Neighbors { get; set; }

		// right now value is key, but GenInts will produce dupes...
		public T Value { get; set; }

		public GraphNode(T value)
		{
			Costs = new LinkedList<int>();
			Neighbors = new LinkedList<GraphNode<T>>();
			Value = value;
		}
	}

	public class Graph<T>
	{
		public LinkedList<GraphNode<T>> Nodes { get; set; }

		public Graph()
		{
			Nodes = new LinkedList<GraphNode<T>>();
		}

		public GraphNode<T> AddNode(T value)
		{
			var node = new GraphNode<T>(value);
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

		public LinkedList<GraphNode<T>> DFSTraverse(GraphNode<T> start)
		{
			// TODO make HashTable?
			var visited = new LinkedList<GraphNode<T>>();
			DFSTraverse(start, visited);
			return visited;
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
					// reference comparison...
					if (neighbor.Value == node)
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

		private void DFSTraverse(GraphNode<T> start, LinkedList<GraphNode<T>> visited)
		{
			if (start != null && visited.Find(start) == null) // TODO O(n), can be improved
			{
				visited.InsertBack(start);

				var neighbor = start.Neighbors.Head;
				while (neighbor != null)
				{
					DFSTraverse(neighbor.Value, visited);
					neighbor = neighbor.Next;
				}

			}
		}
	}
}