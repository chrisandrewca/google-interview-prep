using System;

namespace GPrep
{
	public class Queue<T>
	{
		private LinkedList<T> items = new LinkedList<T>();

		public int Count { get => items.Count; }

		public T Dequeue()
		{
			if (items.Tail == null)
			{
				throw new InvalidOperationException();
			}

			var first = items.Tail;
			items.Remove(first);
			return first.Value;
		}

		public void Enqueue(T value)
		{
			items.InsertFront(value);
		}
	}
}