using System;

namespace GPrep
{
	public class HashTable<K, V> // assume K is int lol
	{
		/*
		private int p = 0;
		private int[] primes = new int[] { 193, 389 };
		*/

		private class HashKey
		{
			public K Key { get; set; }
			public V Value { get; set; }

			public HashKey(K key, V value)
			{
				Key = key;
				Value = value;
			}

			public override bool Equals(object other)
			{
				var o = (HashKey)other;
				return Key.Equals(o.Key);
			}

			public override int GetHashCode()
			{
				return Key.GetHashCode();
			}
		}

		private LinkedList<HashKey>[] buckets = new LinkedList<HashKey>[97];

		public int Count { get; set; }

		public V this[K k]
		{
			get => Find(k);
			set => Add(k, value);
		}

		// TODO RB Tree if wanting to enforce unique keys
		// linked list still used for collision of differing keys
		public void Add(K key, V value)
		{
			var i = HashIndex(buckets.Length, key);
			var kvp = new HashKey(key, value);
			if (buckets[i] == null)
			{
				var items = new LinkedList<HashKey>();
				items.InsertBack(kvp);
				buckets[i] = items;
			}
			else
			{
				buckets[i].InsertBack(kvp);
			}

			Count++;
		}

		public V Find(K key)
		{
			var i = HashIndex(buckets.Length, key);
			if (buckets[i] != null)
			{
				return buckets[i].Find(new HashKey(key, default(V))).Value.Value;
			}
			else
			{
				return default(V);
			}
		}

/*
		private void Grow()
		{
			// Count is inserted(V) not unique(K)...
			if (Count >= buckets.Length / 2)
			{
				if (p < primes.Length)
				{
					var temp = new LinkedList<V>[primes[p]];
					for (var i = 0; i < buckets.Length; i++)
					{
						var values = buckets[i];
						if (values != null)
						{
							var k = HashIndex(temp.Length, )
						}
					}
				}
				else
				{
					throw new InvalidOperationException();
				}
			}
		}
*/

		private int HashIndex(int length, K key)
		{
			// TODO fix or use another hashing algorithm
			dynamic k = key;
			if (k == 0) k = 1;
			return length % k;
		}
	}
}