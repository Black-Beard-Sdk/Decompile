using System;
using System.Collections.Generic;
using System.Text;

namespace Bb.Sdk.Decompiler.IlParser
{

    /// <summary>
    /// Union-Find data structure.
    /// </summary>
    public class UnionFind<T>
    {
        Dictionary<T, Node> mapping;

        class Node
        {
            public int rank;
            public Node parent;
            public T value;
        }

        public UnionFind()
        {
            mapping = new Dictionary<T, Node>();
        }

        Node GetNode(T element)
        {
            Node node;
            if (!mapping.TryGetValue(element, out node))
            {
                node = new Node
                {
                    value = element,
                    rank = 0
                };
                node.parent = node;
                mapping.Add(element, node);
            }
            return node;
        }

        public T Find(T element)
        {
            return FindRoot(GetNode(element)).value;
        }

        Node FindRoot(Node node)
        {
            if (node.parent != node)
                node.parent = FindRoot(node.parent);
            return node.parent;
        }

        public void Merge(T a, T b)
        {
            var rootA = FindRoot(GetNode(a));
            var rootB = FindRoot(GetNode(b));
            if (rootA == rootB)
                return;
            if (rootA.rank < rootB.rank)
                rootA.parent = rootB;
            else if (rootA.rank > rootB.rank)
                rootB.parent = rootA;
            else
            {
                rootB.parent = rootA;
                rootA.rank++;
            }
        }
    }


}
