using Microsoft.VisualStudio.TestTools.UnitTesting;
using Octarines.Mars.Models.Enums;
using Octarines.Mars.Models.Results;
using Octarines.Mars.Models.Rovers;
using Octarines.Mars.Services.Rovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octarines.Mars.Services.UnitTests.Rovers
{
    [TestClass]
    public class RoversServiceUnitTests
    {
        private const int LOCATION_INITIAL_X = 0;
        private const int LOCATION_INITIAL_Y = 0;

        private RoversService _roverService;

        [TestInitialize]
        public void Initialize()
        {
            _roverService = new RoversService();
        }

        [TestMethod]
        public void MoveRover_ValidInstructionString_RoverBearingAndLocationUpdated()
        {
            // arrange
            Rover rover = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            Result<Rover> result = _roverService.MoveRover("MRMMLMLM", rover);

            // assert
            Assert.IsFalse(result.HasErrors);

            Assert.AreEqual(Bearing.West, rover.Bearing);
            Assert.AreEqual(2, rover.LocationY);
            Assert.AreEqual(1, rover.LocationX);
        }

        [TestMethod]
        public void MoveRover_InvalidCharactersInInstructionString_InvalidResult()
        {
            // arrange
            Rover rover = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            Result<Rover> result = _roverService.MoveRover("MRM MXMLMP", rover);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
            Assert.AreEqual(3, result.Errors.Count());
        }

        [TestMethod]
        public void MoveRover_EmptyInstructionString_InvalidResult()
        {
            // arrange
            Rover rover = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            Result<Rover> result = _roverService.MoveRover(string.Empty, rover);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
            Assert.AreEqual(1, result.Errors.Count());
        }

        [TestMethod]
        public void MoveRover_NullInstructionString_InvalidResult()
        {
            // arrange
            Rover rover = new Rover(LOCATION_INITIAL_X, LOCATION_INITIAL_Y, Bearing.North);

            // act
            Result<Rover> result = _roverService.MoveRover(null, rover);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
            Assert.AreEqual(1, result.Errors.Count());
        }

        [TestMethod]
        public void ParsePlateuaDetails_ValidCoordinatesString_CoordinatesObject()
        {
            // arrange
            string coordinatesString = "5 17";

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsFalse(result.HasErrors);

            Coordinates coordinates = result.Data;

            Assert.IsNotNull(coordinates);
            Assert.AreEqual(5, coordinates.X);
            Assert.AreEqual(17, coordinates.Y);
        }

        [TestMethod]
        public void ParsePlateuaDetails_InvalidCoordinatesNumnber_InvalidResult()
        {
            // arrange
            string coordinatesString = "5 17 9";

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Coordinates>);
        }

        [TestMethod]
        public void ParsePlateuaDetails_EmptyCoordinates_InvalidResult()
        {
            // arrange
            string coordinatesString = string.Empty;

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Coordinates>);
        }

        [TestMethod]
        public void ParsePlateuaDetails_NullCoordinates_InvalidResult()
        {
            // arrange
            string coordinatesString = string.Empty;

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Coordinates>);
        }

        [TestMethod]
        public void ParsePlateuaDetails_InvalidXCoordinateCharacter_InvalidResult()
        {
            // arrange
            string coordinatesString = "& 17";

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Coordinates>);
        }

        [TestMethod]
        public void ParsePlateuaDetails_InvalidYCoordinateCharacter_InvalidResult()
        {
            // arrange
            string coordinatesString = "5 #";

            // act
            Result<Coordinates> result = _roverService.ParsePlateuaDetails(coordinatesString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Coordinates>);
        }

        [TestMethod]
        public void InitializeNewRover_ValidPositionString_RoverObject()
        {
            // arrange
            string positionString = "8 17 E";

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsFalse(result.HasErrors);

            Rover rover = result.Data;

            Assert.IsNotNull(rover);
            Assert.AreEqual(8, rover.LocationX);
            Assert.AreEqual(17, rover.LocationY);
            Assert.AreEqual(Bearing.East, rover.Bearing);            
        }

        [TestMethod]
        public void InitializeNewRover_InvalidPositionArgumentNumber_InvalidResult()
        {
            // arrange
            string positionString = "8 7 E Z";

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void InitializeNewRover_EmptyPosition_InvalidResult()
        {
            // arrange
            string positionString = string.Empty;

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void InitializeNewRover_NullPosition_InvalidResult()
        {
            // arrange
            string positionString = null;

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void InitializeNewRover_InvalidXCoordinateCharacter_InvalidResult()
        {
            // arrange
            string positionString = "{ 7 E";

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void InitializeNewRover_InvalidYCoordinateCharacter_InvalidResult()
        {
            // arrange
            string positionString = "8 H E";

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void InitializeNewRover_InvalidBearingCharacter_InvalidResult()
        {
            // arrange
            string positionString = "8 7 Q";

            // act
            Result<Rover> result = _roverService.InitializeNewRover(positionString);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<Rover>);
        }

        [TestMethod]
        public void ExplorePlateau_ValidInstructionsOneRover_RoverPositionOutput()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "8 8",
                "3 6 S",
                "MMMLMMLMRMRM"
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsFalse(result.HasErrors);
            List<string> roverPositions = result.Data.ToList();

            Assert.IsNotNull(roverPositions);
            Assert.AreEqual(1, roverPositions.Count());
            Assert.AreEqual("6 3 S", roverPositions[0]);
        }

        [TestMethod]
        public void ExplorePlateau_ValidInstructionsFourRovers_RoverPositionOutput()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "6 6",
                "6 6 S",
                "MRMMLM",
                "1 4 E",
                "RMMRMLMMR",
                "4 3 E",
                "MMLMLMRMM",
                "2 5 W",
                "LMLMRMMMRRM"
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsFalse(result.HasErrors);
            List<string> roverPositions = result.Data.ToList();

            Assert.IsNotNull(roverPositions);
            Assert.AreEqual(4, roverPositions.Count());
            Assert.AreEqual("4 4 S", roverPositions[0]);
            Assert.AreEqual("0 0 W", roverPositions[1]);
            Assert.AreEqual("5 6 N", roverPositions[2]);
            Assert.AreEqual("3 2 N", roverPositions[3]);
        }

        [TestMethod]
        public void ExplorePlateau_InstructionNumberLessThanThree_InvalidResult()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "8 8",
                "3 6 S"
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<IEnumerable<string>>);
        }

        [TestMethod]
        public void ExplorePlateau_NullInstructions_InvalidResult()
        {
            // arrange
            List<string> instructions = null;

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<IEnumerable<string>>);
        }

        [TestMethod]
        public void ExplorePlateau_InstructionNumberEven_InvalidResult()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "6 6",
                "6 6 S",
                "MRMMLM",
                "1 4 E",
                "RMMRMLMMR",
                "4 3 E",
                "MMLMLMRMM",
                "2 5 W"
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<IEnumerable<string>>);
        }

        [TestMethod]
        public void ExplorePlateau_InstructionsNavigateOutOfPlateauSize_InvalidResult()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "6 6",
                "6 6 S",
                "LMM"
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsTrue(result.HasErrors);
            Assert.IsTrue(result is InvalidResult<IEnumerable<string>>);
        }

        [TestMethod]
        public void MarsRoverTechnicalChallengeTestData()
        {
            // arrange
            List<string> instructions = new List<string>()
            {
                "5 5",
                "1 2 N",
                "LMLMLMLMM",
                "3 3 E",
                "MMRMMRMRRM",
            };

            // act
            Result<IEnumerable<string>> result = _roverService.ExplorePlateau(instructions);

            // assert
            Assert.IsFalse(result.HasErrors);
            List<string> roverPositions = result.Data.ToList();

            Assert.IsNotNull(roverPositions);
            Assert.AreEqual(2, roverPositions.Count());
            Assert.AreEqual("1 3 N", roverPositions[0]);
            Assert.AreEqual("5 1 E", roverPositions[1]);
        }
    }
}
