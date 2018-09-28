using HotelEvents;
using HotelSimulationTheLock.Interfaces;
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

            HotelEventManager.Start();

            float newHTE = 10000f;

            HotelEventManager.HTE_Factor = newHTE;

            Console.WriteLine(!HotelEventManager.Running);

            Model.showListener temp = new Model.showListener();

            HotelEventManager.Register(temp);

            new Thread((ThreadStart)(() =>
            {
                while (true)
                {

                }
            })).Start();

            foreach(IArea item in layout)
            {
               Console.WriteLine(item.ID);
               Console.WriteLine(item.AreaType);
               Console.WriteLine(item.Capacity);
               Console.WriteLine(item.Classification);
               Console.WriteLine(item.Dimension);
               Console.WriteLine(item.Position);

                _eventsOutput.Text += item.Classification;              
                _eventsOutput.Text += item.AreaType;
                _eventsOutput.Text += item.ID;
                _eventsOutput.Text += item.Position;
                _eventsOutput.Text += item.Dimension;
                _eventsOutput.Text += item.Capacity;

                _eventsOutput.Text += "\n";
            }

         
        }

    }
}
