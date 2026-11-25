
using ConsoleApp1.models;

namespace ConsoleApp1;
class Game
{
    private readonly Deck deck = new();
    private readonly PokerHandEvaluator evaluator = new();
    private readonly List<Card> hand = new();

    private int score = 0;
    private int handsLeft = 5;
    private int discardsLeft = 3;

    private const int HandSize = 8;
    private const int TargetScore = 300;
    private const int MaxCardsPlayed = 5;

    public void Start()
    {
        DrawCards(HandSize);

        Console.WriteLine("=== MINI BALATRO ===");
        Console.WriteLine($"Scoredoel: {TargetScore}");

        while (score < TargetScore && handsLeft > 0)
        {
            ShowStatus();
            ShowHand();

            Console.WriteLine();
            Console.WriteLine("P = spelen, W = weggooien, Q = stoppen");
            Console.Write("> ");

            string choice = (Console.ReadLine() ?? "")
                .Trim().ToUpperInvariant();

            if (choice == "Q")
                break;

            if (choice != "P" && choice != "W")
            {
                Console.WriteLine("Ongeldige keuze.");
                continue;
            }

            List<int> selected = ReadSelection();

            if (selected.Count == 0)
                continue;

            if (choice == "W")
            {
                if (discardsLeft <= 0)
                {
                    Console.WriteLine("Geen discards meer!");
                    continue;
                }

                RemoveSelectedCards(selected);
                DrawCards(selected.Count);
                discardsLeft--;

                Console.WriteLine("Kaarten vervangen.");
                continue;
            }

            List<Card> played = selected
                .Select(i => hand[i])
                .ToList();

            var result = evaluator.Evaluate(played);
            int bonusMult = played
                .OfType<MultCard>()
                .Sum(c => c.BonusMult);

            int glassMult = played
                .OfType<Glass>()
                .Aggregate(1, (current, card) => current * card.Times);

            int cardChips = played.Sum(c => c.Chips);
            int chips = result.BaseChips + cardChips;

            int totalMult = (result.Mult + bonusMult) * glassMult;

            int points = chips * totalMult;

            score += points;
            handsLeft--;

            Console.WriteLine();
            Console.WriteLine($"Basis Mult: x{result.Mult}");
            Console.WriteLine($"Bonus Mult: +{bonusMult}");
            Console.WriteLine($"Totale Mult: x{totalMult}");
            Console.WriteLine($"Punten verdiend: {points}");

            RemoveSelectedCards(selected);
            DrawCards(selected.Count);

            Console.WriteLine("Druk op Enter om door te gaan.");
            Console.ReadLine();
        }

        Console.WriteLine();

        if (score >= TargetScore)
            Console.WriteLine("GEWONNEN!");
        else if (handsLeft == 0)
            Console.WriteLine("GAME OVER!");
        else
            Console.WriteLine("Spel gestopt.");

        Console.WriteLine($"Eindscore: {score}/{TargetScore}");
    }

    private void DrawCards(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Card? card = deck.Draw();

            if (card == null)
                break;

            hand.Add(card);
        }
    }

    private void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Score: {score}/{TargetScore}");
        Console.WriteLine($"Handen over: {handsLeft}");
        Console.WriteLine($"Discards over: {discardsLeft}");
        Console.WriteLine($"Kaarten in deck: {deck.Count}");
        Console.WriteLine("------------------------------");
    }

    private void ShowHand()
    {
        Console.WriteLine();
        Console.WriteLine("JOUW HAND:");

        List<string[]> renderedCards = new();

        for (int i = 0; i < hand.Count; i++)
        {
            renderedCards.Add(RenderCard(hand[i], i + 1));
        }

        for (int row = 0; row < 7; row++)
        {
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Suit == Suit.Hearts ||
                    hand[i].Suit == Suit.Diamonds)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.Write(renderedCards[i][row] + "  ");
            }

            Console.WriteLine();
        }

        Console.ResetColor();
    }
    private string[] RenderCard(Card card, int number)
    {
        string suit = card.Suit switch
        {
            Suit.Spades => "♠",
            Suit.Hearts => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs => "♣",
            _ => "?"
        };

        string special = card switch
        {
            BonusCard => "BONUS +10",
            MultCard m => $"MULT +{m.BonusMult}",
            Glass g => $"GLASS x{g.Times}",
            _ => ""
        };

        return new string[]
        {
        "+-----------+",
        $"|{Center($"#{number}", 11)}|",
        "|           |",
        $"|{Center(card.RankName, 11)}|",
        $"|{Center(suit, 11)}|",
        $"|{Center(special, 11)}|",
        "+-----------+"
        };
    }
    private string Center(string text, int width)
    {
        if (text.Length > width)
            text = text.Substring(0, width);

        int left = (width - text.Length) / 2;

        return text.PadLeft(text.Length + left)
                   .PadRight(width);
    }
    private List<int> ReadSelection()
    {
        Console.WriteLine(
            $"Kies 1 t/m {Math.Min(MaxCardsPlayed, hand.Count)} kaarten, gescheiden door spaties.");
        Console.Write("Kaarten: ");

        string input = Console.ReadLine() ?? "";
        var selected = new List<int>();

        foreach (string part in input.Split(
            new[] { ' ', ',', ';', '\t' },
            StringSplitOptions.RemoveEmptyEntries))
        {
            if (!int.TryParse(part, out int number) ||
                number < 1 || number > hand.Count)
            {
                Console.WriteLine("Ongeldig kaartnummer.");
                return new List<int>();
            }

            int index = number - 1;

            if (selected.Contains(index))
            {
                Console.WriteLine("Je mag een kaart niet dubbel kiezen.");
                return new List<int>();
            }

            selected.Add(index);
        }

        if (selected.Count == 0 || selected.Count > MaxCardsPlayed)
        {
            Console.WriteLine("Kies minimaal 1 en maximaal 5 kaarten.");
            return new List<int>();
        }

        return selected;
    }

    private void RemoveSelectedCards(List<int> selected)
    {
        foreach (int index in selected.OrderByDescending(i => i))
            hand.RemoveAt(index);
    }
}
