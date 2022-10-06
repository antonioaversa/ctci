using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Xml;
using static CTCI.Exercises2;

namespace CTCI.Tests
{
    [TestClass]
    public class Exercises2Tests
    {
        [DataRow(1, new int[] { }, new int[] { })]
        [DataRow(2, new[] { 1 }, new[] { 1 })]
        [DataRow(3, new[] { 1, 1 }, new[] { 1 })]
        [DataRow(4, new[] { 1, 2 }, new[] { 1, 2 })]
        [DataRow(5, new[] { 1, 2, 1 }, new[] { 1, 2 })]
        [DataRow(6, new[] { 1, 2, 1, 2, 1, 1 }, new[] { 1, 2 })]
        [DataRow(7, new[] { 1, 3, 3, 2, 1, 2, 3, 1, 1 }, new[] { 1, 3, 2 })]
        [DataTestMethod]
        public void Ex1_RemoveDuplicates(int id, int[] list, int[] expectedOutput)
        {
            var linkedList = new LinkedList<int>(list);
            Exercises2.Ex1_RemoveDuplicates(linkedList);
            Assert.IsTrue(linkedList.SequenceEqual(expectedOutput), 
                $"Id = {id}, Expected = [{string.Join(", ", expectedOutput)}], " +
                $"Actual = [{string.Join(", ", linkedList)}]");
        }

        [DataRow(1, new int[] { }, new int[] { })]
        [DataRow(2, new[] { 1 }, new[] { 1 })]
        [DataRow(3, new[] { 1, 1 }, new[] { 1 })]
        [DataRow(4, new[] { 1, 2 }, new[] { 1, 2 })]
        [DataRow(5, new[] { 1, 2, 1 }, new[] { 1, 2 })]
        [DataRow(6, new[] { 1, 2, 1, 2, 1, 1 }, new[] { 1, 2 })]
        [DataRow(7, new[] { 1, 3, 3, 2, 1, 2, 3, 1, 1 }, new[] { 1, 3, 2 })]
        [DataTestMethod]
        public void Ex1_RemoveDuplicatesNoBuffer(int id, int[] list, int[] expectedOutput)
        {
            var linkedList = new LinkedList<int>(list);
            Exercises2.Ex1_RemoveDuplicatesNoBuffer(linkedList);
            Assert.IsTrue(linkedList.SequenceEqual(expectedOutput),
                $"Id = {id}, Expected = [{string.Join(", ", expectedOutput)}], " +
                $"Actual = [{string.Join(", ", linkedList)}]");
        }

        [DataRow(1, new int[] { }, 1, null)]
        [DataRow(2, new[] { 1 }, 1, 1)]
        [DataRow(3, new[] { 1, 1 }, 1, 1)]
        [DataRow(4, new[] { 1, 1 }, 2, 1)]
        [DataRow(5, new[] { 1, 1 }, 3, null)]
        [DataRow(6, new[] { 1, 2 }, 1, 2)]
        [DataRow(7, new[] { 1, 2 }, 2, 1)]
        [DataRow(8, new[] { 1, 2 }, 3, null)]
        [DataRow(9, new[] { 1, 2 }, 4, null)]
        [DataRow(10, new[] { 1, 2, 1, 2, 1, 1 }, 3, 2)]
        [DataRow(11, new[] { 1, 2, 1, 2, 1, 1 }, 4, 1)]
        [DataRow(12, new[] { 1, 2, 1, 2, 1, 1 }, 6, 1)]
        [DataRow(13, new[] { 1, 2, 1, 2, 1, 1 }, 7, null)]
        [DataTestMethod]
        public void Ex2_FindNthToLast(int id, int[] list, int n, int? expectedOutput)
        {
            var linkedList = new LinkedList<int>(list);
            var (output, validOutput) = Exercises2.Ex2_FindNthToLast(linkedList, n);
            Assert.IsTrue((!validOutput && expectedOutput == null) || (validOutput && expectedOutput == output), 
                $"Id = {id}, Expected = [{expectedOutput}], Actual = [{(output, validOutput)}]");
        }

        [TestMethod]
        public void Ex3_DeleteNode()
        {
            BuildExample(
                out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode, 
                out var linkedList);

            Assert.AreEqual("1 2 3 4 5", linkedList.ToString());
            Exercises2.Ex3_DeleteNode(forthNode);
            Assert.AreEqual("1 2 3 5", linkedList.ToString());
            Assert.ThrowsException<Exception>(() => Exercises2.Ex3_DeleteNode(forthNode));
            Assert.AreEqual("1 2 3 5", linkedList.ToString());
            Exercises2.Ex3_DeleteNode(firstNode);
            Assert.AreEqual("2 3 5", linkedList.ToString());
            Exercises2.Ex3_DeleteNode(thirdNode);
            Assert.AreEqual("2 5", linkedList.ToString());
            Assert.ThrowsException<Exception>(() => Exercises2.Ex3_DeleteNode(thirdNode));
        }

