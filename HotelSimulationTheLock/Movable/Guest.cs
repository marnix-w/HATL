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

        public int ID { get; set; }
        public string Name { get; set; }
        public int RoomRequest { get; set; }
        public IArea Area { get; set; }
        public IArea MyRoom { get; set; }

        public bool Registerd { get; set; } = false;
        public int DeathAt { get; set; } = 20;
        public int DeathCounter { get; set; } = 0;

        public Queue<IArea> Path { get; set; }
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();

        Random rnd = new Random();

        public Guest(string name, int roomRequest, Point point, int id)
        {
            Name = name;
            RoomRequest = roomRequest;
            Position = point;
            ID = id;
            FitnessDuration = rnd.Next(0, 11);
            
            Actions.Add(MovableStatus.CHEKING_IN, ChekIn);
            Actions.Add(MovableStatus.GOING_TO_ROOM, GoingToRoom);
            Actions.Add(MovableStatus.LEAVING, RemoveMe);
            Actions.Add(MovableStatus.GET_FOOD, GetFood);
            Actions.Add(MovableStatus.IN_ROOM, null);
        }

        public void RegisterAs()
        {
            if (!Registerd)
            {
                Console.WriteLine("ÏAM REGISTERD");
                HotelEventManager.Register(this);
                Registerd = true;
            }
        }

        public void PerformAction()
        {
            if (!(Actions[Status] == null))
            {
                Actions[Status]();
            }
        }

        public void SetPath(IArea destination)
        {
            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, destination));
        }

        public void Move()
        {
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;
        }

        private void AddDeathCounter()
        {
            if (DeathCounter == DeathAt)
            {
                // KILL
            }
            else
            {
                DeathCounter++;
            }
        }

        // Actions list
        private void ChekIn()
        {
            if (Path.Any())
            {
                Move();
            }
            else if (Area is Reception)
            {
                if (((Reception)Area).EnterArea(this))
                {
                    
                    if (((Reception)Area).Receptionist.GiveThisGuestHisRoom(RoomRequest) is null)
                    {
                        Status = MovableStatus.LEAVING;
                    }
                    else
                    {
                        SetPath(((Reception)Area).Receptionist.GiveThisGuestHisRoom(RoomRequest));
                        Path.Last().AreaStatus = AreaStatus.OCCUPIED;
                        IArea error = Path.Dequeue();
                        MyRoom = Path.Last();

                        switch (((Room)MyRoom).Classification)
                        {
                            case 1:
                                MyRoom.Art = Properties.Resources.room_one_star_locked;
                                break;
                            case 2:
                                MyRoom.Art = Properties.Resources.room_two_star_locked;
                                break;
                            case 3:
                                MyRoom.Art = Properties.Resources.room_three_star_locked;
                                break;
                            case 4:
                                MyRoom.Art = Properties.Resources.room_four_star_locked;
                                break;
                            case 5:
                                MyRoom.Art = Properties.Resources.room_five_star_locked;
                                break;
                            default:
                                break;
                        }
                       
                        Status = MovableStatus.GOING_TO_ROOM;
                    }
                }
                else if(!((Reception)Area).CheckInQueue.Contains(this))
                {                  
                    ((Reception)Area).CheckInQueue.Enqueue(this);
                }
                else
                {

                }
            }

        }

        private void GoingToRoom()
        {
            if (Area is Reception)
            {
                ((Reception)Area).CheckInQueue.Dequeue();
            }
           

            if (Path.Any())
            {
                Move();
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
            if (Area is Reception)
            {
                ((Reception)Area).CheckInQueue.Dequeue();
            }

            ((Reception)Area).Receptionist.RemoveGuest(this);
        }
        
        private void GetFood()
        {

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
                    if (evt.Data != null)
                    {
                        foreach (var item in evt.Data)
                        {
                            if (item.Key.Contains("Gast"))
                            {
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GET_FOOD;
                                }
                            }
                        }
                    }
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

       

    }
}
