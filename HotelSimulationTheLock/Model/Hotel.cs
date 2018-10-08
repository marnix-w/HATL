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
            BuildHotel(layout);

            // Right?
            HotelEventManager.HTE_Factor = 1 / Setting.HTEPerSeconds;

            // Methods for final initialization
            RemoveNullValues();
            SetNeighbour();
            Dijkstra.IntilazeDijkstra(HotelAreas, this);
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
                        continue;
                    }
                }
                // Add left neighbour
                for (int i = 0; i < HotelWidth - 1; i++)
                {
                    if (AddNeighbour(area, -i, 0, i))
                    {
                        continue;
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
                int requestInt = 0;

                if (!(evt.Data is null))
                {
                    name = evt.Data.FirstOrDefault().Key;
                    request = evt.Data.FirstOrDefault().Value;

                    requestInt = int.Parse(Regex.Match(request, @"\d+").Value);
                }
                else
                {
                    name = "test guest";
                    request = "no request";
                }

                HotelMovables.Add(new Guest(name, requestInt, new Point(0, HotelHeight)));
                //Guest guest = new Guest(name, requestInt, new Point(0, HotelHeight + 1));


                //IMovableList.Add(guest);

                Console.WriteLine($"new guest = name: {name}, request: {request} ");
            }
        }

        /// <summary>
        /// Removes all null values from the hotelarea and hotelmovable lists
        /// </summary>
        private void RemoveNullValues()
        {
            // Preventing null reference errors
            foreach (var area in HotelAreas)
            {
                if (area is null)
                {
                    HotelAreas.Remove(area);
                }
            }
            foreach (var movable in HotelMovables)
            {
                if (movable is null)
                {
                    HotelMovables.Remove(movable);
                }
            }
        }

        private void BuildHotel(List<JsonModel> layout)
        {
            // Create a factory to make the rooms
            AreaFactory Factory = new AreaFactory();

            // Read out the json and add rooms to the layout
            foreach (JsonModel i in layout)
            {
                int clasifactionNum = 0;

                if (i.Classification != null)
                {
                    clasifactionNum = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }

                IArea area = Factory.GetArea(i.AreaType);
                area.SetJsonValues(i.Position, i.Capacity, i.Dimension, clasifactionNum);
                HotelAreas.Add(area);
            }

            HotelWidth = HotelAreas.OrderBy(X => X.Position.X).Last().Position.X + 1;
            HotelHeight = HotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y + 1;

            // Set elevator and staircase
            for (int i = 1; i < HotelHeight + 1; i++)
            {
                // 5 is the capacity get from setting screen
                IArea elevator = Factory.GetArea("Elevator");
                IArea staircase = Factory.GetArea("Staircase");

                elevator.SetJsonValues(new Point(0, i), Setting.ElevatorCapicity, new Size(1, 1), i);
                staircase.SetJsonValues(new Point(HotelWidth, i), 5, new Size(1, 1), 0);

                HotelAreas.Add(elevator);
                HotelAreas.Add(staircase);
            }

            // Set reception and lobby
            for (int i = 1; i < HotelWidth; i++)
            {
                if (i == 1)
                {
                    IArea reception = Factory.GetArea("Reception");

                    reception.SetJsonValues(new Point(1, HotelHeight), 5, new Size(1, 1), 1);

                    HotelAreas.Add(reception);
                }
                else
                {
                    IArea Lobby = Factory.GetArea("Lobby");

                    Lobby.SetJsonValues(new Point(i, HotelHeight), 5, new Size(1, 1), i);

                    HotelAreas.Add(Lobby);
                }
            }

            // Set Amount of maids
            for (int i = 0; i < Setting.AmountOfMaids; i++)
            {
                HotelMovables.Add(new Maid(new Point(4, HotelHeight)));
            }

            // Set settings for cinema
            foreach (Cinema cinema in HotelAreas.Where(X => X is Cinema))
            {
                cinema.Duration = Setting.CinemaDuration;
            }

            // Set settigns for fitness
            foreach (Fitness fitness in HotelAreas.Where(X => X is Fitness))
            {
                fitness.Capacity = Setting.FitnessCapicity;
            }

            // Set settings for restaurant
            foreach (Restaurant restaurant in HotelAreas.Where(X => X is Restaurant))
            {
                restaurant.Capacity = Setting.RestaurantCapicity;
            }
        }

    }
}