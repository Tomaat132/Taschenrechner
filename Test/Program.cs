using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your Query: ");
            string query = Console.ReadLine();
            double result = MathHelper.MathString.Calculate(query);
            Console.WriteLine("Result: " + result);
            Console.ReadLine();
        }
    }
}
