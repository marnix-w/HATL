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
    public class Guest : IMovable, IListner
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
        public IArea FinalDes { get; set; }

        public bool Registerd { get; set; } = false;
        public int DeathAt { get; set; } = 20;
        public int DeathCounter { get; set; } = 0;

        public Queue<IArea> Path { get; set; }
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();
        
        
        public Hotel Hotel { get; set; }

        Random rnd = new Random();

        public Guest(Hotel hotel, string name, int roomRequest, Point point, int id)
        {
            Hotel = hotel;
            Name = name;
            RoomRequest = roomRequest;
            Position = point;
            ID = id;
            FitnessDuration = rnd.Next(0, 11);

            SetBahvior();
        }

        public void SetBahvior()
        {
            Actions.Add(MovableStatus.CHEKING_IN, ChekIn);
            Actions.Add(MovableStatus.GOING_TO_ROOM, GoingToRoom);
            Actions.Add(MovableStatus.LEAVING, RemoveMe);
            Actions.Add(MovableStatus.GET_FOOD, GetFood);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);
            Actions.Add(MovableStatus.IN_ROOM, null);
        }

        public void RegisterAs()
        {
            if (!Registerd)
            {
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
            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, Dijkstra.IsElevatorCloser(Area, destination)));
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

        private void Move()
        {            
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;  
            
            if (Area is Elevator && Status != MovableStatus.CHEKING_IN && Status != MovableStatus.LEAVING)
            {
                if (((Elevator)Area).ElevatorCart != null)
                {
                    ((Elevator)Area).ElevatorCart.EnterElevator(this, FinalDes.Position.Y);                   
                    return;
                }
            }
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
                    
                    if (Hotel.GetArea(RoomRequest) is null)
                    {
                        Status = MovableStatus.LEAVING;
                    }
                    else
                    {
                        IArea newRoom = Hotel.GetArea(RoomRequest);
                        SetPath(Hotel.GetArea(RoomRequest));
                        newRoom.AreaStatus = AreaStatus.OCCUPIED;
                        FinalDes = newRoom;
                        MyRoom = newRoom;
                        
                        // Count extra first step or not
                        Path.Dequeue();
                        
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
                        ((Reception)Area).CheckInQueue.Dequeue();
                    }
                }
                else if(!((Reception)Area).CheckInQueue.Contains(this))
                {                  
                    ((Reception)Area).CheckInQueue.Enqueue(this);
                }
                else
                {
                    // kill timer
                }
            }

        }

        private void GoingToRoom()
        {
            FinalDes = MyRoom;
                
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
            if (Path.Any())
            {
                Move();
            }
            else
            {
                SetPath(Hotel.GetArea(typeof(Reception)));
            }

            if (Area is Reception)
            {
                Hotel.RemoveGuest(this);
            }
           
        }
        
        private void GetFood()
        {

        }

        

       

    }
}
