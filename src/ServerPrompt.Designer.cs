namespace MtgOffline
{
    partial class ServerPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerPrompt));
            this.nameBackground = new System.Windows.Forms.Panel();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.CreateCard = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Port = new System.Windows.Forms.TextBox();
            this.nameBackground.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameBackground
            // 
            this.nameBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameBackground.BackColor = System.Drawing.Color.LightBlue;
            this.nameBackground.Controls.Add(this.NameBox);
            this.nameBackground.Location = new System.Drawing.Point(12, 12);
            this.nameBackground.Name = "nameBackground";
            this.nameBackground.Size = new System.Drawing.Size(216, 34);
            this.nameBackground.TabIndex = 2;
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
            this.NameBox.Size = new System.Drawing.Size(210, 27);
            this.NameBox.TabIndex = 1;
            this.NameBox.Text = "Address";
            // 
            // CreateCard
            // 
            this.CreateCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.CreateCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateCard.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateCard.Location = new System.Drawing.Point(12, 92);
            this.CreateCard.Name = "CreateCard";
            this.CreateCard.Size = new System.Drawing.Size(216, 31);
            this.CreateCard.TabIndex = 6;
            this.CreateCard.Text = "Join Server";
            this.CreateCard.UseVisualStyleBackColor = false;
            this.CreateCard.Click += new System.EventHandler(this.CreateCard_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.Port);
            this.panel1.Location = new System.Drawing.Point(12, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 34);
            this.panel1.TabIndex = 7;
            // 
            // Port
            // 
            this.Port.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Port.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Port.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Port.Font = new System.Drawing.Font("Lucida Console", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Port.ForeColor = System.Drawing.Color.LightBlue;
            this.Port.Location = new System.Drawing.Point(3, 3);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(210, 27);
            this.Port.TabIndex = 1;
            this.Port.Text = "Port";
            // 
            // ServerPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(240, 135);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CreateCard);
            this.Controls.Add(this.nameBackground);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(256, 174);
            this.MinimumSize = new System.Drawing.Size(256, 174);
            this.Name = "ServerPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Join Server";
            this.Load += new System.EventHandler(this.ServerPrompt_Load);
            this.nameBackground.ResumeLayout(false);
            this.nameBackground.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel nameBackground;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Button CreateCard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox Port;
    }
}