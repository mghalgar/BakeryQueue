
public static class Extenssions
{
    public static Queue<T> RemoveItem<T>(this Queue<T> queue, T item)
    {
        var newQueue = new Queue<T>();
        while (queue.Count != 0)
        {
            var itemInQueue = queue.Dequeue();
            if (!itemInQueue.Equals(item))
                newQueue.Enqueue(itemInQueue);
        }
        return newQueue;
    }
}
