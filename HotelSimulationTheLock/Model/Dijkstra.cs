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

        public static void SetList(List<IArea> areas)
        {
            Areas = areas;
        }

        public static void IntilazeDijkstra(Hotel hotel, List<IArea> hotelAreaList)
        {
            Hotel = hotel;
            Areas = hotelAreaList;
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
            Areas = Hotel.GetAreas();
            return shortestPath;
        }

        public static IArea IsElevatorCloser(IArea from, IArea to)
        {
            IArea ev = Areas.Find(X => X.Position.Y == from.Position.Y && X is Elevator);

            if (GetShortestPathDijkstra(from, to).Count > GetShortestPathDijkstra(from, ev).Count && ((Elevator)ev).ElevatorCart != null)
            {               
                return ev;
            }

            return to;
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

            } while (toVisit.Any());
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
