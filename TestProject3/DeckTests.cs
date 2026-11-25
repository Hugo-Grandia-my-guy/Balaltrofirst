
using System;
using ConsoleApp1.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DeckTests
{
    [TestMethod]
    public void NewDeck_ShouldContain52Cards()
    {
        // Arrange
        Deck deck = new Deck();

        // Act
        int count = deck.Count;

        // Assert
        Assert.AreEqual(52, count);
    }

    [TestMethod]
    public void DrawCard_ShouldReduceDeckCount()
    {
        // Arrange
        Deck deck = new Deck();

        // Act
        deck.Draw();

        // Assert
        Assert.AreEqual(51, deck.Count);
    }

    [TestMethod]
    public void DrawCard_ShouldNotBeNull()
    {
        // Arrange
        Deck deck = new Deck();

        // Act
        Card? card = deck.Draw();

        // Assert
        Assert.IsNotNull(card);
    }

    [TestMethod]
    public void EmptyDeck_ShouldReturnNull()
    {
        // Arrange
        Deck deck = new Deck();

        // Act
        for (int i = 0; i < 52; i++)
        {
            deck.Draw();
        }

        Card? card = deck.Draw();

        // Assert
        Assert.IsNull(card);
    }
}
