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

        private List<IMovable> InElevator { get; set; } = new List<IMovable>();

        public int Capacity { get; set; }

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
            Area = Hotel.GetRoom(new System.Drawing.Point(0, Hotel.HotelHeight));
            ((Elevator)Area).ElevatorCart = this;
        }

        public void EnterElevator(IMovable movable)
        {
            if (InElevator.Count < Capacity)
            {
                movable.Status = MovableStatus.IN_ELEVATOR;
                InElevator.Add(movable);
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
