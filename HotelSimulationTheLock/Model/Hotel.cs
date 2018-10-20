using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The simulation object that handels the main opperations
    /// </summary>
    public class Hotel : IListener
    {    
        // Properties

        #region Main Properties
        /// <summary>
        /// Stores all the areas in the hotel
        /// </summary>
        public List<IArea> HotelAreas { get; set; } = new List<IArea>(); // We wanted this list private but coudnt make it because of problems with dijkstra
        /// <summary>
        /// Stores all the movables in the hotel
        /// </summary>
        private List<IMovable> HotelMovables { get; set; } = new List<IMovable>();
        
        // Adding and removing guests while handeling events caused mayor errors
        // we save them in a temparary list and add them in the thread hotel is running on
        // this seems to solve the problem
        // there probably might be a better solution but we havn'_timer had time to go in depth on threads
        
        /// <summary>
        /// Guests that are leaving
        /// </summary>
        private List<IMovable> LeavingGuests { get; } = new List<IMovable>(); 
        /// <summary>
        /// Guests that are ariving
        /// </summary>
        private List<IMovable> ArivingGuests { get;  } = new List<IMovable>();
        #endregion

        #region Utility Properties
        // using the SOLID preinciple of dependency onversion
        // so that hotel has less string coupeling

        /// <summary>
        /// The hotel builder for this hotel
        /// </summary>
        private IHotelBuilder HotelBuilder { get; set; }
        /// <summary>
        /// The hotel drawer for this hotel
        /// </summary>
        private IHotelDrawer HotelDrawer { get; set; } = new HotelSimDrawer();
        #endregion

        #region Calculation properties
        /// <summary>
        /// The hieght of the current hotel
        /// </summary>
        public static int HotelHeight { get; set; }
        /// <summary>
        /// The widht of the curretn hotel
        /// </summary>
        public static int HotelWidth { get; set; }
        #endregion

        #region Statistic Properties
        /// <summary>
        /// A list of statistic from the movables
        /// </summary>
        private List<string> ValueofMoveable { get; } = new List<string>();
        /// <summary>
        /// A list of statistic from the areas
        /// </summary>
        private List<string> ValueofIArea { get; } = new List<string>();
        #endregion

        #region Other properties
        /// <summary>
        /// Taking out the elevator cart for easy calling
        /// </summary>
        private ElevatorCart _elevatorCart { get; set; }
        #endregion

        // Methods

        #region Constructors
        /// <summary>
        /// Basic constructor used for testing dijkstra
        /// </summary>
        public Hotel()
        {
            // Write implemtation if needed in the future
        }

        /// <summary>
        /// Creates a function hotel 
        /// </summary>
        /// <param name="layout">A file wich contains a funcitoning layout</param>
        /// <param name="settings">the Settings for the simulation</param>
        /// <param name="TypeOfBuilder">a type of builder that can handle the provided file</param>
        public Hotel(List<JsonModel> layout, SettingsModel settings, IHotelBuilder TypeOfBuilder)
        {
            // Hotel will handle the CheckIn_events so it can add them to its list
            // making it posible to keep the list private
            HotelEventManager.Register(this);

            HotelBuilder = TypeOfBuilder;

            // Build the hotel
            HotelAreas = HotelBuilder.BuildHotel(layout, settings);
            HotelMovables = HotelBuilder.BuildMovable(settings, this);

            // set calculation properties
            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y;
            
            _elevatorCart = (ElevatorCart)HotelMovables.Find(X => X is ElevatorCart);
            
            // Methods for final initialization           
            Dijkstra.IntilazeDijkstra(this);
            HotelEventManager.Start();
        }
        #endregion

        #region Simulation
        /// <summary>
        /// Performs all the actions every HTE
        /// </summary>
        public void PerformAllActions()
        {
            // Adding new guests
            foreach (var item in ArivingGuests)
            {
                ((Guest)item).RegisterAs();
                item.SetPath(GetArea(typeof(Reception)));
                HotelMovables.Add(item);
            }

            ArivingGuests.Clear();

            // Performing all actions
            lock (HotelMovables)
            {
                foreach (IMovable item in HotelMovables)
                {
                    if (!(item is null))
                    {
                        item.PerformAction();
                    }
                }
            }

            // removing checkoud out guests
            foreach (var item in LeavingGuests)
            {
                HotelMovables.Remove(item);
            }
               
            LeavingGuests.Clear();

        }

        /// <summary>
        /// Removing all dijkstra properties
        /// </summary>
        public void RemoveSearchProperties()
        {
            foreach (IArea area in HotelAreas)
            {
                area.BackTrackCost = null;
                area.NearestToStart = null;
                area.Visited = false;
            }
        }
        #endregion

        #region Drawing
        /// <summary>
        /// Calls the hotel drawer and delivers an bitmap of the hotel
        /// </summary>
        /// <returns></returns>
        public Bitmap DrawMap()
        {
            return HotelDrawer.DrawHotel(HotelAreas, HotelMovables);
        }
        #endregion

        #region EventHandeling
        /// <summary>
        /// Handles checkin events and add the the ariving guest list
        /// </summary>
        /// <param name="evt"></param>
        public void Notify(HotelEvent evt)
        {
            if (evt.EventType.Equals(HotelEventType.CHECK_IN))
            {
                string name = string.Empty;
                string request = string.Empty;
                int requestInt;
                int id = 0;

                if (!(evt.Data is null))
                {
                    foreach (var DataSet in evt.Data)
                    {
                        if (DataSet.Key.Contains("Gast"))
                        {
                            id = int.Parse(Regex.Match(DataSet.Key, @"\d+").Value);
                        }
                    }
                    
                    name = evt.Data.FirstOrDefault().Key;
                    request = evt.Data.FirstOrDefault().Value;

                    requestInt = int.Parse(Regex.Match(request, @"\d+").Value);
                }
                else
                {
                    // kill test events
                    return;
                }

                Guest guest = new Guest(this, name, requestInt, new Point(0, HotelHeight), id)
                {
                    Area = HotelAreas.Find(X => X.Position == new Point(0, HotelHeight))
                };

                guest.Area = HotelAreas.Find(X => X.Position == guest.Position);


                guest.Hotel = this;




                ArivingGuests.Add(guest);

            }
        }

        /// <summary>
        /// Removes the guest and dergesiters him
        /// </summary>
        /// <param name="guest"></param>
        public void RemoveGuest(Guest guest)
        {
            HotelEventManager.Deregister(guest);
            LeavingGuests.Add(guest);
        }


        #endregion

        #region GetInformation
        /// <summary>
        /// Gives an IArea that needs to be cleaned
        /// </summary>
        /// <returns></returns>
        public IArea GetRoomToClean()
        {
            if (HotelAreas.Where(X => X.AreaStatus == AreaStatus.NEED_CLEANING).Any())
            {
                IArea temp = HotelAreas.Where(X => X.AreaStatus == AreaStatus.NEED_CLEANING).First();
                temp.AreaStatus = AreaStatus.IN_CLEANING_QUEUE;
                return temp;
            }

            return null;
        }

        /// <summary>
        /// Checks if everyone is out of the hotel
        /// </summary>
        /// <returns></returns>
        public bool IsHotelSafe()
        {
            if (HotelMovables.Where(X => !(X is ElevatorCart) && X.Area is Reception).Count() == HotelMovables.Where(X => !(X is ElevatorCart)).Count())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks how long the movie has been playing
        /// </summary>
        /// <returns></returns>
        public int HowLongWillMovieTake()
        {
            return HotelMovables.Where(X => X is Guest && ((Guest)X).Status == MovableStatus.WATCHING && ((Guest)X)._hteTime != 0).Select(X => ((Guest)X)._hteTime - ((Guest)X)._hteCalculateCounter).First();
        }

        /// <summary>
        /// Finds an area based on its position
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public IArea GetArea(Point location)
        {
            return HotelAreas.Find(X => X.Position == location);
        }

        /// <summary>
        /// Finds an area based on its ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IArea GetAreaByID(int ID)
        {
            return HotelAreas.Find(X => X.ID == ID);
        }

        /// <summary>
        /// returns a room based on the request
        /// if no room is avalibe it will check for higher rooms
        /// else it will return null
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IArea GetArea(int request)
        {
            IArea result = null;

            // upgrade guests room if request is not available
            for (int i = request; i <= 5; i++)
            {
                if (!(FindRoom(i) is null))
                {
                    result = FindRoom(i);
                    break;
                }
            }

            return result;
          
        }

        /// <summary>
        /// Finds an area based on its type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IArea GetArea(Type type)
        {
            if (!(HotelAreas.Any()))
            {
                return null;
            }
            return HotelAreas.Find(X => X.GetType() == type);
        }
        
        /// <summary>
        /// Exstansion of the GetArea(int request) mehtod
        /// </summary>
        /// <param name="request">Requested classifacation it should find</param>
        /// <returns>The coresponding IArea if none found, null</returns>
        private IArea FindRoom(int request)
        {
            List<IArea> CurrentShortest = HotelAreas;

            IArea guestRoom = null;

            foreach (Room area in HotelAreas.Where(X => X is Room))
            {
                if (area.AreaStatus.Equals(AreaStatus.EMPTY) && area.Classification == request)
                {
                    if (Dijkstra.GetShortestPathDijkstra(HotelAreas.Find(X => X is Reception), area).Count < CurrentShortest.Count)
                    {

                        CurrentShortest = Dijkstra.GetShortestPathDijkstra(HotelAreas.Find(X => X is Reception), area);
                        guestRoom = area;
                    }
                }
            }

            //this room needs to be casted to the guest
            return guestRoom;
        }

        /// <summary>
        /// Finds an area based on its current area and type
        /// </summary>
        /// <param name="CurrentArea"></param>
        /// <param name="newArea"></param>
        /// <returns></returns>
        public IArea GetArea(IArea CurrentArea, Type newArea)
        {
            List<IArea> CurrentShortest = HotelAreas;

            IArea guestRoom = null;

            foreach (IArea area in HotelAreas.Where(X => X.GetType() == newArea))
            {
                if (Dijkstra.GetShortestPathDijkstra(CurrentArea, area).Count < CurrentShortest.Count)
                {

                    CurrentShortest = Dijkstra.GetShortestPathDijkstra(CurrentArea, area);
                    guestRoom = area;
                }
            }

            //this room needs to be casted to the guest
            return guestRoom;
        }

        #endregion

        #region Elevator
        /// <summary>
        /// Puts an elevator request
        /// </summary>
        /// <param name="guest"></param>
        public void CallElevator(IMovable guest)
        {
            _elevatorCart.RequestElevator(guest, HotelHeight);
        }
        #endregion

        #region Statistics
        /// <summary>
        /// Sets Movable statics
        /// </summary>
        /// <returns></returns>
        public List<string> CurrentValue()
        {
            ValueofMoveable.Clear();

            foreach (IMovable a in HotelMovables)
            {
                if (a is Guest g)
                {
                    if (g.FinalDes != null)
                    {
                        ValueofMoveable.Add(g.Name + " " + g.RoomRequest + "\t" + g.Status + "\t" + g.Position + "\n");
                    }
                }
                if (a is Maid m)
                {
                    ValueofMoveable.Add("Maid" + " \t " + m.Status + " \t " + m.Position + "\n");
                }

                if (a is ElevatorCart e)
                {
                    ValueofMoveable.Add("Elevator" + " \t " + e.Status + " \t " + e.Position + "\n");
                }
            }

            return ValueofMoveable;
        }

        /// <summary>
        /// Sets Area statics
        /// </summary>
        /// <returns></returns>
        public List<string> CurrentValueIArea()
        {
            ValueofIArea.Clear();

            foreach (IArea a in HotelAreas)
            {
                if (a is Room r)
                {

                    ValueofIArea.Add("ID: " + r.ID + "\t " + r.GetType().ToString().Replace("HotelSimulationTheLock.", "") + r.Classification + " star \t" + r.AreaStatus + " \t" + r.Position + "\n");

                }

                if (a is Fitness || a is Restaurant || a is Reception || a is Cinema)
                {
                    ValueofIArea.Add("ID: " + a.ID + "\t " + a.GetType().ToString().Replace("HotelSimulationTheLock.", "") + " \t" + a.Capacity + " \t" + a.Position + "\n");


                }

            }

            return ValueofIArea;
        }
        #endregion
    }
}