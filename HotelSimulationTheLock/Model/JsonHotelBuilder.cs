using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    class JsonHotelBuilder : IHotelBuilder
    {
        private List<IArea> HotelAreas { get; set; }

        private int HotelWidth { get; set; }
        private int HotelHeight { get; set; }

        public List<IArea> BuildHotel<T>(T file, SettingsModel settings)
        {
            HotelAreas = new List<IArea>();

            List<JsonModel> jsonModel;

            if (file is List<JsonModel>)
            {
                jsonModel = file as List<JsonModel>;
            }
            else
            {
                // the provided file is incorrect
                return null;
            }

            // Create a factory to make the rooms
            AreaFactory Factory = new AreaFactory();

            // Read out the json file and add rooms to the layout
            foreach (JsonModel i in jsonModel)
            {
                int classificationNum = 0;

                if (i.Classification != null)
                {
                    classificationNum = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }

                IArea area = Factory.GetArea(i.AreaType);
                area.SetJsonValues(i.ID, i.Position, i.Capacity, i.Dimension, classificationNum);
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

                elevator.SetJsonValues(300, new Point(0, i), settings.ElevatorCapicity, new Size(1, 1), i);
                staircase.SetJsonValues(400, new Point(HotelWidth, i), 5, new Size(1, 1), 0);

                HotelAreas.Add(elevator);
                HotelAreas.Add(staircase);
            }

            // Set reception and lobby
            for (int i = 1; i < HotelWidth; i++)
            {
                if (i == 1)
                {
                    IArea reception = Factory.GetArea("Reception");

                    reception.SetJsonValues(500, new Point(1, HotelHeight), 5, new Size(1, 1), 1);

                    HotelAreas.Add(reception);
                }
                else
                {
                    IArea Lobby = Factory.GetArea("Lobby");

                    Lobby.SetJsonValues(600, new Point(i, HotelHeight), 5, new Size(1, 1), i);

                    HotelAreas.Add(Lobby);
                }
            }

            foreach (IArea area in HotelAreas)
            {
                // Set settings for cinema
                if (area is Cinema)
                {
                    ((Cinema)area).Duration = settings.CinemaDuration;
                }

                // Set settings for fitness
                else if (area is Fitness)
                {
                    ((Fitness)area).Capacity = settings.FitnessCapicity;
                }

                // Set settings for restaurant
                else if (area is Restaurant)
                {
                    ((Restaurant)area).Capacity = settings.RestaurantCapicity;
                    ((Restaurant)area).Duration = settings.RestaurantDuration;
                }

                else if (area is null)
                {
                    HotelAreas.Remove(area);
                }

                // Add right neighbour
                for (int i = 1; i < HotelWidth; i++)
                {
                    if (AddNeighbour(area, i, 0, i))
                    {
                        break;
                    }
                }
                // Add left neighbour
                for (int i = 1; i < HotelWidth - 1; i++)
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

                    if (area is Elevator)
                    {
                        weight = 100;
                    }

                    // Add top neighbour
                    AddNeighbour(area, 0, 1, weight);
                    // Add bottom neighbour
                    AddNeighbour(area, 0, -1, weight);
                }
            }

            return HotelAreas;
        }

        public List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel)
        {
            List<IMovable> movables = new List<IMovable>();

            for (int i = 0; i < settings.AmountOfMaids; i++)
            {
                movables.Add(new Maid(new Point(4, HotelHeight)));
            }

            movables.Add(new Receptionist(new Point(1, HotelHeight), hotel));

            foreach (var movable in movables)
            {
                if (movable is null)
                {
                    movables.Remove(movable);
                }
            }

            return movables;
        }

        private bool AddNeighbour(IArea area, int xOffset, int yOffset, int weight)
        {
            if (!(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)) is null))
            {
                area.Edge.Add(HotelAreas.Find(X => X.Position == new Point(area.Position.X + xOffset, area.Position.Y + yOffset)), weight);
                return true;
            }
            return false;
        }
    }
}
