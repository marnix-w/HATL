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
    public partial class Simulation : Form, HotelEventListener
    {
        public Hotel HotelArea { get; set; }
        public int UnitTestvalue { get; set; }
        public List<JsonModel> test_model { get; set; }
        private PictureBox HotelBackground { get; set; }


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
            _fillRichTextBox();
            //_drawHotel();
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

            List<IMovable> a = HotelArea.IMovableList;

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

                HotelBackground.Image = HotelArea.Superimpose(HotelArea.DrawHotel(), HotelArea.DrawMovables());
                HotelBackground.Refresh();
            }
        }

        //Overview of hotel facilities
        private void _fillRichTextBox()
        {
            foreach (IArea i in HotelArea.HotelAreaList)
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

            HotelBackground = new PictureBox
            {
                Location = new Point(50, 100),
                Width = (Hotel.HotelWidth + 1) * 96,
                Height = (Hotel.HotelHeight) * 96,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = HotelArea.Superimpose(HotelArea.DrawHotel(), HotelArea.DrawMovables())
            };

            Controls.Add(HotelBackground);
        }
        

        public void Notify(HotelEvent evt)
        {
           
        }
    }
}