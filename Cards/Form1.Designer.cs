namespace Cards
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRedeal = new System.Windows.Forms.Button();
            this.btnHand2 = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHand1 = new System.Windows.Forms.Button();
            this.pnlTrump = new System.Windows.Forms.Panel();
            this.pnlPlayZone = new System.Windows.Forms.Panel();
            this.pnlScoreP1 = new System.Windows.Forms.Panel();
            this.pnlScoreP2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblP1Score = new System.Windows.Forms.Label();
            this.lblP2Score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRedeal
            // 
            this.btnRedeal.Location = new System.Drawing.Point(468, 388);
            this.btnRedeal.Margin = new System.Windows.Forms.Padding(2);
            this.btnRedeal.Name = "btnRedeal";
            this.btnRedeal.Size = new System.Drawing.Size(72, 33);
            this.btnRedeal.TabIndex = 12;
            this.btnRedeal.Text = "New Deck";
            this.btnRedeal.Click += new System.EventHandler(this.btnRedeal_Click);
            // 
            // btnHand2
            // 
            this.btnHand2.Location = new System.Drawing.Point(115, 388);
            this.btnHand2.Margin = new System.Windows.Forms.Padding(2);
            this.btnHand2.Name = "btnHand2";
            this.btnHand2.Size = new System.Drawing.Size(72, 33);
            this.btnHand2.TabIndex = 11;
            this.btnHand2.Text = "Deal Hand2";
            this.btnHand2.Click += new System.EventHandler(this.btnHand2_Click);
            // 
            // Panel2
            // 
            this.Panel2.Location = new System.Drawing.Point(17, 264);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(523, 102);
            this.Panel2.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Player 2";
            // 
            // Panel1
            // 
            this.Panel1.Location = new System.Drawing.Point(17, 31);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(523, 102);
            this.Panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Player 1";
            // 
            // btnHand1
            // 
            this.btnHand1.Location = new System.Drawing.Point(26, 388);
            this.btnHand1.Margin = new System.Windows.Forms.Padding(2);
            this.btnHand1.Name = "btnHand1";
            this.btnHand1.Size = new System.Drawing.Size(72, 33);
            this.btnHand1.TabIndex = 9;
            this.btnHand1.Text = "Deal Hand1";
            this.btnHand1.Click += new System.EventHandler(this.btnHand1_Click);
            // 
            // pnlTrump
            // 
            this.pnlTrump.Location = new System.Drawing.Point(17, 137);
            this.pnlTrump.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTrump.Name = "pnlTrump";
            this.pnlTrump.Size = new System.Drawing.Size(81, 110);
            this.pnlTrump.TabIndex = 11;
            // 
            // pnlPlayZone
            // 
            this.pnlPlayZone.Location = new System.Drawing.Point(191, 137);
            this.pnlPlayZone.Margin = new System.Windows.Forms.Padding(2);
            this.pnlPlayZone.Name = "pnlPlayZone";
            this.pnlPlayZone.Size = new System.Drawing.Size(207, 110);
            this.pnlPlayZone.TabIndex = 13;
            // 
            // pnlScoreP1
            // 
            this.pnlScoreP1.Location = new System.Drawing.Point(555, 31);
            this.pnlScoreP1.Margin = new System.Windows.Forms.Padding(2);
            this.pnlScoreP1.Name = "pnlScoreP1";
            this.pnlScoreP1.Size = new System.Drawing.Size(544, 102);
            this.pnlScoreP1.TabIndex = 16;
            // 
            // pnlScoreP2
            // 
            this.pnlScoreP2.Location = new System.Drawing.Point(555, 264);
            this.pnlScoreP2.Margin = new System.Windows.Forms.Padding(2);
            this.pnlScoreP2.Name = "pnlScoreP2";
            this.pnlScoreP2.Size = new System.Drawing.Size(544, 102);
            this.pnlScoreP2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(561, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Player 1\'s Score";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(561, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Player 2\'s Score";
            // 
            // lblP1Score
            // 
            this.lblP1Score.AutoSize = true;
            this.lblP1Score.Location = new System.Drawing.Point(660, 9);
            this.lblP1Score.Name = "lblP1Score";
            this.lblP1Score.Size = new System.Drawing.Size(13, 13);
            this.lblP1Score.TabIndex = 20;
            this.lblP1Score.Text = "0";
            // 
            // lblP2Score
            // 
            this.lblP2Score.AutoSize = true;
            this.lblP2Score.Location = new System.Drawing.Point(660, 249);
            this.lblP2Score.Name = "lblP2Score";
            this.lblP2Score.Size = new System.Drawing.Size(13, 13);
            this.lblP2Score.TabIndex = 21;
            this.lblP2Score.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 446);
            this.Controls.Add(this.lblP2Score);
            this.Controls.Add(this.lblP1Score);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnlScoreP2);
            this.Controls.Add(this.pnlScoreP1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlPlayZone);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.pnlTrump);
            this.Controls.Add(this.btnRedeal);
            this.Controls.Add(this.btnHand2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.btnHand1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnRedeal;
        internal System.Windows.Forms.Button btnHand2;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnHand1;
        internal System.Windows.Forms.Panel pnlTrump;
        internal System.Windows.Forms.Panel pnlPlayZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Panel pnlScoreP1;
        internal System.Windows.Forms.Panel pnlScoreP2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblP1Score;
        private System.Windows.Forms.Label lblP2Score;
    }
}