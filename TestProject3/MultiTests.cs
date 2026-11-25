
using System;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MultCardTests
{
    [TestMethod]
    public void MultCard_ShouldHaveBonusMult4()
    {
        // Arrange
        MultCard card = new MultCard(
            Value.Two,
            Suit.Hearts,
            4
        );

        // Act
        int bonus = card.BonusMult;

        // Assert
        Assert.AreEqual(4, bonus);
    }

    [TestMethod]
    public void MultCard_DefaultBonus_ShouldBe4()
    {
        // Arrange
        MultCard card = new MultCard(
            Value.Two,
            Suit.Hearts
        );

        // Act
        int bonus = card.BonusMult;

        // Assert
        Assert.AreEqual(4, bonus);
    }

    [TestMethod]
    public void MultCard_ShouldDisplayBonus()
    {
        // Arrange
        MultCard card = new MultCard(
            Value.Two,
            Suit.Hearts,
            4
        );

        // Act
        string result = card.ToString();

        // Assert
        StringAssert.Contains(result, "MULT +4");
    }
}
