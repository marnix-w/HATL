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

        public Simulation(List<JsonModel> layout)
        {
            InitializeComponent();

            HotelArea = new Hotel(layout, 0.5f);

            Reception test = new Reception();

            HotelEventManager.Start();
            Console.WriteLine(!HotelEventManager.Running);



            //foreach (JsonModel item in layout)
            //{
            //    _eventsOutput.Text += item.Classification;
            //    _eventsOutput.Text += item.AreaType;
            //    _eventsOutput.Text += item.ID;
            //    _eventsOutput.Text += item.Position;
            //    _eventsOutput.Text += item.Dimension;
            //    _eventsOutput.Text += item.Capacity;

            //    _eventsOutput.Text += "\n";
            //}

            showHotelAreaOverView();

            HotelEventManager.HTE_Factor = 0.5f;

        }

        public void showHotelAreaOverView()
        {

            foreach (IArea i in HotelArea.HotelAreaList)
            {
                //_eventsOutput.Text += i.Position;
                //_eventsOutput.Text += i.Status;
                //_eventsOutput.Text += i.Dimension;
                //_eventsOutput.Text += i.Capacity;
                //_eventsOutput.Text += i.GetType().ToString();

                //_eventsOutput.Text += "\n";

                string type = i.GetType().ToString().Replace("HotelSimulationTheLock.", "");

                if (type.Equals("Room"))
                {
                    _eventsOutput.Text += type + " " + ((Room)i).Classification + " Star: " + i.Status;
                }
                else
                {
                    _eventsOutput.Text += i.GetType().ToString().Replace("HotelSimulationTheLock.", "") + ": " + i.Status;
                }

                _eventsOutput.Text += "\n";
            }

        }

    }
}
