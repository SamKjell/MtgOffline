namespace MtgOffline
{
    partial class CardPoolViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardPoolViewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.libraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.millToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topCardsOnBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shuffleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.handToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.discardBulkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graveyardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewTranscriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCardOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.discardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toExileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toHandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toGraveyardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardPoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleHiddenCardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CardList = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Window;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.libraryToolStripMenuItem,
            this.handToolStripMenuItem,
            this.graveyardToolStripMenuItem,
            this.exileToolStripMenuItem,
            this.selectedCardToolStripMenuItem,
            this.cardPoolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(759, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // libraryToolStripMenuItem
            // 
            this.libraryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.millToolStripMenuItem,
            this.topCardsOnBottomToolStripMenuItem,
            this.shuffleToolStripMenuItem});
            this.libraryToolStripMenuItem.Enabled = false;
            this.libraryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.libraryToolStripMenuItem.Name = "libraryToolStripMenuItem";
            this.libraryToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.libraryToolStripMenuItem.Text = "Library";
            this.libraryToolStripMenuItem.Visible = false;
            // 
            // millToolStripMenuItem
            // 
            this.millToolStripMenuItem.Name = "millToolStripMenuItem";
            this.millToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.millToolStripMenuItem.Text = "Mill";
            this.millToolStripMenuItem.Click += new System.EventHandler(this.millToolStripMenuItem_Click);
            // 
            // topCardsOnBottomToolStripMenuItem
            // 
            this.topCardsOnBottomToolStripMenuItem.Name = "topCardsOnBottomToolStripMenuItem";
            this.topCardsOnBottomToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.topCardsOnBottomToolStripMenuItem.Text = "Top Card(s) on Bottom";
            this.topCardsOnBottomToolStripMenuItem.Click += new System.EventHandler(this.topCardsOnBottomToolStripMenuItem_Click);
            // 
            // shuffleToolStripMenuItem
            // 
            this.shuffleToolStripMenuItem.Name = "shuffleToolStripMenuItem";
            this.shuffleToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.shuffleToolStripMenuItem.Text = "Shuffle";
            this.shuffleToolStripMenuItem.Click += new System.EventHandler(this.shuffleToolStripMenuItem_Click);
            // 
            // handToolStripMenuItem
            // 
            this.handToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.discardBulkToolStripMenuItem});
            this.handToolStripMenuItem.Enabled = false;
            this.handToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.handToolStripMenuItem.Name = "handToolStripMenuItem";
            this.handToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.handToolStripMenuItem.Text = "Hand";
            this.handToolStripMenuItem.Visible = false;
            // 
            // discardBulkToolStripMenuItem
            // 
            this.discardBulkToolStripMenuItem.Name = "discardBulkToolStripMenuItem";
            this.discardBulkToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.discardBulkToolStripMenuItem.Text = "Discard";
            this.discardBulkToolStripMenuItem.Click += new System.EventHandler(this.discardBulkToolStripMenuItem_Click);
            // 
            // graveyardToolStripMenuItem
            // 
            this.graveyardToolStripMenuItem.Enabled = false;
            this.graveyardToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.graveyardToolStripMenuItem.Name = "graveyardToolStripMenuItem";
            this.graveyardToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.graveyardToolStripMenuItem.Text = "Graveyard";
            this.graveyardToolStripMenuItem.Visible = false;
            // 
            // exileToolStripMenuItem
            // 
            this.exileToolStripMenuItem.Enabled = false;
            this.exileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exileToolStripMenuItem.Name = "exileToolStripMenuItem";
            this.exileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.exileToolStripMenuItem.Text = "Exile";
            this.exileToolStripMenuItem.Visible = false;
            // 
            // selectedCardToolStripMenuItem
            // 
            this.selectedCardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTranscriptToolStripMenuItem,
            this.viewCardOnlineToolStripMenuItem,
            this.toolStripSeparator1,
            this.discardToolStripMenuItem1,
            this.toLibraryToolStripMenuItem,
            this.toExileToolStripMenuItem,
            this.toHandToolStripMenuItem,
            this.toGraveyardToolStripMenuItem});
            this.selectedCardToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.selectedCardToolStripMenuItem.Name = "selectedCardToolStripMenuItem";
            this.selectedCardToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.selectedCardToolStripMenuItem.Text = "Selected Card";
            // 
            // viewTranscriptToolStripMenuItem
            // 
            this.viewTranscriptToolStripMenuItem.Name = "viewTranscriptToolStripMenuItem";
            this.viewTranscriptToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.viewTranscriptToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.viewTranscriptToolStripMenuItem.Text = "View Transcript";
            this.viewTranscriptToolStripMenuItem.Click += new System.EventHandler(this.viewTranscriptToolStripMenuItem_Click);
            // 
            // viewCardOnlineToolStripMenuItem
            // 
            this.viewCardOnlineToolStripMenuItem.Name = "viewCardOnlineToolStripMenuItem";
            this.viewCardOnlineToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.viewCardOnlineToolStripMenuItem.Text = "View Card Online";
            this.viewCardOnlineToolStripMenuItem.Click += new System.EventHandler(this.viewCardOnlineToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // discardToolStripMenuItem1
            // 
            this.discardToolStripMenuItem1.Enabled = false;
            this.discardToolStripMenuItem1.Name = "discardToolStripMenuItem1";
            this.discardToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            this.discardToolStripMenuItem1.Text = "Discard";
            this.discardToolStripMenuItem1.Visible = false;
            this.discardToolStripMenuItem1.Click += new System.EventHandler(this.discardToolStripMenuItem1_Click);
            // 
            // toLibraryToolStripMenuItem
            // 
            this.toLibraryToolStripMenuItem.Enabled = false;
            this.toLibraryToolStripMenuItem.Name = "toLibraryToolStripMenuItem";
            this.toLibraryToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toLibraryToolStripMenuItem.Text = "To Library";
            this.toLibraryToolStripMenuItem.Visible = false;
            this.toLibraryToolStripMenuItem.Click += new System.EventHandler(this.toLibraryToolStripMenuItem_Click);
            // 
            // toExileToolStripMenuItem
            // 
            this.toExileToolStripMenuItem.Enabled = false;
            this.toExileToolStripMenuItem.Name = "toExileToolStripMenuItem";
            this.toExileToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toExileToolStripMenuItem.Text = "To Exile";
            this.toExileToolStripMenuItem.Visible = false;
            this.toExileToolStripMenuItem.Click += new System.EventHandler(this.toExileToolStripMenuItem_Click);
            // 
            // toHandToolStripMenuItem
            // 
            this.toHandToolStripMenuItem.Enabled = false;
            this.toHandToolStripMenuItem.Name = "toHandToolStripMenuItem";
            this.toHandToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toHandToolStripMenuItem.Text = "To Hand";
            this.toHandToolStripMenuItem.Visible = false;
            this.toHandToolStripMenuItem.Click += new System.EventHandler(this.toHandToolStripMenuItem_Click);
            // 
            // toGraveyardToolStripMenuItem
            // 
            this.toGraveyardToolStripMenuItem.Enabled = false;
            this.toGraveyardToolStripMenuItem.Name = "toGraveyardToolStripMenuItem";
            this.toGraveyardToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.toGraveyardToolStripMenuItem.Text = "To Graveyard";
            this.toGraveyardToolStripMenuItem.Visible = false;
            this.toGraveyardToolStripMenuItem.Click += new System.EventHandler(this.toGraveyardToolStripMenuItem_Click);
            // 
            // cardPoolToolStripMenuItem
            // 
            this.cardPoolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleHiddenCardsToolStripMenuItem});
            this.cardPoolToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cardPoolToolStripMenuItem.Name = "cardPoolToolStripMenuItem";
            this.cardPoolToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cardPoolToolStripMenuItem.Text = "Card Pool";
            // 
            // toggleHiddenCardsToolStripMenuItem
            // 
            this.toggleHiddenCardsToolStripMenuItem.Name = "toggleHiddenCardsToolStripMenuItem";
            this.toggleHiddenCardsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.toggleHiddenCardsToolStripMenuItem.Text = "Toggle Hidden Cards";
            this.toggleHiddenCardsToolStripMenuItem.Click += new System.EventHandler(this.toggleHiddenCardsToolStripMenuItem_Click);
            // 
            // CardList
            // 
            this.CardList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.CardList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CardList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CardList.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardList.ForeColor = System.Drawing.Color.LightBlue;
            this.CardList.FormattingEnabled = true;
            this.CardList.Items.AddRange(new object[] {
            "Hello World"});
            this.CardList.Location = new System.Drawing.Point(0, 24);
            this.CardList.Name = "CardList";
            this.CardList.Size = new System.Drawing.Size(759, 551);
            this.CardList.TabIndex = 3;
            this.CardList.SelectedIndexChanged += new System.EventHandler(this.CardList_SelectedIndexChanged);
            // 
            // CardPoolViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(759, 575);
            this.Controls.Add(this.CardList);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "CardPoolViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CardPoolViewer";
            this.Load += new System.EventHandler(this.CardPoolViewer_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListBox CardList;
        private System.Windows.Forms.ToolStripMenuItem libraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem millToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topCardsOnBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shuffleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem handToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem discardBulkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graveyardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectedCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardPoolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleHiddenCardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewTranscriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem discardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toExileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toHandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toGraveyardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCardOnlineToolStripMenuItem;
    }
}