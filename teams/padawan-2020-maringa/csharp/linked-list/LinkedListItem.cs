namespace LinkedList
{
    public class LinkedListItem<T>
    {
        public T Information { get; set; }
        public LinkedListItem<T> NodeToHisRight { get; set; }
        public LinkedListItem<T> NodeToHisLeft { get; set; }
    }
}