
class BonusCard : Card
{
    public BonusCard(int rank, string suit)
        : base(rank, suit)
    {
    }

    public override int Chips => base.Chips + 7;

    public override string ToString()
    {
        return $"{RankName}{Suit} [BONUS +7]";
    }
}
