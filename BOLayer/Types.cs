using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum FaceValueRank
    {
        Ace = 11,
        Ten = 10,
        King = 4,
        Queen = 3,
        Jack = 2,
        Nine = 0
    }

    public enum BonusPoints
    {
        LastTrick = 10,
        TrumpMarriage = 40,
        Marriage = 20
    }
}
