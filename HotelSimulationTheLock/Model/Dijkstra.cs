using System.Collections.Generic;
using System.Linq;

namespace HotelSimulationTheLock
{
    /// <summary>
    /// A variation on the implementation of dijkstra
    /// </summary>
    public static class Dijkstra
    {
        // This dijkstra is taken from my DSALG project
        // and alterted to work here
        // on some points its not optimal and incomplete
        // we are still missing a reavaluation for calling the elevator
        // this might be added in the future

        // I made this a static class to make sure it works everywhere and from everywhere
        // this was helpfull during development but might not be very pretty

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
        /// <param name="hotel">The current hotel</param>
        public static void IntilazeDijkstra(Hotel hotel)
        {
            Hotel = hotel;
            Areas = Hotel.HotelAreas;
        }

        /// <summary>
        /// Creates a list of areas that make up the shortest path it could find
        /// </summary>
        /// <param name="from">The area the movable is comming form</param>
        /// <param name="to">The area the movable wants to go to</param>
        /// <returns></returns>
        public static List<IArea> GetShortestPathDijkstra(IArea from, IArea to)
        {
            // Setting the dijkstra variables
            SetDijkstraSearchValues(from, to);

            // Building the path
            var shortestPath = new List<IArea>
            {
                to
            };

            BuildShortestPath(shortestPath, to);
            shortestPath.Reverse();

            // Resetting the variables
            Hotel.RemoveSearchProperties();
            Areas = Hotel.HotelAreas;

            // Returning the path
            return shortestPath;
        }

        /// <summary>
        /// <para>A not optimal way of deciding whether the elevevator is faster</para>
        /// <para>Big 0 notation: 0(n)</para>
        /// </summary>
        /// <param name="from">From area</param>
        /// <param name="to">To Area</param>
        /// <returns>Returns the elevator or the To Area, depending on what is closer</returns>
        public static IArea IsElevatorCloser(IArea from, IArea to)
        {
            Elevator ev = (Elevator)Areas.Find(X => X.Position.Y == from.Position.Y && X is Elevator);

            if (ev.Position.Y == to.Position.Y)
            {
                return to;
            }

            int dictanceWithStairs = 0;
            int dictanceWithElevator = 0;

            // Calculating the time to walk or take the elevator
            #region
            // this part is not completely true but it works for now
            // it should count the backtrack weights
            // This is for the future to change

            // - 1 for the dequeue of the first area
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
            
            // Adding the distance it has to walk from elevator to room
            dictanceWithElevator += GetShortestPathDijkstra((Elevator)Areas.Find(X => X.Position.Y == to.Position.Y && X is Elevator), to).Count() - 1;
            #endregion

            // Movables will favor the elevator over the stairs if the distance is the same
            if (dictanceWithStairs >= dictanceWithElevator)
            {
                return ev;
            }

            return to;
        }

        /// <summary>
        /// Builds the shortest path based on the Area that is nearest to start
        /// this is a recursive method that keeps buidling till its done and returns the list
        /// </summary>
        /// <param name="list">The list of IAreas</param>
        /// <param name="node">The current node of the path</param>
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
        /// Big O notation: O(n)
        /// </summary>
        /// <param name="from">The IArea the movable is comming from</param>
        /// <param name="to">The IArea the movable wants to go to</param>
        private static void SetDijkstraSearchValues(IArea from, IArea to)
        { 
            from.BackTrackCost = 0;

            List<IArea> toVisit = new List<IArea>
            {
                from
            };

            // Looping till it finds its destination
            // and looked through the paths if a shorter one is available
            do
            {
                toVisit = toVisit.OrderBy(x => x.BackTrackCost.Value).ToList();

                IArea current = toVisit.First();

                toVisit.Remove(current);

                // Trying all the edges to find the shortest one
                foreach (var edge in current.Edge.OrderBy(x => x.Value))
                {
                    IArea childNode = edge.Key;

                    // Skip visited nodes
                    if (childNode.Visited)
                    {
                        continue;
                    }
                    // Checking if the edge's backtrackcost is less then it was
                    if (childNode.BackTrackCost == null ||
                        current.BackTrackCost + edge.Value < childNode.BackTrackCost)
                    {
                        // Setting the new closest node
                        childNode.BackTrackCost = current.BackTrackCost + edge.Value;
                        childNode.NearestToStart = current;
                        
                        // Looking for a shorter path back
                        if (!toVisit.Contains(childNode))
                        {
                            // Adding a new toVisit node
                            toVisit.Add(childNode);
                        }
                    }
                }

                // Setting the current node as visited
                current.Visited = true;

                // If the current node is the destination it will end
                if (current == to)
                {
                    break;
                }

            }
            while (toVisit.Any());
        }
    }
}
