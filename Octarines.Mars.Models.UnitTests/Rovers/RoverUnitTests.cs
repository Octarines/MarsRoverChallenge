using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octarines.Mars.Models.Enums;
using Octarines.Mars.Models.Rovers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octarines.Mars.Models.UnitTests.Rovers
{
    [TestClass]
    public class RoverUnitTests
    {
        private const int LOCATION_INITIAL_X = 0;
        private const int LOCATION_INITIAL_Y = 0;

        [TestMethod]
        public void TurnLeft_BearingNorth_BearingWest()
        {
            // arrange
            Rover roverNorth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            roverNorth.TurnLeft();

            // assert
            Assert.AreEqual(Bearing.West, roverNorth.Bearing);
        }

        [TestMethod]
        public void TurnLeft_BearingEast_BearingNorth()
        {
            // arrange
            Rover roverEast = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.East);

            // act
            roverEast.TurnLeft();

            // assert
            Assert.AreEqual(Bearing.North, roverEast.Bearing);
        }

        [TestMethod]
        public void TurnLeft_BearingSouth_BearingEast()
        {
            // arrange
            Rover roverSouth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.South);

            // act
            roverSouth.TurnLeft();

            // assert
            Assert.AreEqual(Bearing.East, roverSouth.Bearing);
        }

        [TestMethod]
        public void TurnLeft_BearingWest_BearingSouth()
        {
            // arrange
            Rover roverWest = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.West);

            // act
            roverWest.TurnLeft();

            // assert
            Assert.AreEqual(Bearing.South, roverWest.Bearing);
        }

        [TestMethod]
        public void TurnRight_BearingNorth_BearingEast()
        {
            // arrange
            Rover roverNorth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            roverNorth.TurnRight();

            // assert
            Assert.AreEqual(Bearing.East, roverNorth.Bearing);
        }

        [TestMethod]
        public void TurnRight_BearingEast_BearingSouth()
        {
            // arrange
            Rover roverEast = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.East);

            // act
            roverEast.TurnRight();

            // assert
            Assert.AreEqual(Bearing.South, roverEast.Bearing);
        }

        [TestMethod]
        public void TurnRight_BearingSouth_BearingWest()
        {
            // arrange
            Rover roverSouth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.South);

            // act
            roverSouth.TurnRight();

            // assert
            Assert.AreEqual(Bearing.West, roverSouth.Bearing);
        }

        [TestMethod]
        public void TurnRight_BearingWest_BearingNorth()
        {
            // arrange
            Rover roverWest = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.West);

            // act
            roverWest.TurnRight();

            // assert
            Assert.AreEqual(Bearing.North, roverWest.Bearing);
        }

        [TestMethod]
        public void MoveForward_BearingNorth_LocationYIncreased()
        {
            // arrange
            Rover roverNorth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            roverNorth.MoveForward();

            // assert
            Assert.AreEqual(roverNorth.LocationX, LOCATION_INITIAL_X);
            Assert.AreEqual(roverNorth.LocationY, LOCATION_INITIAL_Y + 1);
        }

        [TestMethod]
        public void MoveForward_BearingEast_LocationXIncreased()
        {
            // arrange
            Rover roverEast = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.East);

            // act
            roverEast.MoveForward();

            // assert
            Assert.AreEqual(roverEast.LocationX, LOCATION_INITIAL_X + 1);
            Assert.AreEqual(roverEast.LocationY, LOCATION_INITIAL_Y);
        }

        [TestMethod]
        public void MoveForward_BearingSouth_LocationXDecreased()
        {
            // arrange
            Rover roverSouth = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.South);

            // act
            roverSouth.MoveForward();

            // assert
            Assert.AreEqual(roverSouth.LocationX, LOCATION_INITIAL_X);
            Assert.AreEqual(roverSouth.LocationY, LOCATION_INITIAL_Y - 1);
        }

        [TestMethod]
        public void MoveForward_BearingWest_LocationXDecreased()
        {
            // arrange
            Rover roverWest = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.West);

            // act
            roverWest.MoveForward();

            // assert
            Assert.AreEqual(roverWest.LocationX, LOCATION_INITIAL_X - 1);
            Assert.AreEqual(roverWest.LocationY, LOCATION_INITIAL_Y);
        }
    }
}
