public class Card
{
    public Value Value { get; set; }
    public Suit Suit { get; set; }

    public int Chips { get; set; }

    public Card(Value value, Suit suit)
    {
        Value = value;
        Suit = suit;
        Chips = (int)value;
    }

    public string RankName
    {
        get
        {
            return Value switch
            {
                Value.Jack => "J",
                Value.Queen => "Q",
                Value.King => "K",
                Value.Ace => "A",
                _ => ((int)Value).ToString()
            };
        }
    }

    public override string ToString()
    {
        return $"{RankName} of {Suit}";
    }
}