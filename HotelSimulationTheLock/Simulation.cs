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

        public void ShowHotelAreaOverView()
        {
            foreach (IArea i in HotelArea.HotelAreaList)
            {

                string type = i.GetType().ToString().Replace("HotelSimulationTheLock.", "");


                if (type.Equals("Room"))
                {
                    _roomsStatus.Text += type + " " + ((Room)i).Classification + " Star: " + i.Status;
                    _roomsStatus.Text += "\n";
                }
                else if (type.Equals("Restaurant"))
                {
                    _restaurantStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.Status;
                    _restaurantStatus.Text += "\n";
                }
                else if (type.Equals("Fitness"))
                {
                    _fitnessStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.Status;
                    _fitnessStatus.Text += "\n";
                }
                else if (type.Equals("Pool"))
                {
                    _poolStatus.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.Status;
                    _poolStatus.Text += "\n";
                }
                else
                {
          
                }




                PictureBox test = new PictureBox();
                test.Location = new Point(i.Position.X * 96, i.Position.Y * 96);
                test.Width = i.Dimension.X * 96;
                test.Height = i.Dimension.Y * 96;
                test.SizeMode = PictureBoxSizeMode.StretchImage;
                test.Image = i.Art;        

                this.Controls.Add(test);
            }
                       

            for (int i = 1; i < 8; i++)
            {
                //elavator
                PictureBox elevator = new PictureBox();
                elevator.Location = new Point(0, i * 96);
                if (i == 7)
                {
                    elevator.Image = Properties.Resources.elevator_pressent;
                }
                else
                {
                    elevator.Image = Properties.Resources.elevator_not_pressent;
                }
                elevator.SizeMode = PictureBoxSizeMode.AutoSize;
                this.Controls.Add(elevator);

                //staircase
                PictureBox staircase = new PictureBox();
                staircase.Location = new Point(9 * 96, i * 96);
                staircase.Image = Properties.Resources.staircase;
                staircase.SizeMode = PictureBoxSizeMode.AutoSize;
                this.Controls.Add(staircase);
            }

            //lobby
            for (int i = 1; i < 9; i++)
            {
                PictureBox lobby = new PictureBox();
                lobby.Location = new Point(i * 96, 7 * 96);
                if (i%2 == 0)
                {
                    lobby.Image = Properties.Resources.lobby_couch;
                }                
                else
                {
                    lobby.Image = Properties.Resources.lobby_window;
                }
                if (i == 8)
                {
                    lobby.Image = Properties.Resources.reception;
                }


                lobby.SizeMode = PictureBoxSizeMode.AutoSize;
                this.Controls.Add(lobby);
            }



        }



    }
}
