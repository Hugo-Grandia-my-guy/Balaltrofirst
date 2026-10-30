using System;
using System.Collections.Generic;
using System.Text;

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
