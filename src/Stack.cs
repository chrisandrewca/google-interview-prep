using System;

namespace GPrep
{
	public class Stack<T>
	{
		private LinkedList<T> items = new LinkedList<T>();

		public int Count { get => items.Count; }

		public T Pop()
		{
			if (items.Head == null)
			{
				throw new InvalidOperationException();
			}

			var last = items.Head;
			items.Remove(last);
			return last.Value;
		}

		public void Push(T value)
		{
			items.InsertFront(value);
		}
	}
}