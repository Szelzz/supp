using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supp.Core
{
    public class Result
    {
        private Result() { }

        public bool Succeeded { get; private set; }

        public static Result Success()
        {
            return new Result()
            {
                Succeeded = true
            };
        }

        public static Result Fail()
        {
            return new Result()
            {
                Succeeded = false
            };
        }
    }
}
