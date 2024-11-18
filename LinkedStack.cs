using System;
using System.Collections;
using System.Collections.Generic;

namespace Clones
{
    public class LinkedStack<T> : IEnumerable<T>
    {
        public LinkedStack() { }

        public LinkedStack(LinkedStack<T> otherStack) 
        {
            foreach (var item in otherStack) 
                this.Push(item);
        }


        private StackNode<T> head;
        private StackNode<T> tail;

        public int Count { get; private set; }


        public void Push(T item)
        {
            if(head == null)
            {
                head = new StackNode<T>(item);
                tail = head;
            }
            else
            {
                var newItem = new StackNode<T>(item);

                tail.Next = newItem;
                newItem.Previous = tail;

                tail = newItem;
            }

            Count++;
        }

        public T Pop()
        {
            if (head == null) throw new InvalidOperationException("Stack is empty");

            var result = tail.Value;

            if(head != tail)
                tail.Previous.Next = null;

            tail = tail.Previous;

            Count--;

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
           var item = head;

            while(item != null)
            {
                yield return item.Value;
                item = item.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal class StackNode<T>
        {
            public T Value;
            public StackNode<T> Next;
            public StackNode<T> Previous;

            public StackNode(T value)
            {
                this.Value = value;
            }
        }
    }
}
