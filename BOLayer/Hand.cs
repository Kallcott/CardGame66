using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();

        /// <summary>
        /// C# Indexer
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Card this[int index]
        {
            get
            {
                return cards[index];
            }
        }
        public int Count
        {
            get
            {
                return cards.Count();
            }
        }

        public void AddCard(Card newCard)
        {
            // the List<T>.Contains method cannot be used since it only checks if the same reference object exists
            if (ContainsCard(newCard))
            {
                throw new ConstraintException(newCard.FaceValue.ToString() + " of " +
                    newCard.Suit.ToString() + " already exists in the Hands");
            }
            cards.Add(newCard);
        }

        public void RemoveCard(Card c)
        {
            if (ContainsCard(c))
            {
                Card findCard = cards.Where(card => card.FaceValue == c.FaceValue && card.Suit == c.Suit).FirstOrDefault();
                cards.Remove(findCard);
            }
        }

        public void DiscardHand()

        {
            cards.Clear();
        }

        #region Private Methods
        private bool ContainsCard(Card cardToCheck)
        {
            foreach (Card card in cards)
            {
                if (card.FaceValue == cardToCheck.FaceValue && card.Suit == cardToCheck.Suit)
                {
                    return true;
                }
            }

            return false;
        }
        internal bool ContainsSuit(Suit suitToCheck)
        {
            foreach (Card card in cards)
            {
                if (card.Suit == suitToCheck)
                {
                    return true;
                }
            }

            return false;
        }

        public int getScore()
        {
            int score = 0;
            foreach (Card c in cards)
            {
                score += Convert.ToInt32(c.FaceValue);
            }

            score = score + ScoreMarriages();

            return score;
        }

        private int ScoreMarriages()
        {
            int marriageScore = 0;

            //find King
            foreach (Card card in cards)
            {
                if (card.FaceValue == FaceValueRank.King)
                {
                    Suit KingSuit = card.Suit;

                    foreach (Card c in cards)
                    {
                        if (c.Suit == KingSuit && c.FaceValue == FaceValueRank.Queen)
                        {
                            marriageScore = marriageScore + 20;
                        }
                    }
                }
            }

            return marriageScore;
            ;
        }
        #endregion
    }
}
