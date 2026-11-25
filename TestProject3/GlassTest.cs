using ConsoleApp1.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class GlassTests
{
    [TestMethod]
    public void Glass_ShouldHaveMultiplier2()
    {
        // Arrange
        Glass glass = new Glass(
            Value.Four,
            Suit.Spades
        );

        // Act
        int multiplier = glass.Times;

        // Assert
        Assert.AreEqual(2, multiplier);
    }

    [TestMethod]
    public void Glass_CustomMultiplier_ShouldBe4()
    {
        // Arrange
        Glass glass = new Glass(
            Value.Four,
            Suit.Spades,
            4
        );

        // Act
        int multiplier = glass.Times;

        // Assert
        Assert.AreEqual(4, multiplier);
    }
}