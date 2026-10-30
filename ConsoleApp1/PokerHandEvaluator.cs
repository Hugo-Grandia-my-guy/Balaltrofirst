
using System;
using System.Collections.Generic;
using System.Linq;

class PokerHandEvaluator
{
    public (string Name, int BaseChips, int Mult)
        Evaluate(List<Card> cards)
    {
        if (cards == null || cards.Count == 0)
            throw new ArgumentException(
                "Selecteer minimaal één kaart.",
                nameof(cards));

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
