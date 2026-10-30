
using System;
using System.Collections.Generic;

class Deck
{
    private readonly List<Card> cards = new();
    private readonly Random rng = new();

    public int Count => cards.Count;

    public Deck()
    {
        CreateDeck();
        Shuffle();
    }

    private void CreateDeck()
    {
        string[] suits = { "♠", "♥", "♦", "♣" };

        foreach (string suit in suits)
        {
            for (int rank = 2; rank <= 14; rank++)
            {
                cards.Add(new Card(rank, suit));
            }
        }
    }

    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Card? Draw()
    {
        if (cards.Count == 0)
            return null;

        Card card = cards[0];
        cards.RemoveAt(0);

        return card;
    }
}
