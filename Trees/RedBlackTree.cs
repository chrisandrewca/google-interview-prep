using System;
using Utilities;

namespace Trees
{
	public class RBTNode
	{
		public int Key { get; set; }
		public object Data { get; set; }
		public bool IsBlack { get; set; }
		public RBTNode P { get; set; }
		public RBTNode Left { get; set; }
		public RBTNode Right { get; set; }

		public RBTNode()
		{
			Left = RedBlackTree.Nil;
			Right = RedBlackTree.Nil;
			P = RedBlackTree.Nil;
		}

		public override string ToString()
		{
			if (this == RedBlackTree.Nil)
			{
				return "(Nil)";
			}

			var leftColor = "";
			var rightColor = "";

			if (Left != null) leftColor = Left.IsBlack ? "Left is Black" : "Left is Red";
			if (Right != null) rightColor = Right.IsBlack ? "Right is Black" : "Right is Red";

			return $"({Key}, {(IsBlack ? "Black" : "Red")}, {leftColor}, {rightColor}";
		}
	}

	/*
	 * Properties of red-black trees
	 * 1. Every node is either red or black
	 * 2. The root is black
	 * 3. Every NIL leaf is black
	 * 4. If a node is red, then both its children are black
	 * 5. For each node, all simple paths from the node to the descendant leaves contain the same number of black nodes
	 */
	public class RedBlackTree : IRunnable
	{
		public void Run()
		{
			Console.WriteLine("Red Black Tree");

			var tree = new RedBlackTree();
			tree.Insert(new RBTNode() { Key = 8 });
			tree.Insert(new RBTNode() { Key = 5 });
			tree.Insert(new RBTNode() { Key = 2 });
			tree.Insert(new RBTNode() { Key = 7 });
			tree.Insert(new RBTNode() { Key = 11 });
			tree.Insert(new RBTNode() { Key = 14 });
			tree.Insert(new RBTNode() { Key = 15 });
			tree.Insert(new RBTNode() { Key = 1 });

			tree.PrintInOrder(tree.root);
		}

		public static RBTNode Nil = new RBTNode()
		{
			IsBlack = true
		};

		public RBTNode root = Nil;

		public void Insert(RBTNode node)
		{
			var y = Nil;
			var x = root;

			while (x != Nil)
			{
				y = x;
				if (node.Key < x.Key)
				{
					x = x.Left;
				}
				else
				{
					x = x.Right;
				}
			}

			node.P = y;
			Console.WriteLine($"::Insert - P:{node.P}");

			if (y == Nil)
			{
				root = node;
			}
			else if (node.Key < y.Key)
			{
				y.Left = node;
			}
			else
			{
				y.Right = node;
			}

			node.IsBlack = false;

			Console.WriteLine($"::Insert - {node}");
			RebalanceInsert(node);
		}

		public void Delete(RBTNode node)
		{
			var x = Nil;
			var y = node;
			var yWasBlack = y.IsBlack;

			if (node.Left == Nil)
			{
				x = node.Right;
				Transplant(node, node.Right);
			}
			else
			{
				y = Minimum(node.Right);
				yWasBlack = y.IsBlack;
				x = y.Right;
				if (y.P == node)
				{
					x.P = y;
				}
				else
				{
					Transplant(y, y.Right);
					y.Right = node.Right;
					y.Right.P = y;
				}

				Transplant(node, y);
				y.Left = node.Left;
				y.Left.P = y;
				y.IsBlack = node.IsBlack;
			}

			if (yWasBlack == true)
			{
				RebalanceDelete(x);
			}
		}

		public RBTNode Minimum(RBTNode node)
		{
			while (node.Left != Nil)
			{
				node = node.Left;
			}
			return node;
		}

		public RBTNode Maximum(RBTNode node)
		{
			while (node.Right != Nil)
			{
				node = node.Right;
			}
			return node;
		}

		public void PrintInOrder(RBTNode node)
		{
			if (node.Left != Nil)
			{
				//Console.WriteLine($"::PrintInOrder - {node}");
				PrintInOrder(node.Left);
			}

			Console.Write($"({node.Key}, {(node.IsBlack ? "Black" : "Red")}),");

			if (node.Right != Nil)
			{
				PrintInOrder(node.Right);
			}
		}

