namespace MtgOffline
{
    partial class ModBrowser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModBrowser));
            this.ModListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ModName = new System.Windows.Forms.Label();
            this.ModIconBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ModAuthor = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ModIdBox = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Credits = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Description = new System.Windows.Forms.Label();
            this.SettingsButton = new System.Windows.Forms.Button();
            this.ModVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ModIconBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ModListBox
            // 
            this.ModListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.ModListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ModListBox.ForeColor = System.Drawing.Color.LightBlue;
            this.ModListBox.FormattingEnabled = true;
            this.ModListBox.ItemHeight = 15;
            this.ModListBox.Location = new System.Drawing.Point(511, 42);
            this.ModListBox.Name = "ModListBox";
            this.ModListBox.Size = new System.Drawing.Size(223, 255);
            this.ModListBox.TabIndex = 0;
            this.ModListBox.SelectedIndexChanged += new System.EventHandler(this.ModListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Lucida Console", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(511, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mods";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModName
            // 
            this.ModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModName.Font = new System.Drawing.Font("Lucida Console", 13.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.ModName.Location = new System.Drawing.Point(171, 9);
            this.ModName.Name = "ModName";
            this.ModName.Size = new System.Drawing.Size(334, 30);
            this.ModName.TabIndex = 2;
            this.ModName.Text = "MTGO Modloader";
            this.ModName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ModIconBox
            // 
            this.ModIconBox.InitialImage = global::MtgOffline.Properties.Resources.ModloaderIcon;
            this.ModIconBox.Location = new System.Drawing.Point(12, 12);
            this.ModIconBox.Name = "ModIconBox";
            this.ModIconBox.Size = new System.Drawing.Size(150, 150);
            this.ModIconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ModIconBox.TabIndex = 3;
            this.ModIconBox.TabStop = false;
            this.ModIconBox.Click += new System.EventHandler(this.ClickOnIcon);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Lucida Console", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(168, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Author:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ModAuthor
            // 
            this.ModAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModAuthor.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModAuthor.Location = new System.Drawing.Point(168, 57);
            this.ModAuthor.Name = "ModAuthor";
            this.ModAuthor.Size = new System.Drawing.Size(337, 22);
            this.ModAuthor.TabIndex = 5;
            this.ModAuthor.Text = "Sam Kjell";
            this.ModAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ModAuthor.Click += new System.EventHandler(this.ModAuthor_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Lucida Console", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(168, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Mod Id:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ModIdBox
            // 
            this.ModIdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModIdBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModIdBox.Location = new System.Drawing.Point(168, 94);
            this.ModIdBox.Name = "ModIdBox";
            this.ModIdBox.Size = new System.Drawing.Size(337, 22);
            this.ModIdBox.TabIndex = 7;
            this.ModIdBox.Text = "N.A. (Vanilla Code)";
            this.ModIdBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Lucida Console", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(168, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Credits:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Credits
            // 
            this.Credits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Credits.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Credits.Location = new System.Drawing.Point(168, 131);
            this.Credits.Name = "Credits";
            this.Credits.Size = new System.Drawing.Size(337, 61);
            this.Credits.TabIndex = 9;
            this.Credits.Text = "Sam Kjell";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Lucida Console", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Description:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Description
            // 
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Description.Font = new System.Drawing.Font("Lucida Console", 9F);
            this.Description.Location = new System.Drawing.Point(12, 204);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(493, 62);
            this.Description.TabIndex = 11;
            this.Description.Text = "This is the vanilla modloading support. It provides framework to all of the other" +
    " mods and also allows for their integration into the vanilla game code!";
            this.Description.Click += new System.EventHandler(this.label6_Click);
            // 
            // SettingsButton
            // 
            this.SettingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SettingsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.SettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsButton.Font = new System.Drawing.Font("Lucida Console", 10F);
            this.SettingsButton.Location = new System.Drawing.Point(12, 269);
            this.SettingsButton.Name = "SettingsButton";
            this.SettingsButton.Size = new System.Drawing.Size(124, 31);
            this.SettingsButton.TabIndex = 17;
            this.SettingsButton.Text = "Mod Settings";
            this.SettingsButton.UseVisualStyleBackColor = false;
            this.SettingsButton.Click += new System.EventHandler(this.SettingsButton_Click);
            // 
            // ModVersion
            // 
            this.ModVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ModVersion.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModVersion.Location = new System.Drawing.Point(511, 300);
            this.ModVersion.Name = "ModVersion";
            this.ModVersion.Size = new System.Drawing.Size(223, 17);
            this.ModVersion.TabIndex = 18;
            this.ModVersion.Text = "version";
            this.ModVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ModBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(746, 319);
            this.Controls.Add(this.ModVersion);
            this.Controls.Add(this.SettingsButton);
            this.Controls.Add(this.Description);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Credits);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ModIdBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ModAuthor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ModIconBox);
            this.Controls.Add(this.ModName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModListBox);
            this.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(762, 340);
            this.Name = "ModBrowser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mod Browser";
            this.Load += new System.EventHandler(this.ModBrowser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ModIconBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ModListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ModName;
        private System.Windows.Forms.PictureBox ModIconBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ModAuthor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label ModIdBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Credits;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Description;
        private System.Windows.Forms.Button SettingsButton;
        private System.Windows.Forms.Label ModVersion;
    }
}