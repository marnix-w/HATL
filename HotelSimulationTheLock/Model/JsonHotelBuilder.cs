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
            List<IArea> hotelAreas = new List<IArea>();

            List<JsonModel> jsonModel;

            if (file is List<JsonModel>)
            {
                jsonModel  = file as List<JsonModel>;
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
                area.SetJsonValues(i.Position, i.Capacity, i.Dimension, classificationNum);
                hotelAreas.Add(area);
            }

            HotelWidth = hotelAreas.OrderBy(X => X.Position.X).Last().Position.X + 1;
            HotelHeight = hotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y + 1;

            // Set elevator and staircase
            for (int i = 1; i < HotelHeight + 1; i++)
            {
                // 5 is the capacity get from setting screen
                IArea elevator = Factory.GetArea("Elevator");
                IArea staircase = Factory.GetArea("Staircase");

                elevator.SetJsonValues(new Point(0, i), settings.ElevatorCapicity, new Size(1, 1), i);
                staircase.SetJsonValues(new Point(HotelWidth, i), 5, new Size(1, 1), 0);

                hotelAreas.Add(elevator);
                hotelAreas.Add(staircase);
            }

            // Set reception and lobby
            for (int i = 1; i < HotelWidth; i++)
            {
                if (i == 1)
                {
                    IArea reception = Factory.GetArea("Reception");

                    reception.SetJsonValues(new Point(1, HotelHeight), 5, new Size(1, 1), 1);

                    hotelAreas.Add(reception);
                }
                else
                {
                    IArea Lobby = Factory.GetArea("Lobby");

                    Lobby.SetJsonValues(new Point(i, HotelHeight), 5, new Size(1, 1), i);

                    hotelAreas.Add(Lobby);
                }
            }

            foreach (IArea area in hotelAreas)
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
                }

                else if (area is null)
                {
                    hotelAreas.Remove(area);
                }
            }


            //// Zit nu in de foreach loop met settings enzo hierboven
            //foreach (IArea area in hotelAreas)
            //{
            //    if (area is null)
            //    {
            //        hotelAreas.Remove(area);
            //    }
            //}

            HotelAreas = hotelAreas;

            foreach (IArea area in hotelAreas)
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
                        weight = settings.StairsDuration;
                    }

                    // Add top neighbour
                    AddNeighbour(area, 0, 1, weight);
                    // Add bottom neighbour
                    AddNeighbour(area, 0, -1, weight);
                }
            }



            return hotelAreas;

        }
        
        public List<IMovable> BuildMovable(SettingsModel settings)
        {
            List<IMovable> movables = new List<IMovable>();

            for (int i = 0; i < settings.AmountOfMaids; i++)
            {
                movables.Add(new Maid(new Point(4, HotelHeight)));
            }

            movables.Add(new Receptionist(new Point(1, HotelHeight)));

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
