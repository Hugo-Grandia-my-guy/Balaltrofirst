using System;
using System.Collections.Generic;
using System.Linq;

public class HandResult
{
    public string HandName { get; set; }
    public int BaseChips { get; set; }
    public int Mult { get; set; }
}

public class PokerHandEvaluator
{
    public HandResult Evaluate(List<Card> cards)
    {
        if (cards == null || cards.Count == 0)
        {
            return new HandResult
            {
                HandName = "High Card",
                BaseChips = 5,
                Mult = 1
            };
        }

        var values = cards
            .GroupBy(c => c.Value)
            .Select(g => g.Count())
            .OrderByDescending(x => x)
            .ToList();

        bool flush = cards
            .GroupBy(c => c.Suit)
            .Any(g => g.Count() == cards.Count);

        var ranks = cards
            .Select(c => (int)c.Value)
            .Distinct()
            .OrderBy(x => x)
            .ToList();

        bool straight = false;

        if (ranks.Count >= 5)
        {
            straight = ranks
                .Zip(ranks.Skip(1), (a, b) => b - a)
                .All(diff => diff == 1);
        }

        // Royal Flush
        if (flush && straight &&
            ranks.Contains(10) &&
            ranks.Contains(14))
        {
            return Result("Royal Flush", 100, 8);
        }

        // Straight Flush
        if (flush && straight)
            return Result("Straight Flush", 80, 8);

        // Four of a Kind
        if (values.Contains(4))
            return Result("Four of a Kind", 60, 7);

        // Full House
        if (values.Contains(3) && values.Contains(2))
            return Result("Full House", 40, 4);

        // Flush
        if (flush)
            return Result("Flush", 35, 4);

        // Straight
        if (straight)
            return Result("Straight", 30, 4);

        // Three of a Kind
        if (values.Contains(3))
            return Result("Three of a Kind", 30, 3);

        // Two Pair
        if (values.Count(x => x == 2) == 2)
            return Result("Two Pair", 20, 2);

        // Pair
        if (values.Contains(2))
            return Result("Pair", 10, 2);

        // High Card
        return Result("High Card", 5, 1);
    }

    private HandResult Result(string name, int chips, int mult)
    {
        return new HandResult
        {
            HandName = name,
            BaseChips = chips,
            Mult = mult
        };
    }
}
