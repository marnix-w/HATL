using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    [Export(typeof(AreaFactory))]
    class AreaFactory 
    {
        [ImportMany]
        IEnumerable<Lazy<IArea, IAreaData>> AreaTypes;
        
        public IArea GetArea(string typeOfArea)
        {
            foreach (Lazy<IArea, IAreaData> i in AreaTypes)
            {
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea();
            }

            //Error handeling
            Debug.WriteLine("Error there was no requested ruum");

            return null;

        }
      
    }
}
