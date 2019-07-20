namespace WaitingOrderSalon
{
    partial class FrmListOrder
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
            this.FLP = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FLP
            // 
            this.FLP.BackColor = System.Drawing.Color.MediumTurquoise;
            this.FLP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FLP.Location = new System.Drawing.Point(0, 0);
            this.FLP.Name = "FLP";
            this.FLP.Size = new System.Drawing.Size(1561, 815);
            this.FLP.TabIndex = 0;
            this.FLP.DoubleClick += new System.EventHandler(this.FLP_DoubleClick);
            // 
            // FrmListOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1561, 815);
            this.Controls.Add(this.FLP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmListOrder";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmListOrder_FormClosing);
            this.Load += new System.EventHandler(this.FrmListOrder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel FLP;

    }
}

