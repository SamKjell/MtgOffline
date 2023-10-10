namespace MtgOffline
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.NickName = new System.Windows.Forms.TextBox();
            this.PlayerOneTitle = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Information = new System.Windows.Forms.Button();
            this.LightNotifications = new System.Windows.Forms.CheckBox();
            this.LoadOnlineCardImages = new System.Windows.Forms.CheckBox();
            this.DefaultPortBox = new System.Windows.Forms.TextBox();
            this.DefaultPortLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NickName
            // 
            this.NickName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.NickName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NickName.Font = new System.Drawing.Font("Lucida Console", 13F);
            this.NickName.ForeColor = System.Drawing.Color.LightBlue;
            this.NickName.Location = new System.Drawing.Point(12, 31);
            this.NickName.MaxLength = 20;
            this.NickName.Name = "NickName";
            this.NickName.Size = new System.Drawing.Size(205, 18);
            this.NickName.TabIndex = 0;
            this.NickName.Text = "Player 1";
            // 
            // PlayerOneTitle
            // 
            this.PlayerOneTitle.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOneTitle.Location = new System.Drawing.Point(12, 2);
            this.PlayerOneTitle.Name = "PlayerOneTitle";
            this.PlayerOneTitle.Size = new System.Drawing.Size(205, 26);
            this.PlayerOneTitle.TabIndex = 4;
            this.PlayerOneTitle.Text = "Nickname";
            this.PlayerOneTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButton.Location = new System.Drawing.Point(12, 105);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(154, 31);
            this.CancelButton.TabIndex = 17;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(172, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(230, 31);
            this.button1.TabIndex = 18;
            this.button1.Text = "Save and Exit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Information
            // 
            this.Information.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Information.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Information.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Information.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Information.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.Information.Location = new System.Drawing.Point(277, 24);
            this.Information.Name = "Information";
            this.Information.Size = new System.Drawing.Size(125, 31);
            this.Information.TabIndex = 19;
            this.Information.Text = "Credits";
            this.Information.UseVisualStyleBackColor = false;
            this.Information.Click += new System.EventHandler(this.Information_Click);
            // 
            // LightNotifications
            // 
            this.LightNotifications.AutoSize = true;
            this.LightNotifications.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.LightNotifications.Location = new System.Drawing.Point(12, 55);
            this.LightNotifications.Name = "LightNotifications";
            this.LightNotifications.Size = new System.Drawing.Size(197, 19);
            this.LightNotifications.TabIndex = 20;
            this.LightNotifications.Text = "Light Notifications";
            this.LightNotifications.UseVisualStyleBackColor = true;
            // 
            // LoadOnlineCardImages
            // 
            this.LoadOnlineCardImages.AutoSize = true;
            this.LoadOnlineCardImages.Font = new System.Drawing.Font("Lucida Console", 11F);
            this.LoadOnlineCardImages.Location = new System.Drawing.Point(12, 80);
            this.LoadOnlineCardImages.Name = "LoadOnlineCardImages";
            this.LoadOnlineCardImages.Size = new System.Drawing.Size(233, 19);
            this.LoadOnlineCardImages.TabIndex = 21;
            this.LoadOnlineCardImages.Text = "Load Online Card Images";
            this.LoadOnlineCardImages.UseVisualStyleBackColor = true;
            this.LoadOnlineCardImages.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DefaultPortBox
            // 
            this.DefaultPortBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultPortBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.DefaultPortBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DefaultPortBox.Font = new System.Drawing.Font("Lucida Console", 13F);
            this.DefaultPortBox.ForeColor = System.Drawing.Color.LightBlue;
            this.DefaultPortBox.Location = new System.Drawing.Point(251, 80);
            this.DefaultPortBox.MaxLength = 20;
            this.DefaultPortBox.Name = "DefaultPortBox";
            this.DefaultPortBox.Size = new System.Drawing.Size(151, 18);
            this.DefaultPortBox.TabIndex = 22;
            this.DefaultPortBox.Text = "1313";
            // 
            // DefaultPortLabel
            // 
            this.DefaultPortLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultPortLabel.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultPortLabel.Location = new System.Drawing.Point(251, 55);
            this.DefaultPortLabel.Name = "DefaultPortLabel";
            this.DefaultPortLabel.Size = new System.Drawing.Size(151, 26);
            this.DefaultPortLabel.TabIndex = 23;
            this.DefaultPortLabel.Text = "Default Port";
            this.DefaultPortLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(414, 148);
            this.Controls.Add(this.DefaultPortLabel);
            this.Controls.Add(this.DefaultPortBox);
            this.Controls.Add(this.LoadOnlineCardImages);
            this.Controls.Add(this.LightNotifications);
            this.Controls.Add(this.Information);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.PlayerOneTitle);
            this.Controls.Add(this.NickName);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(430, 187);
            this.MinimumSize = new System.Drawing.Size(430, 187);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NickName;
        private System.Windows.Forms.Label PlayerOneTitle;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Information;
        private System.Windows.Forms.CheckBox LightNotifications;
        private System.Windows.Forms.CheckBox LoadOnlineCardImages;
        private System.Windows.Forms.TextBox DefaultPortBox;
        private System.Windows.Forms.Label DefaultPortLabel;
    }
}