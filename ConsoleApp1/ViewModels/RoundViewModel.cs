using ConsoleApp1.models;

namespace ConsoleApp1.ViewModels;

public class RoundViewModel
{
    private readonly Round round;

    public RoundViewModel(Round round)
    {
        this.round = round;
    }

    public int Score => round.Score;
    public int HandsLeft => round.HandsLeft;
    public int DiscardsLeft => round.DiscardsLeft;
    public int TargetScore => Round.TargetScore;
}