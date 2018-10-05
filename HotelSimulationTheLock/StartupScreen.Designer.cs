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
            this.maid_TB = new System.Windows.Forms.TrackBar();
            this.maid_LB = new System.Windows.Forms.Label();
            this.elevator_hte_LB = new System.Windows.Forms.Label();
            this.elevator_hte_TB = new System.Windows.Forms.TrackBar();
            this.elevator_cap_LB = new System.Windows.Forms.Label();
            this.elevator_cap_TB = new System.Windows.Forms.TrackBar();
            this.hte_per_sec_LB = new System.Windows.Forms.Label();
            this.hte_per_sec_TB = new System.Windows.Forms.TrackBar();
            this.staircase_hte_LB = new System.Windows.Forms.Label();
            this.staircase_hte_TB = new System.Windows.Forms.TrackBar();
            this.cinema_dur_LB = new System.Windows.Forms.Label();
            this.cinema_dur_TB = new System.Windows.Forms.TrackBar();
            this.restaurant_cap_LB = new System.Windows.Forms.Label();
            this.restaurant_cap_TB = new System.Windows.Forms.TrackBar();
            this.fitness_cap_LB = new System.Windows.Forms.Label();
            this.fitness_cap_TB = new System.Windows.Forms.TrackBar();
            this.fitnes_dur_LB = new System.Windows.Forms.Label();
            this.fitness_dur_TB = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_dur_TB)).BeginInit();
            this.SuspendLayout();
            // 
            // _runSimulation
            // 
            this._runSimulation.Location = new System.Drawing.Point(126, 510);
            this._runSimulation.Name = "_runSimulation";
            this._runSimulation.Size = new System.Drawing.Size(237, 23);
            this._runSimulation.TabIndex = 0;
            this._runSimulation.Text = "Run Simulation";
            this._runSimulation.UseVisualStyleBackColor = true;
            this._runSimulation.Click += new System.EventHandler(this._runSimulation_Click);
            // 
            // maid_TB
            // 
            this.maid_TB.Location = new System.Drawing.Point(260, 44);
            this.maid_TB.Maximum = 20;
            this.maid_TB.Name = "maid_TB";
            this.maid_TB.Size = new System.Drawing.Size(161, 45);
            this.maid_TB.TabIndex = 3;
            this.maid_TB.Value = 2;
            this.maid_TB.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // maid_LB
            // 
            this.maid_LB.AutoSize = true;
            this.maid_LB.Location = new System.Drawing.Point(36, 44);
            this.maid_LB.Name = "maid_LB";
            this.maid_LB.Size = new System.Drawing.Size(35, 13);
            this.maid_LB.TabIndex = 4;
            this.maid_LB.Text = "label1";
            // 
            // elevator_hte_LB
            // 
            this.elevator_hte_LB.AutoSize = true;
            this.elevator_hte_LB.Location = new System.Drawing.Point(36, 95);
            this.elevator_hte_LB.Name = "elevator_hte_LB";
            this.elevator_hte_LB.Size = new System.Drawing.Size(35, 13);
            this.elevator_hte_LB.TabIndex = 6;
            this.elevator_hte_LB.Text = "label1";
            // 
            // elevator_hte_TB
            // 
            this.elevator_hte_TB.Location = new System.Drawing.Point(260, 95);
            this.elevator_hte_TB.Maximum = 20;
            this.elevator_hte_TB.Name = "elevator_hte_TB";
            this.elevator_hte_TB.Size = new System.Drawing.Size(161, 45);
            this.elevator_hte_TB.TabIndex = 5;
            this.elevator_hte_TB.Value = 2;
            this.elevator_hte_TB.ValueChanged += new System.EventHandler(this.Elevator_hte_TB_ValueChanged);
            // 
            // elevator_cap_LB
            // 
            this.elevator_cap_LB.AutoSize = true;
            this.elevator_cap_LB.Location = new System.Drawing.Point(36, 146);
            this.elevator_cap_LB.Name = "elevator_cap_LB";
            this.elevator_cap_LB.Size = new System.Drawing.Size(35, 13);
            this.elevator_cap_LB.TabIndex = 8;
            this.elevator_cap_LB.Text = "label1";
            // 
            // elevator_cap_TB
            // 
            this.elevator_cap_TB.Location = new System.Drawing.Point(260, 146);
            this.elevator_cap_TB.Maximum = 20;
            this.elevator_cap_TB.Name = "elevator_cap_TB";
            this.elevator_cap_TB.Size = new System.Drawing.Size(161, 45);
            this.elevator_cap_TB.TabIndex = 7;
            this.elevator_cap_TB.Value = 2;
            this.elevator_cap_TB.ValueChanged += new System.EventHandler(this.Elevator_cap_TB_ValueChanged);
            // 
            // hte_per_sec_LB
            // 
            this.hte_per_sec_LB.AutoSize = true;
            this.hte_per_sec_LB.Location = new System.Drawing.Point(36, 197);
            this.hte_per_sec_LB.Name = "hte_per_sec_LB";
            this.hte_per_sec_LB.Size = new System.Drawing.Size(35, 13);
            this.hte_per_sec_LB.TabIndex = 10;
            this.hte_per_sec_LB.Text = "label1";
            // 
            // hte_per_sec_TB
            // 
            this.hte_per_sec_TB.Location = new System.Drawing.Point(260, 197);
            this.hte_per_sec_TB.Maximum = 20;
            this.hte_per_sec_TB.Name = "hte_per_sec_TB";
            this.hte_per_sec_TB.Size = new System.Drawing.Size(161, 45);
            this.hte_per_sec_TB.TabIndex = 9;
            this.hte_per_sec_TB.Value = 2;
            this.hte_per_sec_TB.ValueChanged += new System.EventHandler(this.Hte_per_sec_TB_ValueChanged);
            // 
            // staircase_hte_LB
            // 
            this.staircase_hte_LB.AutoSize = true;
            this.staircase_hte_LB.Location = new System.Drawing.Point(36, 248);
            this.staircase_hte_LB.Name = "staircase_hte_LB";
            this.staircase_hte_LB.Size = new System.Drawing.Size(35, 13);
            this.staircase_hte_LB.TabIndex = 12;
            this.staircase_hte_LB.Text = "label1";
            // 
            // staircase_hte_TB
            // 
            this.staircase_hte_TB.Location = new System.Drawing.Point(260, 248);
            this.staircase_hte_TB.Maximum = 20;
            this.staircase_hte_TB.Name = "staircase_hte_TB";
            this.staircase_hte_TB.Size = new System.Drawing.Size(161, 45);
            this.staircase_hte_TB.TabIndex = 11;
            this.staircase_hte_TB.Value = 2;
            this.staircase_hte_TB.ValueChanged += new System.EventHandler(this.Staircase_hte_TB_ValueChanged);
            // 
            // cinema_dur_LB
            // 
            this.cinema_dur_LB.AutoSize = true;
            this.cinema_dur_LB.Location = new System.Drawing.Point(36, 297);
            this.cinema_dur_LB.Name = "cinema_dur_LB";
            this.cinema_dur_LB.Size = new System.Drawing.Size(35, 13);
            this.cinema_dur_LB.TabIndex = 14;
            this.cinema_dur_LB.Text = "label1";
            // 
            // cinema_dur_TB
            // 
            this.cinema_dur_TB.Location = new System.Drawing.Point(260, 297);
            this.cinema_dur_TB.Maximum = 20;
            this.cinema_dur_TB.Name = "cinema_dur_TB";
            this.cinema_dur_TB.Size = new System.Drawing.Size(161, 45);
            this.cinema_dur_TB.TabIndex = 13;
            this.cinema_dur_TB.Value = 2;
            this.cinema_dur_TB.ValueChanged += new System.EventHandler(this.Cinema_dur_TB_ValueChanged);
            // 
            // restaurant_cap_LB
            // 
            this.restaurant_cap_LB.AutoSize = true;
            this.restaurant_cap_LB.Location = new System.Drawing.Point(36, 344);
            this.restaurant_cap_LB.Name = "restaurant_cap_LB";
            this.restaurant_cap_LB.Size = new System.Drawing.Size(35, 13);
            this.restaurant_cap_LB.TabIndex = 16;
            this.restaurant_cap_LB.Text = "label1";
            // 
            // restaurant_cap_TB
            // 
            this.restaurant_cap_TB.Location = new System.Drawing.Point(260, 344);
            this.restaurant_cap_TB.Maximum = 20;
            this.restaurant_cap_TB.Name = "restaurant_cap_TB";
            this.restaurant_cap_TB.Size = new System.Drawing.Size(161, 45);
            this.restaurant_cap_TB.TabIndex = 15;
            this.restaurant_cap_TB.Value = 2;
            this.restaurant_cap_TB.ValueChanged += new System.EventHandler(this.Restaurant_cap_TB_ValueChanged);
            // 
            // fitness_cap_LB
            // 
            this.fitness_cap_LB.AutoSize = true;
            this.fitness_cap_LB.Location = new System.Drawing.Point(36, 440);
            this.fitness_cap_LB.Name = "fitness_cap_LB";
            this.fitness_cap_LB.Size = new System.Drawing.Size(35, 13);
            this.fitness_cap_LB.TabIndex = 20;
            this.fitness_cap_LB.Text = "label1";
            // 
            // fitness_cap_TB
            // 
            this.fitness_cap_TB.Location = new System.Drawing.Point(260, 440);
            this.fitness_cap_TB.Maximum = 20;
            this.fitness_cap_TB.Name = "fitness_cap_TB";
            this.fitness_cap_TB.Size = new System.Drawing.Size(161, 45);
            this.fitness_cap_TB.TabIndex = 19;
            this.fitness_cap_TB.Value = 2;
            this.fitness_cap_TB.ValueChanged += new System.EventHandler(this.Fitness_cap_TB_ValueChanged);
            // 
            // fitnes_dur_LB
            // 
            this.fitnes_dur_LB.AutoSize = true;
            this.fitnes_dur_LB.Location = new System.Drawing.Point(36, 393);
            this.fitnes_dur_LB.Name = "fitnes_dur_LB";
            this.fitnes_dur_LB.Size = new System.Drawing.Size(35, 13);
            this.fitnes_dur_LB.TabIndex = 18;
            this.fitnes_dur_LB.Text = "label1";
            // 
            // fitness_dur_TB
            // 
            this.fitness_dur_TB.Location = new System.Drawing.Point(260, 393);
            this.fitness_dur_TB.Maximum = 20;
            this.fitness_dur_TB.Name = "fitness_dur_TB";
            this.fitness_dur_TB.Size = new System.Drawing.Size(161, 45);
            this.fitness_dur_TB.TabIndex = 17;
            this.fitness_dur_TB.Value = 2;
            this.fitness_dur_TB.ValueChanged += new System.EventHandler(this.Fitness_dur_TB_ValueChanged);
            // 
            // StartupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 590);
            this.Controls.Add(this.fitness_cap_LB);
            this.Controls.Add(this.fitness_cap_TB);
            this.Controls.Add(this.fitnes_dur_LB);
            this.Controls.Add(this.fitness_dur_TB);
            this.Controls.Add(this.restaurant_cap_LB);
            this.Controls.Add(this.restaurant_cap_TB);
            this.Controls.Add(this.cinema_dur_LB);
            this.Controls.Add(this.cinema_dur_TB);
            this.Controls.Add(this.staircase_hte_LB);
            this.Controls.Add(this.staircase_hte_TB);
            this.Controls.Add(this.hte_per_sec_LB);
            this.Controls.Add(this.hte_per_sec_TB);
            this.Controls.Add(this.elevator_cap_LB);
            this.Controls.Add(this.elevator_cap_TB);
            this.Controls.Add(this.elevator_hte_LB);
            this.Controls.Add(this.elevator_hte_TB);
            this.Controls.Add(this.maid_LB);
            this.Controls.Add(this.maid_TB);
            this.Controls.Add(this._runSimulation);
            this.Name = "StartupScreen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_dur_TB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _runSimulation;
        protected System.Windows.Forms.TrackBar maid_TB;
        protected System.Windows.Forms.Label elevator_hte_LB;
        protected System.Windows.Forms.TrackBar elevator_hte_TB;
        protected System.Windows.Forms.Label elevator_cap_LB;
        protected System.Windows.Forms.TrackBar elevator_cap_TB;
        protected System.Windows.Forms.Label hte_per_sec_LB;
        protected System.Windows.Forms.TrackBar hte_per_sec_TB;
        protected System.Windows.Forms.Label staircase_hte_LB;
        protected System.Windows.Forms.TrackBar staircase_hte_TB;
        protected System.Windows.Forms.Label cinema_dur_LB;
        protected System.Windows.Forms.TrackBar cinema_dur_TB;
        protected System.Windows.Forms.Label restaurant_cap_LB;
        protected System.Windows.Forms.TrackBar restaurant_cap_TB;
        protected System.Windows.Forms.Label fitness_cap_LB;
        protected System.Windows.Forms.TrackBar fitness_cap_TB;
        protected System.Windows.Forms.Label fitnes_dur_LB;
        protected System.Windows.Forms.TrackBar fitness_dur_TB;
        public System.Windows.Forms.Label maid_LB;
    }
}

