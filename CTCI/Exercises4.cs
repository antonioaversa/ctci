using static CTCI.Exercises4;

namespace CTCI;

public class Exercises4
{
    public record Node(List<Node> Children);

    public static bool Ex1_TreeIsBalanced(Node node)
    {
        return MaxHeight(node) - MinHeight(node) <= 1;

        static int MaxHeight(Node n) => n.Children.Select(c => MaxHeight(c)).DefaultIfEmpty(0).Max() + 1;
        static int MinHeight(Node n) => n.Children.Select(c => MinHeight(c)).DefaultIfEmpty(0).Min() + 1;
    }

    public static bool Ex2_GraphRouteDfs(Node n1, Node n2)
    {
        return Reachable(n1, n2) || Reachable(n2, n1);

        static bool Reachable(Node from, Node to, HashSet<Node>? alreadyVisited = null)
        {
            alreadyVisited ??= new HashSet<Node>();

            if (from == to) return true;

            alreadyVisited.Add(from);
            foreach (var neighbor in from.Children)
                if (!alreadyVisited.Contains(neighbor))
                    if (Reachable(neighbor, to, alreadyVisited))
                        return true;

            return false;
        }
    }

    public record BSTNode<T>(T Value, BSTNode<T>? Left, BSTNode<T>? Right);

    public static BSTNode<T>? Ex3_BSTFromSortedList<T>(IList<T> l)
    {
        return RecursiveBSTFromSortedList<T>(l, 0, l.Count - 1);
    }

    private static BSTNode<T>? RecursiveBSTFromSortedList<T>(IList<T> l, int s, int e)
    {
        if (s > e) return null;
        if (s == e) return new BSTNode<T>(l[s], null, null);
        var m = s + (e - s) / 2;
        return new BSTNode<T>(
            l[m], RecursiveBSTFromSortedList(l, s, m - 1), RecursiveBSTFromSortedList(l, m + 1, e));
    }

    private record struct NodeAndLevel<T>(BSTNode<T> Node, int Level);

    public static IList<LinkedList<BSTNode<T>>> Ex4_Levels<T>(BSTNode<T>? root)
    {
        if (root == null) return Array.Empty<LinkedList<BSTNode<T>>>();

        var levels = new List<LinkedList<BSTNode<T>>>();
        var nodesQueue = new Queue<NodeAndLevel<T>>();
        nodesQueue.Enqueue(new(root, 0));
        while (nodesQueue.Count > 0)
        {
            var (node, level) = nodesQueue.Dequeue();

            if (level == levels.Count)
                levels.Add(new LinkedList<BSTNode<T>>());
            levels[level].AddLast(node);

            if (node.Left != null) nodesQueue.Enqueue(new(node.Left, level + 1));
            if (node.Right != null) nodesQueue.Enqueue(new(node.Right, level + 1));
        }

        return levels;
    }

    public class BSTNodeWithParent
    {
        public BSTNodeWithParent? Parent { get; private set; } = null;
        public BSTNodeWithParent? Left { get; private set; } = null;
        public BSTNodeWithParent? Right { get; private set; } = null;

        public void SetLeft(BSTNodeWithParent? left)
        {
            Left = left;
            if (left != null)
                left.Parent = this;
        }

        public void SetRight(BSTNodeWithParent? right)
        {
            Right = right;
            if (right != null)
                right.Parent = this;
        }
    };

    public static BSTNodeWithParent? Ex5_NextNode(BSTNodeWithParent? node)
    {
        if (node.Right != null)
        {
            var next = node.Right;
            while (next.Left != null)
                next = next.Left;
            return next;
        }
        if (node.Parent != null)
        {
            var next = node;
            while (next.Parent != null && next.Parent.Right == node)
                next = next.Parent;
            return next?.Parent;
        }

        return null;
    }

    public static bool Ex7_IsSubtree<T>(BSTNode<T> t1, BSTNode<T> t2)
        where T : IComparable<T>
    {
        return RecursiveIsSubtree(t1, t2);
    }

