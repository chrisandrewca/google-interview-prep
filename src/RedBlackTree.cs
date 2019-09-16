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

		public void Insert(T value)
		{
			var v = new RBTNode<T>(value) { Color = RBTColor.Red };

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
						InsertFixup(node.Left);
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
						InsertFixup(node.Right);
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
			InsertFixup(root);
		}

		public void Remove(T value)
		{
			var node = Find(value);
			RBTNode<T> minSub;

			if (node != null)
			{
				var oColor = node.Color;

				if (node.Left == null)
				{
					minSub = node.Right;
					Transplant(node, node.Right);
				}
				else if (node.Right == null)
				{
					minSub = node.Left;
					Transplant(node, node.Left);
				}
				else
				{
					var min = Minimum(node.Right);
					oColor = min.Color;

					minSub = min.Right;
					if (minSub != null && min.Parent ==  node)
					{
						minSub.Parent = min;
					}
					else
					{
						Transplant(min, min.Right);
						min.Right = node.Right;

						if (min.Right != null)
						{
							min.Right.Parent = min;
						}
					}

					Transplant(node, min);
					min.Left = node.Left;
					min.Left.Parent = min;
					min.Color = node.Color;
				}

				if (oColor == RBTColor.Black)
				{
					RemoveFixup(minSub);
				}

				Count--;
			}
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

		private void InsertFixup(RBTNode<T> node)
		{
			while (node.Parent != null && node.Parent.Color == RBTColor.Red)
			{
				if (node.Parent == node.Parent.Parent.Left)
				{
					var uncle = node.Parent.Parent?.Right;
					if (uncle != null && uncle.Color == RBTColor.Red)
					{
						node.Parent.Color = RBTColor.Black;
						uncle.Color = RBTColor.Black;
						node.Parent.Parent.Color = RBTColor.Red;
						node = node.Parent.Parent; // iterate
					}
					else
					{
						if (node == node.Parent.Right)
						{
							node = node.Parent;
							LeftRotate(node); // "hoist"
						}
						node.Parent.Color = RBTColor.Black;
						node.Parent.Parent.Color = RBTColor.Red;
						RightRotate(node.Parent.Parent); // "lower"
					}
				}
				else
				{
					var uncle = node.Parent.Parent?.Left;
					if (uncle != null && uncle.Color == RBTColor.Red)
					{
						node.Parent.Color = RBTColor.Black;
						uncle.Color = RBTColor.Black;
						node.Parent.Parent.Color = RBTColor.Red;
						node = node.Parent.Parent; // iterate
					}
					else
					{
						if (node == node.Parent.Left)
						{
							node = node.Parent;
							RightRotate(node); // "hoist"
						}
						node.Parent.Color = RBTColor.Black;
						node.Parent.Parent.Color = RBTColor.Red;
						LeftRotate(node.Parent.Parent); // "lower"
					}
				}
			}

			root.Color = RBTColor.Black;
		}

		private void LeftRotate(RBTNode<T> node)
		{
			var right = node.Right;
			node.Right = right.Left;

			if (right.Left != null)
			{
				right.Left.Parent = node;
			}

			right.Parent = node.Parent;

			if (node.Parent == null)
			{
				root = right;
			}
			else if (node == node.Parent.Left)
			{
				node.Parent.Left = right;
			}
			else
			{
				node.Parent.Right = right;
			}

			right.Left = node;
			node.Parent = right;
		}

		private RBTNode<T> Minimum(RBTNode<T> node)
		{
			while (node.Left != null)
			{
				node = node.Left;
			}
			return node;
		}

		private void RemoveFixup(RBTNode<T> node)
		{
			while (node != null && node != root && node.Color == RBTColor.Black)
			{
				if (node == node.Parent.Left)
				{
					var sibling = node.Parent.Right;
					if (sibling.Color == RBTColor.Red)
					{
						sibling.Color = RBTColor.Black;
						node.Parent.Color = RBTColor.Red;
						LeftRotate(node.Parent);
						sibling = node.Parent.Right;
					}

					if (sibling.Left != null &&
						sibling.Left.Color == RBTColor.Black &&
						sibling.Right != null &&
						sibling.Right.Color == RBTColor.Black)
					{
						sibling.Color = RBTColor.Red;
						node = node.Parent; // iterate
					}
					else
					{
						if (sibling.Right.Color == RBTColor.Black)
						{
							sibling.Left.Color = RBTColor.Black;
							sibling.Color = RBTColor.Red;
							RightRotate(sibling);
							sibling = node.Parent.Right;
						}

						sibling.Color = node.Parent.Color;
						node.Parent.Color = RBTColor.Black;
						sibling.Right.Color = RBTColor.Black;
						LeftRotate(node.Parent);
						node = root;
					}
				}
				else
				{
					var sibling = node.Parent.Left;
					if (sibling.Color == RBTColor.Red)
					{
						sibling.Color = RBTColor.Black;
						node.Parent.Color = RBTColor.Red;
						RightRotate(node.Parent);
						sibling = node.Parent.Left;
					}

					if (sibling.Left != null &&
						sibling.Left.Color == RBTColor.Black &&
						sibling.Right != null &&
						sibling.Right.Color == RBTColor.Black)
					{
						sibling.Color = RBTColor.Red;
						node = node.Parent; // iterate
					}
					else
					{
						if (sibling.Left.Color == RBTColor.Black)
						{
							sibling.Right.Color = RBTColor.Black;
							sibling.Color = RBTColor.Red;
							LeftRotate(sibling);
							sibling = node.Parent.Left;
						}

						sibling.Color = node.Parent.Color;
						node.Parent.Color = RBTColor.Black;
						sibling.Left.Color = RBTColor.Black;
						RightRotate(node.Parent);
						node = root;
					}
				}
			}

			if (node != null)
			{
				node.Color = RBTColor.Black;
			}
		}

		private void RightRotate(RBTNode<T> node)
		{
			var left = node.Left;
			node.Left = left.Right;

			if (left.Right != null)
			{
				left.Right.Parent = node;
			}

			left.Parent = node.Parent;

			if (node.Parent == null)
			{
				root = left;
			}
			else if (node == node.Parent.Right)
			{
				node.Parent.Right = left;
			}
			else
			{
				node.Parent.Left = left;
			}

			left.Right = node;
			node.Parent = left;
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