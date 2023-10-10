namespace MtgOffline
{
    partial class Setup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setup));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PlayerOneTitle = new System.Windows.Forms.Label();
            this.PlayerOne = new System.Windows.Forms.ListBox();
            this.PlayerThree = new System.Windows.Forms.Panel();
            this.PlayerThreeDeckLabel = new System.Windows.Forms.Label();
            this.PlayerThreeBotLabel = new System.Windows.Forms.Label();
            this.PlayerThreeDecks = new System.Windows.Forms.ListBox();
            this.PlayerThreeLabel = new System.Windows.Forms.Label();
            this.PlayerThreeBots = new System.Windows.Forms.ListBox();
            this.PlayerTwo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerTwoBotLabel = new System.Windows.Forms.Label();
            this.PlayerTwoDecks = new System.Windows.Forms.ListBox();
            this.PlayerTwoTitle = new System.Windows.Forms.Label();
            this.PlayerTwoBots = new System.Windows.Forms.ListBox();
            this.PlayerFour = new System.Windows.Forms.Panel();
            this.PlayerFourDeckLabel = new System.Windows.Forms.Label();
            this.PlayerFourBotLabel = new System.Windows.Forms.Label();
            this.PlayerFourDecks = new System.Windows.Forms.ListBox();
            this.PlayerFourTitle = new System.Windows.Forms.Label();
            this.PlayerFourBots = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.JoinGame = new System.Windows.Forms.Button();
            this.DeckCollection = new System.Windows.Forms.ListBox();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.DecksButton = new System.Windows.Forms.Button();
            this.GameModesLabel = new System.Windows.Forms.Label();
            this.GameModeList = new System.Windows.Forms.ListBox();
            this.ViewMods = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PlayerThree.SuspendLayout();
            this.PlayerTwo.SuspendLayout();
            this.PlayerFour.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(249, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start Game";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.PlayerThree);
            this.panel1.Controls.Add(this.PlayerTwo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 527);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.panel3.Controls.Add(this.PlayerOneTitle);
            this.panel3.Controls.Add(this.PlayerOne);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(376, 90);
            this.panel3.TabIndex = 15;
            // 
            // PlayerOneTitle
            // 
            this.PlayerOneTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerOneTitle.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOneTitle.Location = new System.Drawing.Point(3, 3);
            this.PlayerOneTitle.Name = "PlayerOneTitle";
            this.PlayerOneTitle.Size = new System.Drawing.Size(361, 26);
            this.PlayerOneTitle.TabIndex = 3;
            this.PlayerOneTitle.Text = "Player 1";
            this.PlayerOneTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerOne
            // 
            this.PlayerOne.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerOne.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerOne.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerOne.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerOne.FormattingEnabled = true;
            this.PlayerOne.Items.AddRange(new object[] {
            "You are this player and cannot select a bot ",
            "nor a deck in the program. Ready your cards ",
            "on your play surface!!"});
            this.PlayerOne.Location = new System.Drawing.Point(3, 32);
            this.PlayerOne.Name = "PlayerOne";
            this.PlayerOne.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.PlayerOne.Size = new System.Drawing.Size(370, 52);
            this.PlayerOne.TabIndex = 2;
            // 
            // PlayerThree
            // 
            this.PlayerThree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerThree.Controls.Add(this.PlayerThreeDeckLabel);
            this.PlayerThree.Controls.Add(this.PlayerThreeBotLabel);
            this.PlayerThree.Controls.Add(this.PlayerThreeDecks);
            this.PlayerThree.Controls.Add(this.PlayerThreeLabel);
            this.PlayerThree.Controls.Add(this.PlayerThreeBots);
            this.PlayerThree.Location = new System.Drawing.Point(0, 309);
            this.PlayerThree.Name = "PlayerThree";
            this.PlayerThree.Size = new System.Drawing.Size(376, 218);
            this.PlayerThree.TabIndex = 12;
            // 
            // PlayerThreeDeckLabel
            // 
            this.PlayerThreeDeckLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerThreeDeckLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerThreeDeckLabel.Location = new System.Drawing.Point(3, 112);
            this.PlayerThreeDeckLabel.Name = "PlayerThreeDeckLabel";
            this.PlayerThreeDeckLabel.Size = new System.Drawing.Size(361, 21);
            this.PlayerThreeDeckLabel.TabIndex = 11;
            this.PlayerThreeDeckLabel.Text = "Deck:";
            this.PlayerThreeDeckLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerThreeBotLabel
            // 
            this.PlayerThreeBotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerThreeBotLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerThreeBotLabel.Location = new System.Drawing.Point(3, 35);
            this.PlayerThreeBotLabel.Name = "PlayerThreeBotLabel";
            this.PlayerThreeBotLabel.Size = new System.Drawing.Size(361, 17);
            this.PlayerThreeBotLabel.TabIndex = 10;
            this.PlayerThreeBotLabel.Text = "Bot:";
            this.PlayerThreeBotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerThreeDecks
            // 
            this.PlayerThreeDecks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerThreeDecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerThreeDecks.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerThreeDecks.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerThreeDecks.FormattingEnabled = true;
            this.PlayerThreeDecks.Items.AddRange(new object[] {
            "Decks"});
            this.PlayerThreeDecks.Location = new System.Drawing.Point(3, 136);
            this.PlayerThreeDecks.Name = "PlayerThreeDecks";
            this.PlayerThreeDecks.Size = new System.Drawing.Size(370, 52);
            this.PlayerThreeDecks.TabIndex = 9;
            // 
            // PlayerThreeLabel
            // 
            this.PlayerThreeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerThreeLabel.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerThreeLabel.Location = new System.Drawing.Point(3, 9);
            this.PlayerThreeLabel.Name = "PlayerThreeLabel";
            this.PlayerThreeLabel.Size = new System.Drawing.Size(361, 26);
            this.PlayerThreeLabel.TabIndex = 8;
            this.PlayerThreeLabel.Text = "Player 3";
            this.PlayerThreeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerThreeBots
            // 
            this.PlayerThreeBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerThreeBots.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerThreeBots.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerThreeBots.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerThreeBots.FormattingEnabled = true;
            this.PlayerThreeBots.Items.AddRange(new object[] {
            "Bots"});
            this.PlayerThreeBots.Location = new System.Drawing.Point(3, 55);
            this.PlayerThreeBots.Name = "PlayerThreeBots";
            this.PlayerThreeBots.Size = new System.Drawing.Size(370, 52);
            this.PlayerThreeBots.TabIndex = 7;
            // 
            // PlayerTwo
            // 
            this.PlayerTwo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerTwo.Controls.Add(this.label1);
            this.PlayerTwo.Controls.Add(this.PlayerTwoBotLabel);
            this.PlayerTwo.Controls.Add(this.PlayerTwoDecks);
            this.PlayerTwo.Controls.Add(this.PlayerTwoTitle);
            this.PlayerTwo.Controls.Add(this.PlayerTwoBots);
            this.PlayerTwo.Location = new System.Drawing.Point(0, 94);
            this.PlayerTwo.Name = "PlayerTwo";
            this.PlayerTwo.Size = new System.Drawing.Size(376, 211);
            this.PlayerTwo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Deck:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerTwoBotLabel
            // 
            this.PlayerTwoBotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTwoBotLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTwoBotLabel.Location = new System.Drawing.Point(3, 35);
            this.PlayerTwoBotLabel.Name = "PlayerTwoBotLabel";
            this.PlayerTwoBotLabel.Size = new System.Drawing.Size(361, 17);
            this.PlayerTwoBotLabel.TabIndex = 10;
            this.PlayerTwoBotLabel.Text = "Bot:";
            this.PlayerTwoBotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerTwoDecks
            // 
            this.PlayerTwoDecks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerTwoDecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerTwoDecks.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerTwoDecks.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerTwoDecks.FormattingEnabled = true;
            this.PlayerTwoDecks.Items.AddRange(new object[] {
            "Decks"});
            this.PlayerTwoDecks.Location = new System.Drawing.Point(3, 136);
            this.PlayerTwoDecks.Name = "PlayerTwoDecks";
            this.PlayerTwoDecks.Size = new System.Drawing.Size(370, 52);
            this.PlayerTwoDecks.TabIndex = 9;
            // 
            // PlayerTwoTitle
            // 
            this.PlayerTwoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerTwoTitle.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerTwoTitle.Location = new System.Drawing.Point(3, 9);
            this.PlayerTwoTitle.Name = "PlayerTwoTitle";
            this.PlayerTwoTitle.Size = new System.Drawing.Size(361, 26);
            this.PlayerTwoTitle.TabIndex = 8;
            this.PlayerTwoTitle.Text = "Player 2";
            this.PlayerTwoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerTwoBots
            // 
            this.PlayerTwoBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerTwoBots.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerTwoBots.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerTwoBots.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerTwoBots.FormattingEnabled = true;
            this.PlayerTwoBots.Items.AddRange(new object[] {
            "Bots"});
            this.PlayerTwoBots.Location = new System.Drawing.Point(3, 55);
            this.PlayerTwoBots.Name = "PlayerTwoBots";
            this.PlayerTwoBots.Size = new System.Drawing.Size(370, 52);
            this.PlayerTwoBots.TabIndex = 7;
            // 
            // PlayerFour
            // 
            this.PlayerFour.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerFour.Controls.Add(this.PlayerFourDeckLabel);
            this.PlayerFour.Controls.Add(this.PlayerFourBotLabel);
            this.PlayerFour.Controls.Add(this.PlayerFourDecks);
            this.PlayerFour.Controls.Add(this.PlayerFourTitle);
            this.PlayerFour.Controls.Add(this.PlayerFourBots);
            this.PlayerFour.Location = new System.Drawing.Point(379, 0);
            this.PlayerFour.Name = "PlayerFour";
            this.PlayerFour.Size = new System.Drawing.Size(386, 209);
            this.PlayerFour.TabIndex = 13;
            // 
            // PlayerFourDeckLabel
            // 
            this.PlayerFourDeckLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerFourDeckLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerFourDeckLabel.Location = new System.Drawing.Point(3, 112);
            this.PlayerFourDeckLabel.Name = "PlayerFourDeckLabel";
            this.PlayerFourDeckLabel.Size = new System.Drawing.Size(371, 21);
            this.PlayerFourDeckLabel.TabIndex = 11;
            this.PlayerFourDeckLabel.Text = "Deck:";
            this.PlayerFourDeckLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerFourBotLabel
            // 
            this.PlayerFourBotLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerFourBotLabel.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerFourBotLabel.Location = new System.Drawing.Point(3, 35);
            this.PlayerFourBotLabel.Name = "PlayerFourBotLabel";
            this.PlayerFourBotLabel.Size = new System.Drawing.Size(371, 17);
            this.PlayerFourBotLabel.TabIndex = 10;
            this.PlayerFourBotLabel.Text = "Bot:";
            this.PlayerFourBotLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerFourDecks
            // 
            this.PlayerFourDecks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerFourDecks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerFourDecks.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerFourDecks.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerFourDecks.FormattingEnabled = true;
            this.PlayerFourDecks.Items.AddRange(new object[] {
            "Decks"});
            this.PlayerFourDecks.Location = new System.Drawing.Point(3, 136);
            this.PlayerFourDecks.Name = "PlayerFourDecks";
            this.PlayerFourDecks.Size = new System.Drawing.Size(370, 52);
            this.PlayerFourDecks.TabIndex = 9;
            // 
            // PlayerFourTitle
            // 
            this.PlayerFourTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlayerFourTitle.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerFourTitle.Location = new System.Drawing.Point(3, 9);
            this.PlayerFourTitle.Name = "PlayerFourTitle";
            this.PlayerFourTitle.Size = new System.Drawing.Size(371, 26);
            this.PlayerFourTitle.TabIndex = 8;
            this.PlayerFourTitle.Text = "Player 4";
            this.PlayerFourTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayerFourBots
            // 
            this.PlayerFourBots.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.PlayerFourBots.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PlayerFourBots.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.PlayerFourBots.ForeColor = System.Drawing.Color.LightBlue;
            this.PlayerFourBots.FormattingEnabled = true;
            this.PlayerFourBots.Items.AddRange(new object[] {
            "Bots"});
            this.PlayerFourBots.Location = new System.Drawing.Point(3, 55);
            this.PlayerFourBots.Name = "PlayerFourBots";
            this.PlayerFourBots.Size = new System.Drawing.Size(370, 52);
            this.PlayerFourBots.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.panel2.Controls.Add(this.ViewMods);
            this.panel2.Controls.Add(this.JoinGame);
            this.panel2.Controls.Add(this.DeckCollection);
            this.panel2.Controls.Add(this.SettingsButton);
            this.panel2.Controls.Add(this.DecksButton);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.GameModesLabel);
            this.panel2.Controls.Add(this.GameModeList);
            this.panel2.Location = new System.Drawing.Point(379, 213);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 314);
            this.panel2.TabIndex = 14;
            // 
            // JoinGame
            // 
            this.JoinGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.JoinGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.JoinGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.JoinGame.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinGame.Location = new System.Drawing.Point(249, 243);
            this.JoinGame.Name = "JoinGame";
            this.JoinGame.Size = new System.Drawing.Size(124, 31);
            this.JoinGame.TabIndex = 18;
            this.JoinGame.Text = "Join Game";
            this.JoinGame.UseVisualStyleBackColor = false;
            this.JoinGame.Click += new System.EventHandler(this.JoinGame_Click);
            // 
            // DeckCollection
            // 
            this.DeckCollection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.DeckCollection.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DeckCollection.Enabled = false;
            this.DeckCollection.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.DeckCollection.ForeColor = System.Drawing.Color.LightBlue;
            this.DeckCollection.FormattingEnabled = true;
            this.DeckCollection.HorizontalScrollbar = true;
            this.DeckCollection.Location = new System.Drawing.Point(110, 155);
            this.DeckCollection.Name = "DeckCollection";
            this.DeckCollection.Size = new System.Drawing.Size(158, 91);
            this.DeckCollection.TabIndex = 17;
            this.DeckCollection.Visible = false;
            this.DeckCollection.SelectedIndexChanged += new System.EventHandler(this.DeckCollection_SelectedIndexChanged);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsButton.Location = new System.Drawing.Point(6, 280);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(124, 31);
            this.SettingsButton.TabIndex = 16;
            this.SettingsButton.Text = "Settings";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // DecksButton
            // 
            this.DecksButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DecksButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.DecksButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DecksButton.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.DecksButton.Location = new System.Drawing.Point(136, 243);
            this.DecksButton.Name = "DecksButton";
            this.DecksButton.Size = new System.Drawing.Size(107, 31);
            this.DecksButton.TabIndex = 15;
            this.DecksButton.Text = "View Decks";
            this.DecksButton.UseVisualStyleBackColor = false;
            this.DecksButton.Click += new System.EventHandler(this.DecksButton_Click);
            // 
            // GameModesLabel
            // 
            this.GameModesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GameModesLabel.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameModesLabel.Location = new System.Drawing.Point(3, 0);
            this.GameModesLabel.Name = "GameModesLabel";
            this.GameModesLabel.Size = new System.Drawing.Size(371, 26);
            this.GameModesLabel.TabIndex = 13;
            this.GameModesLabel.Text = "Game Modes";
            this.GameModesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameModeList
            // 
            this.GameModeList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.GameModeList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GameModeList.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.GameModeList.ForeColor = System.Drawing.Color.LightBlue;
            this.GameModeList.FormattingEnabled = true;
            this.GameModeList.Items.AddRange(new object[] {
            "Gamemodes"});
            this.GameModeList.Location = new System.Drawing.Point(3, 25);
            this.GameModeList.Name = "GameModeList";
            this.GameModeList.Size = new System.Drawing.Size(370, 221);
            this.GameModeList.TabIndex = 14;
            this.GameModeList.SelectedIndexChanged += new System.EventHandler(this.GameModeList_SelectedIndexChanged);
            // 
            // ViewMods
            // 
            this.ViewMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewMods.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ViewMods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ViewMods.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.ViewMods.Location = new System.Drawing.Point(136, 280);
            this.ViewMods.Name = "ViewMods";
            this.ViewMods.Size = new System.Drawing.Size(107, 31);
            this.ViewMods.TabIndex = 19;
            this.ViewMods.Text = "View Mods";
            this.ViewMods.UseVisualStyleBackColor = false;
            this.ViewMods.Click += new System.EventHandler(this.ViewMods_Click);
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(756, 527);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PlayerFour);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mtg Offline Game Setup";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Setup_FormClosed);
            this.Load += new System.EventHandler(this.Setup_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.PlayerThree.ResumeLayout(false);
            this.PlayerTwo.ResumeLayout(false);
            this.PlayerFour.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PlayerThree;
        private System.Windows.Forms.Label PlayerThreeDeckLabel;
        private System.Windows.Forms.Label PlayerThreeBotLabel;
        private System.Windows.Forms.ListBox PlayerThreeDecks;
        private System.Windows.Forms.Label PlayerThreeLabel;
        private System.Windows.Forms.ListBox PlayerThreeBots;
        private System.Windows.Forms.Panel PlayerTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PlayerTwoBotLabel;
        private System.Windows.Forms.ListBox PlayerTwoDecks;
        private System.Windows.Forms.Label PlayerTwoTitle;
        private System.Windows.Forms.ListBox PlayerTwoBots;
        private System.Windows.Forms.Panel PlayerFour;
        private System.Windows.Forms.Label PlayerFourDeckLabel;
        private System.Windows.Forms.Label PlayerFourBotLabel;
        private System.Windows.Forms.ListBox PlayerFourDecks;
        private System.Windows.Forms.Label PlayerFourTitle;
        private System.Windows.Forms.ListBox PlayerFourBots;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label GameModesLabel;
        private System.Windows.Forms.ListBox GameModeList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label PlayerOneTitle;
        private System.Windows.Forms.ListBox PlayerOne;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Button DecksButton;
        private System.Windows.Forms.ListBox DeckCollection;
        private System.Windows.Forms.Button JoinGame;
        private System.Windows.Forms.Button ViewMods;
    }
}