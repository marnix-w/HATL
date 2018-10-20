using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace HotelSimulationTheLock
{
    public class Hotel : IListner
    {
        public List<IArea> HotelAreas { get; set; } = new List<IArea>();
        private List<IMovable> HotelMovables { get; set; } = new List<IMovable>();

        private List<IMovable> LeavingGuests { get; } = new List<IMovable>();
        private List<IMovable> ArivingGuests { get;  } = new List<IMovable>();

        private IHotelBuilder HotelBuilder { get; set; }
        private IHotelDrawer HotelDrawer { get; set; } = new HotelSimDrawer();

        // Hotel dimensions for calcuations
        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }

        private List<string> ValueofMoveable { get;  } = new List<string>();
        private List<string> ValueofIArea { get;  } = new List<string>();

        private ElevatorCart _elevatorCart { get; set; }

        public Hotel()
        {

        }

        public Hotel(List<JsonModel> layout, SettingsModel settings, IHotelBuilder TypeOfBuilder)
        {
            // Hotel will handle the CheckIn_events so it can add them to its list
            // making it posible to keep the list private
            HotelEventManager.Register(this);

            HotelBuilder = TypeOfBuilder;

            // Build the hotel
            HotelAreas = HotelBuilder.BuildHotel(layout, settings);
            HotelMovables = HotelBuilder.BuildMovable(settings, this);

            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y;
   

            _elevatorCart = (ElevatorCart)HotelMovables.Find(X => X is ElevatorCart);

            // Right?
            HotelEventManager.HTE_Factor = 2;


            // Methods for final initialization           
            Dijkstra.IntilazeDijkstra(this);
            HotelEventManager.Start();
        }

        public void RemoveSearchProperties()
        {
            foreach (IArea area in HotelAreas)
            {
                area.BackTrackCost = null;
                area.NearestToStart = null;
                area.Visited = false;
            }
        }
        
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

        public bool IsHotelSafe()
        {
            if (HotelMovables.Where(X =>!(X is ElevatorCart) && X.Area is Reception).Count() == HotelMovables.Where(X => !(X is ElevatorCart)).Count())
            {
                return true;
            }
            return false;
        }

        // call drawer

        public Bitmap DrawMap()
        {
            return HotelDrawer.DrawHotel(HotelAreas, HotelMovables);
        }

        public int HowLongWillMovieTake()
        {
            return HotelMovables.Where(X => X is Guest && ((Guest)X).Status == MovableStatus.WATCHING && ((Guest)X)._hteTime != 0).Select(X => ((Guest)X)._hteTime - ((Guest)X)._hteCalculateCounter).First();
        }

        // Get room overlaods

        /// <summary>
        /// Find an area in the hotel based on its position
        /// </summary>
        /// <param name="location">A point indecating its location</param>
        /// <returns>The coresponding IArea if none found, null</returns>
        public IArea GetArea(Point location)
        {
            return HotelAreas.Find(X => X.Position == location);
        }

        public IArea GetAreaByID(int ID)
        {
            return HotelAreas.Find(X => X.ID == ID);
        }

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
        /// Find an area of a specefic type
        /// </summary>
        /// <param name="type">Specifi as "typeof([specilazation])"</param>
        /// <returns>The coresponding IArea if none found, null</returns>
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

        // end get room
        public IArea GetNewLocation(IArea CurrentArea, Type newArea)
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
        public void CallElevator(IMovable guest)
        {
                _elevatorCart.RequestElevator(guest, HotelHeight);
          //if(_elevatorCart.RequestList.Count < _elevatorCart.Capacity)
          //  {
          //  }
          //  else
          //  {
          //     // guest.ElevatorIsFullNewPath(guest.FinalDes);
          //      guest.Status = MovableStatus.GOING_TO_ROOM;
          //      guest.Path(guest.FinalDes);
          //  }


        }


        public void PerformAllActions()
        {

            foreach (var item in ArivingGuests)
            {
                ((Guest)item).RegisterAs();
                item.SetPath(GetArea(typeof(Reception)));
                HotelMovables.Add(item);
            }

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

            foreach (var item in LeavingGuests)
            {
                HotelMovables.Remove(item);
            }

            ArivingGuests.Clear();
            LeavingGuests.Clear();

        }

        public void RemoveGuest(Guest guest)
        {
            HotelEventManager.Deregister(guest);
            LeavingGuests.Add(guest);

        }

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

        // motivate your choice here jasper
        public List<string> CurrentValue()
        {
            ValueofMoveable.Clear();

            foreach (IMovable a in HotelMovables)
            {
                if (a is Guest g)
                {
                    if (g.FinalDes != null)
                    {
                        ValueofMoveable.Add(g.Name + " " + g.RoomRequest + "\t" + g.Status + "\t" + g.Position + "\t" + g.FinalDes.Position + "\n");
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

    }
}