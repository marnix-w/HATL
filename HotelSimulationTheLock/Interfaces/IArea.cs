using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;
using Newtonsoft.Json;

namespace HotelSimulationTheLock
{
    public interface IArea
    {
        IArea CreateArea();
    }

}
