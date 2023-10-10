namespace MtgOffline
{
    partial class SpellCaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpellCaster));
            this.IsSplitSecond = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CannotBeCountered = new System.Windows.Forms.CheckBox();
            this.BypassStack = new System.Windows.Forms.CheckBox();
            this.ActivateSpellOrAbility = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IsSplitSecond
            // 
            this.IsSplitSecond.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IsSplitSecond.AutoSize = true;
            this.IsSplitSecond.Location = new System.Drawing.Point(236, 28);
            this.IsSplitSecond.Name = "IsSplitSecond";
            this.IsSplitSecond.Size = new System.Drawing.Size(157, 20);
            this.IsSplitSecond.TabIndex = 0;
            this.IsSplitSecond.Text = "Split Second?";
            this.IsSplitSecond.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(231, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Stack Settings";
            // 
            // CannotBeCountered
            // 
            this.CannotBeCountered.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CannotBeCountered.AutoSize = true;
            this.CannotBeCountered.Font = new System.Drawing.Font("Lucida Console", 9F);
            this.CannotBeCountered.Location = new System.Drawing.Point(236, 54);
            this.CannotBeCountered.Name = "CannotBeCountered";
            this.CannotBeCountered.Size = new System.Drawing.Size(157, 16);
            this.CannotBeCountered.TabIndex = 2;
            this.CannotBeCountered.Text = "Cannot be Countered";
            this.CannotBeCountered.UseVisualStyleBackColor = true;
            this.CannotBeCountered.CheckedChanged += new System.EventHandler(this.CannotBeCountered_CheckedChanged);
            // 
            // BypassStack
            // 
            this.BypassStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BypassStack.AutoSize = true;
            this.BypassStack.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.BypassStack.Location = new System.Drawing.Point(236, 76);
            this.BypassStack.Name = "BypassStack";
            this.BypassStack.Size = new System.Drawing.Size(157, 20);
            this.BypassStack.TabIndex = 3;
            this.BypassStack.Text = "Bypass Stack?";
            this.BypassStack.UseVisualStyleBackColor = true;
            // 
            // ActivateSpellOrAbility
            // 
            this.ActivateSpellOrAbility.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivateSpellOrAbility.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ActivateSpellOrAbility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ActivateSpellOrAbility.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ActivateSpellOrAbility.Location = new System.Drawing.Point(3, 113);
            this.ActivateSpellOrAbility.Name = "ActivateSpellOrAbility";
            this.ActivateSpellOrAbility.Size = new System.Drawing.Size(390, 31);
            this.ActivateSpellOrAbility.TabIndex = 4;
            this.ActivateSpellOrAbility.Text = "Cast Spell/Activate Ability";
            this.ActivateSpellOrAbility.UseVisualStyleBackColor = false;
            this.ActivateSpellOrAbility.Click += new System.EventHandler(this.ActivateSpellOrAbility_Click);
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Lucida Console", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.Title.Location = new System.Drawing.Point(3, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(222, 87);
            this.Title.TabIndex = 5;
            this.Title.Text = "Title";
            // 
            // SpellCaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(396, 148);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.ActivateSpellOrAbility);
            this.Controls.Add(this.BypassStack);
            this.Controls.Add(this.CannotBeCountered);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IsSplitSecond);
            this.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "SpellCaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Spell/Ability Manager";
            this.Load += new System.EventHandler(this.SpellCaster_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox IsSplitSecond;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox CannotBeCountered;
        private System.Windows.Forms.CheckBox BypassStack;
        private System.Windows.Forms.Button ActivateSpellOrAbility;
        private System.Windows.Forms.Label Title;
    }
}