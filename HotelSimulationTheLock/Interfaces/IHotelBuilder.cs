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
        /// Through these parameters create a list of IAreas
        /// This method is also responsable for setting the neighbours
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file">A file providing information to build an IArea list</param>
        /// <param name="settings">The Settings model used for the simulation</param>
        /// <returns></returns>
        List<IArea> BuildHotel<T>(T file, SettingsModel settings);

        /// <summary>
        /// Creates a list of set movables fot the hotel
        /// This list is also responsable for creating the elevator
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="hotel"></param>
        /// <returns></returns>
        List<IMovable> BuildMovable(SettingsModel settings, Hotel hotel);
    }
}
