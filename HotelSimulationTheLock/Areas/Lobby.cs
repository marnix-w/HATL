using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Lobby")]
    public class Lobby : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; } = new Point(1, 1);
        public int Capacity { get; set; }
        public Image Art { get; set; } = Properties.Resources.lobby_window;
        public AreaStatus AreaStatus { get; set; }
        
        // dijkstra variables
        public double? BackTrackCost { get; set; }
        public IArea NearestToStart { get; set; }
        public bool Visited { get; set; }
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        public Lobby()
        {

        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Lobby lb = new Lobby()
            {
                Position = position,
                Capacity = capacity,
            };

            if (clasification % 2 == 0)
            {
                lb.Art = Properties.Resources.lobby_couch;
            }

            return lb;

        }
    }
}
