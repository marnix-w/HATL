using HotelEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

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

            // Right?
            HotelEventManager.HTE_Factor = 1 / settings.HTEPerSeconds;

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

        public void PerformAllActions()
        {
            lock (HotelMovables)
            {
                foreach (var item in HotelMovables)
                {
                    try
                    {
                        if (!(item is null))
                        {
                            item.PerformAction();
                        }
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Could not find {0}", e.Message);
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

        public void RemoveGuest(Guest guest)
        {           
            LeavingGuests.Add(guest);
        }

        public void Notify(HotelEvent evt)
        {
            // Redo event handler
            if (evt.EventType.Equals(HotelEventType.CHECK_IN))
            {
                string name = string.Empty;
                string request = string.Empty;
                int requestInt;

                if (!(evt.Data is null))
                {
                    name = evt.Data.FirstOrDefault().Key;
                    request = evt.Data.FirstOrDefault().Value;

                    requestInt = int.Parse(Regex.Match(request, @"\d+").Value);
                }
                else
                {
                    return;
                }

                Guest guest = new Guest(name, requestInt, new Point(0, HotelHeight))
                {
                    Area = HotelAreas.Find(X => X.Position == new Point(0, HotelHeight))
                };


                guest.Area = HotelAreas.Find(X => X.Position == guest.Position);
                guest.SetPath(HotelAreas.Find(X => X.Position == new Point(1, HotelHeight)));

                guest.SetPath(HotelAreas.Find(X => X is Reception));

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
                    valueofIArea.Add("ID: " + r.ID + "\t " +r.GetType().ToString().Replace("HotelSimulationTheLock", "")+ r.Classification + " star \t" + r.AreaStatus + " \t" +r.Position +"\n");
                }
                if (a is Fitness || a is Restaurant|| a is Reception)
                {
                    valueofIArea.Add("ID: " + a.ID + "\t " + a.GetType().ToString().Replace("HotelSimulationTheLock", "") +" \t" + a.Capacity + " \t" + a.Position + "\n");
                }

            }

            return valueofIArea;
        }

    }
}