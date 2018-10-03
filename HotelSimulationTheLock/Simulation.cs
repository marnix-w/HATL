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
        PictureBox Background = new PictureBox();

        public Simulation(List<JsonModel> layout)
        {
            InitializeComponent();

            HotelArea = new Hotel(layout, 0.5f);

            Reception test = new Reception();

            HotelEventManager.Start();
            Console.WriteLine(!HotelEventManager.Running);

            ShowHotelAreaOverView(); 

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
                }
                if (g is Maid)
                {
                    Maid m = (Maid)g;
                    _maidStatus.Text += "Maid \t" + m.Status + "\t" + m.Position;
                    _maidStatus.Text += "\n";
                }                
            }    
        }

        //Overview of hotel facilities
        public void ShowHotelAreaOverView()
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

                PictureBox test = new PictureBox();
                test.Location = new Point(10, 10);
                test.Width = (Hotel.HotelWidth + 1) * 96;
                test.Height = (Hotel.HotelHieght) * 96;
                test.SizeMode = PictureBoxSizeMode.StretchImage;
                test.Image = HotelArea.DrawHotel();

                this.Controls.Add(test);
            }
        }

    }
}
