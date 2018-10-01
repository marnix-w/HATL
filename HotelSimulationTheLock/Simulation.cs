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
      
        public Simulation(List<JsonModel> layout)
        {
            InitializeComponent();

            Reception test = new Reception();

            HotelEventManager.Start();
            Console.WriteLine(!HotelEventManager.Running);



            foreach (JsonModel item in layout)
            {
                _eventsOutput.Text += item.Classification;
                _eventsOutput.Text += item.AreaType;
                _eventsOutput.Text += item.ID;
                _eventsOutput.Text += item.Position;
                _eventsOutput.Text += item.Dimension;
                _eventsOutput.Text += item.Capacity;

                _eventsOutput.Text += "\n";
            }

            Hotel pleasewerk = new Hotel(layout, 0.5f);
            
            HotelEventManager.HTE_Factor = 0.5f;

        }

    }
}
