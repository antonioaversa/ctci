using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using static CTCI.Exercises4;

namespace CTCI.Tests;

[TestClass]
public class Exercises4Tests
{
    [TestMethod]
    public void Ex1_TreeIsBalanced()
    {
        var tree = new Node(new() { });
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));
        tree.Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[0].Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[0].Children[0].Children.Add(new Node(new() { }));
        Assert.IsFalse(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[1].Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[1].Children[0].Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[1].Children[0].Children[0].Children.Add(new Node(new() { }));
        Assert.IsTrue(Exercises4.Ex1_TreeIsBalanced(tree));

        tree.Children[1].Children[0].Children[0].Children[0].Children.Add(new Node(new() { }));
        Assert.IsFalse(Exercises4.Ex1_TreeIsBalanced(tree));
    }

    [TestMethod]
    public void Ex2_GraphRoute()
    {
        var n1 = new Node(new() { });
        var n2 = new Node(new() { });
        var n3 = new Node(new() { });
        var n4 = new Node(new() { });
        var n5 = new Node(new() { });

        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n1, n1));
        Assert.IsFalse(Exercises4.Ex2_GraphRouteDfs(n1, n2));

        n1.Children.Add(n2);
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n1, n2));
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n2, n1));

        n2.Children.Add(n3);
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n2, n3));
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n1, n3));
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n3, n1));
        Assert.IsFalse(Exercises4.Ex2_GraphRouteDfs(n1, n4));

        n2.Children.Add(n4);
        Assert.IsFalse(Exercises4.Ex2_GraphRouteDfs(n3, n4));
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n1, n3));
        Assert.IsTrue(Exercises4.Ex2_GraphRouteDfs(n1, n4));
    }

    [TestMethod]
    public void Ex3_BSTFromSortedList()
    {
        var list = new List<int> { 1, 2, 4, 5, 6, 8, 10, 11 };
        var tree = Exercises4.Ex3_BSTFromSortedList(list);
        Assert.IsTrue(IsBST(tree));
        Assert.IsTrue(IsBalanced(tree));
    }

    private static bool IsBST<T>(BSTNode<T> node)
        where T : IComparable<T>
    {
        return RIsBST(node).Item1;

        static (bool, int, int) RIsBST(BSTNode<T>? node)
        {
            if (node == null) return (true, int.MaxValue, int.MinValue);
            
            var (bstLeft, minLeft, maxLeft) = RIsBST(node.Left);
            if (!bstLeft) return (false, int.MinValue, int.MaxValue);

            var (bstRight, minRight, maxRight) = RIsBST(node.Right);
            if (!bstRight) return (false, int.MinValue, int.MaxValue);

            var bst = maxLeft.CompareTo(node.Value) <= 0 && minRight.CompareTo(node.Value) >= 0;
            return (bst, minLeft, maxRight);
        }
    }

    private static bool IsBalanced<T>(BSTNode<T> node)
    {
        return MaxHeight(node) - MinHeight(node) <= 1;
        
        static int MaxHeight(BSTNode<T>? node)
        {
            if (node == null) return 0;
            return Math.Max(MaxHeight(node.Left), MaxHeight(node.Right)) + 1;
        }

        static int MinHeight(BSTNode<T>? node)
        {
            if (node == null) return 0;
            return Math.Min(MinHeight(node.Left), MinHeight(node.Right)) + 1;
        }
    }

    [TestMethod]
    public void Ex4_Levels()
    {
        var list = new List<int> { 1, 2, 4, 5, 6, 8, 10, 11 };
        var tree = Exercises4.Ex3_BSTFromSortedList(list);

        var levels = Exercises4.Ex4_Levels(tree);
        Assert.AreEqual(4, levels.Count);
        Assert.IsTrue(new[] { 5 }.SequenceEqual(levels[0].Select(n => n.Value)));
        Assert.IsTrue(new[] { 2, 8 }.SequenceEqual(levels[1].Select(n => n.Value)));
        Assert.IsTrue(new[] { 1, 4, 6, 10 }.SequenceEqual(levels[2].Select(n => n.Value)));
        Assert.IsTrue(new[] { 11 }.SequenceEqual(levels[3].Select(n => n.Value)));
    }

    [TestMethod]
    public void Ex5_NextNode()
    {
        var n1 = new BSTNodeWithParent();
        var n2 = new BSTNodeWithParent();
        var n3 = new BSTNodeWithParent();
        var n4 = new BSTNodeWithParent();
        var n5 = new BSTNodeWithParent();
        var n6 = new BSTNodeWithParent();
        var n7 = new BSTNodeWithParent();
        var n8 = new BSTNodeWithParent();

        n1.SetLeft(n2);
        n1.SetRight(n6);
        n2.SetLeft(n3);
        n2.SetRight(n4);
        n4.SetLeft(n5);
        n6.SetLeft(n7);
        n7.SetRight(n8);

        Assert.AreSame(n7, Exercises4.Ex5_NextNode(n1));
        Assert.AreSame(n5, Exercises4.Ex5_NextNode(n2));
        Assert.AreSame(n2, Exercises4.Ex5_NextNode(n3));
        Assert.AreSame(n1, Exercises4.Ex5_NextNode(n4));
        Assert.AreSame(n4, Exercises4.Ex5_NextNode(n5));
        Assert.IsNull(Exercises4.Ex5_NextNode(n6));
        Assert.AreSame(n8, Exercises4.Ex5_NextNode(n7));
        Assert.AreSame(n6, Exercises4.Ex5_NextNode(n8));
    }

    [TestMethod]
    public void Ex7_IsSubtree()
    {
        var t1 = BuildFromPreOrder(new List<int> { 8, 4, 2, 1, 3, 6, 5, 7, 12, 10, 9, 11, 14, 13, 15 });
        var t2 = BuildFromPreOrder(new List<int> { 6 }); // intermediate
        var t3 = BuildFromPreOrder(new List<int> { 3 }); // leaf
        var t4 = BuildFromPreOrder(new List<int> { 8 }); // root
        var t5 = BuildFromPreOrder(new List<int> { 4, 2 }); // left 2-chain
        var t6 = BuildFromPreOrder(new List<int> { 4, 6, 7 }); // right 3-chain
        var t7 = BuildFromPreOrder(new List<int> { 6, 5, 7 }); // bottom balanced 3-nodes tree
        var t8 = BuildFromPreOrder(new List<int> { 4, 2, 6 }); // intermediate balanced 3-nodes tree
        var t9 = BuildFromPreOrder(new List<int> { 4, 2, 7 }); // non-existent balanced 3-nodes tree


        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t2));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t3));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t4));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t5));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t6));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t7));
        Assert.IsTrue(Exercises4.Ex7_IsSubtree(t1, t8));
        Assert.IsFalse(Exercises4.Ex7_IsSubtree(t1, t9));
    }

    private static BSTNode<T>? BuildFromPreOrder<T>(IList<T> preOrder)
        where T : IComparable<T>
    {
        return RecursiveBuildFromPreOrder(preOrder, 0, default, false).Item1;
    }

    private static (BSTNode<T>?, int) RecursiveBuildFromPreOrder<T>(IList<T> preOrder, int i, T? max, bool maxDefined)
        where T : IComparable<T>
    {
        if (i >= preOrder.Count)
            return (null, i);
        
        var rootValue = preOrder[i];
        if (maxDefined && rootValue.CompareTo(max) >= 0)
            return (null, i);

        var (firstChild, nextIndex1) = RecursiveBuildFromPreOrder<T>(preOrder, i + 1, rootValue, true);
        var (secondChild, nextIndex2) = RecursiveBuildFromPreOrder<T>(preOrder, nextIndex1, max, maxDefined);
        return (new BSTNode<T>(rootValue, firstChild, secondChild), nextIndex2);
    }

    [TestMethod]
    public void Ex8_PathsSummingToTarget()
    {
        var n1 = new BSTNode<int>(1,
            new BSTNode<int>(1,
                new BSTNode<int>(0,
                    new BSTNode<int>(1, null, null),
                    null),
                new BSTNode<int>(1,
                    null,
                    new BSTNode<int>(2, null, null))),
            new BSTNode<int>(2,
                new BSTNode<int>(1, null, null),
                null));
        var n2 = n1.Left!;
        var n3 = n1.Right!;
        var n4 = n2.Left!;
        var n5 = n2.Right!;
        var n6 = n3.Left!;
        var n7 = n4.Left!;
        var n8 = n5.Right!;

        var paths1 = Exercises4.Ex8_PathsSummingToTarget(n1, 3);
        Assert.AreEqual(5, paths1.Count);
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4, n7 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n5 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n5, n8 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n3 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3, n6 })));

        var paths2 = Exercises4.Ex8_PathsSummingToTarget(n1, 2);
        Assert.AreEqual(6, paths2.Count);
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n4, n7 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n5 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n8 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3 })));
    }

    [TestMethod]
    public void Ex8_PathsSummingToTargetOptimized()
    {
        var n1 = new BSTNode<int>(1,
            new BSTNode<int>(1,
                new BSTNode<int>(0,
                    new BSTNode<int>(1, null, null),
                    null),
                new BSTNode<int>(1,
                    null,
                    new BSTNode<int>(2, null, null))),
            new BSTNode<int>(2,
                new BSTNode<int>(1, null, null),
                null));
        var n2 = n1.Left!;
        var n3 = n1.Right!;
        var n4 = n2.Left!;
        var n5 = n2.Right!;
        var n6 = n3.Left!;
        var n7 = n4.Left!;
        var n8 = n5.Right!;

        var paths1 = Exercises4.Ex8_PathsSummingToTargetOptimized(n1, 3);
        Assert.AreEqual(5, paths1.Count);
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4, n7 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n5 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n5, n8 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n3 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3, n6 })));

        var paths2 = Exercises4.Ex8_PathsSummingToTargetOptimized(n1, 2);
        Assert.AreEqual(6, paths2.Count);
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n4, n7 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n5 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n8 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3 })));
    }

    [TestMethod]
    public void Ex8_PathsSummingToTargetEarly()
    {
        var n1 = new BSTNode<int>(1,
            new BSTNode<int>(1,
                new BSTNode<int>(0,
                    new BSTNode<int>(1, null, null),
                    null),
                new BSTNode<int>(1,
                    null,
                    new BSTNode<int>(2, null, null))),
            new BSTNode<int>(2,
                new BSTNode<int>(1, null, null),
                null));
        var n2 = n1.Left!;
        var n3 = n1.Right!;
        var n4 = n2.Left!;
        var n5 = n2.Right!;
        var n6 = n3.Left!;
        var n7 = n4.Left!;
        var n8 = n5.Right!;

        var paths1 = Exercises4.Ex8_PathsSummingToTargetEarly(n1, 3);
        Assert.AreEqual(5, paths1.Count);
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4, n7 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n5 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n5, n8 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n3 })));
        Assert.IsTrue(paths1.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3, n6 })));

        var paths2 = Exercises4.Ex8_PathsSummingToTargetEarly(n1, 2);
        Assert.AreEqual(6, paths2.Count);
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n1, n2, n4 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n4, n7 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n2, n5 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n8 })));
        Assert.IsTrue(paths2.Any(path => path.SequenceEqual(new List<BSTNode<int>> { n3 })));
    }
}
