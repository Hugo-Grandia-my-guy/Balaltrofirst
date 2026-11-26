using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards = new List<Card>();
    private Random random = new Random();

    public int Count => cards.Count;

    public Deck()
    {
        CreateDeck();
        Shuffle();
    }

    private void CreateDeck()
    {
        cards.Clear();

        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                if (value == Value.Seven)
                {
                    cards.Add(new BonusCard(value, suit));
                }
                else if (value == Value.Two)
                {
                    cards.Add(new MultCard(value, suit, 2));
                }
                else if (value == Value.Four && suit == Suit.Spades)
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
            int j = random.Next(i + 1);

            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card Draw()
    {
        if (cards.Count == 0)
            return null;

        Card card = cards[0];
        cards.RemoveAt(0);

        return card;
    }
}