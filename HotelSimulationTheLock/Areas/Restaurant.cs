using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock.Areas
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Restaurant")]
    class Restaurant : IArea
    {
        public IArea CreateArea()
        {
            return new Restaurant();
        }
    }
}
