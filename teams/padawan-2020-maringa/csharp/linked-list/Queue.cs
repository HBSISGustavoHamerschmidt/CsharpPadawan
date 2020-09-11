using System;

namespace LinkedList
{
    public class QueueDeque<T>
    {
        private readonly Deque<T> _deque = new Deque<T>();
        
        // Adiciona node na �ltima posi��o
        public void Enqueue(T value) => _deque.Push(value);
        // Remove node na primeira posi��o
        public T Dequeue() => _deque.Shift();
        
    }
}