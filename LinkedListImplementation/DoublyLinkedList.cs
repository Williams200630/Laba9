using System;
using System.Collections.Generic;

namespace LinkedListImplementation
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
        }
    }

    public class DoublyLinkedList<T>
    {
        private Node<T>? head;
        private Node<T>? tail;
        private int count;

        public int Count => count;
        public bool IsEmpty => count == 0;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public void Add(T data)
        {
            Node<T> newNode = new Node<T>(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Previous = tail;
                tail!.Next = newNode;
                tail = newNode;
            }

            count++;
        }

        public void Remove(T data)
        {
            Node<T>? current = head;

            while (current != null)
            {
                if (current.Data!.Equals(data))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;
                    }
                    else
                    {
                        head = current.Next;
                    }

                    if (current.Next != null)
                    {
                        current.Next.Previous = current.Previous;
                    }
                    else
                    {
                        tail = current.Previous;
                    }

                    count--;
                    return;
                }

                current = current.Next;
            }
        }

        public bool Contains(T data)
        {
            Node<T>? current = head;

            while (current != null)
            {
                if (current.Data!.Equals(data))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public T[] ToArray()
        {
            T[] array = new T[count];
            Node<T>? current = head;
            int index = 0;

            while (current != null)
            {
                array[index] = current.Data;
                current = current.Next;
                index++;
            }

            return array;
        }

        public void RemoveNonUnique()
        {
            var dict = new Dictionary<T, int>();
            Node<T>? current = head;
            while (current != null)
            {
                if (dict.ContainsKey(current.Data!))
                    dict[current.Data!]++;
                else
                    dict[current.Data!] = 1;
                current = current.Next;
            }
            current = head;
            while (current != null)
            {
                var next = current.Next;
                if (dict[current.Data!] != 1)
                    Remove(current.Data!);
                current = next;
            }
        }
    }
} 