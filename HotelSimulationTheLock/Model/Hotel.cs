using HotelEvents;
using System;
using System.Collections.Generic;
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
        public IHotelBuilder HotelBuilder { get; set; } = new JsonHotelBuilder();
        private SettingsModel Setting { get; set; }

        // Hotel dimensions for calcuations
        public static int HotelHeight { get; set; }
        public static int HotelWidth { get; set; }

        public Hotel(List<JsonModel> layout, SettingsModel settings)
        {
            // Hotel will handle the ChekIn_events so it can add them to its list
            // making it posible to keep the list private
            HotelEventManager.Register(this);
            
            // Set settings file
            Setting = settings;

            // Build the hotel
            HotelAreas = HotelBuilder.BuildHotel(layout, settings);

            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y;
            
            // Right?
            HotelEventManager.HTE_Factor = 1 / Setting.HTEPerSeconds;

            // Set Amount of maids
            for (int i = 0; i < Setting.AmountOfMaids; i++)
            {
                HotelMovables.Add(new Maid(new Point(4, HotelHeight)));
            }

            // Methods for final initialization
            RemoveNullValues();
            SetNeighbour();
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

        // Fix code duplication
        public Bitmap DrawHotel()
        {
            int artSize = Simulation.RoomArtSize;

            Bitmap buffer = new Bitmap((HotelWidth + 1) * artSize, (HotelHeight) * artSize);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                lock (HotelAreas)
                {
                    foreach (IArea area in HotelAreas)
                    {
                        graphics.DrawImage(area.Art,
                                            area.Position.X * artSize,
                                            (area.Position.Y - 1) * artSize,
                                            area.Dimension.Width * artSize,
                                            area.Dimension.Height * artSize);
                    }
                }
            }
            return buffer;
        }

        public Bitmap DrawMovables()
        {
            int artSize = Simulation.RoomArtSize;

            Bitmap buffer = new Bitmap((HotelWidth + 1) * artSize, (HotelHeight) * artSize);

            using (Graphics graphics = Graphics.FromImage(buffer))
            {
                // Prevent opperation from coliding with eachother
                lock (HotelMovables)
                {
                    try
                    {
                        foreach (IMovable movable in HotelMovables)
                        {
                            graphics.DrawImage(movable.Art,
                                                movable.Position.X * artSize * 1.05f,
                                                (movable.Position.Y - 1) * artSize * 1.05f,
                                                movable.Art.Width, movable.Art.Height);
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        return buffer;
                    }

                    
                }            
            }
            return buffer;
        }

        public void PerformAllActions()
        {
            foreach (var item in HotelMovables)
            {
                item.PerformAction();
            }
        }
        
        /// <summary>
        /// Mehtod must be called when initilizing a hotel
        /// This makes sure Dijkstra can provide a path
        /// </summary>
        private void SetNeighbour()
        {
            // Exclude lift when lift is done

            foreach (IArea area in HotelAreas)
            {
                // Add right neighbour
                for (int i = 1; i < HotelWidth; i++)
                {
                    if (AddNeighbour(area, i, 0, i))
                    {
                        break;
                    }                 
                }
                // Add left neighbour
                for (int i = 0; i < HotelWidth - 1; i++)
                {
                    if (AddNeighbour(area, -i, 0, i))
                    {
                        break;
                    }
                }
                if (area.Position.X == 0 || area.Position.X == HotelWidth)
                {
                    // Keep lift weight in mind needs a rework
                    int weight = 1;

                    if (area is Staircase)
                    {
                        weight = Setting.StairsDuration;
                    }

                    // Add top neighbour
                    AddNeighbour(area, 0, 1, weight);
                    // Add bottom neighbour
                    AddNeighbour(area, 0, -1, weight);
                }
            }
        }

        private bool AddNeighbour(IArea area, int xOffset, int yOffset, int wieght)
        {
            if (!(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)) is null))
            {
                area.Edge.Add(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)), wieght);
                return true;
            }
            return false;
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

                guest.SetPath(HotelAreas.Find(X => X is Reception));

                HotelMovables.Add(guest);
               
            }
        }

        /// <summary>
        /// Removes all null values from the hotelarea and hotelmovable lists
        /// </summary>
        private void RemoveNullValues()
        {
            // Preventing null reference errors            
            foreach (var movable in HotelMovables)
            {
                if (movable is null)
                {
                    HotelMovables.Remove(movable);
                }
            }
        }
    }
}