using System;
using System.Collections.Generic;
using System.Linq;

class Card
{
    public int Rank { get; }
    public string Suit { get; }

    public Card(int rank, string suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public int Chips => Rank == 14 ? 11 : Math.Min(Rank, 10);

    public string RankName => Rank switch
    {
        11 => "J",
        12 => "Q",
        13 => "K",
        14 => "A",
        _ => Rank.ToString()
    };

    public override string ToString()
    {
        return $"{RankName}{Suit}";
    }
}

class Program
{
    static readonly Random rng = new Random();

    const int TargetScore = 300;
    const int MaxDiscards = 3;
    const int HandSize = 8;
    const int MaxHands = 5;

    static List<Card> deck = new();
    static List<Card> hand = new();

    static int score = 0;
    static int handsLeft = MaxHands;
    static int discardsLeft = MaxDiscards;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        CreateDeck();
        ShuffleDeck();
        DrawCards(HandSize);

        Console.WriteLine("=== MINI BALATRO ===");
        Console.WriteLine($"Scoredoel: {TargetScore}");
        Console.WriteLine();

        while (score < TargetScore && handsLeft > 0)
        {
            ShowStatus();
            ShowHand();

            Console.WriteLine();
            Console.WriteLine("Kies: P = spelen, W = weggooien, Q = stoppen");
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
            {
                Console.WriteLine("Selecteer minimaal 1 kaart.");
                continue;
            }

            if (choice == "W")
            {
                if (discardsLeft <= 0)
                {
                    Console.WriteLine("Je hebt geen discards meer!");
                    continue;
                }

                RemoveSelectedCards(selected);
                DrawCards(selected.Count);
                discardsLeft--;

                Console.WriteLine("Kaarten weggegooid en vervangen.");
                continue;
            }

            List<Card> played = selected
                .Select(i => hand[i])
                .ToList();

            var result = EvaluateHand(played);

            int cardChips = played.Sum(c => c.Chips);
            int chips = result.BaseChips + cardChips;
            int mult = result.Mult;
            int points = chips * mult;

            score += points;
            handsLeft--;

            Console.WriteLine();
            Console.WriteLine($"Pokerhand: {result.Name}");
            Console.WriteLine($"Basischips: {result.BaseChips}");
            Console.WriteLine($"Chips van kaarten: {cardChips}");
            Console.WriteLine($"Totaal chips: {chips}");
            Console.WriteLine($"Mult: x{mult}");
            Console.WriteLine($"Score: {chips} x {mult} = {points}");

            RemoveSelectedCards(selected);
            DrawCards(selected.Count);

            Console.WriteLine();
            Console.WriteLine($"Totale score: {score}/{TargetScore}");
            Console.WriteLine("Druk op Enter om door te gaan.");
            Console.ReadLine();
        }

        Console.WriteLine();

        if (score >= TargetScore)
            Console.WriteLine("GEWONNEN! Je hebt het scoredoel gehaald!");
        else if (handsLeft == 0)
            Console.WriteLine("GAME OVER! Je hebt niet genoeg punten gehaald.");
        else
            Console.WriteLine("Spel gestopt.");

        Console.WriteLine($"Eindscore: {score}/{TargetScore}");
    }

    static void CreateDeck()
    {
        string[] suits = { "♠", "♥", "♦", "♣" };

        deck.Clear();

        foreach (string suit in suits)
        {
            for (int rank = 2; rank <= 14; rank++)
                deck.Add(new Card(rank, suit));
        }
    }

    static void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }
    }

    static void DrawCards(int amount)
    {
        for (int i = 0; i < amount && deck.Count > 0; i++)
        {
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }

    static void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Score: {score}/{TargetScore}");
        Console.WriteLine($"Handen over: {handsLeft}");
        Console.WriteLine($"Discards over: {discardsLeft}");
        Console.WriteLine($"Kaarten in deck: {deck.Count}");
        Console.WriteLine("------------------------------");
    }

    static void ShowHand()
    {
        Console.WriteLine("Jouw hand:");

        for (int i = 0; i < hand.Count; i++)
            Console.WriteLine($"{i + 1}. {hand[i]}");

        Console.WriteLine();
        Console.WriteLine("Voer de nummers in, gescheiden door spaties.");
        Console.WriteLine("Voorbeeld: 1 3 5");
    }

    static List<int> ReadSelection()
    {
        Console.Write("Kaarten: ");
        string input = Console.ReadLine() ?? "";

        var numbers = new List<int>();

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

            if (numbers.Contains(number - 1))
            {
                Console.WriteLine("Je mag een kaart niet dubbel kiezen.");
                return new List<int>();
            }

            numbers.Add(number - 1);
        }

        if (numbers.Count > 5)
        {
            Console.WriteLine("Je mag maximaal 5 kaarten kiezen.");
            return new List<int>();
        }

        return numbers;
    }

    static void RemoveSelectedCards(List<int> selected)
    {
        foreach (int index in selected.OrderByDescending(i => i))
            hand.RemoveAt(index);
    }

    static (string Name, int BaseChips, int Mult)
        EvaluateHand(List<Card> cards)
    {
        var groups = cards
            .GroupBy(c => c.Rank)
            .Select(g => g.Count())
            .OrderByDescending(n => n)
            .ToList();

        bool flush = cards.Count == 5 &&
            cards.All(c => c.Suit == cards[0].Suit);

        var ranks = cards.Select(c => c.Rank)
            .Distinct()
            .OrderBy(r => r)
            .ToList();

        bool straight = false;

        if (cards.Count == 5 && ranks.Count == 5)
        {
            straight = ranks[4] - ranks[0] == 4;

            // A-2-3-4-5 is ook een straight.
            if (ranks.SequenceEqual(new[] { 2, 3, 4, 5, 14 }))
                straight = true;
        }

        if (straight && flush)
            return ("Straight Flush", 100, 8);

        if (groups[0] == 4)
            return ("Four of a Kind", 60, 7);

        if (groups.Count >= 2 &&
            groups[0] == 3 && groups[1] == 2)
            return ("Full House", 40, 4);

        if (flush)
            return ("Flush", 35, 4);

        if (straight)
            return ("Straight", 30, 4);

        if (groups[0] == 3)
            return ("Three of a Kind", 30, 3);

        if (groups.Count >= 2 &&
            groups[0] == 2 && groups[1] == 2)
            return ("Two Pair", 20, 2);

        if (groups[0] == 2)
            return ("Pair", 10, 2);

        return ("High Card", 5, 1);
    }
}
