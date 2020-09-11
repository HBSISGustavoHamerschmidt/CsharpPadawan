using System;

namespace LinkedList
{
    public class QueueDeque<T>
    {
        private readonly Deque<T> _deque = new Deque<T>();
        
        // Adiciona node na última posição
        public void Enqueue(T value) => _deque.Push(value);
        // Remove node na primeira posição
        public T Dequeue() => _deque.Shift();
        
    }
}