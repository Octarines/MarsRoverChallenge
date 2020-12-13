using Octarines.Mars.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Octarines.Mars.Models.Results
{
    public abstract class Result<T>
    {
        public abstract ResultType ResultType { get; }
        public virtual IEnumerable<string> Errors { get; }
        public virtual T Data { get => default; } 

        public bool HasErrors { get => Errors != null && Errors.Any(); }
    }
}
