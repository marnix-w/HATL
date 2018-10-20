using System.Collections.Generic;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// An varation on the implemtation of dijkstra
    /// </summary>
    public static class Dijkstra
    {
        // This dijkstra is taken from my DSALG project
        // And alterted to work here
        // on some points its not optimal and incomplete
        // we are still missing a reavaluation for calling the elevator
        // this meight be added in the future

        // I made this a static class to make sure it works everywhere and from everywhere
        // this was helpfull during develpment but might not be very pretty

        /// <summary>
        /// The hotel area list
        /// </summary>
        private static List<IArea> Areas { get; set; }

        /// <summary>
        /// The hotel its working on
        /// </summary>
        private static Hotel Hotel { get; set; }

        /// <summary>
        /// Sets the properties to use dijkstra
        /// </summary>
        /// <param name="hotel"></param>
        public static void IntilazeDijkstra(Hotel hotel)
        {
            Hotel = hotel;
            Areas = Hotel.HotelAreas;
        }

        /// <summary>
        /// Creates a list of areas that is the shortest path it could find
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<IArea> GetShortestPathDijkstra(IArea from, IArea to)
        {
            // setting the dijkstra variables
            SetDijkstraSearchValues(from, to);

            // building the path
            var shortestPath = new List<IArea>
            {
                to
            };

            BuildShortestPath(shortestPath, to);
            shortestPath.Reverse();

            // reseting the varables
            Hotel.RemoveSearchProperties();
            Areas = Hotel.HotelAreas;

            // returning the path
            return shortestPath;
        }

        /// <summary>
        /// <para>An not optimal way of deciding the elevevator is faster</para>
        /// <para>Big 0 Natation: 0(n)</para>
        /// </summary>
        /// <param name="from">From area</param>
        /// <param name="to">to Area</param>
        /// <returns>Returns the elevator or to depending on what is closer</returns>
        public static IArea IsElevatorCloser(IArea from, IArea to)
        {
            Elevator ev = (Elevator)Areas.Find(X => X.Position.Y == from.Position.Y && X is Elevator);

            if (ev.Position.Y == to.Position.Y)
            {
                return to;
            }

            int dictanceWithStairs = 0;
            int dictanceWithElevator = 0;

            // Calculating time to walk or take elevator
            #region
            // this part is not completetly true but it works for now
            // it should count the backtrack wieghts
            // This is for the future to change

            // - 1 for the deque of the first area
            dictanceWithStairs += GetShortestPathDijkstra(from, to).Count - 1;     
            dictanceWithElevator += GetShortestPathDijkstra(from, ev).Count - 1;
 
            if (ev.Position.Y > to.Position.Y)
            {
                dictanceWithElevator += ev.Position.Y - to.Position.Y;
            }
            else
            {
                dictanceWithElevator += to.Position.Y - ev.Position.Y;
            }
            
            // Adding the dictance it has to walk from elevator to room
            dictanceWithElevator += GetShortestPathDijkstra((Elevator)Areas.Find(X => X.Position.Y == to.Position.Y && X is Elevator), to).Count() - 1;
            #endregion

            // Movables will favor the elevator over the stairs if the dictance is the same
            if (dictanceWithStairs >= dictanceWithElevator)
            {
                return ev;
            }

            return to;
        }

        /// <summary>
        /// Builds the shortest path based on the Area that is neaest to start
        /// this is a recursive method that keeps buidling till its done and returns the list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="node"></param>
        private static void BuildShortestPath(List<IArea> list, IArea node)
        {
            if (node.NearestToStart == null)
            {
                return;
            }

            list.Add(node.NearestToStart);
            BuildShortestPath(list, node.NearestToStart);
        }

        /// <summary>
        /// Searching for the shortest path
        /// Big O natation: O(n)
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private static void SetDijkstraSearchValues(IArea from, IArea to)
        { 
            from.BackTrackCost = 0;

            List<IArea> toVisit = new List<IArea>
            {
                from
            };

            // looping till it finds its destatnation
            // and looked trugh the paths if a shorter one is achavible
            do
            {
                toVisit = toVisit.OrderBy(x => x.BackTrackCost.Value).ToList();

                IArea current = toVisit.First();

                toVisit.Remove(current);

                // Trying all the edges to find the shortest one
                foreach (var edge in current.Edge.OrderBy(x => x.Value))
                {
                    IArea childNode = edge.Key;

                    // Skip viseted nodes
                    if (childNode.Visited)
                    {
                        continue;
                    }
                    // checking if the edge is backtrackcost is less then it was
                    if (childNode.BackTrackCost == null ||
                        current.BackTrackCost + edge.Value < childNode.BackTrackCost)
                    {
                        // setting the new closests node
                        childNode.BackTrackCost = current.BackTrackCost + edge.Value;
                        childNode.NearestToStart = current;
                        
                        // looking for a shorter path back
                        if (!toVisit.Contains(childNode))
                        {
                            // adding a new to visiti node
                            toVisit.Add(childNode);
                        }
                    }
                }

                // setting the current node as visited
                current.Visited = true;

                // if the current no is the destination it will end
                if (current == to)
                {
                    break;
                }

            }
            while (toVisit.Any());
        }
        
    }
}
