public class MultCard : Card
{
    public int BonusMult { get; set; }

    public MultCard(Value value, Suit suit, int bonusMult)
        : base(value, suit)
    {
        BonusMult = bonusMult;
    }
}