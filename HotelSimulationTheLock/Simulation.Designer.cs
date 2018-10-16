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
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // roomTB
            // 
            this.roomTB.Location = new System.Drawing.Point(50, 383);
            this.roomTB.Multiline = true;
            this.roomTB.Name = "roomTB";
            this.roomTB.ReadOnly = true;
            this.roomTB.Size = new System.Drawing.Size(350, 392);
            this.roomTB.TabIndex = 6;
            // 
            // facillityTB
            // 
            this.facillityTB.Location = new System.Drawing.Point(50, 791);
            this.facillityTB.Multiline = true;
            this.facillityTB.Name = "facillityTB";
            this.facillityTB.ReadOnly = true;
            this.facillityTB.Size = new System.Drawing.Size(350, 141);
            this.facillityTB.TabIndex = 7;
            // 
            // guestTB
            // 
            this.guestTB.Location = new System.Drawing.Point(50, 100);
            this.guestTB.Multiline = true;
            this.guestTB.Name = "guestTB";
            this.guestTB.ReadOnly = true;
            this.guestTB.Size = new System.Drawing.Size(350, 267);
            this.guestTB.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(728, 29);
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
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(522, 821);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 68);
            this.button3.TabIndex = 12;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(743, 821);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(86, 68);
            this.button4.TabIndex = 13;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(944, 821);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(86, 68);
            this.button5.TabIndex = 14;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1346, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "simInfo";
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1676, 976);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.guestTB);
            this.Controls.Add(this.facillityTB);
            this.Controls.Add(this.roomTB);
            this.Name = "Simulation";
            this.Text = "7";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Simulation_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox roomTB;
        private System.Windows.Forms.TextBox facillityTB;
        private System.Windows.Forms.TextBox guestTB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
    }
}