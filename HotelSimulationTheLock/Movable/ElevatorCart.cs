using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulationTheLock
{
    public class ElevatorCart : IMovable
    {
        /// <summary>
        /// it's current area
        /// </summary>
        public IArea Area { get; set; }
        /// <summary>
        /// the elevatorCart is part of Hotel
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// amount of capacity of the Elevator (while during the meetings no end capacity)
        /// </summary>
        public int Capacity { get; set; }

        // Iarea information
        #region
        public Point Position { get; set; }

        public Bitmap Art { get; set; } = Properties.Resources.elevator_pressent;

        public MovableStatus Status { get; set; }

        public Dictionary<MovableStatus, Action> Actions { get; set; }
        #endregion
               
        /// <summary>
        ///  Since we cannot remove a Guest during the threat we need to make a second second list in order to remove
        /// the guest in the next loop
        /// </summary>
        List<IMovable> RemoveGuests = new List<IMovable>();
        /// <summary>
        /// all the request from calling the elevator for the first time will be put into this requestlist
        /// </summary>
        public List<IMovable> RequestList { get; set; } = new List<IMovable>();
        /// <summary>
        /// once they are in the Request list we remove them from the requestlist and it goes into the guest list 
        /// this is because the elevator knows who is inside the elevator
        /// </summary>
        public List<IMovable> GuestList { get; set; } = new List<IMovable>();
        /// <summary>
        /// final destination point of the guest
        /// </summary>
        public IArea FinalDes { get; set; }


        //'U' for UP, 'D' for DOWN, 'I' for IDLE
        /// <summary>
        ///  we need to compare these lists with eachother in order to have an 'smart' elevator
        ///  if up list is bigger than down elevatorCart goes up
        ///  if uplist is smaller than the down list elevatorCart goes down
        /// </summary>
        public List<int> Up = new List<int>();
        public List<int> Down = new List<int>();

        /// <summary>
        /// Sets the parameter from the builder file and the jsonfile
        /// </summary>
        /// <param name="position"></param>
        /// <param name="hotel"></param>
        /// <param name="capacity"></param>
        public ElevatorCart(Point position, Hotel hotel, int capacity)
        {
            Hotel = hotel;        
            Capacity = capacity;
            Status = MovableStatus.IDLE;
            Position = position;        
            Area = Hotel.GetArea(Position);

            ((Elevator)Area).ElevatorCart = this;
        }

        /// <summary>
        /// Each time an PerformAction is called we check what Status the elevator 
        /// is in and we are removing the guests from the RemoveGuest list
        /// Because we have a "Smart elevator" the elevator will check wich list is bigger and then handle the biggest list first untill it's empty
        /// which means that the elevator will always make the uplist smaller and if the list is equal to zero the elevator will go down
        /// 
        /// this is just like in reallife if there are 3 people, each person is on a different floor 
        /// and lets say guestA presses the button first and the elevator is also on floor 3 now 
        /// GuestA,B wants to go down but guestC wants to go up. 
        /// 
        /// which means our Up list is filled with guestC and our Down list is filled with guestA and B 
        /// this will lead to that our down list is bigger and the lift will drop first guestA and B to the requested floor and will then pick up guestC
        /// this elevator will not jump up and down
        /// floor: 5
        /// floor: 4 guestC         
        /// floor: 3 guestA       elevator
        /// floor: 2 guestB          
        /// floor: 1 
        /// </summary>
        public void PerformAction()
        {
            //We wanted to use a foreach loop but because of the threading we had to do a forloop
            //difference between foreach and forloop is that a foreach will crash when something is changed during the simulation and forloop can adjust
            RemoveGuests.Clear();
            for (int i = 0; i < GuestList.Count; i++)
            {
                if (GuestList[i] is IMovable g)
                {
                    if (Position.Y == g.FinalDes.Position.Y)
                    {
                        g.Status = MovableStatus.LEAVING_ELEVATOR;
                        g.Area = Hotel.GetArea(Position);
                        RemoveGuests.Add(GuestList[i]);
                    }
                }
            }
            //second forloop to remove the guest in order to not have an error
            for (int i = 0; i < RemoveGuests.Count; i++)
            {
                GuestList.Remove(RemoveGuests[i]);
            }
            RemoveGuests.Clear();

            //removing Down list items
            if (Down.Count != 0 && Down[0] == Position.Y)
            {
                Down.RemoveAt(0);
            }
            //removing Up list items
            if (Up.Count != 0 && Up[0] == Position.Y)
            {
                Up.RemoveAt(0);
            }

            //if the status is up we perform the method _elevatorGoesup();
            if (Status == MovableStatus.UP)
            {
                _elevatorGoesUp();
            }
            //if the status is up we perform the method _elevatorGoesDown() with an extra check that the elevator will not leave the field.
            else if (Status == MovableStatus.DOWN && Position.Y < Hotel.HotelHeight)
            {

                _elevatorGoesDown();
            }
            else
            {
                Status = MovableStatus.IDLE;
            }
            //in order to draw the guests correctly on the field we change the position to the elevator position
            foreach (IMovable human in GuestList)
            {
                human.Position = Position;
            }
            AddDestinationFloor();
          
            //if both list are empty we are changing the elevatorCart status and the elevator will go down
            if (Up.Count == 0 && Down.Count == 0)
            {
                Status = MovableStatus.IDLE;
            }

            if (Status == MovableStatus.IDLE)
            {
                _ElevatorDoesNothing();
            }
        }


        public void RequestElevator(IMovable RequestFloor, int height)

        {
            //Extra Check
            //Check for Current floor

            if (RequestFloor.FinalDes.Position.Y <= height && RequestFloor.FinalDes.Position.Y >= 0)
            {
                RequestList.Add(RequestFloor);
                //Goes UP
                if (RequestFloor.Position.Y < Position.Y)
                {
                    Up.Add(RequestFloor.Position.Y);
                    UpdateList();
                }
                //Goes DOWN
                else
                {
                    Down.Add(RequestFloor.Position.Y);
                    UpdateList();
                }
            }

        }
        private void _elevatorGoesUp()
        {
            //if for some reason the Down list is bigger we change the status and elevator will go down instead of going up
            if (Up.Count == 0 && Down.Count != 0)
            {
                this.Status = MovableStatus.DOWN;
                PerformAction();
            }
            else
            {
                Position = new Point(Position.X, Position.Y - 1);
                Area = Hotel.GetArea(Position);

                for (int i = 0; i < Up.Count; i++)
                {
                    if (Up[i] == Position.Y)
                    {
                        Up.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// The elevator will always go down by 1 aslong the downlist is filled
        /// </summary>
        private void _elevatorGoesDown()
        {
            //extra check if the uplist is suddenly bigger than the downlist we change the status
            if (Up.Count != 0 && Down.Count == 0)
            {
                this.Status = MovableStatus.UP;
                PerformAction();
            }
            else
            {
                Position = new Point(Position.X, Position.Y + 1);
                Area = Hotel.GetArea(Position);

                for (int i = 0; i < Down.Count; i++)
                {
                    if (Down[i] == Position.Y)
                    {
                        Down.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// If the status is Idle we still want the elevator to be smart enough to check the up and down list
        /// while we haven'_timer asked if the elevator need to stay on the same floor is the status is Idle we wanted to put the 
        /// elevator always down when no actions are performed just like in real life
        /// </summary>
        private void _ElevatorDoesNothing()
        {
            if (Up.Count > Down.Count)
            {
                Status = MovableStatus.UP;
            }
            else
            {
                Status = MovableStatus.DOWN;
            }
        }
        /// <summary>
        /// When a Imoveable is  making a request we are adding it in the request list
        /// in this case if the maid or guest want to use the elevator they will be added to the list and 
        /// the status will be changed
        /// </summary>
        public void AddDestinationFloor()
        {
            //same issues as in perform action since we acannot use a foreach loop because of threading issues we 
            //are using a forloop to counter it.
            //this is how we can remove guest without breaking the application. If we had more time we wanted to implement it differently
            for (int i = 0; i < RequestList.Count; i++)
            {

                if(RequestList[i] is IMovable g)
                {
                    if (g.Position.Y == Position.Y)
                    {
                        GuestList.Add(RequestList[i]);
                        if (g.FinalDes.Position.Y < Position.Y)
                        {
                            Up.Add(g.FinalDes.Position.Y);
                            UpdateList();
                        }
                        else
                        {
                            Down.Add(g.FinalDes.Position.Y);
                            UpdateList();
                        }
                        try
                        {
                            GuestList[i].Status = MovableStatus.IN_ELEVATOR;
                            RemoveGuests.Add(GuestList[i]);
                        }
                        catch (Exception)
                        {
                            
                        }

                    }
                }

            }
            for (int i = 0; i < RemoveGuests.Count; i++)
            {
                if (RemoveGuests[i] is IMovable g)
                {
                    RequestList.Remove(RemoveGuests[i]);
                }
            }
        }

        /// <summary>
        /// We are re-ordering the list for our smart elevator because
        /// there can be alot of request with different kind of end destinations so for
        /// example we have 5 requests and when they are thrown in the list it could be something like
        /// { 5, 6, 1, 3, 2}
        /// while the elevator is on floor 7. We don'_timer want our elevator to jump from floor 7 to floor 1 back to 5 we want to update the list into 
        /// a reasonable order so
        /// { 6, 5, 3, 2, 1 } would be the proper way to handle the request and will let our elevator be a "smart elevator"
        /// </summary>
        private void UpdateList()
        {
            Up = Up.Distinct().OrderByDescending(x => x).ToList();
            Down = Down.Distinct().OrderBy(x => x).ToList();
        }

        public void SetPath(IArea destination)
        {
            // throw new NotImplementedException();
        }


    }
}
