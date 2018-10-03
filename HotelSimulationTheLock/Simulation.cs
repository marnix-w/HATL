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
