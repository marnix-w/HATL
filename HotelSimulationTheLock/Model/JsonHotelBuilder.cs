using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An hotel builder that works with the jsonmodel
    /// </summary>
    public class JsonHotelBuilder : IHotelBuilder
    {
        // Properties:
        #region
        /// <summary>
        /// The areas that are build
        /// </summary>
        private List<IArea> HotelAreas { get; set; } = new List<IArea>();
        /// <summary>
        /// Hotel width that is used for building opperations
        /// </summary>
        private int HotelWidth { get; set; }
        /// <summary>
        /// Hotel hieght that is used for building opperations
        /// </summary>
        private int HotelHeight { get; set; }
        /// <summary>
        /// Create the area facotry wich the Hotel builder will use
        /// </summary>
        private AreaFactory Factory { get; set; } = new AreaFactory();

        #endregion

        /// <summary>
        /// Method used for testing testing the neighbor function
        /// </summary>
        /// <param name="from">Area from wich the edge goes</param>
        /// <param name="to">Area to wich te edge arives</param>
        /// <param name="weight">The weight thats added tot the edge</param>
        public static void AddDirectedEdge(IArea from, IArea to, int weight)
        {
            from.Edge.Add(to, weight);
        }

        /// <summary>
        /// Provide a generic file and an settings model
        /// Truh these parameters create an list of Iareas
        /// This method is also resposable for setting the neighbors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">An file provding information to build an Iarea list</param>
        /// <param name="settings">The settings model used for the simulation</param>
        /// <returns></returns>
        public List<IArea> BuildHotel<T>(T file, SettingsModel settings)
        {
            // Casting the file to a List<JsonModel>
            #region
            List<JsonModel> jsonModel;

            if (file is List<JsonModel>)
            {
                jsonModel = file as List<JsonModel>;
            }
            else
            {
                // the provided file is incorrect
                // The error handeling is not fully implemented
                return null;
            }
            #endregion

            
            // Read out the json file and add rooms to the layout
            #region
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
            #endregion

            // Set constant objects
            #region
            // Set Elevator and staircase
            for (int i = 1; i < HotelHeight + 1; i++)
            {                
                IArea elevator = Factory.GetArea("Elevator");
                IArea staircase = Factory.GetArea("Staircase");

                elevator.SetJsonValues(HotelAreas.Count() + 1, new Point(0, i), settings.ElevatorCapicity, new Size(1, 1), i);
                staircase.SetJsonValues(HotelAreas.Count() + 1, new Point(HotelWidth, i), 5, new Size(1, 1), 0);

                HotelAreas.Add(elevator);
                HotelAreas.Add(staircase);
            }

            // Set reception and lobby
            for (int i = 1; i < HotelWidth; i++)
            {
                if (i == 1)
                {
                    IArea reception = Factory.GetArea("Reception");

                    reception.SetJsonValues(HotelAreas.Count() + 1, new Point(1, HotelHeight), 5, new Size(1, 1), 1);

                    HotelAreas.Add(reception);
                }
                else
                {
                    IArea Lobby = Factory.GetArea("Lobby");

                    Lobby.SetJsonValues(HotelAreas.Count() + 1, new Point(i, HotelHeight), 5, new Size(1, 1), i);

                    HotelAreas.Add(Lobby);
                }
            }
            #endregion

            // Settings infromation from the settings model and the neigbors
            #region
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

                // Alle neigbros wights are 1 since it has been stated in one
                // of the meatings with the PO that they can jump over the other rooms
                // it would be nicer to add a hallway tile and walk that way but there
                // was no time to do this

                // Add right neighbour
                for (int i = 1; i < HotelWidth; i++)
                {
                    if (AddNeighbour(area, i, 0, 1))
                    {
                        break;
                    }
                }
                // Add left neighbour
                for (int i = 1; i < HotelWidth - 1; i++)
                {
                    if (AddNeighbour(area, -i, 0, 1))
                    {
                        break;
                    }
                }
                if (area.Position.X == 0 || area.Position.X == HotelWidth)
                {
                    // The elevator wont get any neigbros
                    if (area is Elevator)
                    {
                        continue;                      
                    }
                    
                    // Add top neighbour
                    AddNeighbour(area, 0, 1, 1);
                    // Add bottom neighbour
                    AddNeighbour(area, 0, -1, 1);
                }
            }
            #endregion

            // Returning the hotel layout
            return HotelAreas;
        }

        /// <summary>
        /// Create a list of set movables fot the hotel
        /// This list is also resposable for creating the elevator
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        public List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel)
        {
            List<IMovable> movables = new List<IMovable>();

            // creating the correct amount of maids from the setting
            for (int i = 0; i < settings.AmountOfMaids; i++)
            {
                movables.Add(new Maid(new Point(4, HotelHeight), hotel));
            }

            // Creating the elevator cart and receptionist
            movables.Add(new ElevatorCart(new Point(0, HotelHeight), hotel, settings.ElevatorCapicity));            
            movables.Add(new Receptionist(new Point(1, HotelHeight), hotel));
            
            return movables;
        }

        /// <summary>
        /// An funcition that returns true it has added neighbor
        /// </summary>
        /// <param name="area">From area</param>
        /// <param name="xOffset">Offset for finding neigbor</param>
        /// <param name="yOffset">Offset for finding neigbor</param>
        /// <param name="weight">The weight that is givin to the edge</param>
        /// <returns></returns>
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
