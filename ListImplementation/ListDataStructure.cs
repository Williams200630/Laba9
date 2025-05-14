using System;
using System.Collections.Generic;
using System.Linq;

namespace ListImplementation
{
    public class ListDataStructure<T>
    {
        private List<T> items;

        public int Count => items.Count;
        public bool IsEmpty => items.Count == 0;

        public ListDataStructure()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public T[] ToArray()
        {
            return items.ToArray();
        }

        public void RemoveNonUnique()
        {
            var dict = new Dictionary<T, int>();
            foreach (var item in items)
            {
                if (dict.ContainsKey(item))
                    dict[item]++;
                else
                    dict[item] = 1;
            }
            items = items.Where(x => dict[x] == 1).ToList();
        }
    }
} 