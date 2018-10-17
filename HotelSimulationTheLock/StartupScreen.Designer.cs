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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this._runSimulation = new System.Windows.Forms.Button();
            this.find_file = new System.Windows.Forms.Button();
            this.restaurant_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.file_path_TB = new System.Windows.Forms.TextBox();
            this.fitness_cap_LB = new System.Windows.Forms.Label();
            this.cinema_dur_TB = new System.Windows.Forms.NumericUpDown();
            this.eating_dur_TB = new System.Windows.Forms.NumericUpDown();
            this.eating_dur_LB = new System.Windows.Forms.Label();
            this.hte_per_sec_TB = new System.Windows.Forms.NumericUpDown();
            this.staircase_hte_TB = new System.Windows.Forms.NumericUpDown();
            this.restaurant_cap_LB = new System.Windows.Forms.Label();
            this.fitness_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.elevator_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.cinema_dur_LB = new System.Windows.Forms.Label();
            this.elevator_hte_TB = new System.Windows.Forms.NumericUpDown();
            this.maid_TB = new System.Windows.Forms.NumericUpDown();
            this.staircase_hte_LB = new System.Windows.Forms.Label();
            this.maid_LB = new System.Windows.Forms.Label();
            this.elevator_hte_LB = new System.Windows.Forms.Label();
            this.hte_per_sec_LB = new System.Windows.Forms.Label();
            this.elevator_cap_LB = new System.Windows.Forms.Label();
            this.restaurant_dur_TB = new System.Windows.Forms.NumericUpDown();
            this.restaurant_dur_LB = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eating_dur_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_dur_TB)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // _runSimulation
            // 
            this._runSimulation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(77)))), ((int)(((byte)(127)))));
            this._runSimulation.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this._runSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._runSimulation.Font = new System.Drawing.Font("Lucida Console", 12F);
            this._runSimulation.ForeColor = System.Drawing.Color.Yellow;
            this._runSimulation.Location = new System.Drawing.Point(246, 359);
            this._runSimulation.Name = "_runSimulation";
            this._runSimulation.Size = new System.Drawing.Size(226, 38);
            this._runSimulation.TabIndex = 103;
            this._runSimulation.Text = "Run Simulation";
            this._runSimulation.UseVisualStyleBackColor = false;
            this._runSimulation.Click += new System.EventHandler(this._runSimulation_Click_1);
            // 
            // find_file
            // 
            this.find_file.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(78)))), ((int)(((byte)(160)))));
            this.find_file.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.find_file.FlatAppearance.BorderSize = 0;
            this.find_file.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.find_file.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.find_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.find_file.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.find_file.ForeColor = System.Drawing.Color.Yellow;
            this.find_file.Location = new System.Drawing.Point(393, 274);
            this.find_file.Name = "find_file";
            this.find_file.Size = new System.Drawing.Size(89, 23);
            this.find_file.TabIndex = 122;
            this.find_file.Text = "Browse";
            this.find_file.UseVisualStyleBackColor = false;
            this.find_file.Click += new System.EventHandler(this.find_file_Click_1);
            // 
            // restaurant_cap_TB
            // 
            this.restaurant_cap_TB.Location = new System.Drawing.Point(393, 102);
            this.restaurant_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.restaurant_cap_TB.Name = "restaurant_cap_TB";
            this.restaurant_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.restaurant_cap_TB.TabIndex = 121;
            this.restaurant_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // file_path_TB
            // 
            this.file_path_TB.Enabled = false;
            this.file_path_TB.Location = new System.Drawing.Point(393, 248);
            this.file_path_TB.Name = "file_path_TB";
            this.file_path_TB.ReadOnly = true;
            this.file_path_TB.Size = new System.Drawing.Size(326, 20);
            this.file_path_TB.TabIndex = 123;
            this.file_path_TB.TabStop = false;
            // 
            // fitness_cap_LB
            // 
            this.fitness_cap_LB.AutoSize = true;
            this.fitness_cap_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fitness_cap_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fitness_cap_LB.Location = new System.Drawing.Point(390, 185);
            this.fitness_cap_LB.Name = "fitness_cap_LB";
            this.fitness_cap_LB.Size = new System.Drawing.Size(56, 18);
            this.fitness_cap_LB.TabIndex = 112;
            this.fitness_cap_LB.Text = "label1";
            // 
            // cinema_dur_TB
            // 
            this.cinema_dur_TB.Location = new System.Drawing.Point(393, 48);
            this.cinema_dur_TB.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.cinema_dur_TB.Name = "cinema_dur_TB";
            this.cinema_dur_TB.Size = new System.Drawing.Size(120, 20);
            this.cinema_dur_TB.TabIndex = 120;
            this.cinema_dur_TB.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // eating_dur_TB
            // 
            this.eating_dur_TB.Location = new System.Drawing.Point(393, 156);
            this.eating_dur_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.eating_dur_TB.Name = "eating_dur_TB";
            this.eating_dur_TB.Size = new System.Drawing.Size(120, 20);
            this.eating_dur_TB.TabIndex = 119;
            this.eating_dur_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // eating_dur_LB
            // 
            this.eating_dur_LB.AutoSize = true;
            this.eating_dur_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eating_dur_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.eating_dur_LB.Location = new System.Drawing.Point(388, 131);
            this.eating_dur_LB.Name = "eating_dur_LB";
            this.eating_dur_LB.Size = new System.Drawing.Size(56, 18);
            this.eating_dur_LB.TabIndex = 111;
            this.eating_dur_LB.Text = "label1";
            // 
            // hte_per_sec_TB
            // 
            this.hte_per_sec_TB.Location = new System.Drawing.Point(39, 210);
            this.hte_per_sec_TB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hte_per_sec_TB.Name = "hte_per_sec_TB";
            this.hte_per_sec_TB.Size = new System.Drawing.Size(120, 20);
            this.hte_per_sec_TB.TabIndex = 118;
            this.hte_per_sec_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // staircase_hte_TB
            // 
            this.staircase_hte_TB.Location = new System.Drawing.Point(39, 264);
            this.staircase_hte_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.staircase_hte_TB.Name = "staircase_hte_TB";
            this.staircase_hte_TB.Size = new System.Drawing.Size(120, 20);
            this.staircase_hte_TB.TabIndex = 117;
            this.staircase_hte_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // restaurant_cap_LB
            // 
            this.restaurant_cap_LB.AutoSize = true;
            this.restaurant_cap_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restaurant_cap_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.restaurant_cap_LB.Location = new System.Drawing.Point(390, 77);
            this.restaurant_cap_LB.Name = "restaurant_cap_LB";
            this.restaurant_cap_LB.Size = new System.Drawing.Size(56, 18);
            this.restaurant_cap_LB.TabIndex = 110;
            this.restaurant_cap_LB.Text = "label1";
            // 
            // fitness_cap_TB
            // 
            this.fitness_cap_TB.Location = new System.Drawing.Point(393, 210);
            this.fitness_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.fitness_cap_TB.Name = "fitness_cap_TB";
            this.fitness_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.fitness_cap_TB.TabIndex = 116;
            this.fitness_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // elevator_cap_TB
            // 
            this.elevator_cap_TB.Location = new System.Drawing.Point(39, 156);
            this.elevator_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.elevator_cap_TB.Name = "elevator_cap_TB";
            this.elevator_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.elevator_cap_TB.TabIndex = 115;
            this.elevator_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cinema_dur_LB
            // 
            this.cinema_dur_LB.AutoSize = true;
            this.cinema_dur_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cinema_dur_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cinema_dur_LB.Location = new System.Drawing.Point(390, 23);
            this.cinema_dur_LB.Name = "cinema_dur_LB";
            this.cinema_dur_LB.Size = new System.Drawing.Size(56, 18);
            this.cinema_dur_LB.TabIndex = 109;
            this.cinema_dur_LB.Text = "label1";
            // 
            // elevator_hte_TB
            // 
            this.elevator_hte_TB.Location = new System.Drawing.Point(39, 102);
            this.elevator_hte_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.elevator_hte_TB.Name = "elevator_hte_TB";
            this.elevator_hte_TB.Size = new System.Drawing.Size(120, 20);
            this.elevator_hte_TB.TabIndex = 114;
            this.elevator_hte_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // maid_TB
            // 
            this.maid_TB.Location = new System.Drawing.Point(39, 48);
            this.maid_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maid_TB.Name = "maid_TB";
            this.maid_TB.Size = new System.Drawing.Size(120, 20);
            this.maid_TB.TabIndex = 113;
            this.maid_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // staircase_hte_LB
            // 
            this.staircase_hte_LB.AutoSize = true;
            this.staircase_hte_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staircase_hte_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.staircase_hte_LB.Location = new System.Drawing.Point(36, 239);
            this.staircase_hte_LB.Name = "staircase_hte_LB";
            this.staircase_hte_LB.Size = new System.Drawing.Size(56, 18);
            this.staircase_hte_LB.TabIndex = 108;
            this.staircase_hte_LB.Text = "label1";
            // 
            // maid_LB
            // 
            this.maid_LB.AutoSize = true;
            this.maid_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maid_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.maid_LB.Location = new System.Drawing.Point(34, 23);
            this.maid_LB.Name = "maid_LB";
            this.maid_LB.Size = new System.Drawing.Size(56, 18);
            this.maid_LB.TabIndex = 104;
            this.maid_LB.Text = "label1";
            // 
            // elevator_hte_LB
            // 
            this.elevator_hte_LB.AutoSize = true;
            this.elevator_hte_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elevator_hte_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.elevator_hte_LB.Location = new System.Drawing.Point(35, 77);
            this.elevator_hte_LB.Name = "elevator_hte_LB";
            this.elevator_hte_LB.Size = new System.Drawing.Size(56, 18);
            this.elevator_hte_LB.TabIndex = 105;
            this.elevator_hte_LB.Text = "label1";
            // 
            // hte_per_sec_LB
            // 
            this.hte_per_sec_LB.AutoSize = true;
            this.hte_per_sec_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hte_per_sec_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.hte_per_sec_LB.Location = new System.Drawing.Point(34, 185);
            this.hte_per_sec_LB.Name = "hte_per_sec_LB";
            this.hte_per_sec_LB.Size = new System.Drawing.Size(56, 18);
            this.hte_per_sec_LB.TabIndex = 107;
            this.hte_per_sec_LB.Text = "label1";
            // 
            // elevator_cap_LB
            // 
            this.elevator_cap_LB.AutoSize = true;
            this.elevator_cap_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elevator_cap_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.elevator_cap_LB.Location = new System.Drawing.Point(36, 131);
            this.elevator_cap_LB.Name = "elevator_cap_LB";
            this.elevator_cap_LB.Size = new System.Drawing.Size(56, 18);
            this.elevator_cap_LB.TabIndex = 106;
            this.elevator_cap_LB.Text = "label1";
            // 
            // restaurant_dur_TB
            // 
            this.restaurant_dur_TB.Location = new System.Drawing.Point(39, 321);
            this.restaurant_dur_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.restaurant_dur_TB.Name = "restaurant_dur_TB";
            this.restaurant_dur_TB.Size = new System.Drawing.Size(120, 20);
            this.restaurant_dur_TB.TabIndex = 125;
            this.restaurant_dur_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // restaurant_dur_LB
            // 
            this.restaurant_dur_LB.AutoSize = true;
            this.restaurant_dur_LB.Font = new System.Drawing.Font("Lucida Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restaurant_dur_LB.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.restaurant_dur_LB.Location = new System.Drawing.Point(36, 296);
            this.restaurant_dur_LB.Name = "restaurant_dur_LB";
            this.restaurant_dur_LB.Size = new System.Drawing.Size(56, 18);
            this.restaurant_dur_LB.TabIndex = 124;
            this.restaurant_dur_LB.Text = "label1";
            // 
            // StartupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 421);
            this.Controls.Add(this.restaurant_dur_TB);
            this.Controls.Add(this.restaurant_dur_LB);
            this.Controls.Add(this._runSimulation);
            this.Controls.Add(this.find_file);
            this.Controls.Add(this.restaurant_cap_TB);
            this.Controls.Add(this.file_path_TB);
            this.Controls.Add(this.fitness_cap_LB);
            this.Controls.Add(this.cinema_dur_TB);
            this.Controls.Add(this.eating_dur_TB);
            this.Controls.Add(this.eating_dur_LB);
            this.Controls.Add(this.hte_per_sec_TB);
            this.Controls.Add(this.staircase_hte_TB);
            this.Controls.Add(this.restaurant_cap_LB);
            this.Controls.Add(this.fitness_cap_TB);
            this.Controls.Add(this.elevator_cap_TB);
            this.Controls.Add(this.cinema_dur_LB);
            this.Controls.Add(this.elevator_hte_TB);
            this.Controls.Add(this.maid_TB);
            this.Controls.Add(this.staircase_hte_LB);
            this.Controls.Add(this.maid_LB);
            this.Controls.Add(this.elevator_hte_LB);
            this.Controls.Add(this.hte_per_sec_LB);
            this.Controls.Add(this.elevator_cap_LB);
            this.Name = "StartupScreen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eating_dur_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_dur_TB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button _runSimulation;
        private System.Windows.Forms.Button find_file;
        private System.Windows.Forms.NumericUpDown restaurant_cap_TB;
        private System.Windows.Forms.TextBox file_path_TB;
        protected System.Windows.Forms.Label fitness_cap_LB;
        private System.Windows.Forms.NumericUpDown cinema_dur_TB;
        private System.Windows.Forms.NumericUpDown eating_dur_TB;
        protected System.Windows.Forms.Label eating_dur_LB;
        private System.Windows.Forms.NumericUpDown hte_per_sec_TB;
        private System.Windows.Forms.NumericUpDown staircase_hte_TB;
        protected System.Windows.Forms.Label restaurant_cap_LB;
        private System.Windows.Forms.NumericUpDown fitness_cap_TB;
        private System.Windows.Forms.NumericUpDown elevator_cap_TB;
        protected System.Windows.Forms.Label cinema_dur_LB;
        private System.Windows.Forms.NumericUpDown elevator_hte_TB;
        private System.Windows.Forms.NumericUpDown maid_TB;
        protected System.Windows.Forms.Label staircase_hte_LB;
        public System.Windows.Forms.Label maid_LB;
        protected System.Windows.Forms.Label elevator_hte_LB;
        protected System.Windows.Forms.Label hte_per_sec_LB;
        protected System.Windows.Forms.Label elevator_cap_LB;
        private System.Windows.Forms.NumericUpDown restaurant_dur_TB;
        protected System.Windows.Forms.Label restaurant_dur_LB;
    }
}

