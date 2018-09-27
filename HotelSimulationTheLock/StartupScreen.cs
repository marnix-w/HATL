using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public partial class StartupScreen : Form
    {  

        public StartupScreen()
        {
            InitializeComponent();

            
        }

        private void _runSimulation_Click(object sender, EventArgs e)
        {
            //below the Simulation is linked to this form
            Simulation hotelsimulation = new Simulation();
            hotelsimulation.Show();
            this.Hide();
        }
    }
}
