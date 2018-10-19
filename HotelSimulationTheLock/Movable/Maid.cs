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
    public class CleaningEvent
    {
        public IArea ToClean { get; set; }
        public int Time { get; set; }
    }

    public class Maid : IMovable, IListner
    {
        public Point Position { get; set; }
        public Bitmap Art { get; set; } = Properties.Resources.maid;
        public MovableStatus Status { get; set; }
        public Queue<CleaningEvent> ToCleanList { get; set; } = new Queue<CleaningEvent>();
        public Dictionary<MovableStatus, Action> Actions { get; set; } = new Dictionary<MovableStatus, Action>();
        public Queue<IArea> Path { get; set; } = new Queue<IArea>();
        public bool WantsElevator { get; set; } = false;
        public MovableStatus LastStatus { get; set; }
        public Hotel Hotel { get; set; }
        public int _hteTime { get; set; }
        public int _hteCalculateCounter { get; set; } = 0;
        // Iarea information
        #region
        public IArea Area { get; set; }        
        public IArea FinalDes { get; set; }
        #endregion

        public Maid(Point startLocation, Hotel hotel)
        {
            Hotel = hotel;
            Position = startLocation;
            Area = Hotel.GetArea(Position);

            Status = MovableStatus.IDLE;

            HotelEventManager.Register(this);

            Actions.Add(MovableStatus.IDLE, IsSomthingInQueue);
            Actions.Add(MovableStatus.GOING_TO_ROOM, GoToRoom);
            Actions.Add(MovableStatus.CLEANING, Cleaning);
            Actions.Add(MovableStatus.ELEVATOR_REQUEST, CallElevator);
            Actions.Add(MovableStatus.LEAVING_ELEVATOR, LeavingElevator);
            Actions.Add(MovableStatus.WAITING_FOR_ELEVATOR, Idle);
            Actions.Add(MovableStatus.EVACUATING, _Evacuate);
            Actions.Add(MovableStatus.IN_ELEVATOR, null);
            // CHANGE

        }

        private void IsSomthingInQueue()
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

        private void LeavingElevator()
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

        private void Move()
        {
            IArea destination = Path.Dequeue();
            Area = destination;
            Position = destination.Position;
        }

        private void Cleaning()
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

        private void GoToRoom()
        {
            if (Path.Any())
            {
                Move();
            }
            else if(ToCleanList.Any())
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

        public void PerformAction()
        {
               
            

            if (Actions[Status] != null)
            {               
                Actions[Status]();
                
            }



             
        }
        
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

        public void CallElevator()
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
                Move();
            }
            else if (Area is Elevator && WantsElevator)
            {
                Hotel.CallElevator(this);
                WantsElevator = false;
                Status = MovableStatus.WAITING_FOR_ELEVATOR;
            }
        }
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
                Move();
            }
        }
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
    }
}
