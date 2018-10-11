namespace HotelSimulationTheLock
{
    partial class Simulation
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
            this.restaurantTB = new System.Windows.Forms.TextBox();
            this.roomTB = new System.Windows.Forms.TextBox();
            this.fitnessTB = new System.Windows.Forms.TextBox();
            this.guestTB = new System.Windows.Forms.TextBox();
            this.maidTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // restaurantTB
            // 
            this.restaurantTB.Location = new System.Drawing.Point(1474, 460);
            this.restaurantTB.Multiline = true;
            this.restaurantTB.Name = "restaurantTB";
            this.restaurantTB.ReadOnly = true;
            this.restaurantTB.Size = new System.Drawing.Size(194, 275);
            this.restaurantTB.TabIndex = 5;
            // 
            // roomTB
            // 
            this.roomTB.Location = new System.Drawing.Point(1304, 93);
            this.roomTB.Multiline = true;
            this.roomTB.Name = "roomTB";
            this.roomTB.ReadOnly = true;
            this.roomTB.Size = new System.Drawing.Size(364, 361);
            this.roomTB.TabIndex = 6;
            // 
            // fitnessTB
            // 
            this.fitnessTB.Location = new System.Drawing.Point(928, 460);
            this.fitnessTB.Multiline = true;
            this.fitnessTB.Name = "fitnessTB";
            this.fitnessTB.ReadOnly = true;
            this.fitnessTB.Size = new System.Drawing.Size(194, 275);
            this.fitnessTB.TabIndex = 7;
            // 
            // guestTB
            // 
            this.guestTB.Location = new System.Drawing.Point(928, 93);
            this.guestTB.Multiline = true;
            this.guestTB.Name = "guestTB";
            this.guestTB.ReadOnly = true;
            this.guestTB.Size = new System.Drawing.Size(364, 361);
            this.guestTB.TabIndex = 8;
            // 
            // maidTB
            // 
            this.maidTB.Location = new System.Drawing.Point(1128, 460);
            this.maidTB.Multiline = true;
            this.maidTB.Name = "maidTB";
            this.maidTB.ReadOnly = true;
            this.maidTB.Size = new System.Drawing.Size(340, 275);
            this.maidTB.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(354, 873);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(522, 206);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(8, 8);
            this.button2.TabIndex = 11;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 976);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.maidTB);
            this.Controls.Add(this.guestTB);
            this.Controls.Add(this.fitnessTB);
            this.Controls.Add(this.roomTB);
            this.Controls.Add(this.restaurantTB);
            this.Name = "Simulation";
            this.Text = "Simulation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox restaurantTB;
        private System.Windows.Forms.TextBox roomTB;
        private System.Windows.Forms.TextBox fitnessTB;
        private System.Windows.Forms.TextBox guestTB;
        private System.Windows.Forms.TextBox maidTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}