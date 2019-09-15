using System.Collections.Generic;

namespace GPrep
{
	public class ListNode<T>
	{
		public T Value { get; set; }

		public ListNode<T> Prev { get; set; }
		public ListNode<T> Next { get; set; }
	}

	public class LinkedList<T>
	{
		public ListNode<T> Head { get; private set; }
		public ListNode<T> Tail { get; private set; }

		public ListNode<T> Find(T value)
		{
			var node = Head;
			while (node != null)
			{
				var comparer = EqualityComparer<T>.Default;
				if (comparer.Equals(node.Value, value))
				{
					break;
				}
				node = node.Next;
			}

			return node;
		}

		public ListNode<T> InsertBack(T value)
		{
			var node = new ListNode<T>() { Value = value };

			var _tail = Tail;
			if (_tail != null)
			{
				node.Prev = _tail;
				_tail.Next = node;
			}
			else
			{
				Head = node;
			}

			Tail = node;

			return node;
		}

		public ListNode<T> InsertFront(T value)
		{
			var node = new ListNode<T>() { Value = value };

			var _head = Head;
			if (_head != null)
			{
				node.Next = _head;
				_head.Prev = node;
			}
			else
			{
				Tail = node;
			}

			Head = node;

			return node;
		}

		public void Remove(T value)
		{
			var node = Find(value);
			Remove(node);
		}

		public void Remove(ListNode<T> node)
		{
			if (node != null)
			{
				if (node.Prev != null)
				{
					node.Prev.Next = node.Next;
				}

				if (node.Next != null)
				{
					node.Next.Prev = node.Prev;
				}
			}
		}
	}
}