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
        private string playerName;
        public int GamePoints { get { return gamePoints; } set { gamePoints = value; } }
        public Hand PlayerHand { get { return playerHand; } set { playerHand = value; } }
        public Hand Score { get { return score; } set { score = value; } }

        public string PlayerName { get { return playerName; } set { playerName = value; } }
    }


}
