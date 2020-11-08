using System;
using System.Collections.Generic;
using System.Linq;

namespace UoDCodingExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            DeckOfCards deck = new DeckOfCards();

            Console.WriteLine("Initial deck of cards:");
            Console.WriteLine(deck.ToString());
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Shuffled deck of cards:");
            deck.Shuffle();
            Console.WriteLine(deck.ToString());
            Console.ReadLine();

            Console.Clear();

            Console.WriteLine("Sorted deck of cards (by suit and then by rank)");
            deck.Sort();
            Console.WriteLine(deck.ToString());
            Console.ReadLine();

            Console.Clear();


            Console.WriteLine("Drawing top card:");
            PlayingCard topCard = deck.DealTopCard();
            Console.WriteLine(topCard.ToString());
            Console.ReadLine();

            Console.Clear();
        }
    }

    public class DeckOfCards
    {
        public PlayingCard[] PlayingCards { get; set; }
        private int CardIndex { get; set; }

        /// <summary>
        /// Initialize our deck of cards with a card of each rank and suit
        /// </summary>
        public DeckOfCards()
        {
            // Get the amount of values in the card suit and rank enums
            int suitCount = Enum.GetNames(typeof(PlayingCard.CardSuit)).Count();
            int rankCount = Enum.GetNames(typeof(PlayingCard.CardRank)).Count();

            // Multiply the values to get the maximum combination of cards (e.g. how many cards we have in a deck)
            int deckSize = suitCount * rankCount;

            // Initialize an array of cards with the length as the size of the deck we calculated
            PlayingCards = new PlayingCard[deckSize];

            // Index to track where we are inserting cards into the deck
            CardIndex = 0;

            // Loop through all suits (index i) and ranks (index j)                        
            for (int i = 0; i < suitCount; i++)
            {
                for (int j = 0; j < rankCount; j++)
                {
                    // Insert a new card into the deck with the current indexed suit and rank
                    PlayingCards[CardIndex] = new PlayingCard((PlayingCard.CardRank)j, (PlayingCard.CardSuit)i);

                    // Increment index of where we are in the deck
                    CardIndex++;
                }
            }

            // Decrement index by 1 to get to the top of the deck
            CardIndex--;
        }

        /// <summary>
        /// Outputs a list of cards in the deck with one card on each line
        /// </summary>
        /// <returns>A multi-line string of PlayingCard strings</returns>
        public override string ToString()
        {
            return string.Join("\n", PlayingCards.Select(x => x.ToString()));
        }

        /// <summary>
        /// Using the Fisher-Yates shuffle method, shuffle the deck of cards
        /// </summary>
        public void Shuffle()
        {
            // Random number generator
            Random random = new Random();
            // Loop through our deck of cards
            for (int i = 0; i < PlayingCards.Length; i++)
            {
                // Pick a random number between our current index and the size of the deck
                int randomCardIndex = random.Next(i, PlayingCards.Length);
                // Swap the cards in our current index and our randomly chosen index
                PlayingCard temp = PlayingCards[randomCardIndex];
                PlayingCards[randomCardIndex] = PlayingCards[i];
                PlayingCards[i] = temp;
            }
        }

        /// <summary>
        /// Sorts the deck in ascending order by suit and then by rank
        /// </summary>
        public void Sort()
        {
            PlayingCards = PlayingCards
                .OrderBy(s => s.Suit)
                .ThenBy(r => r.Rank)
                .ToArray();
        }

        /// <summary>
        /// Removes the top card from the deck and deals it
        /// </summary>
        /// <returns>The top card from the deck as a PlayingCard object</returns>
        public PlayingCard DealTopCard()
        {
            // Check that we still have cards in the deck first
            if (CardIndex >= 0)
            {
                // Get the top card in the list of current cards
                PlayingCard topCard = PlayingCards[CardIndex];

                // Set that entry in the array to be null as we have taken the card out
                PlayingCards[CardIndex] = null;

                // Decrement the card index by 1 to move to the next card which is now the top
                CardIndex--;

                // Return the top card
                return topCard;
            }
            else
            {
                throw new Exception("There are no cards left in the deck");
            }
        }
    }

    public class PlayingCard
    {
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }
        public PlayingCard(CardRank _rank, CardSuit _suit)
        {
            Rank = _rank;
            Suit = _suit;
        }
        /// <summary>
        /// Outputs the card's rank and suit in the format "Rank of Suit" as is traditional with cards
        /// </summary>
        /// <returns>A single line string with the rank and suit of a card</returns>
        public override string ToString()
        {
            return Rank + " of " + Suit;
        }

        public enum CardSuit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades,
        }

        public enum CardRank
        {
            Ace,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }
    }
}
