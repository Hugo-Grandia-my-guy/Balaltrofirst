using ConsoleApp1;
using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Game game = new Game();
        game.Start();
    }

}
