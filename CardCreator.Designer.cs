namespace MtgOffline
{
    partial class CardCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardCreator));
            this.nameBackground = new System.Windows.Forms.Panel();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.Types = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StaticAbilities = new System.Windows.Forms.RichTextBox();
            this.Cost = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Power = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.Toughness = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CreateCard = new System.Windows.Forms.Button();
            this.Download = new System.Windows.Forms.PictureBox();
            this.nameBackground.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Download)).BeginInit();
            this.SuspendLayout();
            // 
            // nameBackground
            // 
            this.nameBackground.BackColor = System.Drawing.Color.LightBlue;
            this.nameBackground.Controls.Add(this.NameBox);
            this.nameBackground.Location = new System.Drawing.Point(12, 12);
            this.nameBackground.Name = "nameBackground";
            this.nameBackground.Size = new System.Drawing.Size(294, 34);
            this.nameBackground.TabIndex = 1;
            // 
            // NameBox
            // 
            this.NameBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.NameBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NameBox.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameBox.ForeColor = System.Drawing.Color.LightBlue;
            this.NameBox.Location = new System.Drawing.Point(3, 3);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(288, 27);
            this.NameBox.TabIndex = 1;
            this.NameBox.Text = "Name";
            this.NameBox.TextChanged += new System.EventHandler(this.Name_TextChanged);
            // 
            // Types
            // 
            this.Types.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Types.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Types.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Types.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Types.ForeColor = System.Drawing.Color.LightBlue;
            this.Types.Location = new System.Drawing.Point(3, 3);
            this.Types.Name = "Types";
            this.Types.Size = new System.Drawing.Size(350, 16);
            this.Types.TabIndex = 1;
            this.Types.Text = "Legendary, Artifact Creature - Dog";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.Types);
            this.panel1.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(352, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 23);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.Controls.Add(this.StaticAbilities);
            this.panel2.Location = new System.Drawing.Point(12, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(160, 150);
            this.panel2.TabIndex = 3;
            // 
            // StaticAbilities
            // 
            this.StaticAbilities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StaticAbilities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.StaticAbilities.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StaticAbilities.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StaticAbilities.ForeColor = System.Drawing.Color.LightBlue;
            this.StaticAbilities.Location = new System.Drawing.Point(3, 3);
            this.StaticAbilities.Name = "StaticAbilities";
            this.StaticAbilities.Size = new System.Drawing.Size(154, 144);
            this.StaticAbilities.TabIndex = 4;
            this.StaticAbilities.Text = "Static Abilities: \n\nIndestructible\nDeathtouch\nIslandwalk\n";
            // 
            // Cost
            // 
            this.Cost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Cost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Cost.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Cost.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cost.ForeColor = System.Drawing.Color.LightBlue;
            this.Cost.Location = new System.Drawing.Point(3, 3);
            this.Cost.Name = "Cost";
            this.Cost.Size = new System.Drawing.Size(122, 16);
            this.Cost.TabIndex = 1;
            this.Cost.Text = "Cost: 2,G,U";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.Controls.Add(this.Cost);
            this.panel3.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(178, 52);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(128, 23);
            this.panel3.TabIndex = 3;
            // 
            // Power
            // 
            this.Power.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Power.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Power.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Power.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Power.ForeColor = System.Drawing.Color.LightBlue;
            this.Power.Location = new System.Drawing.Point(3, 3);
            this.Power.Name = "Power";
            this.Power.Size = new System.Drawing.Size(122, 16);
            this.Power.TabIndex = 1;
            this.Power.Text = "Power";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.Controls.Add(this.Power);
            this.panel4.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(178, 81);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(128, 23);
            this.panel4.TabIndex = 4;
            // 
            // Toughness
            // 
            this.Toughness.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Toughness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Toughness.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Toughness.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Toughness.ForeColor = System.Drawing.Color.LightBlue;
            this.Toughness.Location = new System.Drawing.Point(3, 3);
            this.Toughness.Name = "Toughness";
            this.Toughness.Size = new System.Drawing.Size(122, 16);
            this.Toughness.TabIndex = 1;
            this.Toughness.Text = "Toughness";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.Controls.Add(this.Toughness);
            this.panel5.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(178, 110);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(128, 23);
            this.panel5.TabIndex = 4;
            // 
            // CreateCard
            // 
            this.CreateCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.CreateCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateCard.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateCard.Location = new System.Drawing.Point(178, 171);
            this.CreateCard.Name = "CreateCard";
            this.CreateCard.Size = new System.Drawing.Size(530, 31);
            this.CreateCard.TabIndex = 5;
            this.CreateCard.Text = "Create Card";
            this.CreateCard.UseVisualStyleBackColor = false;
            this.CreateCard.Click += new System.EventHandler(this.CreateCard_Click);
            // 
            // Download
            // 
            this.Download.Image = global::MtgOffline.Properties.Resources.FloppyIcon;
            this.Download.Location = new System.Drawing.Point(309, 12);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(40, 34);
            this.Download.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Download.TabIndex = 6;
            this.Download.TabStop = false;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // CardCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(720, 211);
            this.Controls.Add(this.Download);
            this.Controls.Add(this.CreateCard);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.nameBackground);
            this.Font = new System.Drawing.Font("Lucida Console", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.Name = "CardCreator";
            this.Text = "Card Creator";
            this.Load += new System.EventHandler(this.CardCreator_Load);
            this.nameBackground.ResumeLayout(false);
            this.nameBackground.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Download)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel nameBackground;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox Types;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox StaticAbilities;
        private System.Windows.Forms.TextBox Cost;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox Power;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox Toughness;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button CreateCard;
        private System.Windows.Forms.PictureBox Download;
    }
}