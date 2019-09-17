using System;

namespace GPrep
{
	public class TrieTree
	{
		private class TrieNode
		{
			public char C { get; set; }
			public HashTable<int, TrieNode> Children { get; set; }
			public bool IsLeaf { get; set; }

			public TrieNode()
			{
				Children = new HashTable<int, TrieNode>();
			}
		}

		private TrieNode root = new TrieNode();

		public void Insert(string word)
		{
			var children = root.Children;
			for (var i = 0; i < word.Length; i++)
			{
				var c = word[i];
				var node = children.Find(c);
				if (node == null)
				{
					node = new TrieNode() { C = c };
					children.Add(c, node);
				}

				children = node.Children;
				if (i == word.Length - 1)
				{
					node.IsLeaf = true;
				}
			}
		}

		public bool Find(string word)
		{
			var children = root.Children;
			TrieNode node = null;
			for (var i = 0; i < word.Length; i++)
			{
				var c = word[i];
				node = children.Find(c);
				if (node != null)
				{
					children = node.Children;
				}
				else
				{
					node = null;
					break;
				}
			}

			return (node != null && node.IsLeaf);
		}
	}
}