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
        public string _path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"..\..\..\Assets\Libraries\Hotel_reparatie.layout");
        public List<JsonModel> layout;
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
        private string _fitness_dur = "Duration of fitness in hte ";
        private string _fitness_cap = "Capicity for a fitness facility ";


        public StartupScreen()
        {
            InitializeComponent();

            Console.WriteLine(_path);
       
            //casting string to textbox
            maid_LB.Text = _maid_amount;
            elevator_hte_LB.Text = _elevator_dur;
            elevator_cap_LB.Text = _elevator_cap;
            hte_per_sec_LB.Text = _hte_per_sec;
            staircase_hte_LB.Text = _staircase_hte;
            cinema_dur_LB.Text = _cinema_dur;
            restaurant_cap_LB.Text = restaurant_cap;
            fitnes_dur_LB.Text = _fitness_dur;
            fitness_cap_LB.Text = _fitness_cap;  
        }

        private void _runSimulation_Click(object sender, EventArgs e)
        {
            

            if (layout == null)
            {
                layout = ReadLayoutJson(_path);

            }

            //below the Simulation is linked to this form
            Simulation hotelsimulation = new Simulation(layout,
                                                            maid_TB.Value,
                                                            elevator_hte_TB.Value,
                                                            elevator_cap_TB.Value,
                                                            hte_per_sec_TB.Value,
                                                            staircase_hte_TB.Value,
                                                            cinema_dur_TB.Value,
                                                            restaurant_cap_TB.Value,
                                                            fitness_dur_TB.Value,
                                                            fitness_cap_TB.Value
                                                        );
            hotelsimulation.Show();
           // this.Hide();
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


        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            maid_LB.Text = _maid_amount + maid_TB.Value.ToString();
        }

        private void elevator_hte_TB_ValueChanged(object sender, EventArgs e)
        {
            elevator_hte_LB.Text = _elevator_dur + elevator_hte_TB.Value.ToString();
        }

        private void elevator_cap_TB_ValueChanged(object sender, EventArgs e)
        {
            elevator_cap_LB.Text = _elevator_cap + elevator_cap_TB.Value.ToString();
        }

        private void hte_per_sec_TB_ValueChanged(object sender, EventArgs e)
        {
            hte_per_sec_LB.Text = _hte_per_sec + hte_per_sec_TB.Value.ToString();
        }

        private void staircase_hte_TB_ValueChanged(object sender, EventArgs e)
        {
            staircase_hte_LB.Text = _staircase_hte + staircase_hte_TB.Value.ToString();
        }

        private void cinema_dur_TB_ValueChanged(object sender, EventArgs e)
        {
            cinema_dur_LB.Text = _cinema_dur + cinema_dur_TB.Value.ToString();
        }

        private void restaurant_cap_TB_ValueChanged(object sender, EventArgs e)
        {
            restaurant_cap_LB.Text = restaurant_cap + restaurant_cap_TB.Value.ToString();
        }

        private void fitness_dur_TB_ValueChanged(object sender, EventArgs e)
        {
            fitnes_dur_LB.Text = _fitness_dur + fitness_dur_TB.Value.ToString();

        }

        private void fitness_cap_TB_ValueChanged(object sender, EventArgs e)
        {
            fitness_cap_LB.Text = _fitness_cap + fitness_cap_TB.Value.ToString();

        }
    }
}
