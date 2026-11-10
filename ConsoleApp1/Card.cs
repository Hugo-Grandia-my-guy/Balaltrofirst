namespace ConsoleApp1;

public class Card
{
    public Value Rank { get; }
    public Suit Suit { get; }

    public Card(Value rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public virtual int Chips =>
        Rank == Value.Ace ? 11 : Math.Min((int)Rank, 10);

    public string RankName => Rank switch
    {
        Value.Jack => "J",
        Value.Queen => "Q",
        Value.King => "K",
        Value.Ace => "A",
        _ => ((int)Rank).ToString()
    };

    public override string ToString()
    {
        string suitSymbol = Suit switch
        {
            Suit.Spades => "♠",
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            _ => "?"
        };

        return $"{RankName}{suitSymbol}";
    }
}