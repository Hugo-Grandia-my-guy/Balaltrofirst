class BonusCard : Card
{
    public BonusCard(Value rank, Suit suit)
        : base(rank, suit)
    {
    }

    public override int Chips => base.Chips + 7;

    public override string ToString()
    {
        return $"{base.ToString()} [BONUS +7]";
    }
}
