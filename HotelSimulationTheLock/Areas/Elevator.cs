using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Elevator")]
    public class Elevator : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
        public Image Art { get; set; }
        public Enum Status { get; set; }


        private Elevator()
        {
            
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Elevator ev = new Elevator();

            ev.Position = position;
            ev.Capacity = capacity;
            ev.Dimension = dimension;

            if (dimension.Equals(new Point(0, 0)))
            {
                ev.Art = Properties.Resources.elevator_pressent;
            }
            else
            {
                Art = Properties.Resources.elevator_not_pressent;
            }

            return ev;
        }

        

    }
}
