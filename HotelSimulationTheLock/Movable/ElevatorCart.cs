using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public class ElevatorCart : IMovable
    {
        public IArea Area { get; set; }

        public Hotel Hotel { get; set; }

        private Dictionary<IMovable, int> InElevator { get; set; } = new Dictionary<IMovable, int>();

        public int Capacity { get; set; }



        // other shit
        public Point Position { get; set; }

        public Bitmap Art { get; set; } = Properties.Resources.elevator_pressent;

        public MovableStatus Status { get; set; }

        public Dictionary<MovableStatus, Action> Actions { get; set; }


        public Queue<IArea> Path { get; set; }


        public ElevatorCart(Point position, Hotel hotel, int capacity)
        {
            Hotel = hotel;
            // Remind me to set it to capicity
            Capacity = capacity;
            Status = MovableStatus.NOONE_INSIDE;
            // Area = Hotel.GetArea(new Point(0, Hotel.HotelHeight));
            Area = Hotel.GetArea(position);
        
            ((Elevator)Area).ElevatorCart = this;
        }

        public void EnterElevator(IMovable movable, int req)
        {
            if (InElevator.Count < Capacity)
            {
                movable.Status = MovableStatus.IN_ELEVATOR;
                InElevator.Add(movable, req);
            }
        }

        public void PerformAction()
        {
            if (Status == MovableStatus.NOONE_INSIDE)
            {
                goingDOwn();
                Console.WriteLine("ER IS NIEMAND BINNEN");
            }
        }

        public void SetPath(IArea destination)
        {
            // throw new NotImplementedException();
        }

        private void goingDOwn()
        {
            if(Position.Y == Hotel.HotelHeight)
            {                
                Console.WriteLine("beneden verdieping");
            }
            else
            {
                Position = new Point(Position.X, Position.Y + 1);
            }
          
        }
    }
}
