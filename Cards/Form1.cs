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


// TODO
// Validate Setting the User's name is unique. 

namespace Cards
{
    public partial class Form1 : Form
    {
        private Board aBoard;

        //This will hold the result of ShowDialogueBox()
        string txtResult;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            SetUp();
            PlayerNameToFormControls(aBoard.P1, nameof(aBoard.P1));
            PlayerNameToFormControls(aBoard.P2, nameof(aBoard.P2));
        }

        #region User Input

        private void btnRedeal_Click(object sender, EventArgs e)
        {
            SetUp();
        }

        /// <summary>
        /// Watches for mouse clicks on cards
        /// Holds rules for special restrictions if trump is drawn. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Card_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picDragged;
            picDragged = (PictureBox)sender;
            Card c = (Card)picDragged.Tag;

            //For left click
            if (e.Button == MouseButtons.Left)
            {
                //MessageBox.Show($"{c.FaceValue} of {c.Suit}");

                // Can only play from P1, or P2 hands
                if ((picDragged.Parent.Name.Substring(0, 7) == "pnlHand" &&
                    picDragged.Parent.Tag.ToString() == aBoard.PlayerWhoCanPlay.PlayerName))
                {

                    // Case: Trump Card is leading card
                    // Once the deck is empty. The trump is the leading card played. The following player must match it's suit if possible. 
                    if (aBoard.PlayZone.Count == 1 && !pnlTrump.HasChildren)
                    {
                        // Player doesn't have a card that matches the trump card
                        if (!aBoard.HaveCardThatMatchsTrumpSuit(aBoard.Where(picDragged.Parent.Tag.ToString()).PlayerHand))
                        {
                            PlayCard(picDragged);
                        }
                        // Can match Trump Card.
                        else
                        {
                            if (c.Suit == aBoard.Trump.Suit)
                            {
                                PlayCard(picDragged);
                            }
                            else
                            {
                                MessageBox.Show("You must play a card that matches the suit of the leading card. (" + aBoard.Trump.Suit + ")");
                            }
                        }
                    }

                    // Normal Play
                    else
                    {
                        PlayCard(picDragged);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show($"Delete {c.FaceValue} of {c.Suit}");

                //if (picDragged.Parent.Tag == pnlHandP1.Controls &&
                //    picDragged.Parent.Tag == pnlHandP2.Controls &&
                //    picDragged.Parent.Tag.ToString() == aBoard.PlayerWhoCanPlay.PlayerName)
                //{
                //    RemoveCard(picDragged);
                //}
            }
        }

        /// <summary>
        /// Chooses between two different dialoge boxes.
        /// One Path ask for user name input
        /// Second path asks for the coinflip. 
        /// </summary>
        /// <param name="value"></param>
        public void ShowMyDialogBox(string value)
        {
            Form2 testDialog = new Form2();

            testDialog.lblAsk.Text = value;

            // Path if Dialog is for Name Entry
            if (value == "Please enter your name")
            {
                testDialog.TextBox1.Visible = true;
                // Show testDialog as a modal dialog and determine if DialogResult = OK.
                TryAgain:
                testDialog.TextBox1.Text = "";
                if (testDialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (testDialog.TextBox1.Text.Length >= 6 )
                    {
                        MessageBox.Show("Please enter a name less than 6 characters");

                        goto TryAgain;
                    }
                    else
                    {
                    // Read the contents of testDialog's TextBox.
                    this.txtResult = testDialog.TextBox1.Text;

                    }
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            // Path if Dialogue if for Coinflip
            else if (value == "Which player takes heads")
            {
                testDialog.comboBox1.Visible = true;
                testDialog.comboBox1.Items.Add(aBoard.P1.PlayerName);
                testDialog.comboBox1.Items.Add(aBoard.P2.PlayerName);

                if (testDialog.ShowDialog(this) == DialogResult.OK)
                {
                    // Read the contents of testDialog's TextBox.
                    this.txtResult = testDialog.comboBox1.SelectedItem.ToString();
                }
                else
                {
                    Environment.Exit(1);
                }
            }

            testDialog.Dispose();
        }

        #endregion

        #region BoardState

        /// <summary>
        /// Sets up the board. 
        /// </summary>
        private void SetUp()
        {
            aBoard = new Board();
            aBoard.Setup();
            ShowHand(pnlTrump, aBoard.Trump);
        }

        /// <summary>
        /// Prompts which player wants heads, then provides a 50/50 coin flip.
        /// </summary>
        private void Coinflip()
        {
            ShowMyDialogBox("Which player takes heads");
            aBoard.Coinflip(txtResult);
        }

        /// <summary>
        /// Chooses between 3 different draw states, then updates the board.
        /// </summary>
        /// <param name="winningPlayer"></param>
        private void DrawPhase(Player trickWinner)
        {
            Player trickLoser = aBoard.OtherPlayer(trickWinner);

            // Normal Draw
            if (aBoard.Deck.Count >= 3)
            {
                trickWinner.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]);
                trickLoser.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]);
            }

