
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
        Suit[] suits =
        {
        Suit.Spades,
        Suit.Hearts,
        Suit.Diamonds,
        Suit.Clubs
    };

        foreach (Suit suit in suits)
        {
            for (int rank = 2; rank <= 14; rank++)
            {
                Value value = (Value)rank;

                if (value == Value.Seven)
                {
                    cards.Add(new BonusCard(value, suit));
                }
                else if (value == Value.Two)
                {
                    cards.Add(new MultCard(value, suit, 4));
                }
                else if (value == Value.Three)
                {
                    cards.Add(new Glass(value, suit, 2));
                }
                else
                {
                    cards.Add(new Card(value, suit));
                }
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
