using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BOLayer;

namespace Cards
{
    public partial class Form1 : Form
    {
        private Board aBoard;
        private Player p1;
        private Player p2;

        public Form1()
        {
            InitializeComponent();
        }

        private void SetUp()
        {
                aBoard = new Board();
                aBoard.Setup();
                ShowHand(pnlTrump, aBoard.Trump);
        }

        private void btnHand1_Click(object sender, EventArgs e)
        {

            try
            {
                p1 = new Player();
                p1.PlayerHand = aBoard.Deck.DealHand(6);
                ShowHand(Panel1, p1.PlayerHand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHand2_Click(object sender, EventArgs e)
        {

            try
            {
                p2 = new Player();
                p2.PlayerHand = aBoard.Deck.DealHand(6);
                ShowHand(Panel2, p2.PlayerHand);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ShowHand(Panel thePanel, Hand theHand)
        {
            thePanel.Controls.Clear();
            Card aCard;
            PictureBox aPic;
            for (int i = 0; i < theHand.Count; i++)
            {
                aCard = theHand[i];
                string path = @"images\" + aCard.FaceValue.ToString() + aCard.Suit.ToString() + ".jpg";

                aPic = new PictureBox()
                {
                    Image = Image.FromFile(path),
                    Text = aCard.FaceValue.ToString(),
                    Width = 71,
                    Height = 100,
                    Left = 51 * i,
                    Tag = aCard // this allows the card to be identified for deletion when the picturebox is clicked
                };

                // Add a mouseDown event hanlder programmatically
                aPic.MouseDown += Card_MouseDown;

                thePanel.Controls.Add(aPic);
                aPic.BringToFront();
            }
        }

        PictureBox picDragged;
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            picDragged = (PictureBox)sender;
            Card c = (Card)picDragged.Tag;

            if (e.Button == MouseButtons.Left)
            {
                MessageBox.Show($"{c.FaceValue} of {c.Suit}");
                PlayCard(picDragged);
            }
            else if(e.Button == MouseButtons.Right)
            {
                MessageBox.Show($"Delete {c.FaceValue} of {c.Suit}");
                RemoveCard(picDragged);
            }

        }

        private void PlayCard(PictureBox picDragged)
        {
            Card c = (Card)picDragged.Tag;

            Panel cardPanel = (Panel)picDragged.Parent;

            List<Hand> gameHands = new List<Hand> { p1.PlayerHand, p2.PlayerHand };

            Hand theHandPlayed = null;

            foreach (Hand h in gameHands)
            {
                for (int i = 0; i < h.Count; i++)
                {

                    if (h[i] == c)
                    {
                        theHandPlayed = h;
                        break;
                    }
                }

                if (theHandPlayed != null) break;
            }
            if (theHandPlayed != null)
            {
                aBoard.PlayZone.AddCard(c);
                ShowHand(pnlPlayZone, aBoard.PlayZone);
                theHandPlayed.RemoveCard(c);
                ShowHand(cardPanel, theHandPlayed);
            }
            if (aBoard.PlayZone.Count == 2)
            {
                Card winningCard = aBoard.CompareCards(aBoard.PlayZone[0], aBoard.PlayZone[1]);
                string winningPlayer;
                if (winningCard.FaceValue == aBoard.PlayZone[0].FaceValue && winningCard.Suit == aBoard.PlayZone[0].Suit)
                {
                    // Please note leading player is not necesarrily player 1, add this later. 
                    winningPlayer = "Leading Player";
                    p1.Score.AddCard(aBoard.PlayZone[0]);
                    p1.Score.AddCard(aBoard.PlayZone[1]);
                    lblP1Score.Text = p1.Score.getScore().ToString();
                    ShowHand(pnlScoreP1, p1.Score);

                }
                else
                {
                    winningPlayer = "Non Leading Player";
                    p2.Score.AddCard(aBoard.PlayZone[0]);
                    p2.Score.AddCard(aBoard.PlayZone[1]);
                    lblP2Score.Text = p2.Score.getScore().ToString();
                    ShowHand(pnlScoreP2, p2.Score);
                }
                MessageBox.Show($"The winner is {winningPlayer} with the card: {winningCard.FaceValue} of {winningCard.Suit}");

                aBoard.PlayZone.DiscardHand();
                ShowHand(pnlPlayZone, aBoard.PlayZone);
                RoundEnd();
            }
        }

        private void RoundEnd()
        {
            p1.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]);
            ShowHand(Panel1, p1.PlayerHand);

            p2.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]);
            ShowHand(Panel2, p2.PlayerHand);
        }

        private void RemoveCard(PictureBox picDragged)
        {
            Card c = (Card)picDragged.Tag;

            Panel cardPanel = (Panel)picDragged.Parent;

            List<Hand> gameHands = new List<Hand> { p1.PlayerHand, p2.PlayerHand };

            Hand theHandPlayed = null;

            foreach (Hand h in gameHands)
            {
                for (int i = 0; i < h.Count; i++)
                {

                    if (h[i] == c)
                    {
                        theHandPlayed = h;
                        break;
                    }
                }

                if (theHandPlayed != null) break;
            }
            if (theHandPlayed != null)
            {
                theHandPlayed.RemoveCard(c);
                ShowHand(cardPanel, theHandPlayed);
            }
        }
        private void btnRedeal_Click(object sender, EventArgs e)
        {
            SetUp();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            SetUp();
        }
    }
}
