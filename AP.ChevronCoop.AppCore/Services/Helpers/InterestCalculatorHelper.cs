using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities;

namespace AP.ChevronCoop.AppCore.Services.Helpers;

public class InterestCalculatorHelper
{
    void Run()
    {
        var calcResponse = CalcPayment(10000, 12, 10);
        var pmtResponse = Pmt(10, 12, 10000);
        Console.WriteLine("pmt: " + pmtResponse);

        pmtResponse = PMTAnually(10, 12, 10000);
        Console.WriteLine("pmt Annually: " + pmtResponse);

        decimal pmt_Response = PmtMonth(10, 12, 10000);
        Console.WriteLine("pmt Monthly: " + pmt_Response);

        ComputePrincipal();

        var chevronUpperResult = ChevronCompute();

        Console.Write($"Cheveron Result : {chevronUpperResult}");

        Console.WriteLine(calcResponse);
    }

    decimal PmtMonth(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        if (yearlyInterestRate > 0)
        {
            var rate = (double)yearlyInterestRate / 100 / 12;
            var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
            return new decimal((rate + (rate / denominator)) * loanAmount);
        }
        return totalNumberOfMonths > 0 ? new decimal(loanAmount / totalNumberOfMonths) : 0;
    }

    double ChevronCompute()
    {
        double rate = (double)(((10 / 100) * 30) / 365);
        return 10000 * rate * (10 * 30) / 365;
    }

    //Here's the formula Chevron uses for reducing balance

    //(Outstanding principal * rate * no of days in the month)/ 365

    //Yes, they use APR and interest is calculated on a daily basis


    static double PMTAnually(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        var rate = (double)(yearlyInterestRate * 0.3) / 366;
        var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
        return (rate + (rate / denominator)) * loanAmount;
    }

    double CalcPayment(double presentValue, double financingPeriod, double interestRatePerYear)
    {
        double a, b, x;
        double monthlyPayment;

        a = (1 + interestRatePerYear / 1200);

        b = financingPeriod;
        x = Math.Pow(a, b);
        x = 1 / x;
        x = 1 - x;

        monthlyPayment = (presentValue) * (interestRatePerYear / 1200) / x;

        return (monthlyPayment);
    }

    void ComputePrincipal()
    {


        double loanAmount = 10000;
        double interest = 10;
        double numberOfMonth = 10;

        // rate of interest and number of payments for monthly payments
        double rateOfInterest = interest / 1200;
        double numberOfPayments = numberOfMonth * 12;

        var upper = (rateOfInterest * loanAmount);

        var powResult = Math.Pow((1 + rateOfInterest / 1200), numberOfPayments * -1);

        var lower = (1 - powResult);

        var paymentAmount = upper / lower;

        Console.WriteLine("principal= " + paymentAmount);
        Console.ReadLine();
        return;

    }


    static double PayPresentValue(double loanAmount, double interest, double loanTenure)
    {
        var ir = (1 - (Math.Pow((1 + 0.0125), -12))) / 0.0125;

        var result = loanAmount / ir;
        return result;
    }

    static double Pmt(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
    {
        var rate = (double)yearlyInterestRate / 100 / 12;
        var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
        return (rate + (rate / denominator)) * loanAmount;
    }

    public static void ReducingBalanceCalc()
    {

        double principal = 10000; // Initial loan amount
        double interestRate = 0.1; // Annual interest rate (10%)
        double numberOfPeriods = 12; // Number of payment periods (months)
        double remainingBalance = principal; // Initialize remaining balance with the principal


        double repaymentAmount = Pmt((double)interestRate * 100, (int)numberOfPeriods, (double)remainingBalance);

        repaymentAmount = Math.Round(repaymentAmount, 2);
        for (int i = 0; i < numberOfPeriods; i++)
        {
            double interest = (remainingBalance * interestRate) / numberOfPeriods; // Calculate the interest for the current period
            interest = Math.Round(interest, 2);

            double principalCal = repaymentAmount - interest;
            principalCal = Math.Round(principalCal, 2);

            remainingBalance += interest;

            remainingBalance -= repaymentAmount; // Update the remaining balance
            remainingBalance = Math.Round(remainingBalance, 2);

            Console.WriteLine($"Period {i + 1}: Repayment Amount: {repaymentAmount}  Interest: {interest}, Principal: {principalCal}, New Balance: {remainingBalance}");
        }
    }

    /// <summary>
    /// Flat / Simple
    /// </summary>
    public static List<ScheduleModel> SimpleInterestLoanComputation(Tenure TenureUnit, decimal numberOfPeriods, DateTime commencementDate, decimal principal, decimal interestRate)
    {
        decimal interest = (principal * (interestRate / 100 / 12)); // @ a monthly rate
        interest = Math.Round(interest, 2);

        int periodCount = (int)Math.Ceiling(numberOfPeriods);

        decimal principalValue = principal / periodCount;

        principalValue = Math.Round(principalValue, 2);

        double totalRepayment = 0;

        var response = new List<ScheduleModel>();
        for (int i = 1; i <= periodCount; i++)
        {
            var balance = principal - (i * principalValue);
            response.Add(new ScheduleModel
            {
                Interest = interest,
                Balance = balance,
                Principal = principalValue,
                Date = commencementDate,
                Total = interest + principalValue
            });

            commencementDate = commencementDate.AddMonths(1);
        }

        return response;
    }

}