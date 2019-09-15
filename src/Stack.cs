namespace GPrep
{
	public class Stack<T>
	{
		private ListNode<T> last;
		private LinkedList<T> items = new LinkedList<T>();

		public int Count { get; private set; }

		public T Pop()
		{
			if (last == null)
			{
				return default(T);
			}

			var _last = last;
			last = _last.Next;
			items.Remove(_last);

			if (Count > 0)
			{
				Count--;
			}

			return _last.Value;
		}

		public void Push(T value)
		{
			last = items.InsertFront(value);
			Count++;
		}
	}
}