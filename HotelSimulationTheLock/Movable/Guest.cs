using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelEvents;

namespace HotelSimulationTheLock
{
    public class Guest : IMovable, HotelEventListener
    {
        public Random rng = new Random();
        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.customer;
        public MovableStatus Status { get; set; }
        public int FitnessDuration { get; set; }
      
        public string Name { get; set; }
        public int RoomRequest { get; set; }
        public IArea Area { get; set; }

        public Queue<IArea> Path { get; set; }

        Random rnd = new Random();

        public Guest(string name, int roomRequest, Point point)
        {
            Name = name;
            RoomRequest = roomRequest;
            Position = point;          
            FitnessDuration = rnd.Next(0, 11);
        }

        public void SetPath(IArea destination)
        {
            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijikstra(Area, destination));           
        }

        public void MoveFromPath()
        {
            if (Path.Any())
            {              
                if (Path.First().MoveToArea())
                {
                    IArea destination = Path.Dequeue();

                    // remove old position
                    Area.Movables.Remove(this);

                    // add to new position
                    Area = destination;
                    Position = destination.Position;
                    Area.Movables.Add(this);
                }
   
            }   
        }

        public void Notify(HotelEvent evt)
        {
            switch (evt.EventType)
            {         
                
                // find requested guest

                case HotelEventType.CHECK_OUT:
                    // guest.checkout()
                    break;
                case HotelEventType.EVACUATE:
                    // guest.evacuate()
                    break;
                case HotelEventType.NEED_FOOD:
                    // guest.GoToRestaurant()
                    break;
                case HotelEventType.GOTO_CINEMA:
                    // guest.GoToCinema()
                    break;
                case HotelEventType.GOTO_FITNESS:
                    // guest.GoToFitness()
                    break;
                default:
                    break;
            }
        }

        public void PerformAction()
        {
            MoveFromPath();
        }
    }
}
