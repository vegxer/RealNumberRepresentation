using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realnumber
{
    class RealNumber
    {
        int placing, exponentSize, mantissaSize;
        byte sign;
        public byte[] PlacedExponent { get; private set; }

        public byte[] Mantissa { get; private set; }

        public RealNumber(float number)
        {
            exponentSize = 8;
            placing = 127;
            mantissaSize = 24;
            if (number != 0)
            {
                sign = number > 0 ? (byte)0 : (byte)1;
                BinaryNumber binaryNumber = RealToBinary(Math.Abs(number), mantissaSize);
                PlacedExponent = IntToBinary(binaryNumber.exponent + placing, exponentSize);
                Mantissa = SubArray(binaryNumber.mantissa, 1, binaryNumber.mantissa.Length - 1);
            }
            else
            {
                sign = 2;
                Mantissa = new byte[mantissaSize - 1];
                PlacedExponent = new byte[exponentSize];
            }
        }

        public RealNumber(double number)
        {
            mantissaSize = 53;
            exponentSize = 11;
            placing = 1023;
            if (number != 0)
            {
                sign = number > 0 ? (byte)0 : (byte)1;
                BinaryNumber binaryNumber = RealToBinary(Math.Abs(number), mantissaSize);
                PlacedExponent = IntToBinary(binaryNumber.exponent + placing, exponentSize);
                Mantissa = SubArray(binaryNumber.mantissa, 1, binaryNumber.mantissa.Length - 1);
            }
            else
            {
                sign = 2;
                Mantissa = new byte[mantissaSize - 1];
                PlacedExponent = new byte[exponentSize];
            }
        }

        private BinaryNumber RealToBinary(double number, int bitsNum)
        {
            List<byte> binNum = new List<byte>();
            ulong intPart = (ulong)number;
            //int part to binary
            while (intPart != 0)
            {
                binNum.Add((byte)(intPart % 2));
                intPart /= 2;
            }
            byte[] mantissa = new byte[bitsNum];
            int i;
            //creating int part of binary number
            for (i = 0; i < bitsNum && i < binNum.Count; ++i)
                mantissa[i] = binNum[binNum.Count - i - 1];
            //decimal part to binary
            int zeroCount = 0;
            if (binNum.Any())
            {
                for (double decPart = number - (ulong)number; i < bitsNum && decPart != 0; decPart -= (int)decPart)
                {
                    decPart *= 2;
                    mantissa[i++] = (byte)((int)decPart);
                }
            }
            else
            {
                bool oneFound = false;
                for (double decPart = number - (ulong)number; i < bitsNum && decPart != 0; decPart -= (int)decPart)
                {
                    decPart *= 2;
                    if (!oneFound)
                    {
                        if ((int)decPart != 0)
                            oneFound = true;
                        else
                            ++zeroCount;
                    }
                    if (oneFound)
                        mantissa[i++] = (byte)((int)decPart);    
                }
            }
            if (!binNum.Any())
                return new BinaryNumber(mantissa, -(zeroCount + 1)); //мантисса, порядок
            else
                return new BinaryNumber(mantissa, binNum.Count - 1);
        }

        private byte[] IntToBinary(int number, int bitsNum)
        {
            number = Math.Abs(number);
            List<byte> binNum = new List<byte>();
            while (number != 0)
            {
                binNum.Add((byte)(number % 2));
                number /= 2;
            }
            byte[] sizedBinNum = new byte[bitsNum];
            for (int i = bitsNum - binNum.Count, j = 0; i < bitsNum && j < binNum.Count; ++i, ++j)
                sizedBinNum[i] = binNum[binNum.Count - j - 1];
            return sizedBinNum;
        }

        private T[] SubArray<T>(T[] array, int start, int length)
        {
            if (start >= 0 && start < array.Length && length > 0 && length <= array.Length - start)
            {
                T[] result = new T[length];
                Array.Copy(array, start, result, 0, length);
                return result;
            }
            else
                throw new ArgumentException("Неверно указан индекс начала или длина");
        }

        public int PlacedExponentDecimal()
        {
            int decPlacedExponent = 0, degree = (int)Math.Pow(2, exponentSize - 1);
            foreach (byte element in PlacedExponent)
            {
                decPlacedExponent += element * degree;
                degree /= 2;
            }
            return decPlacedExponent;
        }

        public int ExponentDecimal()
        {
            return PlacedExponentDecimal() - placing;
        }

        public string ExponentBinary()
        {
            string exp = sign == 2 ? "-" : "";
            byte[] binNum = IntToBinary(ExponentDecimal(), exponentSize);
            foreach (byte digit in binNum)
                exp += digit.ToString();
            return exp;
        }

        public byte Sign => sign;

        public override string ToString()
        {
            string line = new string('_', 6 + exponentSize + mantissaSize);
            line = "|" + line;
            line = line.Remove(5, 1).Insert(5, "|");
            line = line.Remove(6 + exponentSize, 1).Insert(6 + exponentSize, "|");
            line = line.Remove(line.Length - 1, 1).Insert(line.Length - 1, "|");
            string res = "";
            res += line + '\n';
            res += "|Знак|Порядок";
            res += (exponentSize == 8 ? " " : "    ");
            res += "|Мантисса" + (mantissaSize == 24 ? new string(' ', 15) : new string(' ', 44)) + "|\n";
            res += line + '\n';
            res += "|" + sign.ToString() + "   |";
            foreach (byte element in PlacedExponent)
                res += element.ToString();
            res += "|";
            foreach (byte element in Mantissa)
                res += element.ToString();
            res += "|\n";
            res += line + '\n';
            return res;
        }

    }
}
