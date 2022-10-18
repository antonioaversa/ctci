namespace CTCI.Tests;

using static CTCI.Exercises13;

[TestClass]
public class Exercises13Tests
{
    [TestMethod]
    public void Ex8_Copy()
    {
        var node1 = new Node(null, null);
        var node2 = new Node(node1, node1);
        var node3 = new Node(node2, node2);
        var copy3 = Exercises13.Ex8_Copy(node3);
        Assert.IsNotNull(copy3);
        Assert.AreSame(copy3.Left, copy3.Right);
        Assert.AreSame(copy3.Left?.Left, copy3.Left?.Right);
        Assert.AreSame(copy3.Right?.Left, copy3.Right?.Right);
        Assert.AreSame(copy3.Left?.Left, copy3.Right?.Left);
    }
}
