using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public class Board
    {
        private Hand playZone = new Hand();
        private Deck deck;
        private Player p1 = new Player();
        private Player p2 = new Player();
        private Player playerWhoCanPlay;
        private Player playerLeadingTrick;

        public Card Trump
        {
            get
            {
                if (deck.Count == 0)
                {
                    return playZone[0];
                }
                else
                {
                    return deck.Trump();
                }
            }
        }
        public Hand PlayZone { get { return playZone; } set { playZone = value; } }
        public Deck Deck { get { return deck; } }
        public Player P1 { get { return p1; } set { p1 = value; } }
        public Player P2 { get { return p2; } set { p2 = value; } }
        public Player PlayerWhoCanPlay { get { return playerWhoCanPlay; } set { playerWhoCanPlay = value; } }
        public Player PlayerLeadingTrick { get { return playerLeadingTrick; } }

        /// <summary>
        /// Gets the player out of context.
        /// </summary>
        /// <param name="p"></param> The player in context
        /// <returns></returns>
        public Player OtherPlayer(Player p)
        {
            if (p != P1)
            {
                return P1;
            }
            else
            {
                return P2;
            }
        }

        /// <summary>
        /// Sets the ability to play cards to the other player. 
        /// </summary>
        public void PassTurn()
        {
            if (PlayerWhoCanPlay == P1)
            {
                PlayerWhoCanPlay = P2;
            }
            else
            {
                PlayerWhoCanPlay = P1;
            }
        }


        #region BoardState

        /// <summary>
        /// This sets the initial board state.
        /// </summary>
        public void Setup()
        {
            deck = new Deck();
            Deck.Shuffle();

            P1.PlayerHand = Deck.DealHand(6);
            P2.PlayerHand = Deck.DealHand(6);
        }



        /// <summary>
        /// Identifies who played the first trick. 
        /// </summary>
        /// <param name="playerName"></param> this is the player who will lead the first trick.
        public void SetLeadingPlayer(Player playerName)
        {
            if (playZone.Count == 1)
            {
                playerLeadingTrick = playerName;
            }
        }
        #endregion

        #region BoardUtilities
        /// <summary>
        /// Searches the players on board using a string
        /// </summary>
        /// <param name="value"></param> the string value, typically from a from tag
        /// <returns></returns> returns Player
        public Player Where(string value)
        {
            if (P1.PlayerName == value)
            {
                return P1;
            }

            return P2;

        }


        #endregion

        #region GameRules
        /// <summary>
        /// Used at the start of the game. It will determin who will play first using a coin flip. 
        /// </summary>
        /// <param name="value"></param>
        public void Coinflip(string value)
        {
            Player playerHeads;
            Player playerTails;

            if (value == P1.PlayerName)
            {
                playerHeads = P1;
                playerTails = P2;
            }
            else
            {
                playerHeads = P2;
                playerTails = P1;
            }


            Random coinflip = new Random();
            int flip = coinflip.Next(1, 3);
            if (flip == 1)
            {
                PlayerWhoCanPlay = playerHeads;
            }
            else
            {
                PlayerWhoCanPlay = playerTails;
            }
        }

        /// <summary>
        /// This will compare cards.
        /// </summary>
        /// <param name="c1"></param> The card played by the leading player. 
        /// <param name="c2"></param> The card played by the lagging player.
        /// <returns></returns> returns the winning card. 
        public Card CompareCards(Card c1, Card c2)
        {
            Suit trumpSuit;
            if (Trump != null)
            {
                trumpSuit = Trump.Suit;
            }
            else
            {
                trumpSuit = c1.Suit;
            }

            // Case if both cards are trump suited, or neither are.
            if ((
                trumpSuit != c1.Suit && trumpSuit != c2.Suit) ||
                trumpSuit == c1.Suit && trumpSuit == c2.Suit)
            {
                if (c1.FaceValue < c2.FaceValue)
                {
                    return c2;
                }
                // Leading Player wins ties
                else
                {
                    return c1;
                }
            }
            else if (trumpSuit == c1.Suit && trumpSuit != c2.Suit)
            {
                return c1;
            }
            else
            {
                return c2;
            }
        }

        /// <summary>
        /// This changes the internal board state. Must be called before wiping the playzone. 
        /// </summary>
        /// <returns></returns> Tuple: Item 1 is the winning player, Item 2 is the winning card. 
        public Tuple<Player, Card> ComputePlayzone()
        {
            // Compare both cards
            Card winningCard = CompareCards(PlayZone[0], PlayZone[1]);
            Player trickWinner;

            // If winner card is played first, then leading player wins. 
            if (winningCard.FaceValue == PlayZone[0].FaceValue && winningCard.Suit == PlayZone[0].Suit)
            {
                // Please note leading player is not necesarrily player 1.  
                trickWinner = PlayerLeadingTrick;
            }
            else
            {
                trickWinner = OtherPlayer(PlayerLeadingTrick);
            }
            Tuple<Player, Card> WinnerInfo = new Tuple<Player, Card>(trickWinner, winningCard);

            // adds cards to the scorezone. 
            trickWinner.Score.AddCard(PlayZone[0]);
            trickWinner.Score.AddCard(PlayZone[1]);

            //passes turn to winning player
            PlayerWhoCanPlay = trickWinner;

            // clears board.
            PlayZone.DiscardHand();

            return WinnerInfo;
        }

        /// <summary>
        /// The conditions if the round will end. Sets the round Winner.
        /// </summary>
        /// <param name="roundWinner"></param> the player who one the round
        /// <returns></returns> the player who won the round.
        public Player IfRoundEnd(out Player roundWinner, Player winningPlayer)
        {
            if (P1.PlayerHand.Count == 0 || P2.PlayerHand.Count == 0)
            {
                return roundWinner = winningPlayer;
            }
            if (P1.Score.getScore() >= 66)
            {
                return roundWinner = P1;
            }
            if (P2.Score.getScore() >= 66)
            {
                return roundWinner = P2;
            }
            return roundWinner = null;
        }

        /// <summary>
        /// Applies points when the round ends, then updates the boardstate. 
        /// </summary>
        /// <param name="RoundWinner"></param> the player who won the round
        public void RoundEnd(Player RoundWinner)
        {
            int PointsAwared;
            if (OtherPlayer(RoundWinner).Score.getScore() <= 33)
            {
                PointsAwared = 2;
            }
            else if(OtherPlayer(RoundWinner).Score.getScore() == 0)
            {
                PointsAwared = 3;
            }
            else
            {
                PointsAwared = 1;
            }

            RoundWinner.GamePoints += PointsAwared;

            P1.PlayerHand.DiscardHand();
            P1.Score.DiscardHand();

            P2.PlayerHand.DiscardHand();
            P2.Score.DiscardHand();

            Setup();
        }

        public bool HaveCardThatMatchsTrumpSuit(Hand player)
        {
            if (player.ContainsSuit(Trump.Suit))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion



    }
}
