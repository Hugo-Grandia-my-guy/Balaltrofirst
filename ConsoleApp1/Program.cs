
using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding =
            System.Text.Encoding.UTF8;

        Game game = new Game();
        game.Start();

        Card normalCard = new Card(
            Value.Five,
            Suit.Clubs
        );

        Card bonusCard = new BonusCard(
            Value.Five,
            Suit.Clubs
        );

        Card multiCard = new MultCard(
            Value.Two,
            Suit.Hearts,
            2
        );

        Console.WriteLine(normalCard);
        Console.WriteLine($"Normale punten: {normalCard.Chips}");

        Console.WriteLine(bonusCard);
        Console.WriteLine($"Bonuspunten: {bonusCard.Chips}");

        Console.WriteLine(multiCard);
        Console.WriteLine($"Bonus multiplier: +{((MultCard)multiCard).BonusMult}");
    }
}
