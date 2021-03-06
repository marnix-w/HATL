﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The main movable of the simulation
    /// </summary>
    public class Guest : IMovable, IListener
    {
        #region Properties
        /// <summary>
        /// The guests position in the hotel
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The guests art
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.customer; // <3 Bob
        /// <summary>
        /// Teh guests current status
        /// depending on the status of the guest an action will be performed
        /// </summary>
        public MovableStatus Status { get; set; }
        /// <summary>
        /// Teh guests last status, used for elevator handling
        /// </summary>
        public MovableStatus LastStatus { get; set; }
        /// <summary>
        /// How long does this guest fitness
        /// </summary>
        public int FitnessDuration { get; set; }
        /// <summary>
        /// The guests unique ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// The guests name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The room the guest will request
        /// </summary>
        public int RoomRequest { get; set; }
        /// <summary>
        /// A check if the guest is registerd as listner
        /// </summary>
        private bool Registerd { get; set; } = false;
        /// <summary>
        /// A bool to check if a guest wants to take the elevator
        /// </summary>
        public bool WantsElevator { get; set; }
        /// <summary>
        /// The guests Path it will follow in the hotel
        /// </summary>
        public Queue<IArea> Path { get; set; } = new Queue<IArea>();
        /// <summary>
        /// A list of statuses paired with the corresponding actions
        /// </summary>
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();
        /// <summary>
        /// The hotel the guest is a part of
        /// </summary>
        public Hotel Hotel { get; set; }

        #endregion

        #region IArea Properties
        /// <summary>
        /// The guests current area
        /// </summary>
        public IArea Area { get; set; }
        /// <summary>
        /// The guests own room
        /// </summary>
        public IArea MyRoom { get; set; }
        /// <summary>
        /// The guests final destination
        /// </summary>
        public IArea FinalDes { get; set; }
        #endregion
 
        #region Counter Properties
        // The death counter is not working yet
        // For a future release it can be implemented
        // We just need to check the simulation count timer and the guests timer if its equal we kill the guest

        /// <summary>
        /// Death barrier
        /// </summary>
        private int _deathAt { get; set; } = 20;
        /// <summary>
        /// Deadth counter
        /// </summary>
        private int _deathCounter { get; set; } = 0;
        /// <summary>
        /// Event timer time
        /// </summary>
        public int _hteTime { get; set; }
        /// <summary>
        /// Event time counter
        /// </summary>
        public int _hteCalculateCounter { get; set; } = 0;
        /// <summary>
        /// A random number gen for the fitnnes time
        /// </summary>
        Random rnd = new Random();
        #endregion

        #region Constrcutor
        /// <summary>
        /// Creating a new guest
        /// </summary>
        /// <param name="hotel">The hotel the guest is a part of</param>
        /// <param name="name">The guests name</param>
        /// <param name="roomRequest">The room the guest wants to stay in</param>
        /// <param name="point">The guest location in the hotel</param>
        /// <param name="id">A unique ID</param>
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
        #endregion

        #region Initilizers
        /// <summary>
        /// Setting Guests behaviour
        /// </summary>
        public void SetBahvior()
        {
            //These are all the actions and statuses for guests

            //Elevator actions and statuses
            Actions.Add(MovableStatus.ELEVATOR_REQUEST, CallElevator);
            Actions.Add(MovableStatus.LEAVING_ELEVATOR, _LeavingElevator);
            Actions.Add(MovableStatus.WAITING_FOR_ELEVATOR, _CheckForEvacuate);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);

            //Actions for going to the facility
            Actions.Add(MovableStatus.GOING_TO_ROOM, _goingToRoom);
            Actions.Add(MovableStatus.GOING_TO_CINEMA, _getToCinema);
            Actions.Add(MovableStatus.GET_FOOD, _getFood);
            Actions.Add(MovableStatus.GOING_TO_FITNESS, _goToFitness);

            //Actions during the event
            Actions.Add(MovableStatus.CHEKING_IN, _checkIn);
            Actions.Add(MovableStatus.WATCHING, _addHteCounter);
            Actions.Add(MovableStatus.EATING, _addHteCounter);
            Actions.Add(MovableStatus.WORKING_OUT, _addHteCounter);
            Actions.Add(MovableStatus.WAITING_TO_START, null);
            Actions.Add(MovableStatus.IN_ROOM, null);

            //Actions when the guest is leaving
            Actions.Add(MovableStatus.LEAVING, _removeMe);
            Actions.Add(MovableStatus.CHECKING_OUT, _getCheckOut);
            Actions.Add(MovableStatus.EVACUATING, _Evacuate);
        }
        #endregion

        #region Bahaviours

        // Important note:
        // This list contains a fair amount of code duplication
        // this was the cause of simultaneously working on event implementation
        // we made this choice because of the limited time we had to implement these
        // as cause the actions list has become to big and needs a good refactor
        // there is no time for this so it is an issue that needs to be rethought in the fututure

        /// <summary>
        /// Sets variables for evacuation
        /// </summary>
        private void _EvacuateSequence()
        {
            SetPath(Hotel.GetArea(typeof(Reception)));
            FinalDes = Hotel.GetArea(typeof(Reception));
            Status = MovableStatus.EVACUATING;
            LastStatus = MovableStatus.EVACUATING;
        }

        /// <summary>
        /// Checks if the guest should evacuate or not
        /// </summary>
        private void _CheckForEvacuate()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                _EvacuateSequence();
                return;
            }
        }

        /// <summary>
        /// Makes the guest leave the elevator
        /// </summary>
        private void _LeavingElevator()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                _EvacuateSequence();
                return;
            }

            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, FinalDes));
            Status = LastStatus;

        }

        /// <summary>
        /// Moves the guest to the next position in its path
        /// </summary>
        private void _Move()
        {
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;
        }

        /// <summary>
        /// Kills the guest if he waits to long
        /// Note: Not fully implemented
        /// </summary>
        /// <param name="guest">The guest in question</param>
        private void _AddDeathCounter(Guest guest)
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

        /// <summary>
        /// Handles event calculations
        /// </summary>
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
        
        /// <summary>
        /// Checks in the guest
        /// </summary>
        private void _checkIn()
        {
            if (Path.Any())
            {
                _Move();
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
                        
                            ((Reception)Area).CheckInQueue.Dequeue();
                        Status = MovableStatus.GOING_TO_ROOM;

                    }
                }
                else if (!((Reception)Area).CheckInQueue.Contains(this))
                {
                    ((Reception)Area).CheckInQueue.Enqueue(this);
                }
                else
                {
                    // Kill timer
                }
            }
        }

        /// <summary>
        /// Moves the guest to the fitness
        /// </summary>
        private void _goToFitness()
        {
            if (Path.Any())
            {
                _Move();
            }
            else if (!(Area is Fitness))
            {
                SetPath(Hotel.GetArea(Area, typeof(Fitness)));
                FinalDes = Hotel.GetArea(Area, typeof(Fitness));
            }
            else
            {
                Status = MovableStatus.WORKING_OUT;
                _hteTime = rnd.Next(1, 7);
            }
        }

        /// <summary>
        /// Makes the guest go to its room
        /// </summary>
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
                _Move();
            }
            else if (!(Area == MyRoom))
            {
                SetPath(MyRoom);
                FinalDes = MyRoom;
            }
            else
            {
                Status = MovableStatus.IN_ROOM;
            }

        }

        /// <summary>
        /// Removes the guest from the hotel
        /// </summary>
        private void _removeMe()
        {
            if (Path.Any())
            {
                _Move();
            }
            else
            {
                SetPath(Hotel.GetArea(typeof(Reception)));
                FinalDes = Hotel.GetArea(typeof(Reception));
            }

            if (Area is Reception)
            {
                Hotel.RemoveGuest(this);
            }

        }

        /// <summary>
        /// Makes the guest go get food and find the closest restaurant
        /// </summary>
        private void _getFood()
        {
            if (Path.Any())
            {
                _Move();
            }
            else if (!(Area is Restaurant))
            {
                SetPath(Hotel.GetArea(Area, typeof(Restaurant)));
                FinalDes = Hotel.GetArea(Area, typeof(Restaurant));
            }
            else
            {
                Status = MovableStatus.EATING;
                _hteTime = ((Restaurant)Area).Duration;
            }
        }

        /// <summary>
        /// Makes the guest go to the cinema
        /// </summary>
        private void _getToCinema()
        {

            if (Path.Any())
            {
                _Move();
            }
            else if (!(Area is Cinema))
            {
                SetPath(Hotel.GetArea(Area, typeof(Cinema)));
                FinalDes = Hotel.GetArea(Area, typeof(Cinema));
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

        /// <summary>
        /// Makes the guest checkout
        /// </summary>
        private void _getCheckOut()
        {
            if (Path.Any())
            {
                _Move();
                Status = MovableStatus.CHECKING_OUT;
                MyRoom.AreaStatus = AreaStatus.NEED_CLEANING;
            }

            else if (!(Area is Reception))
            {
                SetPath(Hotel.GetArea(Area, typeof(Reception)));
                FinalDes = Hotel.GetArea(Area, typeof(Reception));
            }
            else
            {
                _removeMe();
            }
        }

        /// <summary>
        /// Makes the guest call the elevator
        /// </summary>
        private void CallElevator()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                _EvacuateSequence();
                return;
            }

            if (Path.Any())
            {
                _Move();
            }
            else if (Area is Elevator && WantsElevator)
            {
                Hotel.CallElevator(this);
                WantsElevator = false;
                Status = MovableStatus.WAITING_FOR_ELEVATOR;
            }
        }

        /// <summary>
        /// Makes the guest evacuate
        /// </summary>
        private void _Evacuate()
        {
            SetPath(Hotel.GetArea(typeof(Reception)));
            FinalDes = Hotel.GetArea(typeof(Reception));

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
            else if (Path.Any())
            {
                _Move();
            }
        }
        #endregion

        #region Operations
        /// <summary>
        /// Perform an action
        /// </summary>
        public void PerformAction()
        {
            if (!(Actions[Status] == null))
            {
                Actions[Status]();
            }
        }

        /// <summary>
        /// Register as an eventlistener
        /// </summary>
        public void RegisterAs()
        {
            if (!Registerd)
            {
                HotelEventManager.Register(this);
                Registerd = true;
            }
        }

        /// <summary>
        /// Set a path to the requested destination
        /// </summary>
        /// <param name="destination">The IArea the movable wants to go to</param>
        public void SetPath(IArea destination)
        {
            if (Dijkstra.IsElevatorCloser(Area, destination) is Elevator && Status != MovableStatus.EVACUATING)
            {
                Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, Dijkstra.IsElevatorCloser(Area, destination)));
                WantsElevator = true;
                LastStatus = Status;
                Status = MovableStatus.ELEVATOR_REQUEST;
            }
            else
            {
                Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, destination));
            }


            // Count extra first step or not
            Path.Dequeue();

        }
        #endregion

        #region EventHandeling
        /// <summary>
        /// Handles guests events
        /// </summary>
        /// <param name="evt">The given event</param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
                LastStatus = MovableStatus.EVACUATING;
                _hteCalculateCounter = 0;
                _hteTime = 5;
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
                    }                    
                    if (item.Key.Contains("Gast"))
                    {
                        switch (evt.EventType)
                        {
                            // Find requested guest

                            case HotelEventType.CHECK_OUT:
                                // guest.checkout()
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.CHECKING_OUT;
                                }
                                break;
                            case HotelEventType.NEED_FOOD:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GET_FOOD;
                                }
                                break;
                            case HotelEventType.GOTO_CINEMA:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GOING_TO_CINEMA;
                                    _AddDeathCounter(this);
                                }
                                break;
                            case HotelEventType.GOTO_FITNESS:
                                if (int.Parse(item.Value) == ID)
                                {
                                    Status = MovableStatus.GOING_TO_FITNESS;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }
        #endregion
    }
}