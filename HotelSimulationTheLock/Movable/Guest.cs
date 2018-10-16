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
        private int _deathAt { get; set; } = 20;
        private int _deathCounter { get; set; } = 0;
        private int _hteCalculateCounter { get; set; } = 0;

        public Queue<IArea> Path { get; set; }
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();


        public Hotel Hotel { get; set; }

        Random rnd = new Random();


        public Hotel _hotel { get; set; }


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
            Actions.Add(MovableStatus.CHEKING_IN, _checkIn);
            Actions.Add(MovableStatus.GOING_TO_ROOM, _goingToRoom);
            Actions.Add(MovableStatus.LEAVING, RemoveMe);
            Actions.Add(MovableStatus.GET_FOOD, _getFood);
            Actions.Add(MovableStatus.GOING_TO_CINEMA, _getToCinema);
            Actions.Add(MovableStatus.CHECKING_OUT, _getCheckOut);
            Actions.Add(MovableStatus.EVACUATING, _getOutOfHotel);
            Actions.Add(MovableStatus.GOING_TO_FITNESS, _goToFitness);
            Actions.Add(MovableStatus.WATCHING, null);
            Actions.Add(MovableStatus.EATING, null);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);
            Actions.Add(MovableStatus.IN_ROOM, null);
            Actions.Add(MovableStatus.WAITING_TO_START, null);
            Actions.Add(MovableStatus.WORKING_OUT, null);
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

            if (evt.Data != null)
            {
                foreach (var item in evt.Data)
                {

                    if (evt.EventType == HotelEventType.START_CINEMA && Status == MovableStatus.WAITING_TO_START)
                    {
                        Status = MovableStatus.WATCHING;
                        Console.WriteLine("i'm watching Marvel movie with Batman as Hoofdrolspeler" + item.Key + item.Value);
                    }

                    if (evt.EventType == HotelEventType.EVACUATE)
                    {
                        Status = MovableStatus.EVACUATING;
                        Console.WriteLine(item + item.Key + item.Value + item.ToString());
                    }

                    if (item.Key.Contains("Gast"))
                    {
                     

                        switch (evt.EventType)
                        {
                            // find requested guest

                            case HotelEventType.CHECK_OUT:
                                // guest.checkout()
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.CHECKING_OUT;
                                    Console.WriteLine("Check out" + item.Key + item.Value);
                                }
                                break;
                            case HotelEventType.EVACUATE:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.EVACUATING;
                                    Console.WriteLine("Evacuating" + item.Key + item.Value);
                                }

                                break;
                            case HotelEventType.NEED_FOOD:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GET_FOOD;
                                    Console.WriteLine("Get food" + item.Key + item.Value);
                                }
                                break;
                            case HotelEventType.GOTO_CINEMA:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GOING_TO_CINEMA;
                                    Console.WriteLine("Going to cinema" + item.Key + item.Value);
                                    AddDeathCounter(this);
                                }
                                break;
                            case HotelEventType.GOTO_FITNESS:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GOING_TO_FITNESS;
                                    Console.WriteLine("going to fitness" + item.Key + item.Value);
                                }
                                break;

                            default:
                                break;


                        }
                    }
                }
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

        private void AddDeathCounter(Guest guest)
        {
            if (_deathCounter == _deathAt)
            {
                // KILL
            }
            else
            {
                _deathCounter++;
                Console.WriteLine(guest.ID + " " + _deathCounter);
            }
        }

        private void _addHteCounter()
        {
            _hteCalculateCounter++;

            if (_hteCalculateCounter == ((Restaurant)Area).Duration)
            {

            }
        }


        // Actions list
        private void _checkIn()
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
                else if (!((Reception)Area).CheckInQueue.Contains(this))
                {
                    ((Reception)Area).CheckInQueue.Enqueue(this);
                }
                else
                {
                    // kill timer
                }
            }

        }
        private void _goToFitness()
        {
            if (Path.Any())
            {
                Move();
            }
            else if (!(Area is Fitness))
            {
                SetPath(_hotel.GetNewLocation(Area, typeof(Fitness)));
            }
            else
            {
                Status = MovableStatus.WORKING_OUT;
            }
        }
        private void _getOutOfHotel()
        {
            if (Path.Any())
            {
                Move();
                Status = MovableStatus.CHECKING_OUT;
            }

            else if (!(Area is Reception))
            {
                SetPath(_hotel.getCheckOutLocation(Area));

            }
            else
            {
                //   RemoveMe();
            }
        }

        private void _goingToRoom()
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

        /// <summary>
        /// Trying to get the first restaurant
        /// </summary>
        private void _getFood()
        {
            if (Path.Any())
            {
                Move();
            }
            else if (!(Area is Restaurant))
            {
                SetPath(_hotel.getLocation(Area));
            }
            else
            {        
                Status = MovableStatus.EATING;
              
            }
        }

        private void _getToCinema()
        {

            if (Path.Any())
            {
                Move();
            }
            else if (!(Area is Cinema))
            {
                SetPath(_hotel.getLocationCinema(Area));
            }
            else
            {
                Status = MovableStatus.WAITING_TO_START;
            }
        }

        private void _getCheckOut()
        {
            if (Path.Any())
            {
                Move();
                Status = MovableStatus.CHECKING_OUT;
            }

            else if (!(Area is Reception))
            {
                SetPath(_hotel.getCheckOutLocation(Area));

            }
            else
            {
                RemoveMe();
            }
        }



    }
}
