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

        public Queue<Guest> gastenlijst { get; set; } = new Queue<Guest>();

        //volgens david
        public List<Guest> RequestList { get; set; } = new List<Guest>();
        public List<Guest> GuestList { get; set; } = new List<Guest>();

        //'U' for UP, 'D' for DOWN, 'I' for IDLE



        private List<int> Up = new List<int>();
        private List<int> Down = new List<int>();



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

        public void PerformAction()
        {
            for (int i = 0; i < GuestList.Count; i++)
            {
                if (Position.Y == GuestList[i].FinalDes.Position.Y)
                {
                    GuestList[i].Status = MovableStatus.LEAVING_ELEVATOR;
                    GuestList[i].Area = Hotel.GetArea(Position);
                    GuestList.Remove(GuestList[i]);
                }
            }
            if (Down.Count != 0 && Down[0] == Position.Y)
            {
                Down.RemoveAt(0);
            }
            if (Up.Count != 0 && Up[0] == Position.Y)
            {
                Up.RemoveAt(0);
            }

            if (Status == MovableStatus.IDLE)
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

            if (Up.Count != 0 || Down.Count != 0)
            {
                if (Status == MovableStatus.UP)
                {
                    if (Up.Count == 0 && Down.Count != 0)
                    {
                        this.Status = MovableStatus.DOWN;
                        PerformAction();
                    }
                    else
                    {
                        Position = new Point(Position.X, Position.Y - 1);

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
                else if (Status == MovableStatus.DOWN)
                {
                    if (Up.Count != 0 && Down.Count == 0)
                    {
                        this.Status = MovableStatus.UP;
                        PerformAction();
                    }
                    else
                    {
                        Position = new Point(Position.X, Position.Y + 1);

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
                foreach (Guest human in GuestList)
                {
                    human.Position = Position;
                }
                AddDestinationFloor();
            }

            if (Up.Count == 0 && Down.Count == 0)
            {
                Status = MovableStatus.IDLE;
            }
            //try
            //{
            //    if (gastenlijst.Count() >= 1)
            //    {
            //        Status = MovableStatus.ELEVATOR_REQUEST;

            //        Guest g = gastenlijst.First();

            //        Console.WriteLine("er zijn op dit moment aanwezig " + gastenlijst.Count());


            //        if (Status == MovableStatus.ELEVATOR_REQUEST)
            //        {
            //            GoingToGuest();
            //        }
            //        if (Status == MovableStatus.GOING_TO_FLOOR)
            //        {
            //            // g.FinalDes = Hotel.GetArea(typeof(Restaurant));
            //            GoingToFloor(g, g.FinalDes);
            //            Console.WriteLine("guest can enter" + g.FinalDes.Position.ToString());
            //        }
            //    }

            //    else
            //    {
            //        Status = MovableStatus.NOONE_INSIDE;
            //        goingDOwn();
            //        Console.WriteLine("ER IS NIEMAND BINNEN");
            //        //   Console.WriteLine("something went wrong");
            //    }

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}



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

        public void AddDestinationFloor()
        {
            List<Guest> RemoveGuests = new List<Guest>();
            for (int i = 0; i < RequestList.Count; i++)
            {
                if (RequestList[i].Position.Y == Position.Y /*&& Capacity < GuestList.Count*/)
                {
                    GuestList.Add(RequestList[i]);
                    if (RequestList[i].FinalDes.Position.Y < Position.Y)
                    {
                        Up.Add(RequestList[i].FinalDes.Position.Y);
                        UpdateList();
                    }
                    else
                    {
                        Down.Add(RequestList[i].FinalDes.Position.Y);
                        UpdateList();
                    }
                    GuestList[i].Status = MovableStatus.IN_ELEVATOR;
                    RemoveGuests.Add(GuestList[i]);
                }
            }
            for (int i = 0; i < RemoveGuests.Count; i++)
            {
                RequestList.Remove(RemoveGuests[i]);
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

        private void goingDOwn()
        {
            if (Position.Y == Hotel.HotelHeight)
            {
                Console.WriteLine("beneden verdieping");
            }
            else
            {
                Position = new Point(Position.X, Position.Y + 1);
                Console.WriteLine("Lift position is " + Position);
            }

        }

        public void GoingToGuest()
        {
            Guest gu = gastenlijst.First();

            foreach (Guest g in gastenlijst)
            {
                //if the elevator is lower than guest we go up
                if (Position.Y > gu.Position.Y)
                {
                    Position = new Point(Position.X, Position.Y - 1);
                    Console.WriteLine("going to guest upwards" + Position.Y);
                }
                //if the elevator is lower than guest we go down
                else if (Position.Y < gu.Position.Y)
                {
                    Position = new Point(Position.X, Position.Y + 1);
                    Console.WriteLine("going to guest downwards" + Position.Y);

                }
                else if (Position.Y == g.Position.Y)
                {
                    Status = MovableStatus.GOING_TO_FLOOR;
                    Console.WriteLine("lift is  op deZelfde verdieping");
                    g.Area = Hotel.GetArea(Position);
                    g.Status = MovableStatus.IN_ELEVATOR;
                }
                else
                {
                    //no request was found
                }
            }

        }

        private void GoingToFloor(Guest guest, IArea floordestination)
        {
            guest = gastenlijst.First();

            foreach (Guest gast in gastenlijst)
            {
                if (Position.Y > floordestination.Position.Y && guest.Status == MovableStatus.IN_ELEVATOR)
                {
                    Position = new Point(Position.X, Position.Y - 1);
                    Console.WriteLine("lets go current" + Position.Y + "\t to :" + floordestination.Position.Y);
                    guest.Area = Hotel.GetArea(Position);
                    guest.Position = Position;

                }
                else if (Position.Y < floordestination.Position.Y && guest.Status == MovableStatus.IN_ELEVATOR)
                {
                    Position = new Point(Position.X, Position.Y + 1);
                    Console.WriteLine("lets go current" + Position.Y + "\t to:" + floordestination.Position.Y);
                    guest.Area = Hotel.GetArea(Position);
                    guest.Position = Position;
                }
                else if (floordestination.Position.Y == Position.Y && gast.Status == MovableStatus.IN_ELEVATOR)
                {
                    Console.WriteLine("guest can leaven");
                    Status = MovableStatus.OPENING_DOORS;
                    guest.Status = MovableStatus.LEAVING_ELEVATOR;
                    guest.Area = Hotel.GetArea(Position);
                    gast.Position = Position;

                    gastenlijst.Dequeue();
                }
            }

        }
    }
}
