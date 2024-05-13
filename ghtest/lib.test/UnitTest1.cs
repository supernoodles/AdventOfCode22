namespace lib.test;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod()
    {
        var system = new Class1();

        Assert.AreEqual(42, system.Test());
    }
}