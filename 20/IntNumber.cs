using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvenOfCode20
{
    internal class IntNumber
    {
        public Int64 number;
        public Int64 address;

        public IntNumber(Int64 index, Int64 number)
        {
            this.address = index;
            this.number = number;
        }

        override
        public string ToString()
        {
            return number + "";
        }
    }
  
}
