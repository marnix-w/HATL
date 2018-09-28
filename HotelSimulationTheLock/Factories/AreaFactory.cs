using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public enum AreaType
    {
        Room,
        Cinema,
        Fitness,
        Pool,
        Reception,
        Elevator,
        Staircase
    }

    class AreaFactory 
    {
        public static IArea GetArea(AreaType typeOfArea, int capicity, Point position, Point dimention, int amountOfStars)
        {
            switch (typeOfArea)
            {
                case AreaType.Room:
                    return new Room();
                case AreaType.Cinema:
                    return new Cinema();
                case AreaType.Fitness:
                    return new Fitness();
                case AreaType.Pool:
                    return new Pool();
                case AreaType.Reception:
                    return new Reception();
                case AreaType.Elevator:
                    return new Elevator();
                case AreaType.Staircase:
                    return new Staircase();
                default:
                    return null;
                   
            }
            
        }
      
    }
}
