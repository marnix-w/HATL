using HotelEvents;
using HotelSimulationTheLock.Model;
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
      
        public Simulation(List<IArea> layout)
        {
            InitializeComponent();

            Reception test = new Reception();

            HotelEventManager.Start();
            Console.WriteLine(!HotelEventManager.Running);


            Model.showListener temp = new Model.showListener();

            HotelEventManager.Register(temp);


            foreach(IArea item in layout)
            {
                _eventsOutput.Text += item.Classification;              
                _eventsOutput.Text += item.AreaType;
                _eventsOutput.Text += item.ID;
                _eventsOutput.Text += item.Position;
                _eventsOutput.Text += item.Dimension;
                _eventsOutput.Text += item.Capacity;

                _eventsOutput.Text += "\n";
            }
         

            
            HotelEventManager.HTE_Factor = 0.5f;

        }

    }
}
