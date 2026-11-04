using System;

class Program
{
    static void Main()
    {
        Console.OutputEncoding =
            System.Text.Encoding.UTF8;

        Game game = new Game();
        game.Start();
        Card normalCard = new Card(5, "♣");
        Card bonusCard = new BonusCard(5, "♣");

        Console.WriteLine(normalCard);
        Console.WriteLine($"Normale punten: {normalCard.Chips}");

        Console.WriteLine(bonusCard);
        Console.WriteLine($"Bonuspunten: {bonusCard.Chips}");
    }
}