using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelper
{
    public class MathOperator
    {
        public MathOperator(MathOperatorType type, string value)
        {
            this.Type = type;
            if(type == MathOperatorType.Number)
            {
                this.Number = Convert.ToDouble(value);
            }
            else
            {
               this.Operator = value;
            }
        }
        public MathOperatorType Type { get; set; }
        public double Number { get; set; }
        public string Operator { get; set; }
        public double Calculate(double before, double after)
        {
            if(Type == MathOperatorType.Operator)
            {
                switch(Operator)
                {
                    case "+":
                        {
                            return before + after;
                        }
                    case "-":
                        {
                            return before - after;
                        }
                    case "*":
                        {
                            return before * after;
                        }
                    case "/":
                        {
                            return before / after;
                        }
                    default:
                        {
                            return 0;
                        }
                }
            }
            else
            {
                return 0;
            }
        }

        public static MathOperator[] BuildList(string query)
        {
            List<MathOperator> operations = new List<MathOperator>();
            string buffer = "";
            foreach(char c in query)
            {
                if(IsOperator(c))
                {
                    if (buffer != "")
                    {
                        operations.Add(new MathOperator(MathOperatorType.Number, buffer));
                        buffer = "";
                    }
                    operations.Add(new MathOperator(MathOperatorType.Operator, "" + c));
                }
                else
                {
                    buffer += c;
                }
            }
            if (buffer != "")
            {
                operations.Add(new MathOperator(MathOperatorType.Number, buffer));
                buffer = "";
            }
            return operations.ToArray();
        }

        private static bool IsOperator(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/') ;
        }

        public static MathOperator[] Calculate(MathOperator[] operations)
        {
            bool worked = false;
            //First test for * or /

            bool containPoint = false;
            for(int i = 0, l = operations.Length; i < l; i++)
            {
                if(operations[i].Type == MathOperatorType.Operator)
                {
                    if(operations[i].Operator == "*" || operations[i].Operator == "/")
                    {
                        containPoint = true;
                        break;
                    }
                }
            }
            for(int i = 0, l = operations.Length; i < l; i++)
            {
                if(operations[i].Type == MathOperatorType.Operator)
                {
                    if (containPoint)
                    {
                        if (operations[i].Operator == "*" || operations[i].Operator == "/")
                        {
                            double result = operations[i].Calculate(operations[i - 1].Number, operations[i + 1].Number);
                            List<MathOperator> newOperations = new List<MathOperator>();
                            for (int j = 0; j < l; j++)
                            {
                                if (j != i - 1 && j != i + 1)
                                {
                                    if (j == i)
                                    {
                                        newOperations.Add(new MathOperator(MathOperatorType.Number, "" + result));
                                    }
                                    else
                                    {
                                        newOperations.Add(operations[j]);
                                    }
                                }
                            }
                            worked = true;
                            operations = newOperations.ToArray();
                            break;
                        }
                    }
                    else
                    {
                        double result = operations[i].Calculate(operations[i - 1].Number, operations[i + 1].Number);
                        List<MathOperator> newOperations = new List<MathOperator>();
                        for (int j = 0; j < l; j++)
                        {
                            if (j != i - 1 && j != i + 1)
                            {
                                if (j == i)
                                {
                                    newOperations.Add(new MathOperator(MathOperatorType.Number, "" + result));
                                }
                                else
                                {
                                    newOperations.Add(operations[j]);
                                }
                            }
                        }
                        worked = true;
                        operations = newOperations.ToArray();
                        break;
                    }
                }
            }

            if(worked)
            {
                return Calculate(operations);
            }
            else
            {
                return operations;
            }
        }
    }
}
