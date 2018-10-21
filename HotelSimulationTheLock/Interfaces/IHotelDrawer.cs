using System.Collections.Generic;
using System.Drawing;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// Creates a type of drawer how you want to see the hotel
    /// </summary>
    public interface IHotelDrawer
    {
        // Might be better when using the Hotel as parameter
        // and returning a generic
        // Something to think about in the future
        
        /// <summary>
        /// Return a bitmap of how the hotel should be visualized
        /// </summary>
        /// <param name="areas">The areas of a hotel</param>
        /// <param name="movables">The movables from a hotel</param>
        /// <returns></returns>
        Bitmap DrawHotel(List<IArea> areas, List<IMovable> movables);
        
    }
}
