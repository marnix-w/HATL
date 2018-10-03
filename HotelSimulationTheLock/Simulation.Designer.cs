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
            this._roomsStatus = new System.Windows.Forms.RichTextBox();
            this._maidStatus = new System.Windows.Forms.RichTextBox();
            this._guestStatus = new System.Windows.Forms.RichTextBox();
            this._restaurantStatus = new System.Windows.Forms.RichTextBox();
            this._fitnessStatus = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _roomsStatus
            // 
            this._roomsStatus.Location = new System.Drawing.Point(1150, 98);
            this._roomsStatus.Name = "_roomsStatus";
            this._roomsStatus.Size = new System.Drawing.Size(183, 399);
            this._roomsStatus.TabIndex = 0;
            this._roomsStatus.Text = "";
            // 
            // _maidStatus
            // 
            this._maidStatus.Location = new System.Drawing.Point(951, 523);
            this._maidStatus.Name = "_maidStatus";
            this._maidStatus.Size = new System.Drawing.Size(184, 245);
            this._maidStatus.TabIndex = 1;
            this._maidStatus.Text = "";
            // 
            // _guestStatus
            // 
            this._guestStatus.Location = new System.Drawing.Point(950, 98);
            this._guestStatus.Name = "_guestStatus";
            this._guestStatus.Size = new System.Drawing.Size(185, 399);
            this._guestStatus.TabIndex = 2;
            this._guestStatus.Text = "";
            // 
            // _restaurantStatus
            // 
            this._restaurantStatus.Location = new System.Drawing.Point(1351, 98);
            this._restaurantStatus.Name = "_restaurantStatus";
            this._restaurantStatus.Size = new System.Drawing.Size(166, 399);
            this._restaurantStatus.TabIndex = 3;
            this._restaurantStatus.Text = "";
            // 
            // _fitnessStatus
            // 
            this._fitnessStatus.Location = new System.Drawing.Point(1150, 523);
            this._fitnessStatus.Name = "_fitnessStatus";
            this._fitnessStatus.Size = new System.Drawing.Size(183, 245);
            this._fitnessStatus.TabIndex = 4;
            this._fitnessStatus.Text = "";
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1530, 845);
            this.Controls.Add(this._fitnessStatus);
            this.Controls.Add(this._restaurantStatus);
            this.Controls.Add(this._guestStatus);
            this.Controls.Add(this._maidStatus);
            this.Controls.Add(this._roomsStatus);
            this.Name = "Simulation";
            this.Text = "Simulation";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox _roomsStatus;
        private System.Windows.Forms.RichTextBox _maidStatus;
        private System.Windows.Forms.RichTextBox _guestStatus;
        private System.Windows.Forms.RichTextBox _restaurantStatus;
        private System.Windows.Forms.RichTextBox _fitnessStatus;
    }
}