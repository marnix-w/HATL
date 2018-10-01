using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Restaurant")]
    public class Restaurant : IArea
    {
        public IArea CreateArea()
        {
            return new Restaurant();
        }
    }
}
