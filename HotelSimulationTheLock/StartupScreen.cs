using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public partial class StartupScreen : Form
    {      
        private string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\Assets\Libraries\Hotel.layout");

        public StartupScreen()
        {
            InitializeComponent();

            Console.WriteLine(_path);
    
            
        }

        private void _runSimulation_Click(object sender, EventArgs e)
        {
            List<JsonModel> layout = null;

            if (layout == null)
            {
                layout = ReadLayoutJson(_path);
              
            }

            //below the Simulation is linked to this form
            Simulation hotelsimulation = new Simulation(layout);
            hotelsimulation.Show();
            this.Hide();
        }


        // Json uitlezen en dan een list van maken voor layout
        private List<JsonModel> ReadLayoutJson(string path)
        {
            try
            {
                StreamReader file = new StreamReader(path);
                string json = file.ReadToEnd();
                file.Close();
                return JsonConvert.DeserializeObject<List<JsonModel>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        private void StartupScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}
