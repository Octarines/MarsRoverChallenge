using Octarines.Mars.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Octarines.Mars.Models.Results
{
    public class SuccessResult<T> : Result<T>
    {
        private readonly T _data;
        public SuccessResult(T data)
        {
            _data = data;
        }
        public override ResultType ResultType => ResultType.Success;

        public override T Data => _data;
    }
}