            // Special draw from Trump
            else if (aBoard.Deck.Count > 1)
            {
                trickWinner.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]);
                trickLoser.PlayerHand.AddCard(aBoard.Deck.DealHand(1)[0]); // The last card of the deck is the Trump
                pnlTrump.Controls.Clear();
            }
            // No Cards in Deck or Trump, No draw

            ShowHand(pnlHandP1, aBoard.P1.PlayerHand);
            ShowHand(pnlHandP2, aBoard.P2.PlayerHand);

        }

        /// <summary>
        /// Scores the playzone, tallies to labels, then completes draw. Also checks if round has been won. 
        /// </summary>
        private void ComputePlayzone()
        {

            //The math is done from the class library.
            Tuple<Player, Card> WinnerInfo = aBoard.ComputePlayzone();
            Player trickWinner = WinnerInfo.Item1;
            Card winningCard = WinnerInfo.Item2;
            MessageBox.Show($"The winner is {trickWinner.PlayerName} with the card: {winningCard.FaceValue} of {winningCard.Suit}");

            // Update Score Point Total and Score Panel
            FindLabel(trickWinner, "lblScore").Text = trickWinner.Score.getScore().ToString();
            Panel panel = FindPanel(trickWinner, "pnlScore");
            ShowHand(panel, trickWinner.Score);

            // Checks if the round has been won
            if (aBoard.IfRoundEnd(out Player roundWinner, trickWinner) != null)
            {
                aBoard.RoundEnd(roundWinner);
                RoundEnd(roundWinner);
                return;
            }

            // Draw Cards
            DrawPhase(trickWinner);

        }

        /// <summary>
        /// Ends the round, awards points, housekeeping.
        /// </summary>
        /// <param name="roundWinner"></param>
        public void RoundEnd(Player roundWinner)
        {
            // Update GamePoints
            FindLabel(roundWinner, "lblGamePoints").Text = roundWinner.GamePoints.ToString();
            MessageBox.Show($"{ roundWinner.PlayerName} won the round!");

            //Clear Score
            FindLabel(aBoard.P1, "lblScore").Text = aBoard.P1.Score.getScore().ToString();
            FindLabel(aBoard.P2, "lblScore").Text = aBoard.P2.Score.getScore().ToString();

            //Refresh Hand
            ShowHand(pnlHandP1, aBoard.P1.PlayerHand);
            ShowHand(pnlHandP2, aBoard.P2.PlayerHand);
            ShowHand(pnlScoreP1, aBoard.P1.Score);
            ShowHand(pnlScoreP2, aBoard.P2.Score);
            ShowHand(pnlTrump, aBoard.Trump);

            if (roundWinner.GamePoints >= 7)
            {
                MessageBox.Show($"Congradulation! {roundWinner.PlayerName} is the winner!");
            }
        }

        #endregion

        #region Card Usage

        /// <summary>
        /// Displays cards in a panel for a given Hand
        /// </summary>
        /// <param name="thePanel"></param> the panel to display to
        /// <param name="theHand"></param> the hand to display
        private void ShowHand(Panel thePanel, Hand theHand)
        {
            // Clears the control
            thePanel.Controls.Clear();

            //Creates the picture box for each card
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

                //adds the controls
                thePanel.Controls.Add(aPic);
                aPic.BringToFront();
            }
        }

        /// <summary>
        /// Display a card given a single card. This is used for The trump card
        /// </summary>
        /// <param name="thePanel"></param> the panel to display to.
        /// <param name="theCard"></param> the card to display
        private void ShowHand(Panel thePanel, Card theCard)
        {
            // Clears panel to refresh
            thePanel.Controls.Clear();

            // Builds card Image
            Card aCard;
            PictureBox aPic;
            aCard = theCard;
            string path = @"images\" + aCard.FaceValue.ToString() + aCard.Suit.ToString() + ".jpg";
            aPic = new PictureBox()
            {
                Image = Image.FromFile(path),
                Text = aCard.FaceValue.ToString(),
                Width = 71,
                Height = 100,
                Left = 0,
                Tag = aCard // this allows the card to be identified for deletion when the picturebox is clicked
            };

            // Add a mouseDown event hanlder programmatically
            aPic.MouseDown += Card_MouseDown;

            thePanel.Controls.Add(aPic);
            aPic.BringToFront();
        }

        private void RemoveCard(PictureBox picDragged)
        {
            Card c = (Card)picDragged.Tag;

            Panel cardPanel = (Panel)picDragged.Parent;

            Player currentPlayer = aBoard.Where(picDragged.Parent.Tag.ToString());

            if (currentPlayer != null)
            {
                currentPlayer.PlayerHand.RemoveCard(c);
                ShowHand(cardPanel, currentPlayer.PlayerHand);
            }
        }

        /// <summary>
        /// Active cards will be sent to the playzone, then 
        /// </summary>
        /// <param name="picDragged"></param> the card that has been clicked.
        private void PlayCard(PictureBox picDragged)
        {
            Card c = (Card)picDragged.Tag;

            Panel cardPanel = (Panel)picDragged.Parent;

            Player currentPlayer = aBoard.Where(picDragged.Parent.Tag.ToString());


            if (currentPlayer != null)
            {
                //add to playzone
                aBoard.PlayZone.AddCard(c);
                ShowHand(pnlPlayZone, aBoard.PlayZone);

                //remove from player
                currentPlayer.PlayerHand.RemoveCard(c);
                ShowHand(cardPanel, currentPlayer.PlayerHand);

                //Updates the Board State
                UpdateTurnState();
                aBoard.SetLeadingPlayer(currentPlayer);
                TrickEndIng();
                ShowHand(pnlPlayZone, aBoard.PlayZone);
            }
        }

        /// <summary>
        /// Passes turn to winning player, and highlights them.
        /// </summary>
        private void TrickEndIng()
        {
            if (aBoard.PlayZone.Count == 2)
            {
                UnHighlightPlayingPlayer();
                ComputePlayzone();
                HighlightPlayingPlayer();
            }
        }

        /// <summary>
        /// Passes turn and highlight to active player.
        /// </summary>
        private void UpdateTurnState()
        {
            UnHighlightPlayingPlayer();
            aBoard.PassTurn();
            HighlightPlayingPlayer();
        }

        #endregion

        #region User Experience

        /// <summary>
        /// Highlights the active Player
        /// </summary>
        public void HighlightPlayingPlayer()
        {
            Panel ActivePnl = FindPanel(aBoard.PlayerWhoCanPlay, "pnlHand");

            ActivePnl.Width = 335;
            ActivePnl.Height = 105;
            ActivePnl.BackColor = Color.Yellow;

        }

        /// <summary>
        /// Unhighlights the active player highlighted
        /// </summary>
        public void UnHighlightPlayingPlayer()
        {
            Panel ActivePnl = FindPanel(aBoard.PlayerWhoCanPlay, "pnlHand");

            ActivePnl.Width = default;
            ActivePnl.Height = default;
            ActivePnl.BackColor = default;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 1. Request Name Input.
        /// 2. Sets name and tags of form elements for internal reference.
        /// 3. Sets labels to personalize the UI to player Names
        /// </summary>
        /// <param name="player"></param>
        /// <param name="nameOfProperty"></param>
        private void PlayerNameToFormControls(Player player, string nameOfProperty)
        {
            try
            {
                // Requests user input
                ShowMyDialogBox("Please enter your name");
                player.PlayerName = txtResult;

                foreach (Control c in this.Controls)
                {
                    // Applies user's name to labels.
                    if (c.Name.Substring(c.Name.Length - 6) == "Zone" + nameOfProperty)
                    {
                        if (c.Name.Substring(0, 9) == "lblPlayer")
                        {
                            c.Text = player.PlayerName;
                        }
                        else
                        {
                            c.Text = player.PlayerName + c.Text;
                        }
                    }
                    // Renames the control names to playernames
                    else if (c.Name.Substring(c.Name.Length - 2) == nameOfProperty)
                    {
                        c.Name = c.Name.Substring(0, c.Name.Length - 2) + player.PlayerName;

                        // Assigns Hand tag for internal use
                        if (c.Name.Substring(0, 7) == "pnlHand")
                        {
                            c.Tag = player.PlayerName;
                        }
                    }
                }

                // Displays initial
                Panel panel = FindPanel(player, "pnlHand");
                ShowHand(panel, player.PlayerHand);

                // If both players are set, Triggers the coinflip. 
                if (aBoard.OtherPlayer(player).PlayerName != null)
                {
                    Coinflip();
                    HighlightPlayingPlayer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Query for a panel
        /// </summary>
        /// <param name="player"></param> the player the panel belongs to.
        /// <param name="pnlPrefix"></param> the prefix that identifies the panel.
        /// <returns></returns> the reference to the panel object
        private Panel FindPanel(Player player, string pnlPrefix)
        {
            return this
                .Controls
                .Find(pnlPrefix + player.PlayerName, false)
                .OfType<Panel>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Query for a label
        /// </summary>
        /// <param name="player"></param> the player the label belongs to.
        /// <param name="pnlPrefix"></param> the prefix that identifies the label.
        /// <returns></returns> the reference to the label object
        private Label FindLabel(Player player, string pnlPrefix)
        {
            return this
                .Controls
                .Find(pnlPrefix + player.PlayerName, false)
                .OfType<Label>()
                .FirstOrDefault();
        }

        #endregion
    }
}
