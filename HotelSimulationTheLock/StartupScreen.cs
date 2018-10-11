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


        // private string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\Assets\Libraries\Hotel.layout");
        public string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\Assets\Libraries\Hotel3.layout");

        public List<JsonModel> layout { get; set; }
        public SettingsModel settings { get; set; }
        //global hte settings
        private string _hte_per_sec = "Amount HTE ticks per second ";

        //setting text for maid
        private string _maid_amount = "Amount of Maids ";

        //elevator settings
        private string _elevator_dur = "Amount of HTE ticks for elevator ";
        private string _elevator_cap = "Capicity for the elevator ";

        //staircase settings  
        private string _staircase_hte = "Amount of HTE ticks for staircase ";

        //cinema settings
        private string _cinema_dur = "Duration of the cinema in hte ";

        //restaurant settings
        private string restaurant_cap = "Capicity of the restaurant ";

        //fitness settings
        private string _eating_dur = "Eating in hte ";
        private string _fitness_cap = "Capicity for a fitness facility ";

        
       
        public StartupScreen()
        {
            InitializeComponent();

            //path for default layout
            file_path_TB.Text = _path;

            //casting string to textbox
            maid_LB.Text = _maid_amount;
            elevator_hte_LB.Text = _elevator_dur;
            elevator_cap_LB.Text = _elevator_cap;
            hte_per_sec_LB.Text = _hte_per_sec;
            staircase_hte_LB.Text = _staircase_hte;
            cinema_dur_LB.Text = _cinema_dur;
            restaurant_cap_LB.Text = restaurant_cap;
            eating_dur_LB.Text = _eating_dur;
            fitness_cap_LB.Text = _fitness_cap;
        }
        
        // Json uitlezen en dan een list van maken voor layout
        public List<JsonModel> ReadLayoutJson(string path)
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

     

        private void _runSimulation_Click_1(object sender, EventArgs e)
        {
            string path = file_path_TB.Text;

            if (layout == null)
            {
                layout = ReadLayoutJson(path);
            }

            settings = new SettingsModel
            {
                AmountOfMaids = Decimal.ToInt32(maid_TB.Value),
                ElevatorDuration = Decimal.ToInt32(elevator_hte_TB.Value),
                ElevatorCapicity = Decimal.ToInt32(elevator_cap_TB.Value),
                HTEPerSeconds = Decimal.ToInt32(hte_per_sec_TB.Value),
                StairsDuration = Decimal.ToInt32(staircase_hte_TB.Value),
                CinemaDuration = Decimal.ToInt32(cinema_dur_TB.Value),
                RestaurantCapicity = Decimal.ToInt32(restaurant_cap_TB.Value),
                EatingDuration = Decimal.ToInt32(eating_dur_TB.Value),
                FitnessCapicity = Decimal.ToInt32(fitness_cap_TB.Value)
            };

            //below the Simulation is linked to this form
            Simulation hotelsimulation = new Simulation(layout, settings);

            hotelsimulation.Show();
            this.Hide();
        }

        private void find_file_Click_1(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                file_path_TB.Text = path;
            }
        
    }
    }
}