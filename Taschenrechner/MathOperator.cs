using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner
{
    public class MathOperator
    {
        public MathOperatorType Type { get; set; }
        public string Operator { get; set; }
        public int Number { get; set; }
        public int Calculate(int op1, int op2)
        {
            if(Type == MathOperatorType.Operator)
            {
                switch (Operator)
                {
                    case "+":
                        {
                            return op1 + op2;
                        }
                    case "-":
                        {
                            return op1 - op2;
                        }
                    case "*":
                        {
                            return op1 * op2;
                        }
                    case "/":
                        {
                            return op1 / op2;
                        }
                    case "%":
                        {
                            return op1 % op2;
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
    }
}
