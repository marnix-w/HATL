using System;
using System.Collections.Generic;

using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Room")]
    public class Room : IArea
    {
        public Point Position { get; set; }
        public Point Dimension { get; set; }
        public int Capacity { get; set; } = 1;
        public int Classification { get; set; }
        public Image Art { get; set; }
        Status IArea.Status { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();

        private Room()
        {
            

        }

        public IArea CreateArea(Point position, int capacity, Point dimension, int clasification)
        {
            Room rm = new Room
            {
                Position = position,
                Dimension = dimension,
                Classification = clasification
            };

            switch (clasification)
            {
                case 1:
                    rm.Art = Properties.Resources.room_one_star_open;
                    break;
                case 2:
                    rm.Art = Properties.Resources.room_two_star_open;
                    break;
                case 3:
                    rm.Art = Properties.Resources.room_three_star_open;
                    break;
                case 4:
                    rm.Art = Properties.Resources.room_four_star_open;
                    break;
                case 5:
                    rm.Art = Properties.Resources.room_five_star_open;
                    break;
                default:
                    // error handeling
                    Debug.WriteLine("The requested classification does not excist");
                    break;
            }

            return rm;
        }

        

    }
}
