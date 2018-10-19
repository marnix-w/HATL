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


        //volgens david
        List<IMovable> RemoveGuests = new List<IMovable>();
        public List<IMovable> RequestList { get; set; } = new List<IMovable>();
        public List<IMovable> GuestList { get; set; } = new List<IMovable>();

        //'U' for UP, 'D' for DOWN, 'I' for IDLE
        private List<int> Up = new List<int>();
        private List<int> Down = new List<int>();

        public ElevatorCart(Point position, Hotel hotel, int capacity)
        {
            Hotel = hotel;
            // Remind me to set it to capicity
            Capacity = capacity;
            Status = MovableStatus.IDLE;
            Position = position;
            // Area = Hotel.GetArea(new Point(0, Hotel.HotelHeight));
            Area = Hotel.GetArea(Position);

            ((Elevator)Area).ElevatorCart = this;
        }

        public void PerformAction()
        {
            //We wanted to use a foreach loop but because
            RemoveGuests.Clear();
            for (int i = 0; i < GuestList.Count; i++)
            {
                if (GuestList[i] is Guest g)
                {
                    if (Position.Y == g.FinalDes.Position.Y)
                    {
                        g.Status = MovableStatus.LEAVING_ELEVATOR;
                        g.Area = Hotel.GetArea(Position);
                        RemoveGuests.Add(GuestList[i]);
                    }
                }
            }
            for (int i = 0; i < RemoveGuests.Count; i++)
            {
                GuestList.Remove(RemoveGuests[i]);
            }
            RemoveGuests.Clear();
            if (Down.Count != 0 && Down[0] == Position.Y)
            {
                Down.RemoveAt(0);
            }
            if (Up.Count != 0 && Up[0] == Position.Y)
            {
                Up.RemoveAt(0);
            }

            if (Status == MovableStatus.UP)
            {
                _elevatorGoesUp();
            }
            else if (Status == MovableStatus.DOWN && Position.Y < Hotel.HotelHeight)
            {

                _elevatorGoesDown();
            }
            else
            {
                Status = MovableStatus.IDLE;
            }
            foreach (Guest human in GuestList)
            {
                human.Position = Position;
            }
            AddDestinationFloor();
          

            if (Up.Count == 0 && Down.Count == 0)
            {
                Status = MovableStatus.IDLE;
            }

            if (Status == MovableStatus.IDLE)
            {
                _ElevatorDoesNothing();
            }
        }


        public void RequestElevator(Guest RequestFloor, int height)
        {
            //Extra Check
            //Check for Current floor

            if (RequestFloor.FinalDes.Position.Y <= height && RequestFloor.FinalDes.Position.Y >= 0)
            {
                RequestList.Add(RequestFloor);
                //Goes UP
                if (RequestFloor.Position.Y < Position.Y)
                {
                    Up.Add(RequestFloor.Position.Y);
                    UpdateList();
                }
                //Goes DOWN
                else
                {
                    Down.Add(RequestFloor.Position.Y);
                    UpdateList();
                }
            }

        }
        private void _elevatorGoesUp()
        {
            if (Up.Count == 0 && Down.Count != 0)
            {
                this.Status = MovableStatus.DOWN;
                PerformAction();
            }
            else
            {
                Position = new Point(Position.X, Position.Y - 1);
                Area = Hotel.GetArea(Position);

                for (int i = 0; i < Up.Count; i++)
                {
                    if (Up[i] == Position.Y)
                    {
                        Up.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void _elevatorGoesDown()
        {
            if (Up.Count != 0 && Down.Count == 0)
            {
                this.Status = MovableStatus.UP;
                PerformAction();
            }
            else
            {
                Position = new Point(Position.X, Position.Y + 1);
                Area = Hotel.GetArea(Position);

                for (int i = 0; i < Down.Count; i++)
                {
                    if (Down[i] == Position.Y)
                    {
                        Down.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void _ElevatorDoesNothing()
        {
            if (Up.Count > Down.Count)
            {
                Status = MovableStatus.UP;
            }
            else
            {
                Status = MovableStatus.DOWN;
            }
        }
        public void AddDestinationFloor()
        {

            for (int i = 0; i < RequestList.Count; i++)
            {
                if (RequestList[i] is Guest g)
                {
                    if (g.Position.Y == Position.Y)
                    {
                        GuestList.Add(RequestList[i]);
                        if (g.FinalDes.Position.Y < Position.Y)
                        {
                            Up.Add(g.FinalDes.Position.Y);
                            UpdateList();
                        }
                        else
                        {
                            Down.Add(g.FinalDes.Position.Y);
                            UpdateList();
                        }
                        try
                        {
                            GuestList[i].Status = MovableStatus.IN_ELEVATOR;
                            RemoveGuests.Add(GuestList[i]);
                        }
                        catch (Exception)
                        {

                        }

                    }
                }

            }
            for (int i = 0; i < RemoveGuests.Count; i++)
            {
                if (RemoveGuests[i] is Guest g)
                {
                    RequestList.Remove(RemoveGuests[i]);
                }
            }
        }

        private void UpdateList()
        {
            Up = Up.Distinct().OrderByDescending(x => x).ToList();
            Down = Down.Distinct().OrderBy(x => x).ToList();
        }

        public void SetPath(IArea destination)
        {
            // throw new NotImplementedException();
        }


    }
}
