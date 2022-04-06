using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOLayer
{
    public class Player
    {
        private Hand playerHand = new Hand();
        private Hand score = new Hand();
        private int gamePoints;
        public bool IsLeadingTrick { get; set; }
        public int GamePoints { get; set; }
        public Hand PlayerHand { get { return playerHand; } set { playerHand = value; } }
        public Hand Score { get { return score; } set { score = value; } }

    }


}
