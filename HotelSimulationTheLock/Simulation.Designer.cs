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
            this.roomTB = new System.Windows.Forms.TextBox();
            this.facillityTB = new System.Windows.Forms.TextBox();
            this.guestTB = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // roomTB
            // 
            this.roomTB.Location = new System.Drawing.Point(1282, 93);
            this.roomTB.Multiline = true;
            this.roomTB.Name = "roomTB";
            this.roomTB.ReadOnly = true;
            this.roomTB.Size = new System.Drawing.Size(386, 361);
            this.roomTB.TabIndex = 6;
            // 
            // facillityTB
            // 
            this.facillityTB.Location = new System.Drawing.Point(928, 460);
            this.facillityTB.Multiline = true;
            this.facillityTB.Name = "facillityTB";
            this.facillityTB.ReadOnly = true;
            this.facillityTB.Size = new System.Drawing.Size(348, 275);
            this.facillityTB.TabIndex = 7;
            // 
            // guestTB
            // 
            this.guestTB.Location = new System.Drawing.Point(928, 93);
            this.guestTB.Multiline = true;
            this.guestTB.Name = "guestTB";
            this.guestTB.ReadOnly = true;
            this.guestTB.Size = new System.Drawing.Size(348, 361);
            this.guestTB.TabIndex = 8;
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
            this.Controls.Add(this.guestTB);
            this.Controls.Add(this.facillityTB);
            this.Controls.Add(this.roomTB);
            this.Name = "Simulation";
            this.Text = "Simulation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox roomTB;
        private System.Windows.Forms.TextBox facillityTB;
        private System.Windows.Forms.TextBox guestTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}