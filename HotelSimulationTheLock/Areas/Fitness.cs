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
    [ExportMetadata("AreaType", "Fitness")]
    public class Fitness : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.fitness;
        Status IArea.Status { get; set; }

        private Fitness()
        {
            
        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Fitness ft = new Fitness();

            ft.Position = position;
            ft.Capacity = capacity;
            ft.Dimension = dimension;

            return ft;
        }

        

    }
}
