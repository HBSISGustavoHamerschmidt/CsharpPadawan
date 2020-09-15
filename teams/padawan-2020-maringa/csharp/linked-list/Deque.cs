namespace LinkedList
{
    public class Deque<T>
    {
        public LinkedListItem<T> FirstOrLastItemFromList(T value) =>
            new LinkedListItem<T>
            {
                Information = value,
                NodeToHisLeft = null,
                NodeToHisRight = null
            };
        private LinkedListItem<T> Head { get; set; }
        private LinkedListItem<T> Tail { get; set; }
        public void Push(T value)
        {
            if (Tail is null)
            {
                Tail = Head;
                if (Tail != null) return;
                Tail = FirstOrLastItemFromList(value);
                Head = Tail;
            }
            else
            {
                var insertedItemOnTail = new LinkedListItem<T>
                {
                    // We are creating a node that shall guard the realms of nodes as the new tail.
                    // which is receiving the info that to his left there will be the guy that currently represents tail.
                    Information = value,
                    NodeToHisRight = null,
                    NodeToHisLeft = Tail
                };
                // Soon after, we tell the current head that from now one he has someone to his right, and that is who shall become the new tail.
                Tail.NodeToHisRight = insertedItemOnTail;
                // The new tail is then henceforth crowned the protector and tail of the Doubled Linked List.
                Tail = insertedItemOnTail;
            }
        }
        public T Pop()
        {
            var infoThatIsBeingRemovedFromHeadAndThrownIntoTheVoid = Tail.Information;
            if (!(Tail.NodeToHisLeft is null))
                Tail.NodeToHisLeft.NodeToHisRight = null;
            Tail = Tail.NodeToHisLeft;
            return infoThatIsBeingRemovedFromHeadAndThrownIntoTheVoid;
        }

        public void Unshift(T value)
        {
            if (Head is null)
            {
                Head = Tail;
                if (Head != null) return;
                Head = FirstOrLastItemFromList(value);
                Tail = Head;
            }
            else
            {
                var insertedItemOnHead = new LinkedListItem<T>
                {
                    // We are here creating a new node that will become the new Head,
                    // which is receiving the info that to his right there will be the guy that currently represents head.
                    Information = value,
                    NodeToHisRight = Head,
                    NodeToHisLeft = null
                };
                // Soon after, we tell the guy that currently sits at Head that, from now on, to his left, there shall be a new man, and he is no longer head.
                Head.NodeToHisLeft = insertedItemOnHead;
                // The new head is then henceforth crowned the leader and head of the Doubled Linked List.
                Head = insertedItemOnHead;
            }
        }

        public T Shift()
        {
            // We want to overthrow the head that currently sits at the iron throne and put the previous head in charge once more.
            // To do that, we first keep the info that the current Head keeps inside this variable.
            var infoThatIsBeingRemovedFromHeadAndThrownIntoTheVoid = Head.Information;
            // Then, we tell the node that is to the right of the current hand that the node on his left, if it exists, that it shall be null once more.
            if (!(Head.NodeToHisRight is null))
                Head.NodeToHisRight.NodeToHisLeft = null;
            // After that, we shall crown the node that once held the place as the leader but was unjustly cast aside by the evil unshift method.
            Head = Head.NodeToHisRight;
            return infoThatIsBeingRemovedFromHeadAndThrownIntoTheVoid;
        }
    }
}