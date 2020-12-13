using Octarines.Mars.Models.Results;
using Octarines.Mars.Models.Rovers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octarines.Mars.Services.Rovers
{
    public interface IRoversService
    {
        /// <summary>
        /// Takes a collection of input strings and navigates a number of "Rover's" around a plateau of defined size
        /// </summary>
        /// <param name="instructions">First instruction defines plateau size (X Y), each subsequent pair of instructions defines a new rover where line 1 indicates initial position (X Y Bearing) and line 2 is navigation instrucitons</param>
        /// <returns>Aresult object containing a collection of the final positions of rovers</returns>
        public Result<IEnumerable<string>> ExplorePlateau(IEnumerable<string> instructions);

        /// <summary>
        /// Converts a string representation of the size of the martian plateau into integer coorindate values.
        /// </summary>
        /// <param name="size">String containing 2 integer coordinate values separated by a space (X Y)</param>
        /// <returns>A result object containing a new instance of acoodinate object with the parsed X and Y coordinate integer values</returns>
        public Result<Coordinates> ParsePlateuaDetails(string size);

        /// <summary>
        /// Creates a new instance of a Rover object by parsing the string representation of the inital position.
        /// </summary>
        /// <param name="position">String containing 2 integer coordinate values and a single character 'Bearing' separated by a space (X Y Bearing)</param>
        /// <returns>Result object containing a new instance of a Rover object</returns>
        public Result<Rover> InitializeNewRover(string position);

        /// <summary>
        /// Updates the locationX, locationY and bearing properties of a rover according to the characters contained within the instruction string
        /// </summary>
        /// <param name="instructionString">A series of character representations of the movements that can be completed by a rover</param>
        /// <param name="rover">The rover for which the position is to be updated</param>
        /// <returns>Result object containing the updated rover</returns>
        public Result<Rover> MoveRover(string instructionString, Rover rover);
    }
}
