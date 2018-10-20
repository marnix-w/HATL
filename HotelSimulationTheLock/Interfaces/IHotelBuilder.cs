using System.Collections.Generic;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// The required methods for building a new hotel
    /// </summary>
    public interface IHotelBuilder
    {
        /// <summary>
        /// Provide a generic file and an Settings model
        /// Truh these parameters create an list of Iareas
        /// This method is also resposable for setting the neighbors
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">An file provding information to build an Iarea list</param>
        /// <param name="settings">The Settings model used for the simulation</param>
        /// <returns></returns>
        List<IArea> BuildHotel<T>(T file, SettingsModel settings);

        /// <summary>
        /// Create a list of set movables fot the hotel
        /// This list is also resposable for creating the elevator
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel);
    }
}
