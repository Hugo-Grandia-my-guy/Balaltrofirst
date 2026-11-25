public class Glass : Card
{
    public int Times { get; set; }

    public Glass(Value value, Suit suit, int times)
        : base(value, suit)
    {
        Times = times;
    }
}
