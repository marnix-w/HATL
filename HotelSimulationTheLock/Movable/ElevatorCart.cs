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
       

        public ElevatorCart(Hotel hotel, int capacity)
        {
            Hotel = hotel;
            // Remind me to set it to capicity
            Capacity = 5;
            Area = Hotel.GetArea(new Point(0, Hotel.HotelHeight));
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
            throw new NotImplementedException();
        }

        public void SetPath(IArea destination)
        {
            throw new NotImplementedException();
        }
    }
}
