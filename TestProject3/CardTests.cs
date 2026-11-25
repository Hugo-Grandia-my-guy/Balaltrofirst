using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

[TestClass]
public class CardTests
{
    [TestMethod]
    public void Ace_ShouldGive11Chips()
    {
        Card card = new Card(Value.Ace, Suit.Spades);

        int chips = card.Chips;

        Assert.AreEqual(11, chips);
    }
}