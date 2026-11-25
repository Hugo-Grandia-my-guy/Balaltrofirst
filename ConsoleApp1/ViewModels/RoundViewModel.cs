using ConsoleApp1.models;
namespace ConsoleApp1;

public class RoundViewModel
{
    private readonly Round round;

    // Properties voor de weergave
    public int Score { get; private set; }
    public int TargetScore { get; private set; }

    public int HandsLeft { get; private set; }
    public int DiscardsLeft { get; private set; }

    public int DeckCount { get; private set; }

    public List<Card> Hand { get; private set; } = new();

    // Model ontvangen
    public RoundViewModel(Round round)
    {
        this.round = round;

        RetrieveData();
    }

    // Stap 3: gegevens ophalen
    public void RetrieveData()
    {
        Score = round.Score;
        TargetScore = round.TargetScore;

        HandsLeft = round.HandsLeft;
        DiscardsLeft = round.DiscardsLeft;

        DeckCount = round.DeckCount;

        Hand = new List<Card>(round.Hand);
    }
    public void Render()
    {
        Console.Clear();

        Console.WriteLine("=== MINI BALATRO ===");
        Console.WriteLine($"Score: {Score}/{TargetScore}");

        Console.WriteLine("------------------------------");
        Console.WriteLine($"Handen over: {HandsLeft}");
        Console.WriteLine($"Discards over: {DiscardsLeft}");
        Console.WriteLine($"Kaarten in deck: {DeckCount}");
        Console.WriteLine("------------------------------");

        Console.WriteLine();
        Console.WriteLine("JOUW HAND:");

        for (int i = 0; i < Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Hand[i]}");
        }
    }
    public void HandleUserInput()
    {
        Console.WriteLine();
        Console.WriteLine("P = spelen");
        Console.WriteLine("W = weggooien");
        Console.WriteLine("Q = stoppen");

        Console.Write("> ");

        string choice = (Console.ReadLine() ?? "")
            .Trim()
            .ToUpperInvariant();

        switch (choice)
        {
            case "P":
                Console.WriteLine("Je wilt kaarten spelen!");
                // Later: round.PlayCards(...)
                break;

            case "W":
                Console.WriteLine("Je wilt kaarten weggooien!");
                // Later: round.DiscardCards(...)
                break;

            case "Q":
                Console.WriteLine("Spel gestopt.");
                break;

            default:
                Console.WriteLine("Ongeldige keuze.");
                break;
        }
    }
}