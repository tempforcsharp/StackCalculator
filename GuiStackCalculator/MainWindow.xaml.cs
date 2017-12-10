using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ConsoleStackCalculator;

namespace GuiStackCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StackCalculatorMainLogic mCalculatorLogic;
        private string mTextExpression;

        public MainWindow()
        {
            InitializeComponent();

            this.Width = btn0.Width * 4 + 50;
            this.Height += 10;

            this.ResizeMode = ResizeMode.NoResize;

            mCalculatorLogic = new StackCalculatorMainLogic();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            mTextExpression = "";
            display.Text = "";
        }       
        private void btnRes_Click(object sender, RoutedEventArgs e)
        {
            mCalculatorLogic.setExpression(mTextExpression);
            double result = mCalculatorLogic.calculate();

            mTextExpression += " = ";
            mTextExpression += result;

            display.Text = mTextExpression;
        }       
        private void btnSign_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string btnText = btn.Content.ToString();
            mTextExpression += btnText;
            display.Text = mTextExpression;
        }
    }
}
