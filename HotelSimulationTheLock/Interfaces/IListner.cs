using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // this also makes it posible to add more might things change in the future
    }
}
