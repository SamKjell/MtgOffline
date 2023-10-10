namespace MtgOffline
{
    partial class CombatPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CombatPrompt));
            this.Attackers = new System.Windows.Forms.ListBox();
            this.AttackerLabel = new System.Windows.Forms.Label();
            this.BlockersLabel = new System.Windows.Forms.Label();
            this.Blockers = new System.Windows.Forms.ListBox();
            this.Done = new System.Windows.Forms.Button();
            this.AssignBlocks = new System.Windows.Forms.Button();
            this.RemoveBlocks = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Attackers
            // 
            this.Attackers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Attackers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.Attackers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Attackers.ForeColor = System.Drawing.Color.LightBlue;
            this.Attackers.FormattingEnabled = true;
            this.Attackers.Location = new System.Drawing.Point(12, 35);
            this.Attackers.Name = "Attackers";
            this.Attackers.Size = new System.Drawing.Size(692, 208);
            this.Attackers.TabIndex = 0;
            this.Attackers.SelectedIndexChanged += new System.EventHandler(this.Attackers_SelectedIndexChanged);
            // 
            // AttackerLabel
            // 
            this.AttackerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AttackerLabel.AutoSize = true;
            this.AttackerLabel.Font = new System.Drawing.Font("Lucida Console", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttackerLabel.Location = new System.Drawing.Point(9, 9);
            this.AttackerLabel.Name = "AttackerLabel";
            this.AttackerLabel.Size = new System.Drawing.Size(107, 15);
            this.AttackerLabel.TabIndex = 1;
            this.AttackerLabel.Text = "Attackers:";
            // 
            // BlockersLabel
            // 
            this.BlockersLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BlockersLabel.AutoSize = true;
            this.BlockersLabel.Font = new System.Drawing.Font("Lucida Console", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BlockersLabel.Location = new System.Drawing.Point(9, 267);
            this.BlockersLabel.Name = "BlockersLabel";
            this.BlockersLabel.Size = new System.Drawing.Size(97, 15);
            this.BlockersLabel.TabIndex = 3;
            this.BlockersLabel.Text = "Defenders";
            // 
            // Blockers
            // 
            this.Blockers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Blockers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.Blockers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Blockers.ForeColor = System.Drawing.Color.LightBlue;
            this.Blockers.FormattingEnabled = true;
            this.Blockers.Location = new System.Drawing.Point(12, 293);
            this.Blockers.Name = "Blockers";
            this.Blockers.Size = new System.Drawing.Size(692, 208);
            this.Blockers.TabIndex = 2;
            this.Blockers.SelectedIndexChanged += new System.EventHandler(this.Blockers_SelectedIndexChanged);
            // 
            // Done
            // 
            this.Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Done.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Done.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.Done.Location = new System.Drawing.Point(12, 507);
            this.Done.Name = "Done";
            this.Done.Size = new System.Drawing.Size(328, 40);
            this.Done.TabIndex = 20;
            this.Done.Text = "Done";
            this.Done.UseVisualStyleBackColor = true;
            this.Done.Click += new System.EventHandler(this.Done_Click);
            // 
            // AssignBlocks
            // 
            this.AssignBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AssignBlocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AssignBlocks.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.AssignBlocks.Location = new System.Drawing.Point(528, 507);
            this.AssignBlocks.Name = "AssignBlocks";
            this.AssignBlocks.Size = new System.Drawing.Size(176, 40);
            this.AssignBlocks.TabIndex = 21;
            this.AssignBlocks.Text = "Assign Blocks";
            this.AssignBlocks.UseVisualStyleBackColor = true;
            this.AssignBlocks.Click += new System.EventHandler(this.AssignBlocks_Click);
            // 
            // RemoveBlocks
            // 
            this.RemoveBlocks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveBlocks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RemoveBlocks.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.RemoveBlocks.Location = new System.Drawing.Point(346, 507);
            this.RemoveBlocks.Name = "RemoveBlocks";
            this.RemoveBlocks.Size = new System.Drawing.Size(176, 40);
            this.RemoveBlocks.TabIndex = 22;
            this.RemoveBlocks.Text = "Unassign Blocks";
            this.RemoveBlocks.UseVisualStyleBackColor = true;
            this.RemoveBlocks.Click += new System.EventHandler(this.RemoveBlocks_Click);
            // 
            // CombatPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(716, 559);
            this.Controls.Add(this.RemoveBlocks);
            this.Controls.Add(this.AssignBlocks);
            this.Controls.Add(this.Done);
            this.Controls.Add(this.BlockersLabel);
            this.Controls.Add(this.Blockers);
            this.Controls.Add(this.AttackerLabel);
            this.Controls.Add(this.Attackers);
            this.Font = new System.Drawing.Font("Lucida Console", 9.75F);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "CombatPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CombatPrompt";
            this.Load += new System.EventHandler(this.CombatPrompt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Attackers;
        private System.Windows.Forms.Label AttackerLabel;
        private System.Windows.Forms.Label BlockersLabel;
        private System.Windows.Forms.ListBox Blockers;
        private System.Windows.Forms.Button Done;
        private System.Windows.Forms.Button AssignBlocks;
        private System.Windows.Forms.Button RemoveBlocks;
    }
}