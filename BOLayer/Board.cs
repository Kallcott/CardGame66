using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public class Board
    {
        private Hand trump = new Hand();
        private Hand playZone = new Hand();
        private Deck deck = new Deck();
        private int leadingPlayer;
        public Hand Trump { get { return trump; } set { trump = value; } }
        public Hand PlayZone { get { return playZone; } set { playZone = value; } }
        public Deck Deck { get { return deck; } set { deck = value; } }
        public int LeadingPlayer { get { return leadingPlayer; } }
        public void Setup()
        {
            try
            {
                Random rGen = new Random();

                Deck.Shuffle();
                trump = Deck.DealHand(1);
                //leadingPlayer = rGen()
            }
            catch (Exception ex)
            {
                throw new Exception("Board Startup Failed");
            }
        }

        public Card CompareCards(Card c1, Card c2)
        {
            Suit trump = Trump[0].Suit;
            if ((trump != c1.Suit && trump != c2.Suit) || trump == c1.Suit && trump == c2.Suit)
            {
                if (c1.FaceValue > c2.FaceValue)
                {
                    return c1;
                }
                else
                {
                    return c2;
                }
            }
            else if (trump == c1.Suit && trump != c2.Suit)
            {
                return c1;
            }
            else
            {
                return c2;
            }
        }
    }
}
