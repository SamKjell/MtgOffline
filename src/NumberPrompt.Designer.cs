namespace MtgOffline
{
    partial class NumberPrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumberPrompt));
            this.Description = new System.Windows.Forms.RichTextBox();
            this.Title = new System.Windows.Forms.Label();
            this.Okay = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numberValue = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberValue)).BeginInit();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Description.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Description.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.Description.ForeColor = System.Drawing.Color.LightBlue;
            this.Description.Location = new System.Drawing.Point(9, 31);
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Size = new System.Drawing.Size(260, 109);
            this.Description.TabIndex = 7;
            this.Description.Text = "Some Important Alert. Programmatically altered";
            this.Description.TextChanged += new System.EventHandler(this.Description_TextChanged);
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Title.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(9, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(260, 23);
            this.Title.TabIndex = 6;
            this.Title.Text = "Alert";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Okay
            // 
            this.Okay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Okay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.Okay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Okay.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Okay.Location = new System.Drawing.Point(9, 214);
            this.Okay.Name = "Okay";
            this.Okay.Size = new System.Drawing.Size(260, 31);
            this.Okay.TabIndex = 5;
            this.Okay.Text = "Okay";
            this.Okay.UseVisualStyleBackColor = false;
            this.Okay.Click += new System.EventHandler(this.Okay_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.panel1.Controls.Add(this.numberValue);
            this.panel1.Controls.Add(this.Okay);
            this.panel1.Controls.Add(this.Description);
            this.panel1.Controls.Add(this.Title);
            this.panel1.Location = new System.Drawing.Point(3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 254);
            this.panel1.TabIndex = 8;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // numberValue
            // 
            this.numberValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(63)))), ((int)(((byte)(79)))));
            this.numberValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numberValue.Font = new System.Drawing.Font("Lucida Console", 30F);
            this.numberValue.ForeColor = System.Drawing.Color.LightBlue;
            this.numberValue.Location = new System.Drawing.Point(13, 146);
            this.numberValue.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numberValue.Name = "numberValue";
            this.numberValue.Size = new System.Drawing.Size(256, 43);
            this.numberValue.TabIndex = 8;
            this.numberValue.ThousandsSeparator = true;
            // 
            // NumberPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.LightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NumberPrompt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NumberPrompt";
            this.Load += new System.EventHandler(this.NumberPrompt_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numberValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Description;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Okay;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown numberValue;
    }
}