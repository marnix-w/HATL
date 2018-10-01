using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.Composition;
=======
using System.Drawing;
>>>>>>> origin/hotel-startup-sprint-2
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;

namespace HotelSimulationTheLock
{
    [Export(typeof(IArea))]
    [ExportMetadata("AreaType", "Staircase")]
    public class Staircase : IArea
    {
<<<<<<< HEAD
        public IArea CreateArea()
        {
            return new Staircase();
        }
=======
        public string AreaType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Point Dimension { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Capacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Image Art { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ArtWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ArtHeight { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Enum Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
>>>>>>> origin/hotel-startup-sprint-2
    }
}
