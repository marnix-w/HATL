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
    public class Hotel : HotelEventListener
    {
        // Change the live stats function so these list can be private
        // Make private
        public List<IArea> HotelAreas { get; set; } = new List<IArea>();
        // Make private 
        public List<IMovable> HotelMovables { get; set; } = new List<IMovable>();
        private List<IMovable> LeavingGuests { get; set; } = new List<IMovable>();
        public IHotelBuilder HotelBuilder { get; set; } = new JsonHotelBuilder();
        public IHotelDrawer HotelDrawer { get; set; } = new BitmapHotelDrawer();

        // Hotel dimensions for calcuations
        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }

        public List<string> valueofMoveable { get; set; } = new List<string>();
        public List<string> valueofIArea { get; set; } = new List<string>();

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
            HotelEventManager.HTE_Factor = 500000;

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

        public IArea GetRoom(Point location)
        {
            return HotelAreas.Find(X => X.Position == location);
        }

        public void PerformAllActions()
        {
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
        }

        public IArea GetRoom(int request)
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

        public IArea getLocation(IArea blabla)
        {
            List<IArea> CurrentShortest = HotelAreas;

            IArea guestRoom = null;

            foreach (Restaurant area in HotelAreas.Where(X => X is Restaurant))
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

        public void RemoveGuest(Guest guest)
        {
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

                Guest guest = new Guest(name, requestInt, new Point(0, HotelHeight), id)
                {
                    Area = HotelAreas.Find(X => X.Position == new Point(0, HotelHeight))
                };

                guest.Area = HotelAreas.Find(X => X.Position == guest.Position);

                guest._hotel = this;

                guest.Path = new Queue<IArea>(Dijkstra.GetShortestPathDijkstra(guest.Area, HotelAreas.Find(X => X is Reception)));


                HotelMovables.Add(guest);

            }
        }

        public List<string> currentValue()
        {
            valueofMoveable.Clear();

            foreach (IMovable a in HotelMovables)
            {
                if (a is Guest g)
                {
                    valueofMoveable.Add(g.Name + " \t " + g.RoomRequest + " \t " + g.Status + " \t " + g.Position + "\n");
                }
                if (a is Maid m)
                {
                    valueofMoveable.Add("Maid" + " \t " + m.Status + " \t " + m.Position + "\n");
                }
            }

            return valueofMoveable;
        }

        public List<string> currentValueIArea()
        {
            valueofIArea.Clear();

            foreach (IArea a in HotelAreas)
            {
                if (a is Room r)
                {
                    valueofIArea.Add("ID: " + r.ID + "\t " + r.GetType().ToString().Replace("HotelSimulationTheLock", "") + r.Classification + " star \t" + r.AreaStatus + " \t" + r.Position + "\n");
                }

                if (a is Fitness || a is Restaurant || a is Reception || a is Cinema)
                {
                    valueofIArea.Add("ID: " + a.ID + "\t " + a.GetType().ToString().Replace("HotelSimulationTheLock", "") + " \t" + a.Capacity + " \t" + a.Position + "\n");
                }

            }

            return valueofIArea;
        }

    }
}