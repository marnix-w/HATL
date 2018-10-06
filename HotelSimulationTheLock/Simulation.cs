using HotelEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace HotelSimulationTheLock
{
    public partial class Simulation : Form
    {
        public Hotel Hotel { get; set; }
        public int UnitTestvalue { get; set; }
        public List<JsonModel> HotelLayout { get; set; }
        private PictureBox HotelBackground { get; set; }
        
        public Simulation(List<JsonModel> layout, List<dynamic> SettingsDataSet)
        {
            // 0.5f should be a varible in the settings data set
            Hotel = new Hotel(layout, SettingsDataSet);
            HotelLayout = layout;
            HotelEventManager.HTE_Factor = 0.5f;
            
            // Does this timer work corectly with the HTE factor? -marnix
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer
            {
                Interval = 1000 // specify interval time as you want
            };
            t.Tick += new EventHandler(timer_Tick);
            t.Start();
            
            // Last methods for setup
            InitializeComponent();
            _fillRichTextBox();
            HotelEventManager.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //guest overview
            _guestStatus.Text = string.Empty;
            //maid overview
            _maidStatus.Text = string.Empty;

            List<IMovable> a = Hotel.HotelMovables;

            foreach (IMovable g in a)
            {
                if (g is Guest t)
                {
                    _guestStatus.Text += t.Name + "\t" + g.Status + "\t" + t.RoomRequest + "\t" + t.Position;
                    _guestStatus.Text += "\n";                         
                    
                }
                if (g is Maid m)
                {
                    _maidStatus.Text += "Maid \t" + m.Status + "\t" + m.Position;
                    _maidStatus.Text += "\n";                    
                }

                
            }

            HotelBackground.Image = Hotel.Superimpose(Hotel.DrawMovables());
        }

        //Overview of hotel facilities
        private void _fillRichTextBox()
        {
            foreach (IArea i in Hotel.HotelAreas)
            {
                string type = i.GetType().ToString().Replace("HotelSimulationTheLock.", "");

                switch (type)
                {

                    case "Room":
                        _roomsStatus.Text += ((Room)i).Classification + " Star " + type + "\t" +  i.AreaStatus + "\t " + ((Room)i).Position;
                        _roomsStatus.Text += "\n";
                        break;
                    case "Restaurant":
                        _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _fitnessStatus.Text += "\n";
                        break;
                    case "Fitness":
                        _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _fitnessStatus.Text += "\n";
                        break;
                    default:
                        break;
                }
               
            }

            Hotel.SetHotelBitmap();

            HotelBackground = new PictureBox
            {
                Location = new Point(50, 100),
                Width = (Hotel.HotelWidth + 1) * 96,
                Height = (Hotel.HotelHeight) * 96,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Hotel.Superimpose(Hotel.DrawMovables())
            };

            Controls.Add(HotelBackground);
        }
        
    }
}