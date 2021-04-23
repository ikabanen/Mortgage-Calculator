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

namespace MortgageCalculator
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        static public double amountBorrowed { get; set; }
        static public double interestRate { get; set; }
        static public int mortgagePeriod { get; set; }
        private void lbl_Calculate_Click(object sender, RoutedEventArgs e)
        {
            amountBorrowed = (double)Int32.Parse(Amount.Text);

            decimal result;
            if (Decimal.TryParse(Interest.Text, out result))
                interestRate = (double)result;

            mortgagePeriod = Int32.Parse(Period.Text);

            MonthlyPayments.Text =
                CalcMortgage(amountBorrowed, interestRate,
                             mortgagePeriod);

            var mnthpaym = Convert.ToDouble(CalcMortgage(amountBorrowed, interestRate,
                             mortgagePeriod));
            double mortper = Convert.ToDouble(mortgagePeriod);
            double TotalPayment_amnt = mnthpaym * mortper * 12;
            TotalPayment.Text = Convert.ToString(Math.Round(TotalPayment_amnt, 2));
            TotalPrincipal.Text = Convert.ToString(Convert.ToDouble(Math.Round(amountBorrowed, 2)));
            TotalInterest.Text = Convert.ToString(Math.Round((TotalPayment_amnt - amountBorrowed),2));
        }

        private string CalcMortgage(double amountBorrowed,
                                    double interestRate,
                                    int mortgagePeriod)
        {
            double p = amountBorrowed;
            double r = ConvertToMonthlyInterest(interestRate);
            double n = YearsToMonths(mortgagePeriod);

            var c = (double)(((r * p) * Math.Pow((1 + r), n)) /
                    (Math.Pow((1 + r), n) - 1));

            return ($"{Math.Round(c, 2)}");
        }

        private int YearsToMonths(int years)
        {
            return (12 * years);
        }

        private double ConvertToMonthlyInterest(double percent)
        {
            return (percent / 12) / 100;
        }
    }
}
