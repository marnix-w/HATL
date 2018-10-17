using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public static class Dijkstra
    {
        private static List<IArea> Areas { get; set; }

        private static Hotel Hotel { get; set; }

        public static void IntilazeDijkstra(Hotel hotel)
        {
            Hotel = hotel;
            Areas = Hotel.HotelAreas;
        }

        public static List<IArea> GetShortestPathDijkstra(IArea from, IArea to)
        {
            SetDijkstraSearchValues(from, to);

            var shortestPath = new List<IArea>
            {
                to
            };

            BuildShortestPath(shortestPath, to);
            shortestPath.Reverse();
            Hotel.RemoveSearchProperties();
            Areas = Hotel.HotelAreas;
            return shortestPath;
        }

        public static IArea IsElevatorCloser(IArea from, IArea to)
        {
            Elevator ev = (Elevator)Areas.Find(X => X.Position.Y == from.Position.Y && X is Elevator);

            if (ev.Position.Y == to.Position.Y)
            {
                return to;
            }

            int dictanceWithStairs = 0;
            int dictanceWithElevator = 0;

            
            dictanceWithStairs += GetShortestPathDijkstra(from, to).Count - 1;
            
            dictanceWithElevator += GetShortestPathDijkstra(from, ev).Count - 1;

            // This takes the best case cenerio to make the decision to go with the lift
            // a re evaluation will be made when the movable is at the elevator 
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

            // Movables will favor the elevator over the stairs if the dictance is the same
            if (dictanceWithStairs >= dictanceWithElevator &&
                ev.ElevatorCart != null)
            {
                return ev;
            }

            return to;
        }

        public static IArea CouldTheElevatorBeFaster(IArea from, IArea to)
        {
            if (!(from is Elevator))
            {

            }

            return null;
        }

        private static void BuildShortestPath(List<IArea> list, IArea node)
        {
            if (node.NearestToStart == null)
            {
                return;
            }

            list.Add(node.NearestToStart);
            BuildShortestPath(list, node.NearestToStart);
        }

        private static void SetDijkstraSearchValues(IArea from, IArea to)
        { 
            from.BackTrackCost = 0;

            List<IArea> toVisit = new List<IArea>
            {
                from
            };

            do
            {
                toVisit = toVisit.OrderBy(x => x.BackTrackCost.Value).ToList();

                IArea current = toVisit.First();

                toVisit.Remove(current);

                foreach (var edge in current.Edge.OrderBy(x => x.Value))
                {
                    IArea childNode = edge.Key;

                    if (childNode.Visited)
                    {
                        continue;
                    }
                    if (childNode.BackTrackCost == null ||
                        current.BackTrackCost + edge.Value < childNode.BackTrackCost)
                    {
                        childNode.BackTrackCost = current.BackTrackCost + edge.Value;
                        childNode.NearestToStart = current;
                        if (!toVisit.Contains(childNode))
                        {
                            toVisit.Add(childNode);
                        }
                    }
                }

                current.Visited = true;

                if (current == to)
                {
                    break;
                }

            }
            while (toVisit.Any());
        }

        public static bool DoesPathExistFunction(IArea from, IArea to)
        {

            List<IArea> visited = new List<IArea>();

            if (from.Edge.ContainsKey(to))
            {
                return true;
            }

            Queue<IArea> queue = new Queue<IArea>();
            queue.Enqueue(from);

            while (queue.Count > 0)
            {
                IArea Current = queue.Dequeue();

                if (visited.Contains(Current))
                    continue;

                visited.Add(Current);

                foreach (IArea neighbor in Current.Edge.Keys)
                {
                    if (!visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                    }
                    if (neighbor.Equals(to))
                    {
                        return true;
                    }
                }

            }

            return false;
        }
    }
}
