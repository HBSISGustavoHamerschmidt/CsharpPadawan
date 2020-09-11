using System;

public class LinkedListItem<T>
{
    public T Information { get; set; }
    public LinkedListItem<T> NodeBehind { get; set; }
    public LinkedListItem<T> NodeAhead { get; set; }
}

public class Deque<T>
{

    public LinkedListItem<T> FirstItemFromList(T value) =>
        new LinkedListItem<T>
        {
            Information = value,
            NodeAhead = null,
            NodeBehind = null
        };

    private LinkedListItem<T> Head { get; set; }

    private LinkedListItem<T> Pointer { get; set; }


    public void Push(T value)
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public T Pop()
    {
        throw new NotImplementedException("You need to implement this function.");
    }

    public void Unshift(T value)
    {
        if (Head is null)
        {
            Head = FirstItemFromList(value);
        }
        else
        {
            var pushedItem = new LinkedListItem<T>
            {
                Information = value,
                NodeBehind = null,
                NodeAhead = Head
            };
            Head.NodeBehind = pushedItem;
            Head = pushedItem;
        }
    }

    public T Shift()
    {
        Pointer = Head;
        while (Pointer.NodeAhead != null)
        {
            Pointer = Pointer.NodeAhead;
        }
        throw new NotImplementedException("You need to implement this function.");
    }
}