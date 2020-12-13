using Octarines.Mars.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octarines.Mars.Models.Results
{
    public class InvalidResult<T> : Result<T>
    {
        public override ResultType ResultType => ResultType.Invalid;

        private IEnumerable<string> _errors = new List<string>();
        public override IEnumerable<string> Errors => _errors;

        public InvalidResult(string error = "Invalid Input")
        {
            _errors = new List<string>() { error };
        }

        public InvalidResult(IEnumerable<string> errors)
        {
            _errors = errors;
        }
        
    }
}