		protected void RebalanceInsert(RBTNode node)
		{
			while (node.P.IsBlack == false)
			{
				if (node.P == node.P.P.Left)
				{
					Console.WriteLine("::RebalanceInsert - left branch");
					Console.WriteLine($"::RebalanceInsert - R: {node.P.P.Right}, {node.P.Right}, {node}");
					Console.WriteLine($"::RebalanceInsert - L: {node.P.P.Left}, {node.P.Left}, {node}");
					RebalanceInsertClimbOrRotate(node, node.P.P.Right, node.P.Right);
				}
				else
				{
					Console.WriteLine("::RebalanceInsert - right branch");
					RebalanceInsertClimbOrRotate(node, node.P.P.Left, node.P.Left);
				}
			}
			root.IsBlack = true;
			Console.WriteLine($"::RebalanceInsert - {node}");
		}

		protected void RebalanceInsertClimbOrRotate(RBTNode node, RBTNode gpb, RBTNode pb)
		{
			Console.WriteLine("::RICOR");
			//var y = node.P.P.Right;
			var y = gpb;
			if (y.IsBlack == false)
			{
				Console.WriteLine("::RICOR - setting node.P.IsBlack=true");
				node.P.IsBlack = true;
				y.IsBlack = true;
				node.P.P.IsBlack = false;
				node = node.P.P;
			}
			//else if (node == node.P.Right)
			else
			{
				if (node == pb)
				{
					Console.WriteLine("::RICOR - setting node = node.P");
					node = node.P;
					LeftRotate(node);
				}
				node.P.IsBlack = true;
				node.P.P.IsBlack = false;
				RightRotate(node.P.P);
			}
		}

		protected void RebalanceDelete(RBTNode node)
		{
			while (node != root && node.IsBlack == true)
			{
				if (node == node.P.Left)
				{
					RebalanceDeleteClimbOrRotate(node, false);
				}
				else
				{
					RebalanceDeleteClimbOrRotate(node, true);
				}
			}
			node.IsBlack = true;
		}

		protected void RebalanceDeleteClimbOrRotate(RBTNode node, bool isRightBranch)
		{
			var w = node.P.Right;
			if (isRightBranch)
			{
				w = node.P.Left;
			}

			if (w.IsBlack == false)
			{
				w.IsBlack = true;
				node.P.IsBlack = false;
				LeftRotate(node.P);

				w = node.P.Right;
				if (isRightBranch)
				{
					w = node.P.Left;
				}
			}

			var sibling = w.Right;
			if (isRightBranch)
			{
				sibling = w.Left;
			}

			if (w.Left.IsBlack == true && w.Right.IsBlack == true)
			{
				w.IsBlack = false;
				node = node.P;
			}
			else if (sibling.IsBlack == true)
			{
				if (isRightBranch)
				{
					w.Right.IsBlack = true;
				}
				else
				{
					w.Left.IsBlack = true;
				}

				w.IsBlack = false;
				RightRotate(w);

				w.IsBlack = node.P.IsBlack;
				node.P.IsBlack = true;

				if (isRightBranch)
				{
					w.Left.IsBlack = true;
				}
				else
				{
					w.Right.IsBlack = true;
				}

				LeftRotate(node.P);
				node = root;
			}
		}

		protected void LeftRotate(RBTNode x)
		{
			var y = x.Right;
			x.Right = y.Left;

			if (y.Left != Nil && y.Left != null)
			{
				y.Left.P = x;
			}

			y.P = x.P;

			if (x.P == Nil)
			{
				root = y;
			}
			else if (x == x.P.Left)
			{
				x.P.Left = y;
			}
			else
			{
				x.P.Right  = y;
			}

			y.Left = x;
			x.P = y;
		}

		protected void RightRotate(RBTNode x)
		{
			Console.WriteLine($"::RightRotate - {x}");
			Console.WriteLine($"::RightRotate - {x.Left}");
			Console.WriteLine($"::RightRotate - {x.Left.Right}");

			var y = x.Left;
			x.Left = y.Right;

			// Console.WriteLine($"::RightRotate - {(y == null ? "y is null" : "y is not null")}");
			// Console.WriteLine($"::RightRotate - {(y.Right == null ? "y.Right is null" : "y.Right is not null")}");


			if (y.Right != Nil && y.Right != null)
			{
				y.Right.P = x;
			}

			y.P = x.P;

			if (x.P == Nil)
			{
				root = y;
			}
			else if (x == x.P.Right)
			{
				x.P.Right = y;
			}
			else
			{
				x.P.Left = y;
			}

			y.Right = x;
			x.P = y;
		}

		protected void Transplant(RBTNode u, RBTNode v)
		{
			if (u.P == Nil)
			{
				root = v;
			}
			else if (u == u.P.Left)
			{
				u.P.Left = v;
			}
			else
			{
				u.P.Right = v;
			}
			v.P = u.P;
		}
	}
}