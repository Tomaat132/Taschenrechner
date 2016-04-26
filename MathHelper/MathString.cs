using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelper
{
    public class MathString
    {
        private List<MathOperator> Operations = new List<MathOperator>();

        public static double Calculate(string mathString)
        {
            MathOperator[] operations = MathOperator.BuildList(mathString);
            MathOperator result = MathOperator.Calculate(operations)[0];
            return result.Number;
        }
    }
}
