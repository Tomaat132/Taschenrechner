using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Bitte geben Sie ihre Aufgabe ein: ");
                string query = Console.ReadLine();
                MathOperator[] operations = GetOperations(query);
                for (int i = 0, l = operations.Length; i < l; i++)
                {
                    if (operations[i].Type == MathOperatorType.Number)
                    {
                        Console.WriteLine("Number: " + operations[i].Number);
                    }
                    else
                    {
                        Console.WriteLine("Operator: " + operations[i].Operator);
                    }
                }
                int result = Calculate(operations);
                Console.WriteLine("Ergebnis: " + result);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static MathOperator[] GetOperations(string query)
        {
            List<MathOperator> operations = new List<MathOperator>();
            string buffer = "";
            foreach (char c in query)
            {
                if (IsOperator(c))
                {
                    if(buffer != "")
                    {
                        MathOperator moNum = new MathOperator();
                        moNum.Type = MathOperatorType.Number;
                        moNum.Number = Convert.ToInt32(buffer);
                        operations.Add(moNum);
                        buffer = "";
                    }
                    MathOperator moOp = new MathOperator();
                    moOp.Type = MathOperatorType.Operator;
                    moOp.Operator = "" + c;
                    operations.Add(moOp);
                }
                else
                {
                    buffer += c;
                }
            }
            if (buffer != "")
            {
                MathOperator moNum = new MathOperator();
                moNum.Type = MathOperatorType.Number;
                moNum.Number = Convert.ToInt32(buffer);
                operations.Add(moNum);
                buffer = "";
            }
            return operations.ToArray();
        }

        public static bool IsOperator(char c)
        {
            return (c == '+' || c == '-' || c == '/' || c == '*' || c == '%');
        }

        static int Calculate(MathOperator[] operations)
        {
            List<MathOperator> newOperations = new List<MathOperator>();
            int arrLength = operations.Length;
            bool worked = false;
            int fullResult = 0;
            for (int i = 0, l = arrLength; i < l; i++)
            {
                if (operations[i].Type == MathOperatorType.Operator)
                {
                    if (operations[i].Operator == "*" || operations[i].Operator == "/")
                    {
                        worked = true;
                        int result = operations[i].Calculate(operations[i - 1].Number, operations[i + 1].Number);
                        for (int b = 0; b < arrLength; b++)
                        {
                            if (b != i - 1 && b != i + 1)
                            {

                            }
                            if (b == i)
                            {
                                MathOperator mo = new MathOperator();
                                mo.Type = MathOperatorType.Number;
                                mo.Number = result;
                                newOperations.Add(mo);
                            }
                        }
                        fullResult = Calculate(newOperations.ToArray());
                    }
                }
            }
            if (!worked)
            {
                for (int i = 1, l = operations.Length; i < l; i++)
                {
                    fullResult = operations[i].Calculate(operations[i - 1].Number, operations[i + 1].Number);
                    operations[i + 1].Number = fullResult;
                }
            }
            return fullResult;
        }
    }
}
