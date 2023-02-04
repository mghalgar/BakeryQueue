
public class Program
{
    static Queue<int> _queue = new();
    static List<Person> _personList = new();
    static int _index=0;
    static int _savedCount = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Add Person by p , Bake by b");
        var command = Console.ReadLine();
        if (command == "p")
        {
            Console.WriteLine("name ?");
            var name = Console.ReadLine();

            Console.WriteLine("orderCount ?");
            var orderCount = int.Parse(Console.ReadLine());

            AddPersonToQueue(name, orderCount);
        }
        else if (command == "b")
        {
            Bake();
        }

        Main(args);
    }
    static void AddPersonToQueue(string name, int orderNumber)
    {
        var number = _index++;
        _queue.Enqueue(number);

        _personList.Add(new(number,name, orderNumber, 0));

        Deliver();
    }

    static void Bake()
    {
        _savedCount++;

        Deliver();

        ShiftOnePerson();

        Console.WriteLine($"SavedCount : {_savedCount}");
    }
    static void ShiftOnePerson()
    {
        if (_queue.Any())
        {
            var number = _queue.Dequeue();

            var item = _personList.Single(x => x.index == number);

            item = item with { ownedCount = item.ownedCount + 1 };

            if (item.ownedCount < item.orderCount)
                _queue.Enqueue(number);

            Console.WriteLine(string.Join("\n", _personList.Select(w => $"{w.name}: total={w.orderCount} ,owned={w.ownedCount} ").ToArray()));
        }
    }
    static void Deliver()
    {
        if (_personList.Count == 0) return;

        var firstPerson = _personList.OrderBy(s => s.orderCount-s.ownedCount).ThenBy(x=>x.index).First();

        if (firstPerson.orderCount > _savedCount) return;

        _personList.Remove(firstPerson);

        _queue = _queue.RemoveItem(firstPerson.index);

        _savedCount -= firstPerson.orderCount;

        Console.WriteLine($"Delivered {firstPerson.orderCount} to {firstPerson}");

        Deliver();
    }
}
