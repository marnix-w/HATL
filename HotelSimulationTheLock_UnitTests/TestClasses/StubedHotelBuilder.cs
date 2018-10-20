using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulationTheLock;
using HotelEvents;
using System.Drawing;

namespace HotelSimulationTheLock
{
    public class StubedHotelBuilder : IHotelBuilder
    {
        List<IArea> areas { get; set; }

        public List<IArea> BuildHotel<T>(T file, SettingsModel settings)
        {
            areas = new List<IArea>
            {
                new Elevator() { ID = 1, Position = new Point(0, 1) },
                new Elevator() { ID = 2, Position = new Point(0, 2) },
                new Elevator() { ID = 3, Position = new Point(0, 3) },
                new Elevator() { ID = 4, Position = new Point(0, 4) },

                new Staircase() { ID = 5, Position = new Point(7, 1) },
                new Staircase() { ID = 6, Position = new Point(7, 2) },
                new Staircase() { ID = 7, Position = new Point(7, 3) },
                new Staircase() { ID = 8, Position = new Point(7, 4) },

                new Reception() { ID = 9, Position = new Point(1, 4) },
                new Lobby() { ID = 10, Position = new Point(2, 4) },
                new Lobby() { ID = 11, Position = new Point(3, 4) },
                new Lobby() { ID = 12, Position = new Point(4, 4) },
                new Lobby() { ID = 12, Position = new Point(5, 4) },
                new Lobby() { ID = 12, Position = new Point(6, 4) },

                new Room() { ID = 13, Position = new Point(1, 2), Classification = 5, },
                new Room() { ID = 14, Position = new Point(3, 2), Classification = 4, },
                new Room() { ID = 15, Position = new Point(3, 3), Classification = 3, },
                new Room() { ID = 16, Position = new Point(5, 2), Classification = 1, },
                new Room() { ID = 17, Position = new Point(5, 2), Classification = 1, },
                new Room() { ID = 18, Position = new Point(5, 3), Classification = 2, },
                new Room() { ID = 19, Position = new Point(5, 3), Classification = 2, },

                new Cinema() { ID = 20, Position = new Point(1, 1) },
                new Fitness() { ID = 21, Position = new Point(3, 1) },
                new Restaurant() { ID = 22, Position = new Point(5, 1) }
            };

            foreach (var item in areas)
            {
                // Add right neighbour
                for (int i = 1; i < 7; i++)
                {
                    if (AddNeighbour(item, i, 0, 1))
                    {
                        break;
                    }
                }
                // Add left neighbour
                for (int i = 1; i < 7 - 1; i++)
                {
                    if (AddNeighbour(item, -i, 0, 1))
                    {
                        break;
                    }
                }
                if (item.Position.X == 0 || item.Position.X == 7)
                {
                    // Keep lift weight in mind needs a rework

                    if (item is Elevator)
                    {
                        continue;
                    }


                    // Add top neighbour
                    AddNeighbour(item, 0, 1, 1);
                    // Add bottom neighbour
                    AddNeighbour(item, 0, -1, 1);
                }
            }

            return areas;
        }

        public List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel)
        {
            List<IMovable> movables = new List<IMovable>();

            movables.Add(new ElevatorCart(new Point(0,4), hotel, 5));






            return movables;
        }

        private bool AddNeighbour(IArea area, int xOffset, int yOffset, int weight)
        {
            if (!(areas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)) is null))
            {
                area.Edge.Add(areas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)), weight);
                return true;
            }
            return false;
        }
    }
}
