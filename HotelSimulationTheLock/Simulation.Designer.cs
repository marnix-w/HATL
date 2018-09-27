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
            this._eventsOutput = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // _eventsOutput
            // 
            this._eventsOutput.Location = new System.Drawing.Point(25, 12);
            this._eventsOutput.Name = "_eventsOutput";
            this._eventsOutput.ReadOnly = true;
            this._eventsOutput.Size = new System.Drawing.Size(514, 426);
            this._eventsOutput.TabIndex = 0;
            this._eventsOutput.Text = "";
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._eventsOutput);
            this.Name = "Simulation";
            this.Text = "Simulation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox _eventsOutput;
    }
}