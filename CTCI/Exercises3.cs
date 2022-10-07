using System.Drawing;
using System.Runtime.CompilerServices;

namespace CTCI;

public static class Exercises3
{
    public class Ex1_SingleArrayTripleStack<T>
    {
        private T[] Items { get; }

        private int[] _first;
        private int[] _sizes;

        public Ex1_SingleArrayTripleStack(int capacity)
        {
            Items = new T[capacity];
            var reminder = capacity % 3;
            _first = new int[3] { 0, capacity / 3 + (reminder > 0 ? 1 : 0), capacity * 2 / 3 + reminder };
            _sizes = new int[3];
        }

        public void Push(int stackId, T value)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (stackId == _first.Length - 1)
            {
                if (_first[stackId] + _sizes[stackId] == Items.Length)
                    throw new InvalidOperationException($"Stack {stackId} is full");
            }
            else
            {
                if (_first[stackId] + _sizes[stackId] == _first[stackId + 1])
                    throw new InvalidOperationException($"Stack {stackId} is full");
            }

            Items[_first[stackId] + _sizes[stackId]] = value;
            _sizes[stackId]++;
        }

        public T Pop(int stackId)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (_sizes[stackId] == 0)
                throw new InvalidOperationException($"Stack {stackId} is empty");
            var result = Items[_first[stackId] + _sizes[stackId] - 1];
            _sizes[stackId]--;
            return result;
        }

        public int GetCount(int stackId) => _sizes[stackId];
    }

    public class Ex1_SingleArrayTripleStackLessWaste<T>
    {
        private T[] Items { get; }
        private int[] _sizes;

        public Ex1_SingleArrayTripleStackLessWaste(int capacity)
        {
            Items = new T[capacity];
            _sizes = new int[3];
        }

        public void Push(int stackId, T value)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (stackId == 2)
            {
                if ((Math.Max(_sizes[0], _sizes[1]) + 1) * 2 > Items.Length - _sizes[2])
                    throw new InvalidOperationException($"Stack {stackId} is full");

                Items[Items.Length - _sizes[2] - 1] = value;
            }
            else
            {
                if (Math.Max(_sizes[0], _sizes[1]) == _sizes[stackId] && 
                    (Math.Max(_sizes[0], _sizes[1]) + 1) * 2 > Items.Length - _sizes[2])
                    throw new InvalidOperationException($"Stack {stackId} is full");

                Items[_sizes[stackId] * 2 + stackId] = value;
            }
            _sizes[stackId]++;
        }

        public T Pop(int stackId)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (_sizes[stackId] == 0)
                throw new InvalidOperationException($"Stack {stackId} is empty");

            T result;
            if (stackId == 2)
            {
                result = Items[Items.Length - _sizes[2]];
            }
            else
            {
                result = Items[(_sizes[stackId] - 1) * 2 + stackId];
            }
            _sizes[stackId]--;
            return result;
        }

        public int GetCount(int stackId) => _sizes[stackId];
    }

    public class Ex1_SingleArrayTripleStackNoWaste<T>
    {
        private record struct Item(T? Value, int IndexPrevious);

        private Item[] Items { get; }
        private int[] TopIndexes { get; }
        private int[] Sizes { get; }
        private int LastEmptySlotIndex { get; set; }

        public Ex1_SingleArrayTripleStackNoWaste(int capacity)
        {
            Items = new Item[capacity];
            TopIndexes = new int[3] { -1, -1, -1 };
            Sizes = new int[3] { 0, 0, 0 };
            LastEmptySlotIndex = -1;
        }

        public void Push(int stackId, T value)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (LastEmptySlotIndex < 0)
            {
                var sizesSum = Sizes[0] + Sizes[1] + Sizes[2];
                if (sizesSum == Items.Length)
                    throw new InvalidOperationException("The stacks are full");

                Items[sizesSum] = new Item(value, TopIndexes[stackId]);
                TopIndexes[stackId] = sizesSum;
                Sizes[stackId]++;
            }
            else
            {
                var secondEmptySlotIndex = Items[LastEmptySlotIndex].IndexPrevious;
                Items[LastEmptySlotIndex] = new Item(value, TopIndexes[stackId]);
                TopIndexes[stackId] = LastEmptySlotIndex;
                Sizes[stackId]++;
                LastEmptySlotIndex = secondEmptySlotIndex;
            }
        }

        public T Pop(int stackId)
        {
            if (stackId < 0 || stackId >= 3)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            if (Sizes[stackId] == 0)
                throw new InvalidOperationException($"Stack {stackId} is empty");

            var topIndex = TopIndexes[stackId];
            var result = Items[topIndex];
            Items[topIndex] = new Item(default, LastEmptySlotIndex);
            TopIndexes[stackId] = result.IndexPrevious;
            Sizes[stackId]--;
            LastEmptySlotIndex = topIndex;

            return result.Value;
        }

        public int GetCount(int stackId) => Sizes[stackId];
    }

    public class Ex2_StackWithMin<T>
        where T: IComparable<T>
    {
        private record struct Item(T Value, int IndexMin);

        private List<Item> Items { get; }
        private int IndexMin { get; set; } = -1;

        public Ex2_StackWithMin()
        {
            Items = new List<Item> { };
        }

        public void Push(T value)
        {
            var indexMin = IndexMin < 0 || Items[IndexMin].Value.CompareTo(value) > 0 ? Items.Count : IndexMin;
            Items.Add(new(value, indexMin));
            IndexMin = indexMin;
        }

        public T Pop()
        {
            var topIndex = Items.Count - 1;
            var result = Items[topIndex];
            Items.RemoveAt(topIndex);
            if (IndexMin == topIndex)
                IndexMin = Items.Count > 0 ? Items[^1].IndexMin : -1;
            return result.Value;
        }

        public T Min() => 
            IndexMin >= 0 ? Items[IndexMin].Value : throw new InvalidOperationException("Stack is empty");

        public int GetCount() => 
            Items.Count;
    }

    public class Ex2_StackWithMinLessSpace<T>
        where T: IComparable<T>
    {
        private List<T> Items { get; }
        private Stack<T> Mins { get; }

        public Ex2_StackWithMinLessSpace()
        {
            Items = new List<T> { };
            Mins = new Stack<T> { };
        }

        public void Push(T value)
        {
            Items.Add(value);
            if (Mins.Count == 0 || Mins.Peek().CompareTo(value) >= 0)
                Mins.Push(value);
        }

        public T Pop()
        {
            var topIndex = Items.Count - 1;
            var result = Items[topIndex];
            Items.RemoveAt(topIndex);
            if (Mins.Peek().CompareTo(result) >= 0)
                Mins.Pop();

            return result;
        }

        public T Min() =>
            Items.Count > 0 ? Mins.Peek() : throw new InvalidOperationException("Stack is empty");

        public int GetCount() =>
            Items.Count;
    }

    public class Ex3_SetOfStacks<T>
    {
        private class Stack
        { 
            public T?[] Items { get; }
            public int Size { get; private set; }

            public Stack(int capacity)
            {
                Items = new T[capacity];
                Size = 0;
            }

            public void Push(T value)
            {
                if (Size == Items.Length)
                    throw new InvalidOperationException("Stack is full");

                Items[Size++] = value;
            }

            public T Pop()
            {
                if (Size == 0)
                    throw new InvalidOperationException("Stack is empty");

                var result = Items[--Size];
                Items[Size] = default;
                return result;
            }
        }

        private List<Stack> Stacks { get; }

        private int Capacity { get; }

        private int Count { get; set; }

        public Ex3_SetOfStacks(int capacity)
        {
            Stacks = new List<Stack>();
            Capacity = capacity;
            Count = 0;
        }

        public void Push(T value)
        {
            if (Stacks.Count == 0 || Stacks[^1].Size == Stacks[^1].Items.Length)
                Stacks.Add(new Stack(Capacity));
            Stacks[^1].Push(value);
            Count++;
        }

        public T Pop()
        {
            if (Stacks.Count == 0)
                throw new InvalidOperationException("Stack set is empty");

            while (Stacks[^1].Size == 0)
                Stacks.RemoveAt(Stacks.Count - 1);

            if (Stacks.Count == 0)
                throw new InvalidOperationException("Stack set is empty");

            Count--;

            return Stacks[^1].Pop();
        }

        public T Pop(int stackId)
        {
            if (stackId < 0 || stackId >= Stacks.Count)
                throw new ArgumentException($"Invalid {nameof(stackId)}");

            return Stacks[stackId].Pop();
        }

        public int GetCount() => Count;
    }

    public static void Ex4_HanoiTowers(int n)
    {
        var towers = new[] { new List<int>(n), new List<int>(n), new List<int>(n) };

        for (var i = 0; i < n; i++)
            towers[0].Add(n - i);

        ValidateTowers(towers);

        MoveFromTo(n, 0, 2);

        void MoveFromTo(int m, int fromStack, int toStack)
        {
            if (m == 0)
                return;
            
            if (m == 1)
            {
                towers[toStack].Add(towers[fromStack][^1]);
                towers[fromStack].RemoveAt(towers[fromStack].Count - 1);

                ValidateTowers(towers);

                return;
            }

            var bufferStack = 3 - fromStack - toStack;

            MoveFromTo(m - 1, fromStack, bufferStack);
            MoveFromTo(1, fromStack, toStack);
            MoveFromTo(m - 1, bufferStack, toStack);
        }
    }

    private static void ValidateTowers(IList<IList<int>> towers)
    {
        Console.WriteLine("\n" + TowersToString(towers));
        if (towers.Any(tower => tower.Zip(tower.Skip(1)).Any(c => c.First <= c.Second)))
            throw new InvalidOperationException($"Invalid state:\n{TowersToString(towers)}");
    }

    private static string TowersToString(IList<IList<int>> towers)
    {
        return string.Join('\n', towers.Select(
            (tower, index) => $"Tower {index + 1} = [{string.Join(" ", tower)}]"));
    }

    public class Ex5_QueueTwoStacks<T>
    {
        private Stack<T> EnqueueStack { get; } = new Stack<T>();
        private Stack<T> DequeueStack { get; } = new Stack<T>();

        public int Count => EnqueueStack.Count + DequeueStack.Count;

        public void Enqueue(T value)
        {
            EnqueueStack.Push(value);
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");

            if (DequeueStack.Count > 0)
                return DequeueStack.Peek();

            while (EnqueueStack.Count > 0)
                DequeueStack.Push(EnqueueStack.Pop());

            return DequeueStack.Peek();
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Queue is empty");

            if (DequeueStack.Count > 0)
                return DequeueStack.Pop();

            while (EnqueueStack.Count > 1)
                DequeueStack.Push(EnqueueStack.Pop());

            return EnqueueStack.Pop();
        }
    }

    public static void Ex6_SortStack<T>(Stack<T> stack) where T : IComparable<T>
    {
        if (stack.Count == 0) return;

        var min = ExtractMin(stack);
        Ex6_SortStack(stack);
        stack.Push(min);

        static T ExtractMin(Stack<T> stack)
        {
            var top = stack.Pop();

            if (stack.Count == 0)
                return top;

            var min = ExtractMin(stack);
            if (top.CompareTo(min) <= 0)
            {
                stack.Push(min);
                return top;
            }
            else
            {
                stack.Push(top);
                return min;
            }
        }
    }

    public static void Ex6_SortStackWithAnotherStack<T>(Stack<T> stack) where T : IComparable<T>
    {
        var buffer = new Stack<T>();

        while (stack.Count > 0)
        {
            var top = stack.Pop();

            if (buffer.Count == 0 || buffer.Peek().CompareTo(top) <= 0)
            {
                buffer.Push(top);
                continue;
            }

            stack.Push(buffer.Pop());
            stack.Push(top);
        }

        while (buffer.Count > 0)
            stack.Push(buffer.Pop());
    }
}
