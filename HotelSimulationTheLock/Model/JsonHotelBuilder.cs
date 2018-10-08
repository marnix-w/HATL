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
        
        public List<IArea> BuildHotel<T>(List<T> file, SettingsModel settings)
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

            // Read out the json and add rooms to the layout
            foreach (JsonModel i in jsonModel)
            {
                int clasifactionNum = 0;

                if (i.Classification != null)
                {
                    clasifactionNum = int.Parse(Regex.Match(i.Classification, @"\d+").Value);
                }

                IArea area = Factory.GetArea(i.AreaType);
                area.SetJsonValues(i.Position, i.Capacity, i.Dimension, clasifactionNum);
                hotelAreas.Add(area);
            }

            int HotelWidth = hotelAreas.OrderBy(X => X.Position.X).Last().Position.X + 1;
            int HotelHeight = hotelAreas.OrderBy(Y => Y.Position.Y).Last().Position.Y + 1;

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
            
            // Set settings for cinema
            foreach (Cinema cinema in hotelAreas.Where(X => X is Cinema))
            {
                cinema.Duration = settings.CinemaDuration;
            }

            // Set settigns for fitness
            foreach (Fitness fitness in hotelAreas.Where(X => X is Fitness))
            {
                fitness.Capacity = settings.FitnessCapicity;
            }

            // Set settings for restaurant
            foreach (Restaurant restaurant in hotelAreas.Where(X => X is Restaurant))
            {
                restaurant.Capacity = settings.RestaurantCapicity;
            }

            foreach (var area in hotelAreas)
            {
                if (area is null)
                {
                    hotelAreas.Remove(area);
                }
            }



            return hotelAreas;

        }
    }
}