        private static void BuildExample(
            out Exercises2.LinkedNode<int> firstNode, 
            out Exercises2.LinkedNode<int> secondNode, 
            out Exercises2.LinkedNode<int> thirdNode, 
            out Exercises2.LinkedNode<int> forthNode, 
            out Exercises2.LinkedNode<int> fifthNode, 
            out Exercises2.LinkedNode<int> linkedList)
        {
            linkedList = firstNode = new Exercises2.LinkedNode<int>
            {
                Value = 1,
                Next = secondNode = new Exercises2.LinkedNode<int>
                {
                    Value = 2,
                    Next = thirdNode = new Exercises2.LinkedNode<int>
                    {
                        Value = 3,
                        Next = forthNode = new Exercises2.LinkedNode<int>
                        {
                            Value = 4,
                            Next = fifthNode = new Exercises2.LinkedNode<int>
                            {
                                Value = 5,
                                Next = null,
                            }
                        }
                    }
                }
            };
        }

        [DataRow(1, new byte[] { }, new byte[] { }, new byte[] { })]
        [DataRow(2, new byte[] { 1 }, new byte[] { }, new byte[] { 1 })]
        [DataRow(3, new byte[] { }, new byte[] { 1, 2 }, new byte[] { 1, 2 })]
        [DataRow(4, new byte[] { 1 }, new byte[] { 2 }, new byte[] { 3 })]
        [DataRow(5, new byte[] { 1 }, new byte[] { 9 }, new byte[] { 0, 1 })]
        [DataRow(6, new byte[] { 1 }, new byte[] { 9, 9, 9, 9 }, new byte[] { 0, 0, 0, 0, 1 })]
        [DataRow(7, new byte[] { 1 }, new byte[] { 9, 9, 9, 9, 0 }, new byte[] { 0, 0, 0, 0, 1 })]
        [DataRow(7, new byte[] { 1, 9, 0, 9 }, new byte[] { 9, 0, 9, 0, 9 }, new byte[] { 0, 0, 0, 0, 0, 1 })]
        [DataRow(7, new byte[] { 1 }, new byte[] { 9, 9, 8, 9, 0 }, new byte[] { 0, 0, 9, 9, 0 })]
        [DataTestMethod]
        public void Ex4_Sum(int id, byte[] n1, byte[] n2, byte[] n3)
        {
            Assert.IsTrue(
                new LinkedList<byte>(n3).SequenceEqual(
                    Exercises2.Ex4_Sum(new LinkedList<byte>(n1), new LinkedList<byte>(n2))));
        }

        [TestMethod]
        public void Ex5_FindLoop()
        {
            Func<LinkedNode<int>?, LinkedNode<int>?> findLoop = Exercises2.Ex5_FindLoop;
            testFindLoop(findLoop);
        }

        [TestMethod]
        public void Ex5_FindLoop_NoHashSet()
        {
            Func<LinkedNode<int>?, LinkedNode<int>?> findLoop = Exercises2.Ex5_FindLoop_NoHashSet;
            testFindLoop(findLoop);
        }

        private static void testFindLoop(Func<LinkedNode<int>?, LinkedNode<int>?> findLoop)
        {
            Assert.IsNull(findLoop(null));

            {
                BuildExample(
                    out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode,
                    out var linkedList);

                Assert.IsNull(findLoop(linkedList));
            }
            {
                BuildExample(
                    out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode,
                    out var linkedList);

                fifthNode.Next = firstNode;

                Assert.AreSame(firstNode, findLoop(linkedList));
            }
            {
                BuildExample(
                    out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode,
                    out var linkedList);

                fifthNode.Next = secondNode;

                Assert.AreSame(secondNode, findLoop(linkedList));
            }
            {
                BuildExample(
                    out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode,
                    out var linkedList);

                secondNode.Next = secondNode;

                Assert.AreSame(secondNode, findLoop(linkedList));
            }
            {
                BuildExample(
                    out var firstNode, out var secondNode, out var thirdNode, out var forthNode, out var fifthNode,
                    out var linkedList);

                forthNode.Next = thirdNode;

                Assert.AreSame(thirdNode, findLoop(linkedList));
            }
        }
    }
}