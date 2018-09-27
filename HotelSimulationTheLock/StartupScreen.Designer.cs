namespace HotelSimulationTheLock
{
    partial class StartupScreen
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
            this._runSimulation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _runSimulation
            // 
            this._runSimulation.Location = new System.Drawing.Point(356, 198);
            this._runSimulation.Name = "_runSimulation";
            this._runSimulation.Size = new System.Drawing.Size(75, 23);
            this._runSimulation.TabIndex = 0;
            this._runSimulation.Text = "Run Simulation";
            this._runSimulation.UseVisualStyleBackColor = true;
            this._runSimulation.Click += new System.EventHandler(this._runSimulation_Click);
            // 
            // StartupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._runSimulation);
            this.Name = "StartupScreen";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _runSimulation;
    }
}

