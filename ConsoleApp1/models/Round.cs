
using ConsoleApp1.models;
using System.Collections.Generic;

namespace ConsoleApp1.Models;

public class Round
{
    private readonly Deck deck = new();

    public int Score { get; private set; } = 0;
    public int TargetScore { get; private set; } = 300;

    public int HandsLeft { get; private set; } = 5;
    public int DiscardsLeft { get; private set; } = 3;

    public List<Card> Hand { get; } = new();

    public int DeckCount => deck.Count;

    public Round()
    {
        DrawCards(8);
    }

    public void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Card? card = deck.Draw();

            if (card == null)
                break;

            Hand.Add(card);
        }
    }

    // Hier komen straks:
    // PlayCards()
    // DiscardCards()
    // NextRound()
}
