using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{  
    public class AreaFactory 
    {
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<Lazy<IArea, IAreaType>> AreaTypes;

        public AreaFactory()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            // this is some black magicx
            // https://stackoverflow.com/questions/4144683/handle-reflectiontypeloadexception-during-mef-composition

            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"..\..\..\Extended_Areas", "*.dll", SearchOption.AllDirectories);

            foreach (var item in files)
            {
                AssemblyCatalog a = new AssemblyCatalog(item);
                
                try
                {
                    a.Parts.ToArray();
                }
                catch (System.Reflection.ReflectionTypeLoadException)
                {
                    continue;
                }

                catalog.Catalogs.Add(a);

            }
            
            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object                     
            _container.ComposeParts(this);
            
            
        }

        public IArea GetArea(string typeOfArea, Point position, int capacity, Point dimension, int clasification)
        {
            foreach (Lazy<IArea, IAreaType> i in AreaTypes)
            {
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea(position, capacity, dimension, clasification);
            }

            //Error handeling
            Debug.WriteLine("Error there was no requested ruum");

            return null;

        }
      
    }
}
