using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Services.Extensions
{
    public static class CharExtensions
    {
        public static bool IsOperator(this char c)
        {
            return "()+-*/".Contains(c);
        }
    }
}
