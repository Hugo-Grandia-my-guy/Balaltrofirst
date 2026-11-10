namespace ConsoleApp1;

public class MultCard : Card
{
    public int BonusMult { get; }

    public MultCard(Value rank, Suit suit, int bonusMult = 4)
        : base(rank, suit)
    {
        BonusMult = bonusMult;
    }

    public override string ToString()
    {
        return $"{base.ToString()} [MULT +{BonusMult}]";
    }
}