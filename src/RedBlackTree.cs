using System;
using System.Collections.Generic;

namespace GPrep
{
	public enum RBTColor
	{
		Black,
		Red
	}

	public class RBTNode<T>
	{
		public RBTColor Color { get; set; }
		public RBTNode<T> Left { get; set; }
		public RBTNode<T> Parent { get; set; }
		public RBTNode<T> Right { get; set; }

		public T Value { get; set; }

		public RBTNode(T value)
		{
			Value = value;
			Color = RBTColor.Black;
		}
	}

	public class RedBlackTree<T>
	{
		private Comparer<T> comparer = Comparer<T>.Default;
		private RBTNode<T> root;

		public int Count { get; private set; }

		public RBTNode<T> Find(T value)
		{
			var node = root;
			while (node != null)
			{
				var result = comparer.Compare(value, node.Value);
				if (result < 0)
				{
					node = node.Left;
				}
				else if (result > 0)
				{
					node = node.Right;
				}
				else
				{
					return node;
				}
			}

			return null;
		}

		public void Insert(T value)
		{
			var v = new RBTNode<T>(value);

			var node = root;
			while (node != null)
			{
				var result = comparer.Compare(v.Value, node.Value);
				if (result <= 0)
				{
					if (node.Left == null)
					{
						node.Left = v;
						node.Left.Parent = node;
						Count++;
						return;
					}
					else
					{
						node = node.Left;
					}
				}
				else
				{
					if (node.Right == null)
					{
						node.Right = v;
						node.Right.Parent = node;
						Count++;
						return;
					}
					else
					{
						node = node.Right;
					}
				}
			}

			root = v;
			Count++;
		}

		public T[] GetInOrder()
		{
			var nodeList = new LinkedList<T>();
			GetInOrder(root, nodeList);

			var i = 0;
			var arr = new T[nodeList.Count];
			var node = nodeList.Head;
			while (node != null)
			{
				arr[i] = node.Value;
				node = node.Next;
				i++;
			}

			return arr;
		}

		public void Remove(T value)
		{
			var node = Find(value);
			if (node != null)
			{
				if (node.Left == null)
				{
					Transplant(node, node.Right);
				}
				else if (node.Right == null)
				{
					Transplant(node, node.Left);
				}
				else
				{
					var min = Minimum(node.Right);
					if (min.Parent != node)
					{
						Transplant(min, min.Right);
						min.Right = node.Right;
						min.Right.Parent = min;
					}

					Transplant(node, min);
					min.Left = node.Left;
					min.Left.Parent = min;
				}
				Count--;
			}
		}

		private RBTNode<T> Minimum(RBTNode<T> node)
		{
			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}

		private void GetInOrder(RBTNode<T> node, LinkedList<T> nodeList)
		{
			if (node != null)
			{
				GetInOrder(node.Left, nodeList);

				nodeList.InsertBack(node.Value);

				GetInOrder(node.Right, nodeList);
			}
		}

		private void Transplant(RBTNode<T> uproot, RBTNode<T> plant)
		{
			if (uproot.Parent == null)
			{
				root = plant;
			}
			else if (uproot == uproot.Parent.Left)
			{
				uproot.Parent.Left = plant;
			}
			else
			{
				uproot.Parent.Right = plant;
			}

			if (plant != null)
			{
				plant.Parent = uproot.Parent;
			}
		}
	}
}