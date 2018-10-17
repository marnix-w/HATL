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
            foreach (Guest g in gastenlijst)
            {
                if (g != null)
                {
                    if (Status == MovableStatus.ELEVATOR_REQUEST)
                    {
                        GoingToGuest();
                    }
                    else if(Status == MovableStatus.GOING_TO_FLOOR)
                    {
                        g.FinalDes = Hotel.GetArea(typeof(Restaurant));
                        GoingToFloor(g, g.FinalDes);
                        Console.WriteLine("guest can enter" + g.FinalDes.Position.ToString());
                    }
                    else
                    {
                       
                    }
                  
                }
                else
                {
                    Status = MovableStatus.NOONE_INSIDE;
                    goingDOwn();
                    Console.WriteLine("ER IS NIEMAND BINNEN");
                    //   Console.WriteLine("something went wrong");
                }
                //if (Status == MovableStatus.ELEVATOR_REQUEST)
                //{
                //  //  GoingToGuest();
                //    Console.WriteLine("onderweg naar gast");
                //}

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

        public void GoingToGuest()
        {
            foreach(Guest g in gastenlijst)
            {
                //if the elevator is lower than guest we go up
                if (Position.Y > g.Position.Y)
                {
                    Position = new Point(Position.X, Position.Y - 1);
                    Console.WriteLine("going to guest upwards" + Position.Y);
                }
                //if the elevator is lower than guest we go down
                else if (Position.Y < g.Position.Y)
                {
                    Position = new Point(Position.X, Position.Y + 1);
                    Console.WriteLine("going to guest downwards" + Position.Y);
                }
                else if (Position.Y == g.Position.Y)
                {
                    Status = MovableStatus.GOING_TO_FLOOR;
                    Console.WriteLine("lift is  op deZelfde verdieping");
                    g.Position = Position;
                }
                else
                {
                    //no request was found
                }
            }
           

        }

        private void GoingToFloor(Guest guest, IArea floordestination)
        {
            if (Position.Y > floordestination.Position.Y)
            {
                Position = new Point(Position.X, Position.Y - 1);
                Console.WriteLine("lets go current" + Position.Y + "\t to :"+ floordestination.Position.Y);
                guest.Position = Position;

            }
            else if(Position.Y < floordestination.Position.Y)
            {
                Position = new Point(Position.X, Position.Y + 1);
                Console.WriteLine("lets go current" + Position.Y + "\t to:"+ floordestination.Position.Y);
                guest.Position = Position;
            }
            else if(floordestination.Position.Y == Position.Y)
            {
                Console.WriteLine("guest can leaven");
                Status = MovableStatus.OPENING_DOORS;
                guest.Position = Position;
            }
            
        }
    }
}
