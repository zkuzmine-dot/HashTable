using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AVLTreeTests
{
    [TestMethod]
    public void InsertAndFindTest()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(1, "One");
        tree.Insert(2, "Two");

        Assert.IsTrue(tree.TryGetValue(1, out string value1));
        Assert.AreEqual("One", value1);
        Assert.IsTrue(tree.TryGetValue(2, out string value2));
        Assert.AreEqual("Two", value2);
    }

    [TestMethod]
    public void RemoveTest()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(1, "One");
        tree.Insert(2, "Two");
        tree.Insert(3, "Three");

        tree.Remove(2);
        Assert.IsFalse(tree.TryGetValue(2, out _));
        Assert.IsTrue(tree.TryGetValue(1, out _));
        Assert.IsTrue(tree.TryGetValue(3, out _));
    }

    [TestMethod]
    public void BalanceTest()
    {
        var tree = new AVLTree<int, string>();
        tree.Insert(3, "Three");
        tree.Insert(2, "Two");
        tree.Insert(1, "One"); // Требуется балансировка

        Assert.IsTrue(tree.TryGetValue(1, out _));
        Assert.IsTrue(tree.TryGetValue(2, out _));
        Assert.IsTrue(tree.TryGetValue(3, out _));
    }

    [TestMethod]
    public void EmptyTreeTest()
    {
        var tree = new AVLTree<int, string>();
        Assert.IsFalse(tree.TryGetValue(1, out _));
    }
}