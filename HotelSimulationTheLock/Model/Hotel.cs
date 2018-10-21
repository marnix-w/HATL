using HotelEvents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The simulation object that handles the main operations
    /// </summary>
    public class Hotel : IListener
    {    
        // Properties

        #region Main Properties
        /// <summary>
        /// Stores all the areas in the hotel
        /// </summary>
        public List<IArea> HotelAreas { get; set; } = new List<IArea>(); // We wanted this list private but couldn't make it because of problems with dijkstra
        /// <summary>
        /// Stores all the movables in the hotel
        /// </summary>
        private List<IMovable> HotelMovables { get; set; } = new List<IMovable>();
        
        // Adding and removing guests while handling events caused serious errors
        // we save them in a temporary list and add them in the thread hotel is running on
        // this seems to solve the problem
        // there probably is a better solution but we havn't had time to go in depth on threads
        
        /// <summary>
        /// Guests that are leaving
        /// </summary>
        private List<IMovable> LeavingGuests { get; } = new List<IMovable>(); 
        /// <summary>
        /// Guests that are arriving
        /// </summary>
        private List<IMovable> ArivingGuests { get;  } = new List<IMovable>();
        #endregion

        #region Utility Properties
        // using the SOLID principle of dependency inversion
        // so that hotel has less string coupeling

        /// <summary>
        /// The hotel builder for this hotel
        /// </summary>
        private IHotelBuilder _hotelBuilder { get; set; }
        /// <summary>
        /// The hotel drawer for this hotel
        /// </summary>
        private IHotelDrawer _hotelDrawer { get; set; } = new HotelSimDrawer();
        #endregion

        #region Calculation properties
        /// <summary>
        /// The height of the current hotel
        /// </summary>
        public static int HotelHeight { get; set; }
        /// <summary>
        /// The width of the current hotel
        /// </summary>
        public static int HotelWidth { get; set; }
        #endregion

        #region Statistic Properties
        /// <summary>
        /// A list of statistics from the movables
        /// </summary>
        private List<string> _listOfMoveables { get; } = new List<string>();
        /// <summary>
        /// A list of statistics from the areas
        /// </summary>
        private List<string> _listOfFacillty { get; } = new List<string>();
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
            // Write implementation if needed in the future
        }

        /// <summary>
        /// Creates a function hotel 
        /// </summary>
        /// <param name="layout">A file which contains a functioning layout</param>
        /// <param name="settings">The Settings for the simulation</param>
        /// <param name="TypeOfBuilder">A type of builder that can handle the provided file</param>
        public Hotel(List<JsonModel> layout, SettingsModel settings, IHotelBuilder TypeOfBuilder)
        {
            // Hotel will handle the CheckIn_events so it can add them to its list
            // making it possible to keep the list private
            HotelEventManager.Register(this);

            _hotelBuilder = TypeOfBuilder;

            // Build the hotel
            HotelAreas = _hotelBuilder.BuildHotel(layout, settings);
            HotelMovables = _hotelBuilder.BuildMovable(settings, this);

            // Set calculation properties
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

            // Removing guests that have checked out
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
        /// Calls the hotel drawer
        /// </summary>
        /// <returns>A bitmap of the hotel</returns>
        public Bitmap DrawMap()
        {
            return _hotelDrawer.DrawHotel(HotelAreas, HotelMovables);
        }
        #endregion

        #region EventHandeling
        /// <summary>
        /// Handles checkin events and add the ariving guest to the list
        /// </summary>
        /// <param name="evt">The given event</param>
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
                    // Kill test events
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
        /// <returns>An IArea or null</returns>
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
        /// <returns>True of false</returns>
        public bool IsHotelSafe()
        {
            if (HotelMovables.Where(X => !(X is ElevatorCart) && X.Area is Reception).Count() == HotelMovables.Where(X => !(X is ElevatorCart)).Count())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks how long the movie has been playing for
        /// </summary>
        /// <returns>The time the movies has already been playing for</returns>
        public int HowLongWillMovieTake()
        {
            return HotelMovables.Where(X => X is Guest && ((Guest)X).Status == MovableStatus.WATCHING && ((Guest)X)._hteTime != 0).Select(X => ((Guest)X)._hteTime - ((Guest)X)._hteCalculateCounter).First();
        }

        /// <summary>
        /// Finds an area based on its position
        /// </summary>
        /// <param name="location">The location of the area you want to find</param>
        /// <returns>The IArea on the given location</returns>
        public IArea GetArea(Point location)
        {
            return HotelAreas.Find(X => X.Position == location);
        }

        /// <summary>
        /// Finds an area based on its ID
        /// </summary>
        /// <param name="ID">The ID of the area you want to find</param>
        /// <returns>The IArea with the given ID</returns>
        public IArea GetAreaByID(int ID)
        {
            return HotelAreas.Find(X => X.ID == ID);
        }

        /// <summary>
        /// returns a room based on the request
        /// if no room is available it will check for higher star rooms
        /// if no higher star room is available it will return null
        /// </summary>
        /// <param name="request">The requested classification of the room</param>
        /// <returns>A room</returns>
        public IArea GetArea(int request)
        {
            IArea result = null;

            // Upgrade a guests room if request is not available
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
        /// <param name="type">The type of area you want to find</param>
        /// <returns>The IArea with the given type</returns>
        public IArea GetArea(Type type)
        {
            if (!(HotelAreas.Any()))
            {
                return null;
            }
            return HotelAreas.Find(X => X.GetType() == type);
        }

        /// <summary>
        /// extension of the GetArea(int request) mehtod
        /// </summary>
        /// <param name="request">Requested classifacation it should find</param>
        /// <returns>The corresponding IArea. If none found, null</returns>
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

            // This room needs to be casted to the guest
            return guestRoom;
        }

        /// <summary>
        /// Finds an area based on its current area and type
        /// </summary>
        /// <param name="CurrentArea">The current area</param>
        /// <param name="newArea">The type of the new area</param>
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

            // This room needs to be casted to the guest
            return guestRoom;
        }

        #endregion

        #region Elevator
        /// <summary>
        /// An elevator request
        /// </summary>
        /// <param name="guest">The guest that requests the elevator</param>
        public void CallElevator(IMovable guest)
        {
            _elevatorCart.RequestElevator(guest, HotelHeight);
        }
        #endregion

        #region Statistics
        /// <summary>
        /// Sets movable statistics
        /// </summary>
        /// <returns></returns>
        public List<string> CurrentValue()
        {
            _listOfMoveables.Clear();

            foreach (IMovable a in HotelMovables)
            {
                if (a is Guest g)
                {
                    if (g.FinalDes != null)
                    {
                        _listOfMoveables.Add(g.Name + " \t " + g.RoomRequest + "\t" + g.Position +  "\t"  +g.Status + "\n");
                    }
                }
                if (a is Maid m)
                {
                    _listOfMoveables.Add("Maid" + " \t\t" + m.Position + " \t " + m.Status + "\n");
                }

                if (a is ElevatorCart e)
                {
                    _listOfMoveables.Add("Elevator" + "\t \t" + e.Position + " \t "  +e.Status + "\n");
                }
            }

            return _listOfMoveables;
        }

        /// <summary>
        /// Sets area statistics reading data from the Facillity list
        /// </summary>
        /// <returns>A list of facillities</returns>
        public List<string> CurrentValueIArea()
        {
            _listOfFacillty.Clear();

            foreach (IArea a in HotelAreas)
            {
                if (a is Room r)
                {

                    _listOfFacillty.Add( r.ID + "\t " + r.GetType().ToString().Replace("HotelSimulationTheLock.", "")+ r.Classification + " star \t"  + r.Position + "\t"+ r.AreaStatus +"\n");
               

                }

                if (a is Fitness || a is Restaurant || a is Reception || a is Cinema)
                {
                    _listOfFacillty.Add(a.ID + " \t" + a.Position + "\t" + a.Capacity + "\t" +a.GetType().ToString().Replace("HotelSimulationTheLock.", "") + "\n");


                }

            }

            return _listOfFacillty;
        }
        #endregion
    }
}