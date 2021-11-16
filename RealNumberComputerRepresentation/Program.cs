using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realnumber
{
    class Program
    {
        static void Main(string[] args)
        {
            bool program = true;
            while (program)
            {
                try
                {
                    Console.Write("Введите тип числа (float, double): ");
                    string type = Console.ReadLine();
                    if (type == "float")
                    {
                        float number;
                        Console.Write("Введите число: ");
                        if (float.TryParse(Console.ReadLine(), out number))
                            DoMethods(number);
                        else
                            throw new FormatException("Введено не число типа float");
                    }
                    else if (type == "double")
                    {
                        double number;
                        Console.Write("Введите число: ");
                        if (double.TryParse(Console.ReadLine(), out number))
                            DoMethods(number);
                        else
                            throw new FormatException("Введено не число типа double");
                    }
                    Console.Write("Выйти из программы? (1 - да): ");
                    if (int.Parse(Console.ReadLine()) == 1)
                        program = false;
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message.ToString());
                }
                finally
                {
                    Console.ReadKey();
                }
            }
        }

        static void DoMethods(float number)
        {
            RealNumber num = new RealNumber(number);
            Console.WriteLine(num.ToString());
            Console.WriteLine("Мантисса: ");
            foreach (byte element in num.Mantissa)
                Console.Write(element);
            Console.WriteLine("\nСмещённый порядок: ");
            foreach (byte element in num.PlacedExponent)
                Console.Write(element);
            Console.WriteLine("\nСмещённый порядок: ");
            Console.WriteLine(num.PlacedExponentDecimal());
            Console.WriteLine("Порядок: ");
            Console.Write(num.ExponentBinary());
            Console.WriteLine("\nПорядок: ");
            Console.WriteLine(num.ExponentDecimal());
            Console.WriteLine("Знак: ");
            Console.WriteLine(num.Sign);
        }

        static void DoMethods(double number)
        {
            RealNumber num = new RealNumber(number);
            Console.WriteLine(num.ToString());
            Console.WriteLine("Мантисса: ");
            foreach (byte element in num.Mantissa)
                Console.Write(element);
            Console.WriteLine("\nСмещённый порядок: ");
            foreach (byte element in num.PlacedExponent)
                Console.Write(element);
            Console.WriteLine("\nСмещённый порядок: ");
            Console.WriteLine(num.PlacedExponentDecimal());
            Console.WriteLine("Порядок: ");
            Console.Write(num.ExponentBinary());
            Console.WriteLine("\nПорядок: ");
            Console.WriteLine(num.ExponentDecimal());
            Console.WriteLine("Знак: ");
            Console.WriteLine(num.Sign);
        }
    }
}
