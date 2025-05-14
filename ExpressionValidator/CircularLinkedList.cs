using System;

namespace ExpressionValidator
{
    public class CircularNode<T>
    {
        public T Data { get; set; }
        public CircularNode<T>? Next { get; set; }
        public CircularNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class CircularLinkedList<T>
    {
        public CircularNode<T>? Head { get; private set; }
        public int Count { get; private set; }

        public CircularLinkedList()
        {
            Head = null;
            Count = 0;
        }

        public void Add(T data)
        {
            var newNode = new CircularNode<T>(data);
            if (Head == null)
            {
                Head = newNode;
                newNode.Next = Head;
            }
            else
            {
                var current = Head;
                while (current!.Next != Head)
                {
                    current = current.Next;
                }
                current.Next = newNode;
                newNode.Next = Head;
            }
            Count++;
        }

        public CircularNode<T>? Find(Predicate<T> match)
        {
            if (Head == null) return null;
            var current = Head;
            do
            {
                if (match(current.Data))
                    return current;
                current = current.Next!;
            } while (current != Head);
            return null;
        }
    }
} 