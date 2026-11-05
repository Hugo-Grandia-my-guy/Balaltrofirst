class Glass : Card
{
    public int Times { get; }

    public Glass(Value rank, Suit suit, int times = 2)
        : base(rank, suit)
    {
        Times = times;
    }

    public override string ToString()
    {
        return $"{base.ToString()} [GLASS x{Times}]";
    }
}
