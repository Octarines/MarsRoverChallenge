using Octarines.Mars.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octarines.Mars.Models.Rovers
{
    /// <summary>
    /// Mars plateau exploration vehicle
    /// </summary>
    public class Rover
    {
        /// <summary>
        /// X coordinate of the rover within the martian plateau
        /// </summary>
        public int LocationX { get; set; }

        /// <summary>
        /// Y coordinate of the rover within the martian plateau
        /// </summary>
        public int LocationY { get; set; }

        /// <summary>
        /// Direction that the rover is facing based on the 4 cardinal compass values.
        /// </summary>
        public Bearing Bearing { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locationX">Initial X location of rover within the martial plateau</param>
        /// <param name="locationY">Initial Y location of rover within the martial plateau</param>
        /// <param name="bearing">Initial direction that the rover is facing</param>
        public Rover(int locationX, int locationY, Bearing bearing)
        {
            LocationX = locationX;
            LocationY = locationY;
            Bearing = bearing;
        }

        /// <summary>
        /// Update the value of Bearing to the cardinal point 90 degrees anticlockwise (-90 degrees)
        /// </summary>
        public void TurnLeft()
        {
            int newBearing = (int)Bearing - 90;
            Bearing = newBearing >= 0 ? (Bearing)newBearing : (Bearing)(newBearing + 360);
        }

        /// <summary>
        /// Update the value of Bearing to the cardinal point 90 degrees clockwise (90 degrees)
        /// </summary>
        public void TurnRight()
        {
            int newBearing = (int)Bearing + 90;
            Bearing = newBearing < 360 ? (Bearing)newBearing : (Bearing)(newBearing - 360);
        }

        /// <summary>
        /// Update the values of LocationX and LocationY, dependent on Bearing, such that the rover moves forward 1 grid point.
        /// </summary>
        public void MoveForward()
        {
            switch (Bearing)
            {
                case Bearing.North:
                    LocationY++;
                    break;
                case Bearing.East:
                    LocationX++;
                    break;
                case Bearing.South:
                    LocationY--;
                    break;
                case Bearing.West:
                    LocationX--;
                    break;
            }
        }
    }
}
