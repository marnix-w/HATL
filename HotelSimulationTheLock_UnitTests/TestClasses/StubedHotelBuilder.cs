using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulationTheLock;
using HotelEvents;
using System.Drawing;

namespace HotelSimulationTheLock_UnitTests
{
    class StubedHotelBuilder : IHotelBuilder
    {
        public List<IArea> BuildHotel<T>(T file, SettingsModel settings)
        {
            List<IArea> areas = new List<IArea>();

            areas.Add(new Elevator() { ID = 1, Position = new Point(0, 0) });
            areas.Add(new Elevator() { ID = 2, Position = new Point(1, 0) });
            areas.Add(new Elevator() { ID = 3, Position = new Point(2, 0) });
            areas.Add(new Elevator() { ID = 4, Position = new Point(3, 0) });

            areas.Add(new Staircase() { ID = 5, Position = new Point(0, 7) });
            areas.Add(new Staircase() { ID = 6, Position = new Point(1, 7) });
            areas.Add(new Staircase() { ID = 7, Position = new Point(2, 7) });
            areas.Add(new Staircase() { ID = 8, Position = new Point(3, 7) });

            areas.Add(new Reception() { ID = 9, Position = new Point(3, 1) });
            areas.Add(new Lobby() { ID = 10, Position = new Point(3, 2) });
            areas.Add(new Lobby() { ID = 11, Position = new Point(3, 3) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(3, 4) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(3, 5) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(3, 6) });

            areas.Add(new Room() { ID = 13, Position = new Point(1, 1), Classification = 5 });
            areas.Add(new Room() { ID = 14, Position = new Point(1, 3), Classification = 4 });
            areas.Add(new Room() { ID = 15, Position = new Point(2, 3), Classification = 3 });
            areas.Add(new Room() { ID = 16, Position = new Point(1, 5), Classification = 1 });
            areas.Add(new Room() { ID = 17, Position = new Point(1, 5), Classification = 1 });
            areas.Add(new Room() { ID = 18, Position = new Point(2, 5), Classification = 2 });
            areas.Add(new Room() { ID = 19, Position = new Point(2, 5), Classification = 2 });

            areas.Add(new Cinema() { ID = 20, Position = new Point(0, 1) });
            areas.Add(new Fitness() { ID = 21, Position = new Point(0, 3) });
            areas.Add(new Restaurant() { ID = 22, Position = new Point(0, 5) });

            return areas;
        }

        public List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel)
        {
            List<IMovable> areas = new List<IMovable>();








            return areas;
        }
    }
}
