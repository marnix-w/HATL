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
    public class AreaFactory 
    {
        [ImportMany]
        IEnumerable<Lazy<IArea, IAreaData>> AreaTypes;
        

        public IArea GetArea(string typeOfArea, Point position, int capacity, Point dimension, int clasification)
        {
            foreach (Lazy<IArea, IAreaData> i in AreaTypes)
            {
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea(position, capacity, dimension, clasification);
            }

            //Error handeling
            Debug.WriteLine("Error there was no requested ruum");

            return null;

        }
      
    }
}
