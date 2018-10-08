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
        public Size Dimension { get; set; }
        public int Capacity { get; set; } = 1;
        public int Classification { get; set; }
        public Bitmap Art { get; set; }
        public AreaStatus AreaStatus { get; set; }

        // Dijkstra search varibles
        public double? BackTrackCost { get; set; } = null;
        public IArea NearestToStart { get; set; } = null;
        public bool Visited { get; set; } = false;
        public Dictionary<IArea, int> Edge { get; set; } = new Dictionary<IArea, int>();
        public List<IMovable> Movables { get; set; } = new List<IMovable>();

        public Room()
        {


        }

        public IArea CreateArea()
        {
            return new Room();
        }

        public void SetJsonValues(Point position, int capacity, Size dimension, int classification)
        {
            Position = position;
            Capacity = capacity;
            Dimension = dimension;
            Classification = classification;

            switch (classification)
            {
                case 1:
                    Art = Properties.Resources.room_one_star_open;
                    break;
                case 2:
                    Art = Properties.Resources.room_two_star_open;
                    break;
                case 3:
                    Art = Properties.Resources.room_three_star_open;
                    break;
                case 4:
                    Art = Properties.Resources.room_four_star_open;
                    break;
                case 5:
                    Art = Properties.Resources.room_five_star_open;
                    break;
                default:
                    break;
            }
        }

        public bool MoveToArea()
        {
            if (Capacity == Movables.Count)
            {
                return false;
            }
            return true;
        }
    }
}