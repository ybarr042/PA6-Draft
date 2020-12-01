namespace PA6_Draft
{
    partial class PromotionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromotionForm));
            this.KnightButton = new System.Windows.Forms.Button();
            this.BishopButton = new System.Windows.Forms.Button();
            this.RookButton = new System.Windows.Forms.Button();
            this.QueenButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KnightButton
            // 
            this.KnightButton.Image = global::PA6_Draft.Properties.Resources.wknpromotion;
            this.KnightButton.Location = new System.Drawing.Point(12, 213);
            this.KnightButton.Name = "KnightButton";
            this.KnightButton.Size = new System.Drawing.Size(96, 61);
            this.KnightButton.TabIndex = 3;
            this.KnightButton.UseVisualStyleBackColor = true;
            this.KnightButton.Click += new System.EventHandler(this.KnightButton_Click);
            // 
            // BishopButton
            // 
            this.BishopButton.Image = global::PA6_Draft.Properties.Resources.wbpromotion;
            this.BishopButton.Location = new System.Drawing.Point(12, 146);
            this.BishopButton.Name = "BishopButton";
            this.BishopButton.Size = new System.Drawing.Size(96, 61);
            this.BishopButton.TabIndex = 2;
            this.BishopButton.UseVisualStyleBackColor = true;
            this.BishopButton.Click += new System.EventHandler(this.Bishop_Click);
            // 
            // RookButton
            // 
            this.RookButton.Image = global::PA6_Draft.Properties.Resources.wrpromotion;
            this.RookButton.Location = new System.Drawing.Point(12, 79);
            this.RookButton.Name = "RookButton";
            this.RookButton.Size = new System.Drawing.Size(96, 61);
            this.RookButton.TabIndex = 1;
            this.RookButton.UseVisualStyleBackColor = true;
            this.RookButton.Click += new System.EventHandler(this.Rook_Click);
            // 
            // QueenButton
            // 
            this.QueenButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("QueenButton.BackgroundImage")));
            this.QueenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.QueenButton.Image = global::PA6_Draft.Properties.Resources.wqpromotion;
            this.QueenButton.Location = new System.Drawing.Point(12, 12);
            this.QueenButton.Name = "QueenButton";
            this.QueenButton.Size = new System.Drawing.Size(96, 61);
            this.QueenButton.TabIndex = 0;
            this.QueenButton.UseVisualStyleBackColor = true;
            this.QueenButton.Click += new System.EventHandler(this.QueenButton_Click);
            // 
            // PromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 286);
            this.Controls.Add(this.KnightButton);
            this.Controls.Add(this.BishopButton);
            this.Controls.Add(this.RookButton);
            this.Controls.Add(this.QueenButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PromotionForm";
            this.Text = "Promotion";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button RookButton;
        private System.Windows.Forms.Button BishopButton;
        private System.Windows.Forms.Button KnightButton;
        private System.Windows.Forms.Button QueenButton;
    }
}