using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realnumber
{
    class BinaryNumber
    {
        public byte[] mantissa;
        public int exponent;

        public BinaryNumber(byte[] mantissa, int exponent)
        {
            this.mantissa = mantissa;
            this.exponent = exponent;
        }
    }
}
