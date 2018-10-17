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

        private bool Registerd { get; set; } = false;
        private bool WantsElevator { get; set; }

        // Iarea information
        #region
        public IArea Area { get; set; }
        public IArea MyRoom { get; set; }
        public IArea FinalDes { get; set; }
        #endregion

        // Counter Properties
        #region
        private int _deathAt { get; set; } = 20;
        private int _deathCounter { get; set; } = 0;
        public int _hteTime { get; set; }
        public int _hteCalculateCounter { get; set; } = 0;
        Random rnd = new Random();
        #endregion


        public Queue<IArea> Path { get; set; }

        /// <summary>
        /// A list of statuses paired with the coresponding action
        /// </summary>
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();


        public Hotel Hotel { get; set; }




      


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
            Actions.Add(MovableStatus.LEAVING, _removeMe);
            Actions.Add(MovableStatus.GET_FOOD, _getFood);
            Actions.Add(MovableStatus.GOING_TO_CINEMA, _getToCinema);
            Actions.Add(MovableStatus.CHECKING_OUT, _getCheckOut);
            Actions.Add(MovableStatus.EVACUATING, _Evacuate);
            Actions.Add(MovableStatus.GOING_TO_FITNESS, _goToFitness);
            Actions.Add(MovableStatus.WATCHING, _addHteCounter);
            Actions.Add(MovableStatus.EATING, _addHteCounter);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);
            Actions.Add(MovableStatus.IN_ROOM, null);
            Actions.Add(MovableStatus.WAITING_TO_START, null);
            Actions.Add(MovableStatus.WORKING_OUT, _addHteCounter);
            Actions.Add(MovableStatus.ELEVATOR_REQUEST, CallElevator);
            Actions.Add(MovableStatus.LEAVING_ELEVATOR, LeavingElevator);
        }

        private void LeavingElevator()
        {
            SetPath(FinalDes);

            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, FinalDes));
            Status = MovableStatus.GOING_TO_ROOM;
            
        }

        public void PerformAction()
        {
            if (!(Actions[Status] == null))
            {
                Actions[Status]();
            }

        }

        public void RegisterAs()
        {
            if (!Registerd)
            {
                HotelEventManager.Register(this);
                Registerd = true;
            }
        }

        public void SetPath(IArea destination)
        {
            if (Dijkstra.IsElevatorCloser(Area, destination) is Elevator)
            {
                Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, Dijkstra.IsElevatorCloser(Area, destination)));
                WantsElevator = true;
            }
            else
            {
                Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, destination));
            }

            
            // Count extra first step or not
            Path.Dequeue();

        }

        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
                _hteCalculateCounter = 0;
                _hteTime = 5;
                SetPath(Hotel.GetArea(typeof(Reception)));
                Console.WriteLine("WHERERE ALLL GONNE DIEEEEE, YEAHHHHHHH");
            }

            if (Status != MovableStatus.IN_ROOM && Status != MovableStatus.WAITING_TO_START)
            {
                return;
            }

            if (evt.Data != null)
            {
                foreach (var item in evt.Data)
                {

                    if (evt.EventType == HotelEventType.START_CINEMA && Status == MovableStatus.WAITING_TO_START)
                    {
                        Status = MovableStatus.WATCHING;
                        Console.WriteLine("i'm watching Marvel movie with Batman as Hoofdrolspeler" + item.Key + item.Value);
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

        public Point GetPoint()
        {
            return Position;
        }

        private void Move()
        {
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;

            if (WantsElevator)
            {
                Status = MovableStatus.ELEVATOR_REQUEST;
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
            if (_hteCalculateCounter == _hteTime)
            {
                Status = MovableStatus.GOING_TO_ROOM;
            }
            else
            {
                _hteCalculateCounter++;
            }
        }


        // Actions list
        private void _checkIn()
        {
            if (MyRoom != null)
            {
                ((Reception)Area).CheckInQueue.Dequeue();
                Status = MovableStatus.GOING_TO_ROOM;
            }
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
                SetPath(Hotel.GetNewLocation(Area, typeof(Fitness)));
            }
            else
            {
                Status = MovableStatus.WORKING_OUT;
                _hteTime = rnd.Next(1, 7);
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
                SetPath(Hotel.GetNewLocation(Area, typeof(Reception)));

            }
            else
            {
                //   RemoveMe();
            }
        }
        private void _goingToRoom()
        {
            _hteCalculateCounter = 0;
            FinalDes = MyRoom;

            
            if (Area is Cinema)
            {
                Area.Art = Properties.Resources.cinema;
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
        private void _removeMe()
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
        private void _getFood()
        {
            if (Path.Any())
            {
                Move();
            }
            else if (!(Area is Restaurant))
            {
                SetPath(Hotel.GetNewLocation(Area, typeof(Restaurant)));
            }
            else
            {
                Status = MovableStatus.EATING;
                _hteTime = ((Restaurant)Area).Duration;
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
                SetPath(Hotel.GetNewLocation(Area, typeof(Cinema)));
            }
            else
            {
                if (Area.AreaStatus == AreaStatus.PLAYING_MOVIE)
                {
                    Status = MovableStatus.WATCHING;
                    _hteTime = Hotel.HowLongWillMovieTake() + Path.Count();
                }
                else
                {
                    Status = MovableStatus.WAITING_TO_START;
                    _hteTime = ((Cinema)Area).Duration;
                }
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
                SetPath(Hotel.GetNewLocation(Area, typeof(Reception)));

            }
            else
            {
                _removeMe();
            }
        }

        public void CallElevator()
        {
            if (Area is Elevator && WantsElevator)
            {
                Hotel.CallElevator(this);
                WantsElevator = false;
            }
        }
        private void _Evacuate()
        {          
            if (Hotel.IsHotelSafe())
            {
                if (_hteTime == _hteCalculateCounter)
                {
                    Status = MovableStatus.GOING_TO_ROOM;
                }
                else
                {
                    _hteCalculateCounter++;
                }
            }
            else if(Path.Any())
            {
                Move();
            }           
        }

    }
}