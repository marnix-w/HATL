using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// Create an type of drawer how you want to see the hotel
    /// </summary>
    public interface IHotelDrawer
    {
        // Might be better when using the Hotel as parameter
        // and returning a generic
        // Somthing to think about in the future
        
        /// <summary>
        /// Return a bitmap of how the hotel should be vizualized
        /// </summary>
        /// <param name="areas">The areas of an hotel</param>
        /// <param name="movables">The movables from an hotel</param>
        /// <returns></returns>
        Bitmap DrawHotel(List<IArea> areas, List<IMovable> movables);
        
    }
}
