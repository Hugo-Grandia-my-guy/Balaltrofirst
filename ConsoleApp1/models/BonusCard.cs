public class BonusCard : Card
{
    public BonusCard(Value value, Suit suit)
        : base(value, suit)
    {
        Chips += 10;
    }
}