namespace ScreenProtect
{
    partial class Setting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
            this.btnClose = new System.Windows.Forms.Button();
            this.trackboxVol = new System.Windows.Forms.TrackBar();
            this.lbVol = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackboxVol)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(84, 75);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "确定";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // trackboxVol
            // 
            this.trackboxVol.Location = new System.Drawing.Point(57, 24);
            this.trackboxVol.Maximum = 100;
            this.trackboxVol.Name = "trackboxVol";
            this.trackboxVol.Size = new System.Drawing.Size(171, 45);
            this.trackboxVol.TabIndex = 2;
            this.trackboxVol.TabStop = false;
            this.trackboxVol.Scroll += new System.EventHandler(this.trackboxVol_Scroll);
            // 
            // lbVol
            // 
            this.lbVol.AutoSize = true;
            this.lbVol.Location = new System.Drawing.Point(22, 28);
            this.lbVol.Name = "lbVol";
            this.lbVol.Size = new System.Drawing.Size(29, 12);
            this.lbVol.TabIndex = 3;
            this.lbVol.Text = "音量";
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 125);
            this.Controls.Add(this.lbVol);
            this.Controls.Add(this.trackboxVol);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)(this.trackboxVol)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TrackBar trackboxVol;
        private System.Windows.Forms.Label lbVol;
    }
}