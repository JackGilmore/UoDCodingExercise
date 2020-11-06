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

            Console.WriteLine(deck.ToString());

            deck.Shuffle();

            Console.WriteLine("=====");

            Console.WriteLine(deck.ToString());

        }
    }

    public class DeckOfCards
    {
        public PlayingCard[] PlayingCards { get; set; }

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
            // TODO: Consider making this a prop so we can track what the top of the deck is
            int cardIndex = 0;

            // Loop through all suits (index i) and ranks (index j)                        
            for (int i = 0; i < suitCount; i++)
            {
                for (int j = 0; j < rankCount; j++)
                {
                    // Insert a new card into the deck with the current indexed suit and rank
                    PlayingCards[cardIndex] = new PlayingCard((PlayingCard.CardRank)j, (PlayingCard.CardSuit)i);
                    // Increment card index to move onto the next array slot
                    cardIndex++;
                }
            }
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

        public override string ToString()
        {
            return string.Join("\n", PlayingCards.Select(x => x.ToString()));
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
