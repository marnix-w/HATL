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
            this.maid_LB = new System.Windows.Forms.Label();
            this.elevator_hte_LB = new System.Windows.Forms.Label();
            this.elevator_cap_LB = new System.Windows.Forms.Label();
            this.hte_per_sec_LB = new System.Windows.Forms.Label();
            this.staircase_hte_LB = new System.Windows.Forms.Label();
            this.cinema_dur_LB = new System.Windows.Forms.Label();
            this.restaurant_cap_LB = new System.Windows.Forms.Label();
            this.fitness_cap_LB = new System.Windows.Forms.Label();
            this.eating_dur_LB = new System.Windows.Forms.Label();
            this.maid_TB = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.find_file = new System.Windows.Forms.Button();
            this.restaurant_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.file_path_TB = new System.Windows.Forms.TextBox();
            this.cinema_dur_TB = new System.Windows.Forms.NumericUpDown();
            this.eating_dur_TB = new System.Windows.Forms.NumericUpDown();
            this.hte_per_sec_TB = new System.Windows.Forms.NumericUpDown();
            this.staircase_hte_TB = new System.Windows.Forms.NumericUpDown();
            this.fitness_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.elevator_cap_TB = new System.Windows.Forms.NumericUpDown();
            this.elevator_hte_TB = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eating_dur_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // _runSimulation
            // 
            this._runSimulation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(77)))), ((int)(((byte)(127)))));
            this._runSimulation.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this._runSimulation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._runSimulation.Font = new System.Drawing.Font("Lucida Console", 12F);
            this._runSimulation.ForeColor = System.Drawing.Color.Yellow;
            this._runSimulation.Location = new System.Drawing.Point(439, 469);
            this._runSimulation.Name = "_runSimulation";
            this._runSimulation.Size = new System.Drawing.Size(226, 38);
            this._runSimulation.TabIndex = 0;
            this._runSimulation.Text = "Run Simulation";
            this._runSimulation.UseVisualStyleBackColor = false;
            this._runSimulation.Click += new System.EventHandler(this._runSimulation_Click);
            // 
            // maid_LB
            // 
            this.maid_LB.AutoSize = true;
            this.maid_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maid_LB.ForeColor = System.Drawing.Color.Yellow;
            this.maid_LB.Location = new System.Drawing.Point(14, 12);
            this.maid_LB.Name = "maid_LB";
            this.maid_LB.Size = new System.Drawing.Size(68, 16);
            this.maid_LB.TabIndex = 4;
            this.maid_LB.Text = "label1";
            // 
            // elevator_hte_LB
            // 
            this.elevator_hte_LB.AutoSize = true;
            this.elevator_hte_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elevator_hte_LB.ForeColor = System.Drawing.Color.Yellow;
            this.elevator_hte_LB.Location = new System.Drawing.Point(15, 66);
            this.elevator_hte_LB.Name = "elevator_hte_LB";
            this.elevator_hte_LB.Size = new System.Drawing.Size(68, 16);
            this.elevator_hte_LB.TabIndex = 6;
            this.elevator_hte_LB.Text = "label1";
            // 
            // elevator_cap_LB
            // 
            this.elevator_cap_LB.AutoSize = true;
            this.elevator_cap_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elevator_cap_LB.ForeColor = System.Drawing.Color.Yellow;
            this.elevator_cap_LB.Location = new System.Drawing.Point(16, 120);
            this.elevator_cap_LB.Name = "elevator_cap_LB";
            this.elevator_cap_LB.Size = new System.Drawing.Size(68, 16);
            this.elevator_cap_LB.TabIndex = 8;
            this.elevator_cap_LB.Text = "label1";
            // 
            // hte_per_sec_LB
            // 
            this.hte_per_sec_LB.AutoSize = true;
            this.hte_per_sec_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hte_per_sec_LB.ForeColor = System.Drawing.Color.Yellow;
            this.hte_per_sec_LB.Location = new System.Drawing.Point(14, 174);
            this.hte_per_sec_LB.Name = "hte_per_sec_LB";
            this.hte_per_sec_LB.Size = new System.Drawing.Size(68, 16);
            this.hte_per_sec_LB.TabIndex = 10;
            this.hte_per_sec_LB.Text = "label1";
            // 
            // staircase_hte_LB
            // 
            this.staircase_hte_LB.AutoSize = true;
            this.staircase_hte_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.staircase_hte_LB.ForeColor = System.Drawing.Color.Yellow;
            this.staircase_hte_LB.Location = new System.Drawing.Point(16, 228);
            this.staircase_hte_LB.Name = "staircase_hte_LB";
            this.staircase_hte_LB.Size = new System.Drawing.Size(68, 16);
            this.staircase_hte_LB.TabIndex = 12;
            this.staircase_hte_LB.Text = "label1";
            // 
            // cinema_dur_LB
            // 
            this.cinema_dur_LB.AutoSize = true;
            this.cinema_dur_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cinema_dur_LB.ForeColor = System.Drawing.Color.Yellow;
            this.cinema_dur_LB.Location = new System.Drawing.Point(16, 282);
            this.cinema_dur_LB.Name = "cinema_dur_LB";
            this.cinema_dur_LB.Size = new System.Drawing.Size(68, 16);
            this.cinema_dur_LB.TabIndex = 14;
            this.cinema_dur_LB.Text = "label1";
            // 
            // restaurant_cap_LB
            // 
            this.restaurant_cap_LB.AutoSize = true;
            this.restaurant_cap_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restaurant_cap_LB.ForeColor = System.Drawing.Color.Yellow;
            this.restaurant_cap_LB.Location = new System.Drawing.Point(16, 336);
            this.restaurant_cap_LB.Name = "restaurant_cap_LB";
            this.restaurant_cap_LB.Size = new System.Drawing.Size(68, 16);
            this.restaurant_cap_LB.TabIndex = 16;
            this.restaurant_cap_LB.Text = "label1";
            // 
            // fitness_cap_LB
            // 
            this.fitness_cap_LB.AutoSize = true;
            this.fitness_cap_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fitness_cap_LB.ForeColor = System.Drawing.Color.Yellow;
            this.fitness_cap_LB.Location = new System.Drawing.Point(16, 444);
            this.fitness_cap_LB.Name = "fitness_cap_LB";
            this.fitness_cap_LB.Size = new System.Drawing.Size(68, 16);
            this.fitness_cap_LB.TabIndex = 20;
            this.fitness_cap_LB.Text = "label1";
            // 
            // eating_dur_LB
            // 
            this.eating_dur_LB.AutoSize = true;
            this.eating_dur_LB.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eating_dur_LB.ForeColor = System.Drawing.Color.Yellow;
            this.eating_dur_LB.Location = new System.Drawing.Point(14, 390);
            this.eating_dur_LB.Name = "eating_dur_LB";
            this.eating_dur_LB.Size = new System.Drawing.Size(68, 16);
            this.eating_dur_LB.TabIndex = 18;
            this.eating_dur_LB.Text = "label1";
            // 
            // maid_TB
            // 
            this.maid_TB.Location = new System.Drawing.Point(19, 37);
            this.maid_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maid_TB.Name = "maid_TB";
            this.maid_TB.Size = new System.Drawing.Size(120, 20);
            this.maid_TB.TabIndex = 21;
            this.maid_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.find_file);
            this.panel1.Controls.Add(this.restaurant_cap_TB);
            this.panel1.Controls.Add(this.file_path_TB);
            this.panel1.Controls.Add(this.fitness_cap_LB);
            this.panel1.Controls.Add(this.cinema_dur_TB);
            this.panel1.Controls.Add(this.eating_dur_TB);
            this.panel1.Controls.Add(this.eating_dur_LB);
            this.panel1.Controls.Add(this.hte_per_sec_TB);
            this.panel1.Controls.Add(this.staircase_hte_TB);
            this.panel1.Controls.Add(this.restaurant_cap_LB);
            this.panel1.Controls.Add(this.fitness_cap_TB);
            this.panel1.Controls.Add(this.elevator_cap_TB);
            this.panel1.Controls.Add(this.cinema_dur_LB);
            this.panel1.Controls.Add(this.elevator_hte_TB);
            this.panel1.Controls.Add(this.maid_TB);
            this.panel1.Controls.Add(this.staircase_hte_LB);
            this.panel1.Controls.Add(this.maid_LB);
            this.panel1.Controls.Add(this.elevator_hte_LB);
            this.panel1.Controls.Add(this.hte_per_sec_LB);
            this.panel1.Controls.Add(this.elevator_cap_LB);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 561);
            this.panel1.TabIndex = 22;
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
            this.find_file.Location = new System.Drawing.Point(19, 533);
            this.find_file.Name = "find_file";
            this.find_file.Size = new System.Drawing.Size(89, 23);
            this.find_file.TabIndex = 101;
            this.find_file.Text = "Browse";
            this.find_file.UseVisualStyleBackColor = false;
            this.find_file.Click += new System.EventHandler(this.find_file_Click);
            // 
            // restaurant_cap_TB
            // 
            this.restaurant_cap_TB.Location = new System.Drawing.Point(19, 361);
            this.restaurant_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.restaurant_cap_TB.Name = "restaurant_cap_TB";
            this.restaurant_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.restaurant_cap_TB.TabIndex = 29;
            this.restaurant_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // file_path_TB
            // 
            this.file_path_TB.Enabled = false;
            this.file_path_TB.Location = new System.Drawing.Point(19, 507);
            this.file_path_TB.Name = "file_path_TB";
            this.file_path_TB.ReadOnly = true;
            this.file_path_TB.Size = new System.Drawing.Size(326, 20);
            this.file_path_TB.TabIndex = 102;
            this.file_path_TB.TabStop = false;
            // 
            // cinema_dur_TB
            // 
            this.cinema_dur_TB.Location = new System.Drawing.Point(19, 307);
            this.cinema_dur_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.cinema_dur_TB.Name = "cinema_dur_TB";
            this.cinema_dur_TB.Size = new System.Drawing.Size(120, 20);
            this.cinema_dur_TB.TabIndex = 28;
            this.cinema_dur_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // eating_dur_TB
            // 
            this.eating_dur_TB.Location = new System.Drawing.Point(19, 415);
            this.eating_dur_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.eating_dur_TB.Name = "eating_dur_TB";
            this.eating_dur_TB.Size = new System.Drawing.Size(120, 20);
            this.eating_dur_TB.TabIndex = 27;
            this.eating_dur_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // hte_per_sec_TB
            // 
            this.hte_per_sec_TB.Location = new System.Drawing.Point(19, 199);
            this.hte_per_sec_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.hte_per_sec_TB.Name = "hte_per_sec_TB";
            this.hte_per_sec_TB.Size = new System.Drawing.Size(120, 20);
            this.hte_per_sec_TB.TabIndex = 26;
            this.hte_per_sec_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // staircase_hte_TB
            // 
            this.staircase_hte_TB.Location = new System.Drawing.Point(19, 253);
            this.staircase_hte_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.staircase_hte_TB.Name = "staircase_hte_TB";
            this.staircase_hte_TB.Size = new System.Drawing.Size(120, 20);
            this.staircase_hte_TB.TabIndex = 25;
            this.staircase_hte_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // fitness_cap_TB
            // 
            this.fitness_cap_TB.Location = new System.Drawing.Point(19, 469);
            this.fitness_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.fitness_cap_TB.Name = "fitness_cap_TB";
            this.fitness_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.fitness_cap_TB.TabIndex = 24;
            this.fitness_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // elevator_cap_TB
            // 
            this.elevator_cap_TB.Location = new System.Drawing.Point(19, 145);
            this.elevator_cap_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.elevator_cap_TB.Name = "elevator_cap_TB";
            this.elevator_cap_TB.Size = new System.Drawing.Size(120, 20);
            this.elevator_cap_TB.TabIndex = 23;
            this.elevator_cap_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // elevator_hte_TB
            // 
            this.elevator_hte_TB.Location = new System.Drawing.Point(19, 91);
            this.elevator_hte_TB.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.elevator_hte_TB.Name = "elevator_hte_TB";
            this.elevator_hte_TB.Size = new System.Drawing.Size(120, 20);
            this.elevator_hte_TB.TabIndex = 22;
            this.elevator_hte_TB.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::HotelSimulationTheLock.Properties.Resources.background_lich_king;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(984, 561);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // StartupScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._runSimulation);
            this.Controls.Add(this.pictureBox1);
            this.Name = "StartupScreen";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.maid_TB)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.restaurant_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cinema_dur_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eating_dur_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hte_per_sec_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staircase_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitness_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_cap_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.elevator_hte_TB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _runSimulation;
        protected System.Windows.Forms.Label elevator_hte_LB;
        protected System.Windows.Forms.Label elevator_cap_LB;
        protected System.Windows.Forms.Label hte_per_sec_LB;
        protected System.Windows.Forms.Label staircase_hte_LB;
        protected System.Windows.Forms.Label cinema_dur_LB;
        protected System.Windows.Forms.Label restaurant_cap_LB;
        protected System.Windows.Forms.Label fitness_cap_LB;
        protected System.Windows.Forms.Label eating_dur_LB;
        public System.Windows.Forms.Label maid_LB;
        private System.Windows.Forms.NumericUpDown maid_TB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown restaurant_cap_TB;
        private System.Windows.Forms.NumericUpDown cinema_dur_TB;
        private System.Windows.Forms.NumericUpDown eating_dur_TB;
        private System.Windows.Forms.NumericUpDown hte_per_sec_TB;
        private System.Windows.Forms.NumericUpDown staircase_hte_TB;
        private System.Windows.Forms.NumericUpDown fitness_cap_TB;
        private System.Windows.Forms.NumericUpDown elevator_cap_TB;
        private System.Windows.Forms.NumericUpDown elevator_hte_TB;
        private System.Windows.Forms.Button find_file;
        private System.Windows.Forms.TextBox file_path_TB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

