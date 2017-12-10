using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStackCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            StackCalculatorMainLogic logic = new StackCalculatorMainLogic();

            string expression = "";

            while (true)
            {
                expression = Console.ReadLine();

                if (expression == "q")
                {
                    break;
                }

                logic.setExpression(expression);
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
