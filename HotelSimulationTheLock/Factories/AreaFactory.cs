using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An implementation of a combenation of a Factory design pattern and a bit of the MEF framework
    /// </summary>
    public class AreaFactory
    {
        // For this project we used a special variation on the Design pattern factory
        // this variation makes the simulation MODULAIR
        // it implements the MEF framework to capture new types of areas and can directly work with them
        // if an area will be added they have to implement the IArea interface and by filling in the create area method 
        // it is compatable with the program. this way it is posible to add loads of new area's without having to deal with 
        // implementation in the program

        // the container in wich the composition will be held
        private CompositionContainer _container;

        [ImportMany]
        IEnumerable<Lazy<IArea, IAreaType>> AreaTypes;

        /// <summary>
        /// When a factory is made it will check the function for what rooms it can make
        /// </summary>
        public AreaFactory()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            // In development of this factory an error occured when we added an external DLL area that didnt fully implement IArea correctly 
            // This caused a mayor error in the project. after searching the web fot a fix we stumbled upon a stack overflow thread that solved our problem
            // https://stackoverflow.com/questions/4144683/handle-reflectiontypeloadexception-during-mef-composition
            // instead of reading all DLL files in at the same time. we now do each one induvidualy. then we force faulty DLL's to crash and catch the exception
            // en exclude them from the project.

            var files = Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"..\..\..\Extended_Areas", "*.dll", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                AssemblyCatalog newArea = new AssemblyCatalog(file);

                try
                {
                    // Forcing faulty DLL's to crash
                    // No idea why this causes them to crash this way
                    // would like to know
                    newArea.Parts.ToArray();
                }
                catch (System.Reflection.ReflectionTypeLoadException) // catching faulty DLL's
                {
                    // The given DLL does not implement IArea correctly please notify the creator
                    continue;
                }
                catalog.Catalogs.Add(newArea);
            }

            //Create the CompositionContainer with the parts in the catalog
            _container = new CompositionContainer(catalog);

            //Fill the imports of this object                     
            _container.ComposeParts(this);
        }

        /// <summary>
        /// The factory method
        /// </summary>
        /// <param name="typeOfArea">A AreaType wich corisponds with the Area's exported metadata</param>
        /// <returns></returns>
        public IArea GetArea(string typeOfArea)
        {
            foreach (Lazy<IArea, IAreaType> i in AreaTypes)
            {
                if (i.Metadata.AreaType.Equals(typeOfArea)) return i.Value.CreateArea();
            }

            //Error handeling TO DO
            Debug.WriteLine("Error there was no requested room");

            return null;

        }

    }
}