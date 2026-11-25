using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using ConsoleApp1;
[TestClass]
public class PokerHandEvaluatorTests
{
    private readonly PokerHandEvaluator evaluator = new();

    [TestMethod]
    public void Pair_ShouldReturnPair()
    {
        // Arrange 
        var cards = new List<Card>
        {
            new Card(Value.Five, Suit.Clubs),
            new Card(Value.Five, Suit.Hearts),
            new Card(Value.Two, Suit.Spades),
            new Card(Value.Eight, Suit.Diamonds),
            new Card(Value.King, Suit.Clubs)
        };

        // Act 
        var result = evaluator.Evaluate(cards);

        // Assert 
        Assert.AreEqual("Pair", result.Name);
        Assert.AreEqual(10, result.BaseChips);
        Assert.AreEqual(2, result.Mult);
    }

    [TestMethod]
    public void Flush_ShouldReturnFlush()
    {
        // Arrange 
        var cards = new List<Card>
        {
            new Card(Value.Two, Suit.Hearts),
            new Card(Value.Five, Suit.Hearts),
            new Card(Value.Seven, Suit.Hearts),
            new Card(Value.Nine, Suit.Hearts),
            new Card(Value.King, Suit.Hearts)
        };

        // Act 
        var result = evaluator.Evaluate(cards);

        // Assert 
        Assert.AreEqual("Flush", result.Name);
    }

    [TestMethod]
    public void Straight_ShouldReturnStraight()
    {
        // Arrange 
        var cards = new List<Card>
        {
            new Card(Value.Five, Suit.Clubs),
            new Card(Value.Six, Suit.Hearts),
            new Card(Value.Seven, Suit.Spades),
            new Card(Value.Eight, Suit.Diamonds),
            new Card(Value.Nine, Suit.Clubs)
        };

        // Act 
        var result = evaluator.Evaluate(cards);

        // Assert 
        Assert.AreEqual("Straight", result.Name);
    }

    [TestMethod]
    public void EmptyHand_ShouldThrowException()
    {
        // Arrange 
        var cards = new List<Card>();

        // Act 
        bool exceptionThrown = false;

        try
        {
            evaluator.Evaluate(cards);
        }
        catch (ArgumentException)
        {
            exceptionThrown = true;
        }

        // Assert 
        Assert.IsTrue(
            exceptionThrown,
            "Evaluate should throw an ArgumentException for an empty hand."
        );
    }
}