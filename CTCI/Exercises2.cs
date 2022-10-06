namespace CTCI;

public static class Exercises2
{
    public static void Ex1_RemoveDuplicates<T>(LinkedList<T> list)
    {
        var distinctItems = new HashSet<T>();
        for (var item = list.First; item != null; item = item.Next)
        {
            if (distinctItems.Contains(item.Value))
            {
                var toRemove = item;
                item = item.Previous;
                list.Remove(toRemove);
            }
            else
                distinctItems.Add(item.Value);
        }
    }

    public static void Ex1_RemoveDuplicatesNoBuffer<T>(LinkedList<T> list)
    {
        for (var i1 = list.First; i1 != null; i1 = i1.Next)
        {
            var i2 = i1.Next;
            while (i2 != null)
            {
                if (Equals(i1.Value, i2.Value))
                {
                    var toRemove = i2;
                    i2 = i2.Next;
                    list.Remove(toRemove);
                }
                else
                    i2 = i2.Next;
            }
        }
    }

    public static (T?, bool) Ex2_FindNthToLast<T>(LinkedList<T> list, int n)
    {
        if (n < 1)
            throw new ArgumentException(nameof(n));

        int i;
        LinkedListNode<T>? current;
        for (i = 0, current = list.First; i < n && current != null; i++, current = current.Next) ;

        if (i < n)
            return (default, false);

        var result = list.First;
        while (current != null)
        {
            current = current.Next;
            result = result!.Next;
        }

        return (result!.Value, true);
    }

    public class LinkedNode<T>
    {
        public T? Value { get; set; }
        public LinkedNode<T>? Next { get; set; }

        public override string ToString()
        {
            return RecursiveToString(0);
        }

        private string RecursiveToString(int level)
        {
            if (level > 10)
                return "...";
            return $"{Value}{(Next != null ? " " + Next.RecursiveToString(level + 1) : "")}";
        }
    }

    public static void Ex3_DeleteNode<T>(LinkedNode<T> node)
    {
        var next = node.Next;
        if (next == null)
            throw new Exception("Last node of the list: cannot remove it");
        node.Value = next.Value;
        node.Next = next.Next;
        next.Value = default;
        next.Next = null;
    }

    public static LinkedList<byte> Ex4_Sum(LinkedList<byte> n1, LinkedList<byte> n2)
    {
        byte reminder = 0;
        var sum = new LinkedList<byte>();
        var i1 = n1.First;
        var i2 = n2.First;
        while (i1 != null || i2 != null)
        {
            var digitsSum = (i1 != null ? i1.Value : 0) + (i2 != null ? i2.Value : 0) + reminder;
            sum.AddLast((byte)(digitsSum % 10));
            reminder = (byte)(digitsSum / 10);
            i1 = i1?.Next;
            i2 = i2?.Next;
        }

        if (reminder != 0)
            sum.AddLast(reminder);

        return sum;
    }

    public static LinkedNode<T>? Ex5_FindLoop<T>(LinkedNode<T>? list)
    {
        var alreadyVisited = new HashSet<LinkedNode<T>> { };
        var current = list;
        while (current != null && !alreadyVisited.Contains(current))
        {
            alreadyVisited.Add(current);
            current = current.Next;
        }
        return current;
    }

    public static LinkedNode<T>? Ex5_FindLoop_NoHashSet<T>(LinkedNode<T>? head)
    {
        if (head == null) return null;

        var fast = head.Next;
        var slow = head;

        LinkedNode<T>? loopingNode = null;
        int loopLength = 0;
        while (slow != null && fast != null)
        {
            if (slow == fast)
            {
                loopingNode = slow;
                break;
            }

            slow = slow.Next;
            fast = fast.Next;

            if (slow == fast)
            {
                loopingNode = slow;
                break;
            }
            fast = fast?.Next;
            loopLength += 2;
        }

        if (loopingNode == null) 
            return null;

        var current = head;
        while (true)
        {
            for (var i = 0; i <= loopLength; i++)
            {
                if (current == loopingNode)
                    return current;
                loopingNode = loopingNode.Next;
            }

            current = current.Next;
        }
    }
}