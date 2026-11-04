
class MultCard : Card
{
    public int BonusMult { get; }

    public MultCard(int rank, string suit, int bonusMult = 2)
        : base(rank, suit)
    {
        BonusMult = bonusMult;
    }

    public override string ToString()
    {
        return $"{RankName}{Suit} [MULT +{BonusMult}]";
    }
}
