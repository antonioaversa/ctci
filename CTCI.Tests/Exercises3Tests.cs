using Microsoft.VisualStudio.TestTools.UnitTesting;
using static CTCI.Exercises3;

namespace CTCI.Tests;

[TestClass]
public class Exercises3Tests
{
    [TestMethod]
    public void Ex1_SingleArrayTripleStack()
    {
        var tstack = new Ex1_SingleArrayTripleStack<int>(10);
        tstack.Push(0, 3);
        Assert.AreEqual(1, tstack.GetCount(0));
        tstack.Push(1, 4);
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.AreEqual(4, tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));

        tstack.Push(0, 3);
        tstack.Push(0, 5);
        tstack.Push(1, 4);
        tstack.Push(2, 6);
        tstack.Push(2, 7);
        tstack.Push(2, 8);
        Assert.AreEqual(2, tstack.GetCount(0));
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.GetCount(2));

        Assert.AreEqual(5, tstack.Pop(0));
        Assert.AreEqual(8, tstack.Pop(2));
        Assert.AreEqual(7, tstack.Pop(2));
        Assert.AreEqual(4, tstack.Pop(1));
        tstack.Push(0, 2);
        tstack.Push(0, 1);
        Assert.AreEqual(1, tstack.Pop(0));
        Assert.AreEqual(2, tstack.Pop(0));

        Assert.AreEqual(1, tstack.GetCount(0));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.AreEqual(1, tstack.GetCount(2));

        tstack.Push(0, 2);
        tstack.Push(0, 3);
        tstack.Push(0, 4);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(0, 5));

        tstack.Push(1, 1);
        tstack.Push(1, 2);
        tstack.Push(1, 3);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(1, 4));

        tstack.Push(2, 2);
        tstack.Push(2, 3);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(2, 4));
    }

    [TestMethod]
    public void Ex1_SingleArrayTripleStackLessWaste()
    {
        var tstack = new Ex1_SingleArrayTripleStackLessWaste<int>(10);
        tstack.Push(0, 3);
        Assert.AreEqual(1, tstack.GetCount(0));
        tstack.Push(1, 4);
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.AreEqual(4, tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));

        tstack.Push(0, 3);
        tstack.Push(0, 5);
        tstack.Push(1, 4);
        tstack.Push(2, 6);
        tstack.Push(2, 7);
        tstack.Push(2, 8);
        Assert.AreEqual(2, tstack.GetCount(0));
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.GetCount(2));

        Assert.AreEqual(5, tstack.Pop(0));
        Assert.AreEqual(8, tstack.Pop(2));
        Assert.AreEqual(7, tstack.Pop(2));
        Assert.AreEqual(4, tstack.Pop(1));
        tstack.Push(0, 2);
        tstack.Push(0, 1);
        Assert.AreEqual(1, tstack.Pop(0));
        Assert.AreEqual(2, tstack.Pop(0));

        Assert.AreEqual(1, tstack.GetCount(0));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.AreEqual(1, tstack.GetCount(2));

        tstack.Push(0, 2);
        tstack.Push(0, 3);
        tstack.Push(0, 4);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(0, 5));

        tstack.Push(1, 1);
        tstack.Push(1, 2);
        tstack.Push(1, 3);
        tstack.Push(1, 4);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(1, 5));

        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(2, 2));
    }

    [TestMethod]
    public void Ex1_SingleArrayTripleStackNoWaste()
    {
        var tstack = new Ex1_SingleArrayTripleStackNoWaste<int>(10);
        tstack.Push(0, 3);
        Assert.AreEqual(1, tstack.GetCount(0));
        tstack.Push(1, 4);
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(0));
        Assert.AreEqual(0, tstack.GetCount(0));
        Assert.AreEqual(4, tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Pop(1));
        Assert.AreEqual(0, tstack.GetCount(1));

        tstack.Push(0, 3);
        tstack.Push(0, 5);
        tstack.Push(1, 4);
        tstack.Push(2, 6);
        tstack.Push(2, 7);
        tstack.Push(2, 8);
        Assert.AreEqual(2, tstack.GetCount(0));
        Assert.AreEqual(1, tstack.GetCount(1));
        Assert.AreEqual(3, tstack.GetCount(2));

        Assert.AreEqual(5, tstack.Pop(0));
        Assert.AreEqual(8, tstack.Pop(2));
        Assert.AreEqual(7, tstack.Pop(2));
        Assert.AreEqual(4, tstack.Pop(1));
        tstack.Push(0, 2);
        tstack.Push(0, 1);
        Assert.AreEqual(1, tstack.Pop(0));
        Assert.AreEqual(2, tstack.Pop(0));

        Assert.AreEqual(1, tstack.GetCount(0));
        Assert.AreEqual(0, tstack.GetCount(1));
        Assert.AreEqual(1, tstack.GetCount(2));

        tstack.Push(0, 2);
        tstack.Push(0, 3);
        tstack.Push(0, 4);
        tstack.Push(0, 5);

        tstack.Push(1, 1);
        tstack.Push(1, 2);
        tstack.Push(1, 3);
        tstack.Push(1, 4);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(1, 5));
        Assert.AreEqual(5, tstack.Pop(0));
        tstack.Push(1, 5);
        Assert.AreEqual(5, tstack.Pop(1));
        Assert.AreEqual(4, tstack.Pop(0));

        tstack.Push(2, 2);
        tstack.Push(2, 3);
        Assert.ThrowsException<InvalidOperationException>(() => tstack.Push(2, 4));
    }

    [TestMethod]
    public void Ex2_StackWithMin()
    {
        var stack = new Ex2_StackWithMin<int>();
        Assert.ThrowsException<InvalidOperationException>(() => stack.Min());
        stack.Push(7);
        stack.Push(3);
        Assert.AreEqual(3, stack.Min());
        stack.Push(5);
        Assert.AreEqual(3, stack.Min());
        stack.Push(4);
        Assert.AreEqual(3, stack.Min());
        stack.Push(2);
        Assert.AreEqual(2, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(7, stack.Min());
    }

    [TestMethod]
    public void Ex2_StackWithMinLessSpace()
    {
        var stack = new Ex2_StackWithMinLessSpace<int>();
        Assert.ThrowsException<InvalidOperationException>(() => stack.Min());
        stack.Push(7);
        stack.Push(3);
        Assert.AreEqual(3, stack.Min());
        stack.Push(5);
        Assert.AreEqual(3, stack.Min());
        stack.Push(4);
        Assert.AreEqual(3, stack.Min());
        stack.Push(2);
        Assert.AreEqual(2, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(3, stack.Min());
        stack.Pop();
        Assert.AreEqual(7, stack.Min());
    }

    [TestMethod]
    public void Ex3_SetOfStacks()
    {
        var stack = new Ex3_SetOfStacks<int>(2);
        Assert.ThrowsException<InvalidOperationException>(() => stack.Pop());
        stack.Push(7);
        stack.Push(3);
        Assert.AreEqual(3, stack.Pop());
        stack.Push(5);
        stack.Push(5);
        Assert.AreEqual(5, stack.Pop());
        Assert.AreEqual(5, stack.Pop());
        stack.Push(4);
        stack.Push(6);
        stack.Push(5);
        Assert.AreEqual(5, stack.Pop());
        Assert.AreEqual(6, stack.Pop());
        Assert.AreEqual(4, stack.Pop());
        Assert.AreEqual(7, stack.Pop());

        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        stack.Push(4);
        stack.Push(5);
        stack.Push(6);
        stack.Push(7);
        Assert.AreEqual(2, stack.Pop(0));
        Assert.AreEqual(4, stack.Pop(1));
        Assert.AreEqual(6, stack.Pop(2));
        Assert.AreEqual(7, stack.Pop(3));
        Assert.AreEqual(5, stack.Pop());
        Assert.AreEqual(3, stack.Pop());
        Assert.AreEqual(1, stack.Pop());
    }

    [TestMethod]
    public void Ex4_HanoiTowers()
    {
        Exercises3.Ex4_HanoiTowers(5);
    }

    [TestMethod]
    public void Ex5_QueueTwoStacks()
    {
        var queue = new Ex5_QueueTwoStacks<int>();
        Assert.AreEqual(0, queue.Count);
        queue.Enqueue(1);
        Assert.AreEqual(1, queue.Peek());
        queue.Enqueue(2);
        Assert.AreEqual(1, queue.Peek());
        queue.Enqueue(3);
        Assert.AreEqual(1, queue.Peek());
        Assert.AreEqual(3, queue.Count);
        Assert.AreEqual(1, queue.Dequeue());
        Assert.AreEqual(2, queue.Peek());
        Assert.AreEqual(2, queue.Dequeue());
        Assert.AreEqual(1, queue.Count);
        queue.Enqueue(4);
        Assert.AreEqual(2, queue.Count);
        queue.Enqueue(5);
        Assert.AreEqual(3, queue.Count);
        Assert.AreEqual(3, queue.Peek());
        Assert.AreEqual(3, queue.Dequeue());
        Assert.AreEqual(4, queue.Peek());
        Assert.AreEqual(4, queue.Dequeue());
        Assert.AreEqual(5, queue.Dequeue());
        Assert.ThrowsException<InvalidOperationException>(() => queue.Dequeue());
    }

    [TestMethod]
    public void Ex6_SortStack()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(7);
        stack.Push(5);
        stack.Push(2);
        stack.Push(3);
        stack.Push(9);
        stack.Push(1);
        stack.Push(3);
        stack.Push(2);
        stack.Push(8);
        stack.Push(8);

        Exercises3.Ex6_SortStack(stack);

        var items = stack.ToList();
        Assert.IsTrue(items.Zip(items.Skip(1)).All(c => c.First <= c.Second));
    }

    [TestMethod]
    public void Ex6_SortStackWithAnotherStack()
    {
        var stack = new Stack<int>();
        stack.Push(1);
        stack.Push(7);
        stack.Push(5);
        stack.Push(2);
        stack.Push(3);
        stack.Push(9);
        stack.Push(1);
        stack.Push(3);
        stack.Push(2);
        stack.Push(8);
        stack.Push(8);

        Exercises3.Ex6_SortStackWithAnotherStack(stack);

        var items = stack.ToList();
        Assert.IsTrue(items.Zip(items.Skip(1)).All(c => c.First <= c.Second));
    }
}
