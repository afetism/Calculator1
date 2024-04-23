using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator1
{
    internal class Calculator
    {

        public static decimal Number1 { get; set; }
        public static decimal Number2 { get; set; }
        public static decimal Result { get; set; }
        public static char Operator { get; set; }


        public static void  Calculate(Char? op)
        {

            switch (op)
            { 
             case '+':
                    Result=Number1+Number2;
                    break;
             case '-':
                    Result=Number1-Number2;
                    break;
             case '×':
                    Result=Number1*Number2;
                    break;
             case '÷':
                    Result=Number1/Number2;
                    break;






            }




        }
    }
}
