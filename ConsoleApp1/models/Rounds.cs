namespace ConsoleApp1.models;

public class Round
{
    public int Score { get; set; } = 0;
    public int HandsLeft { get; set; } = 5;
    public int DiscardsLeft { get; set; } = 3;

    public const int TargetScore = 300;
}
