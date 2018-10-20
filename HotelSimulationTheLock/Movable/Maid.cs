using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An collection of information about a cleaning event
    /// </summary>
    public class CleaningEvent
    {
        /// <summary>
        /// The room to clean
        /// </summary>
        public IArea ToClean { get; set; }
        /// <summary>
        /// the time it will take to clean the room
        /// </summary>
        public int Time { get; set; }
    }

    /// <summary>
    /// The hotels cleaners
    /// </summary>
    public class Maid : IMovable, IListener
    {
        #region Properties
        /// <summary>
        /// The position of the maid
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// The art of the maid
        /// </summary>
        public Bitmap Art { get; set; } = Properties.Resources.maid; // <3 barbra
        /// <summary>
        /// The status of the maid
        /// </summary>
        public MovableStatus Status { get; set; }
        /// <summary>
        /// The queue of cleaning events she has to perfom
        /// </summary>
        public Queue<CleaningEvent> ToCleanList { get; set; } = new Queue<CleaningEvent>();
        /// <summary>
        /// The actions list she can perform
        /// </summary>
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();
        /// <summary>
        /// The path she will move of
        /// </summary>
        public Queue<IArea> Path { get; set; } = new Queue<IArea>();
        /// <summary>
        /// A bool to determine if she wants to use the elevator
        /// </summary>
        public bool WantsElevator { get; set; } = false;
        /// <summary>
        /// An bool for restting the status after the elevator
        /// </summary>
        public MovableStatus LastStatus { get; set; }
        /// <summary>
        /// The hotels she belongs to
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// The time to handle an event
        /// </summary>
        public int _hteTime { get; set; }
        /// <summary>
        /// The time counter
        /// </summary>
        public int _hteCalculateCounter { get; set; } = 0;
        /// <summary>
        /// The area she stands on
        /// </summary>
        public IArea Area { get; set; }
        /// <summary>
        /// Her final destination
        /// </summary>
        public IArea FinalDes { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creating a maid to use in the hotel
        /// </summary>
        /// <param name="startLocation"></param>
        /// <param name="hotel"></param>
        public Maid(Point startLocation, Hotel hotel)
        {
            Hotel = hotel;
            Position = startLocation;
            Area = Hotel.GetArea(Position);

            Status = MovableStatus.IDLE;

            HotelEventManager.Register(this);

            Actions.Add(MovableStatus.IDLE, _IsSomthingInQueue);
            Actions.Add(MovableStatus.GOING_TO_ROOM, _GoToRoom);
            Actions.Add(MovableStatus.CLEANING, _Cleaning);
            Actions.Add(MovableStatus.ELEVATOR_REQUEST, _CallElevator);
            Actions.Add(MovableStatus.LEAVING_ELEVATOR, _LeavingElevator);
            Actions.Add(MovableStatus.WAITING_FOR_ELEVATOR, Idle);
            Actions.Add(MovableStatus.EVACUATING, _Evacuate);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);

        }
        #endregion

        #region Behavoir

        // Important note:
        // This list contains a fair amount of code duplication
        // this was the cause of simaltaniusly working on event implemenation
        // we made this choice because of the limeted time we had to implement these
        // as cause the actions list has become to big and needs a good refactor
        // there is no time for this so it is an issue that needs to be rethaught in the fututure

        /// <summary>
        /// CHecks if there is anything to clean
        /// </summary>
        private void _IsSomthingInQueue()
        {
            IArea toClean = Hotel.GetRoomToClean();

            if (toClean != null)
            {
                ToCleanList.Enqueue(new CleaningEvent() { ToClean = toClean, Time = 2 });
                Status = MovableStatus.GOING_TO_ROOM;
            }

            if (ToCleanList.Any())
            {
                Status = MovableStatus.GOING_TO_ROOM;
            }
        }

        /// <summary>
        /// Leaves the elevator and assigns the correct status
        /// </summary>
        private void _LeavingElevator()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                SetPath(Hotel.GetArea(typeof(Reception)));
                FinalDes = Hotel.GetArea(typeof(Reception));
                Status = MovableStatus.EVACUATING;
                LastStatus = MovableStatus.EVACUATING;
                return;
            }

            SetPath(FinalDes);

            Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(Area, FinalDes));
            Status = LastStatus;

        }

        /// <summary>
        /// Moves the maid to the next position
        /// </summary>
        private void _Move()
        {
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;
        }

        /// <summary>
        /// Start cleaning a room
        /// </summary>
        private void _Cleaning()
        {
            if (_hteCalculateCounter == _hteTime)
            {
                if (ToCleanList.Any())
                {
                    Status = MovableStatus.IDLE;
                    ToCleanList.First().ToClean.AreaStatus = AreaStatus.EMPTY;
                    ToCleanList.Dequeue();
                }
                else
                {
                    Status = MovableStatus.IDLE;
                }

            }
            else
            {
                _hteCalculateCounter++;
            }
        }

        /// <summary>
        /// Goes a room wich needs cleaning
        /// </summary>
        private void _GoToRoom()
        {
            if (Path.Any())
            {
                _Move();
            }
            else if (ToCleanList.Any())
            {
                _hteCalculateCounter = 0;
                FinalDes = ToCleanList.First().ToClean;
                _hteTime = ToCleanList.First().Time;
                SetPath(ToCleanList.First().ToClean);
            }

            if (Area == FinalDes)
            {
                Status = MovableStatus.CLEANING;
            }
        }

        /// <summary>
        /// Calls The Elevator
        /// </summary>
        private void _CallElevator()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                SetPath(Hotel.GetArea(typeof(Reception)));
                FinalDes = Hotel.GetArea(typeof(Reception));
                Status = MovableStatus.EVACUATING;
                LastStatus = MovableStatus.EVACUATING;
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
        /// Evacuates the maid
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

        /// <summary>
        /// Checks if there is an evacuation
        /// </summary>
        private void Idle()
        {
            if (Status == MovableStatus.EVACUATING || LastStatus == MovableStatus.EVACUATING)
            {
                SetPath(Hotel.GetArea(typeof(Reception)));
                FinalDes = Hotel.GetArea(typeof(Reception));
                Status = MovableStatus.EVACUATING;
                LastStatus = MovableStatus.EVACUATING;
                return;
            }
        }
        #endregion

        #region Bahavoir

        /// <summary>
        /// Performs an action
        /// </summary>
        public void PerformAction()
        {
            if (Actions[Status] != null)
            {
                Actions[Status]();

            }
        }

        /// <summary>
        /// Sets a path to the final destanation
        /// </summary>
        /// <param name="destination"></param>
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
        /// Handles incomming events
        /// </summary>
        /// <param name="evt"></param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType == HotelEventType.EVACUATE)
            {
                Status = MovableStatus.EVACUATING;
                LastStatus = MovableStatus.EVACUATING;
                _hteCalculateCounter = 0;
                _hteTime = 5;
            }
            else if (evt.EventType.Equals(HotelEventType.CLEANING_EMERGENCY))
            {

                CleaningEvent ce = new CleaningEvent();

                foreach (var item in evt.Data)
                {
                    if (item.Key.Contains("kamer"))
                    {
                        ce.ToClean = Hotel.GetAreaByID(int.Parse(item.Value));

                        if (Hotel.GetAreaByID(int.Parse(item.Value)).AreaStatus != AreaStatus.NEED_CLEANING)
                        {
                            Hotel.GetAreaByID(int.Parse(item.Value)).AreaStatus = AreaStatus.NEED_CLEANING;
                        }
                        else
                        {
                            return;
                        }

                    }
                    if (item.Key.Contains("HTE"))
                    {
                        ce.Time = int.Parse(item.Value);
                    }
                }

                ToCleanList.Enqueue(ce);
            }
        }
        #endregion     
    }
}
