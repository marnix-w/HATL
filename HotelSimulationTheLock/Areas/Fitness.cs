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
    [ExportMetadata("AreaType", "Fitness")]
    public class Fitness : IArea
    {
        public IArea CreateArea()
        {
            return new Fitness();
        }
    }
}
