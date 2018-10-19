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
        public List<IArea> BuildHotel<T>(T file, SettingsModel settings)
        {
            List<IArea> areas = new List<IArea>();

            areas.Add(new Elevator() { ID = 1, Position = new Point(0, 1) });
            areas.Add(new Elevator() { ID = 2, Position = new Point(0, 2) });
            areas.Add(new Elevator() { ID = 3, Position = new Point(0, 3) });
            areas.Add(new Elevator() { ID = 4, Position = new Point(0, 4) });

            areas.Add(new Staircase() { ID = 5, Position = new Point(7, 1 ) });
            areas.Add(new Staircase() { ID = 6, Position = new Point(7,2 ) });
            areas.Add(new Staircase() { ID = 7, Position = new Point(7,3 ) });
            areas.Add(new Staircase() { ID = 8, Position = new Point(7,4 ) });

            areas.Add(new Reception() { ID = 9, Position = new Point(1, 4) });
            areas.Add(new Lobby() { ID = 10, Position = new Point(2, 4 ) });
            areas.Add(new Lobby() { ID = 11, Position = new Point(3, 4 ) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(4, 4 ) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(5, 4 ) });
            areas.Add(new Lobby() { ID = 12, Position = new Point(6, 4 ) });

            areas.Add(new Room() { ID = 13, Position = new Point(1, 2), Classification = 5, Art = Properties.Resources.room_five_star_open });
            areas.Add(new Room() { ID = 14, Position = new Point(3, 2), Classification = 4, Art = Properties.Resources.room_four_star_open });
            areas.Add(new Room() { ID = 15, Position = new Point(3, 3), Classification = 3, Art = Properties.Resources.room_three_star_open });
            areas.Add(new Room() { ID = 16, Position = new Point(5, 2), Classification = 1, Art = Properties.Resources.room_one_star_open });
            areas.Add(new Room() { ID = 17, Position = new Point(5, 2), Classification = 1, Art = Properties.Resources.room_one_star_open });
            areas.Add(new Room() { ID = 18, Position = new Point(5, 3), Classification = 2, Art = Properties.Resources.room_two_star_open });
            areas.Add(new Room() { ID = 19, Position = new Point(5, 3), Classification = 2, Art = Properties.Resources.room_two_star_open });

            areas.Add(new Cinema() { ID = 20, Position = new Point(1,1) });
            areas.Add(new Fitness() { ID = 21, Position = new Point(3,1) });
            areas.Add(new Restaurant() { ID = 22, Position = new Point(5,1) });

            return areas;
        }

        public List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel)
        {
            List<IMovable> movables = new List<IMovable>();

            movables.Add(new ElevatorCart(new Point(0,4), hotel, 5));






            return movables;
        }
    }
}