    private static bool RecursiveIsSubtree<T>(BSTNode<T>? t1, BSTNode<T>? t2)
        where T : IComparable<T>
    {
        if (t1 == null && t2 == null) return true;
        if (t1 == null) return false;
        if (t2 == null) return true;

        if (t1.Value.CompareTo(t2.Value) == 0)
        {
            if (RecursiveMatchSubtrees(t1, t2))
                return true;
        }
        if (RecursiveIsSubtree(t1.Left, t2))
            return true;
        if (RecursiveIsSubtree(t1.Right, t2))
            return true;
        return false;
    }

    private static bool RecursiveMatchSubtrees<T>(BSTNode<T>? t1, BSTNode<T>? t2)
        where T : IComparable<T>
    {
        if (t1 == null && t2 == null) return true;
        if (t1 == null) return false;
        if (t2 == null) return true;

        return t1.Value.CompareTo(t2.Value) == 0 &&
            RecursiveMatchSubtrees(t1.Left, t2.Left) &&
            RecursiveMatchSubtrees(t1.Right, t2.Right);
    }

    public static IList<IList<BSTNode<int>>> Ex8_PathsSummingToTarget(BSTNode<int> n, int t)
    {
        return RecursivePathsSummingToTarget(n, t).ToList();
    }

    private static IEnumerable<IList<BSTNode<int>>> RecursivePathsSummingToTarget(BSTNode<int>? n, int t)
    {
        if (n == null)
            yield break;

        foreach (var p in MathPathSummingToTarget(n, t))
            yield return p;
        foreach (var p in RecursivePathsSummingToTarget(n.Left, t))
            yield return p;
        foreach (var p in RecursivePathsSummingToTarget(n.Right, t))
            yield return p;
    }

    private static IEnumerable<IList<BSTNode<int>>> MathPathSummingToTarget(BSTNode<int> n, int t)
    {
        if (n.Value == t)
            yield return new List<BSTNode<int>> { n };

        if (n.Left != null)
            foreach (var p in MathPathSummingToTarget(n.Left, t - n.Value))
                yield return p.Prepend(n).ToList();

        if (n.Right != null)
            foreach (var p in MathPathSummingToTarget(n.Right, t - n.Value))
                yield return p.Prepend(n).ToList();
    }

    public static IList<IList<BSTNode<int>>> Ex8_PathsSummingToTargetOptimized(BSTNode<int> n, int t)
    {
        return RecursivePathsSummingToTargetOptimized(n, new List<BSTNode<int>> { }, t).ToList();
    }

    public static IList<IList<BSTNode<int>>> Ex8_PathsSummingToTargetEarly(BSTNode<int> n, int t)
    {
        return DfsWithPaths(n, new List<BSTNode<int>> { }, t).ToList();

        static IList<IList<BSTNode<int>>> DfsWithPaths(BSTNode<int> node, IList<BSTNode<int>> path, int target)
        {
            path.Add(node);

            var result = new List<IList<BSTNode<int>>> { };

            var sum = 0;
            for (var i = path.Count - 1; i >= 0; i--)
            {
                sum += path[i].Value;
                if (sum == target)
                    result.Add(path.TakeLast(path.Count - i).ToList());
            }

            if (node.Left != null)
                result.AddRange(DfsWithPaths(node.Left, path.ToList(), target));
            if (node.Right != null)
                result.AddRange(DfsWithPaths(node.Right, path.ToList(), target));
            return result;
        }
    }

    private static IEnumerable<IList<BSTNode<int>>> RecursivePathsSummingToTargetOptimized(
        BSTNode<int> n, IList<BSTNode<int>> path, int target)
    {
        path.Add(n);

        var sum = 0;
        for (var i = path.Count - 1; i >= 0; i--)
        {
            sum += path[i].Value;
            if (sum == target)
                yield return path.TakeLast(path.Count - i).ToList();
        }

        if (n.Left != null)
        {
            foreach (var p in RecursivePathsSummingToTargetOptimized(n.Left, path.ToList(), target))
                yield return p;
        }

        if (n.Right != null)
        {
            foreach (var p in RecursivePathsSummingToTargetOptimized(n.Right, path.ToList(), target))
                yield return p;
        }
    }
}
