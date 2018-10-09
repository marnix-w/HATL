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

        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.customer;
        public MovableStatus Status { get; set; }
        public int FitnessDuration { get; set; }

        public string Name { get; set; }
        public int RoomRequest { get; set; }
        public IArea Area { get; set; }
        public IArea MyRoom { get; set; }

        public Queue<IArea> Path { get; set; }
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();

        Random rnd = new Random();

        public Guest(string name, int roomRequest, Point point)
        {
            Name = name;
            RoomRequest = roomRequest;
            Position = point;
            FitnessDuration = rnd.Next(0, 11);

            Actions.Add(MovableStatus.CHEKING_IN, MoveFromPath);
            Actions.Add(MovableStatus.GOING_TO_ROOM, GoingToRoom);
            Actions.Add(MovableStatus.LEAVING, RemoveMe);
            Actions.Add(MovableStatus.IN_ROOM, null);
        }


        public void SetPath(IArea destination)
        {
            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijikstra(Area, destination));    
        }

        // Actions list
        private void MoveFromPath()
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
                else
                {

                }
                // else kill the person after 20 itterations or so
            }
            else if (Area is Reception)
            {
             
                if (((Receptionist)Area.Movables.First()).GiveThisGuestHesRoom(RoomRequest) is null)
                {
                    Status = MovableStatus.LEAVING;
                }
                else
                {
                    SetPath(((Receptionist)Area.Movables.First()).GiveThisGuestHesRoom(RoomRequest));
                    Path.Last().AreaStatus = AreaStatus.OCCUPIED;
                    IArea error = Path.Dequeue();
                    MyRoom = Path.Last();
                    Status = MovableStatus.GOING_TO_ROOM;
                }      
                
            }
            
        }

        private void GoingToRoom()
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
                else
                {

                }
                // else kill the person after 20 itterations or so
            }
            else if (!(Area == MyRoom))
            {
                SetPath(MyRoom);
            }
            else
            {
                Status = MovableStatus.IN_ROOM;
            }

        }

        private void RemoveMe()
        {
            ((Receptionist)Area.Movables.First()).RemoveGuest(this);
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
            if (!(Actions[Status] == null))
            {
                Actions[Status]();
            }
        }
      
    }
}
