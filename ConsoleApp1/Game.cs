
using System;
using System.Collections.Generic;
using System.Linq;

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

            int cardChips = played.Sum(c => c.Chips);
            int chips = result.BaseChips + cardChips;
            int points = chips * result.Mult;

            score += points;
            handsLeft--;

            Console.WriteLine();
            Console.WriteLine($"Pokerhand: {result.Name}");
            Console.WriteLine($"Basischips: {result.BaseChips}");
            Console.WriteLine($"Kaartchips: {cardChips}");
            Console.WriteLine($"Multiplier: x{result.Mult}");
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
        Console.WriteLine("Jouw hand:");

        for (int i = 0; i < hand.Count; i++)
            Console.WriteLine($"{i + 1}. {hand[i]}");
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
