using System;

namespace LinkedList
{
    public class StackDeque<T>
    {
        private readonly Deque<T> _deque = new Deque<T>();
        public void Push(T value) => _deque.Push(value);
        public T Pop() => _deque.Pop();
    }
}