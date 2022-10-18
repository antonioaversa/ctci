namespace CTCI;

public static class Exercises13
{
    private class A 
    {
        public virtual int F(int a) => 1;
        public virtual int F(int a, int b) => 2;
    }

    private class B: A 
    {
        public override int F(int a) => 3;
    }

    public static void Ex6_VirtualAndOverloads()
    {
        A a = new A();
        a.F(10);
        a.F(10, 20);

        B b = new B();
        b.F(10);
        b.F(10, 20);

        A bAsA = new B();
        b.F(10);
        b.F(10, 20);
    }

    public record Node(Node? Left, Node? Right);

    public static Node Ex8_Copy(Node node)
    {
        return Copy(node, new Dictionary<Node, Node> { })!;

        static Node? Copy(Node? node, IDictionary<Node, Node> copies)
        {
            if (node == null) 
                return null;
            if (copies.TryGetValue(node, out var copy)) 
                return copy;
            
            var leftChildCopy = Copy(node.Left, copies);
            var rightChildCopy = Copy(node.Right, copies);
            copy = new Node(leftChildCopy, rightChildCopy);
            
            copies[node] = copy;
            return copy;
        }
    }
}
