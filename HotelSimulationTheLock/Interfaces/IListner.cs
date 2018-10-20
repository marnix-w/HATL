using HotelEvents;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// This small varation on a adaptopr
    /// </summary>
    public interface IListner : HotelEventListener
    {
        // tihs makes sure that when the external DLL changes
        // we only need to change the name in 1 place
        // Might not be the cleanest solution
    }
}
