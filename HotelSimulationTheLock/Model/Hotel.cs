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
        private List<IArea> HotelAreas { get; set; } = new List<IArea>();      
        private List<IMovable> HotelMovables { get; set; } = new List<IMovable>();
        
        private List<IMovable> LeavingGuests { get; set; } = new List<IMovable>();
        private IHotelBuilder HotelBuilder { get; set; } = new JsonHotelBuilder();
        private IHotelDrawer HotelDrawer { get; set; } = new BitmapHotelDrawer();

        // Hotel dimensions for calcuations
        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }

        private List<string> ValueofMoveable { get; set; } = new List<string>();
        private List<string> ValueofIArea { get; set; } = new List<string>();

        public Hotel(List<JsonModel> layout, SettingsModel settings)
        {
            // Hotel will handle the CheckIn_events so it can add them to its list
            // making it posible to keep the list private
            HotelEventManager.Register(this);


            // Build the hotel
            HotelAreas = HotelBuilder.BuildHotel(layout, settings);
            HotelMovables = HotelBuilder.BuildMovable(settings, this);          

            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y;

            ElevatorCart elevator = new ElevatorCart(this, settings.ElevatorCapicity);

            // Right?
            HotelEventManager.HTE_Factor = 2;


            // Methods for final initialization           
            Dijkstra.IntilazeDijkstra(this, HotelAreas);
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

        public List<IArea> GetAreas()
        {
            return HotelAreas;
        }

        // call drawer

        public Bitmap DrawMap()
        {
            return HotelDrawer.DrawHotel(HotelAreas, HotelMovables);
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
        public IArea GetNewLocation(IArea blabla, Type fuck)
        {
            
            List<IArea> CurrentShortest = HotelAreas;

            IArea guestRoom = null;

            foreach (IArea area in HotelAreas.Where(X => X.GetType() == fuck))
            {
                if (Dijkstra.GetShortestPathDijkstra(blabla, area).Count < CurrentShortest.Count)
                {

                    CurrentShortest = Dijkstra.GetShortestPathDijkstra(blabla, area);
                    guestRoom = area;
                }
            }

            //this room needs to be casted to the guest
            return guestRoom;
        }       


        public void PerformAllActions()
        {
            
            lock (HotelMovables)
            {
                foreach (IMovable movable in HotelMovables)
                {
                    if (movable is Guest)
                    {
                        ((Guest)movable).RegisterAs();
                    }
                }

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


                guest._hotel = this;

                      
                guest.Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(guest.Area, GetArea(typeof(Reception))));

                HotelMovables.Add(guest);

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
                    ValueofMoveable.Add(g.Name + " \t " + g.RoomRequest + " \t " + g.Status + " \t " + g.Position + "\n");
                }
                if (a is Maid m)
                {
                    ValueofMoveable.Add("Maid" + " \t " + m.Status + " \t " + m.Position + "\n");
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