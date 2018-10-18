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
            gastenlijst.OrderByDescending(X => X.FinalDes.Position.Y);

            try
            {
                if (gastenlijst.Count() >= 1)
                {
                    Status = MovableStatus.ELEVATOR_REQUEST;

                    Guest g = gastenlijst.First();

                    Console.WriteLine("er zijn op dit moment aanwezig " + gastenlijst.Count());


                    if (Status == MovableStatus.ELEVATOR_REQUEST)
                    {
                        GoingToGuest();
                    }
                    if (Status == MovableStatus.GOING_TO_FLOOR)
                    {
                        // g.FinalDes = Hotel.GetArea(typeof(Restaurant));
                        GoingToFloor(g, g.FinalDes);
                        Console.WriteLine("guest can enter" + g.FinalDes.Position.ToString());
                    }
                }

                else
                {
                    Status = MovableStatus.NOONE_INSIDE;
                    goingDOwn();
                    Console.WriteLine("ER IS NIEMAND BINNEN");
                    //   Console.WriteLine("something went wrong");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



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
