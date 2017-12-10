using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleStackCalculator
{
    public class StackCalculatorMainLogic
    {
        private string mExpression;
        private List<string> mPostfixExpression;
        private Stack<string> mTempSignStack;
        private Stack<double> mTempNumStack;

        public StackCalculatorMainLogic()
        {
            mPostfixExpression = new List<string>();
            mTempSignStack = new Stack<string>();
            mTempNumStack = new Stack<double>();
        }

        public void setExpression(string expression)
        {
            mExpression = expression;
            mPostfixExpression.Clear();

            createPostfixExpression();

            Console.Write("Postfix: ");
            for (int i = 0; i < mPostfixExpression.Count; ++i)
            { 
                Console.Write(mPostfixExpression[i] + " ");
            }
            Console.WriteLine();

            calculate();
        }

        public double calculate()
        {
            Stack<string> postfixStack = new Stack<string>();
            mTempNumStack.Clear();

            for (int i = mPostfixExpression.Count - 1; i >= 0; --i)
            {
                postfixStack.Push(mPostfixExpression[i]);
            }

            while (postfixStack.Count > 0)
            {
                string literal = postfixStack.Pop();

                double operand;
                bool isNumber = double.TryParse(literal, out operand);

                if (isNumber == true)
                {
                    mTempNumStack.Push(operand);
                }
                else
                {
                    if (mTempNumStack.Count >= 2)
                    {
                        double operandOne = mTempNumStack.Pop();
                        double operandTwo = mTempNumStack.Pop();

                        double result = doOperation(operandTwo, operandOne, literal);

                        mTempNumStack.Push(result);
                    }
                }               
            }
            //Console.WriteLine($"Result: {mTempNumStack.Peek()}");
            return mTempNumStack.Peek();
        }

        private double doOperation(double a, double b, string sign)
        {
            switch (sign)
            {
                case "+":
                    {
                        double res = a + b;
                        return res;
                    }
                case "-":
                    {
                        double res = a - b;
                        return res;
                    }
                case "*":
                    {
                        double res = a * b;
                        return res;
                    }
                case "/":
                    {
                        double res = a / b;
                        return res;
                    }
                default: return default(double);
            }
        }
        private bool createPostfixExpression()
        {
            for (int i = 0; i < mExpression.Length; ++i)
            {
                switch (mExpression[i])
                {
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '(':
                        {
                            dealWithSignsPriority(mExpression[i]);
                            continue;
                        }
                    case ')':
                        {
                            while (mTempSignStack.Peek() != "(")
                            {                               
                                mPostfixExpression.Add(mTempSignStack.Pop());
                            }
                            mTempSignStack.Pop();
                            continue;
                        }
                    case ' ':
                        {
                            continue;
                        }
                    default: break;
                }

                int number;
                string value = "";
                while (int.TryParse(mExpression[i].ToString(), out number) ||
                        mExpression[i] == '.')
                {
                    value += mExpression[i];
                    i++;

                    if (i >= mExpression.Length)
                    {
                        break;
                    }
                }

                i--;

                if (value != "")
                {
                    mPostfixExpression.Add(value);
                }
            }

            while (mTempSignStack.Count > 0)
            {
                mPostfixExpression.Add(mTempSignStack.Pop());
            }

            return true;
        }
        private void dealWithSignsPriority(char newSign)
        {
            if (mTempSignStack.Count == 0 || newSign == '(')
            {
                mTempSignStack.Push(newSign.ToString());
                return;
            }

            if (mTempSignStack.Peek() == "(")
            {
                mTempSignStack.Push(newSign.ToString());
                return;
            }

            if ((newSign == '*' || newSign == '/') &&
                (mTempSignStack.Peek() == "+" || mTempSignStack.Peek() == "-"))
            {                
                mTempSignStack.Push(newSign.ToString());
                return;
            }

            if ((newSign == '*' || newSign == '/') && 
                (mTempSignStack.Peek() == "*" || mTempSignStack.Peek() == "/"))
            {
                while (mTempSignStack.Count > 0 && (mTempSignStack.Peek() != "(" ||
                    mTempSignStack.Peek() != "+" || mTempSignStack.Peek() != "-"))
                {
                    mPostfixExpression.Add(mTempSignStack.Pop());
                }

                mTempSignStack.Push(newSign.ToString());
                return;
            }

            if (newSign == '+' || newSign == '-')
            {
                while(mTempSignStack.Count > 0 &&
                    mTempSignStack.Peek() != "(")
                {
                    mPostfixExpression.Add(mTempSignStack.Pop());
                }

                mTempSignStack.Push(newSign.ToString());
            }
        }
    }
}
