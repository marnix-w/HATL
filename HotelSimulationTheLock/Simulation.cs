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
        public Hotel HotelArea { get; set; }
        public int UnitTestvalue { get; set; }
        public List<JsonModel> test_model { get; set; }

        public Simulation(List<JsonModel> layout,
                                            int maid_amount,
                                                int elevator_in_hte,
                                                    int elevator_cap,
                                                        int hte_per_sec,
                                                            int staircase_in_hte,
                                                                int cinema_dur,
                                                                    int restaurant_cap,
                                                                        int fitness_dur,
                                                                            int fitness_cap)

        {
            InitializeComponent();

            test_model = layout;


            HotelArea = new Hotel(layout, 0.5f);

            Reception test = new Reception();

            HotelEventManager.Start();
            Console.WriteLine(!HotelEventManager.Running);

            //Calling function to fill RichTextboxes
            _fillHotelSimulation();

            HotelEventManager.HTE_Factor = 0.5f;

            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();


            t.Interval = 1000; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            t.Start();

        }

        void timer_Tick(object sender, EventArgs e)
        {
            //guest overview
            _guestStatus.Text = string.Empty;
            //maid overview
            _maidStatus.Text = string.Empty;

            foreach (IMovable g in HotelArea.IMovableList)
            {
                if (g is Guest)
                {
                    Guest t = (Guest)g;
                    _guestStatus.Text += t.Name + "\t" + g.Status + "\t" + t.RoomRequest;
                    _guestStatus.Text += "\n";

                    this.Controls.Add(g.Art);
                    if (g.Art.Left < 600)
                    {
                        g.Art.Left += 40;
                    }
                    g.Art.BringToFront();
                }
                if (g is Maid)
                {
                    Maid m = (Maid)g;
                    _maidStatus.Text += "Maid \t" + m.Status + "\t" + m.Position;
                    _maidStatus.Text += "\n";
                    this.Controls.Add(m.Art);
                    m.Art.BringToFront();
                }
            }
        }

        //Overview of hotel facilities
        private void _fillHotelSimulation()
        {
            foreach (IArea i in HotelArea.HotelAreaList)
            {
                string type = i.GetType().ToString().Replace("HotelSimulationTheLock.", "");

                switch (type)
                {

                    case "Room":
                        _roomsStatus.Text += type + " " + ((Room)i).Classification + " Star: " + i.AreaStatus;
                        _roomsStatus.Text += "\n";
                        break;
                    case "Restaurant":
                        _restaurantStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _restaurantStatus.Text += "\n";
                        break;
                    case "Fitness":
                        _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.AreaStatus;
                        _fitnessStatus.Text += "\n";
                        break;
                    default:
                        break;
                }

                PictureBox HotelBackground = new PictureBox();
                HotelBackground.Location = new Point(50, 100);
                HotelBackground.Width = (Hotel.HotelWidth + 1) * 96;
                HotelBackground.Height = (Hotel.HotelHeight) * 96;
                HotelBackground.SizeMode = PictureBoxSizeMode.StretchImage;
                HotelBackground.Image = HotelArea.DrawHotel();
                Controls.Add(HotelBackground);
                HotelBackground.SendToBack();
            }
        }

    }
}