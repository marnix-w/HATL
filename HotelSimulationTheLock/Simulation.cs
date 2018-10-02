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
                // DrawHotelToBitmap(i.Art, i.Position.X * 96, i.Position.Y * 96, i.Dimension.X * 96, i.Dimension.Y * 96);

                _eventsOutput.Text += i.Position;
                _eventsOutput.Text += i.Status;
                _eventsOutput.Text += i.Dimension;
                _eventsOutput.Text += i.Capacity;
                _eventsOutput.Text += i.GetType().ToString();

                _eventsOutput.Text += "\n";

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



        }



    }
}
