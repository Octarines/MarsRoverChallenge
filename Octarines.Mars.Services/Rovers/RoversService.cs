using Octarines.Mars.Models.Enums;
using Octarines.Mars.Models.Resources;
using Octarines.Mars.Models.Results;
using Octarines.Mars.Models.Rovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octarines.Mars.Services.Rovers
{
    public class RoversService: IRoversService
    {        
        public Result<IEnumerable<string>> ExplorePlateau(IEnumerable<string> instructions)
        {
            List<string> roverFinalPositions = new List<string>();
            List<string> instructionsList = instructions.ToList();

            //validate that instructions has at least 3 lines (one for plateau size and two for at least one rover) and an odd number after that (2 more per rover)
            if(instructionsList.Count() < 3 || instructionsList.Count() % 2 == 0)
            {
                return new InvalidResult<IEnumerable<string>>(ValidationErrors.RoverInstructionsLines);
            }

            Result<Coordinates> plateauResult = ParsePlateuaDetails(instructionsList[0]);
            if (plateauResult.HasErrors)
            {
                return new InvalidResult<IEnumerable<string>>(plateauResult.Errors);
            }

            Coordinates plateauSize = plateauResult.Data;

            //iterate two rows at a time after the first. 2 rows per rover (position and instructions)
            for (int i = 1; i < instructions.Count(); i += 2)
            {
                Result<Rover> roverResult = InitializeNewRover(instructionsList[i]);
                if (roverResult.HasErrors)
                {
                    return new InvalidResult<IEnumerable<string>>(roverResult.Errors);
                }

                Result<Rover> moveResult = MoveRover(instructionsList[i + 1], roverResult.Data);
                if (moveResult.HasErrors)
                {
                    return new InvalidResult<IEnumerable<string>>(moveResult.Errors);
                }
                Rover movedRover = moveResult.Data;

                bool roverWithinPlateauX = movedRover.LocationX <= plateauSize.X && movedRover.LocationX >= 0;
                bool roverWithinPlateauY = movedRover.LocationY <= plateauSize.Y && movedRover.LocationY >= 0;

                if(!roverWithinPlateauX || !roverWithinPlateauY)
                {
                    return new InvalidResult<IEnumerable<string>>(string.Format(ValidationErrors.RoverInstructionsOutOfArea, instructionsList[i]));
                }

                roverFinalPositions.Add($"{movedRover.LocationX} {movedRover.LocationY} {movedRover.Bearing.ToString().Substring(0, 1)}");
            }

            return new SuccessResult<IEnumerable<string>>(roverFinalPositions);
        }        
                
        public Result<Coordinates> ParsePlateuaDetails(string size)
        {
            int sizeX;
            int sizeY;

            if (string.IsNullOrEmpty(size))
            {
                return new InvalidResult<Coordinates>(ValidationErrors.PlateauSizeNullOrEmpty);
            }

            string[] plateauSize = size.Split(" ");

            if(plateauSize.Count() != 2)
            {
                return new InvalidResult<Coordinates>(ValidationErrors.PlateauSizeNumber);
            }

            if(!int.TryParse(plateauSize[0], out sizeX))
            {
                return new InvalidResult<Coordinates>(string.Format(ValidationErrors.PlateauSizeXChar,plateauSize[0]));
            }

            if (!int.TryParse(plateauSize[1], out sizeY))
            {
                return new InvalidResult<Coordinates>(string.Format(ValidationErrors.PlateauSizeYChar, plateauSize[1]));
            }

            return new SuccessResult<Coordinates>(new Coordinates() { X = sizeX, Y = sizeY });
        }

        public Result<Rover> InitializeNewRover(string position)
        {
            int positionX = 0;
            int positionY = 0;
            Bearing? bearing;

            if (string.IsNullOrEmpty(position))
            {
                return new InvalidResult<Rover>(ValidationErrors.RoverPositionNullOrEmpty);
            }

            string[] roverPosition = position.Split(" ");

            if (roverPosition.Count() != 3)
            {
                return new InvalidResult<Rover>(ValidationErrors.RoverPositionNumber);
            }

            if (!int.TryParse(roverPosition[0], out positionX))
            {
                return new InvalidResult<Rover>(string.Format(ValidationErrors.RoverPositionXChar, roverPosition[0]));
            }

            if (!int.TryParse(roverPosition[1], out positionY))
            {
                return new InvalidResult<Rover>(string.Format(ValidationErrors.RoverPositionYChar, roverPosition[1]));
            }

            switch (roverPosition[2].ToLower())
            {
                case "n":
                    bearing = Bearing.North;
                    break;
                case "e":
                    bearing = Bearing.East;
                    break;
                case "s":
                    bearing = Bearing.South;
                    break;
                case "w":
                    bearing = Bearing.West;
                    break;
                default:
                    return new InvalidResult<Rover>(string.Format(ValidationErrors.RoverPositionBearingChar, roverPosition[2]));
            }

            return new SuccessResult<Rover>(new Rover(positionX, positionY, bearing.Value));
        }

        public Result<Rover> MoveRover(string instructionString, Rover rover)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrEmpty(instructionString))
            {
                return new InvalidResult<Rover>(ValidationErrors.RoverInstructionsNullOrEmpty);
            }

            foreach (char c in instructionString)
            {
                switch (c.ToString().ToLower())
                {
                    case "l":
                        rover.TurnLeft();
                        break;
                    case "r":
                        rover.TurnRight();
                        break;
                    case "m":
                        rover.MoveForward();
                        break;
                    default:
                        errors.Add(string.Format(ValidationErrors.RoverInstructionsChar, c));
                        break;
                }
            }

            if (errors.Any())
            {
                return new InvalidResult<Rover>(errors);
            }

            return new SuccessResult<Rover>(rover);
        }
    }
}
