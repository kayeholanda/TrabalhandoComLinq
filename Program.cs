using System;
using System.Collections.Generic;
using System.Linq;
using LinqFaroShuffle;

public class Program
{
    public static void Main(string[] args)
    {

        IEnumerable<string>? suits = Suits();
        IEnumerable<string>? ranks = Ranks();

        if ((suits is null) || (ranks is null))
            return;

        var startingDeck = (from s in Suits().LogQuery("Suit Generation")
                           from r in Ranks().LogQuery("Rank Generation")
                           select new { Suit = s, Rank = r })
                           .LogQuery("Starting Deck")
                           .ToArray();

        // var startingDeck = Suits().SelectMany(suit => Ranks().Select(rank => new { Suit = suit, Rank = rank }));

        foreach (var card in startingDeck)
        {
            Console.WriteLine(card);
        }

        Console.WriteLine();

        var times = 0;
        var shuffle = startingDeck;

        do
        {
            shuffle = shuffle.Skip(26)
                    .LogQuery("Bottom Half")
                    .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                    .LogQuery("Shuffle")
                    .ToArray();

            foreach (var card in shuffle)
            {
                Console.WriteLine(card);
            }

            times++;
            Console.WriteLine(times);
        } while (!startingDeck.SequenceEquals(shuffle));

        Console.WriteLine(times);

    }

   static IEnumerable<string> Suits()
    {
        yield return "clubs";
        yield return "diamonds";
        yield return "hearts";
        yield return "spades";
    }

    static IEnumerable<string> Ranks()
    {
        yield return "two";
        yield return "three";
        yield return "four";
        yield return "five";
        yield return "six";
        yield return "seven";
        yield return "eight";
        yield return "nine";
        yield return "ten";
        yield return "jack";
        yield return "queen";
        yield return "king";
        yield return "ace";
    }
}



