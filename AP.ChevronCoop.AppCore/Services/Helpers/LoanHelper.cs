using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AP.ChevronCoop.AppCore.Services.Helpers;

public class LoanHelper
{
    public LoanHelper()
    {

    }
    public LoanHelper(decimal principal, decimal interestRate, InterestMethod interestMethod, InterestCalculationMethod interestCalculationMethod,
    Tenure tenureUnit, decimal tenureValue, Tenure repaymentPeriod, decimal daysInYear, DateTime effectiveDate)
    {
        //AccountNo = accountNo;
        Principal = principal;
        InterestRate = interestRate;
        InterestMethod = interestMethod;
        InterestCalculationMethod = interestCalculationMethod;
        TenureUnit = tenureUnit;
        TenureValue = tenureValue;
        RepaymentPeriod = repaymentPeriod;
        DaysInYear = daysInYear;
        EffectiveDate = effectiveDate;

    }

    public LoanHelper(decimal principal, decimal interestRate, InterestMethod interestMethod, InterestCalculationMethod interestCalculationMethod,
    Tenure tenureUnit, decimal tenureValue, Tenure repaymentPeriod, Tenure compoundingPeriod, decimal daysInYear, DateTime effectiveDate)
    {
        //AccountNo = accountNo;
        Principal = principal;
        InterestRate = interestRate;
        InterestMethod = interestMethod;
        InterestCalculationMethod = interestCalculationMethod;
        TenureUnit = tenureUnit;
        TenureValue = tenureValue;
        RepaymentPeriod = repaymentPeriod;
        CompoundingPeriod = compoundingPeriod;
        DaysInYear = daysInYear;
        EffectiveDate = effectiveDate;

    }

    public string AccountNo { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public LoanProductType LoanProductType { get; set; }
    public string CustomerId { get; set; }
    public string CurrencyId { get; set; }

    public decimal Principal { get; set; }
    public decimal DaysInYear { get; set; } //360,365,366
    public Tenure TenureUnit { get; set; }
    public decimal TenureValue { get; set; }
    public Tenure RepaymentPeriod { get; set; } = Tenure.MONTHLY;
    public Tenure CompoundingPeriod { get; set; } = Tenure.MONTHLY;

    public decimal InterestRate { get; set; }
    public InterestMethod InterestMethod { get; set; } = InterestMethod.SIMPLE;
    public InterestCalculationMethod InterestCalculationMethod { get; set; } = InterestCalculationMethod.FLAT_RATE;

    public DateTime EffectiveDate { get; set; }



    public List<AmortizationSchedule> GetAmortizationTable(InterestCalculationMethod method)
    {
        if (method == InterestCalculationMethod.FLAT_RATE)
            return FlatRateSchedule();
        else
            return ReducingBalanceSchedule();
    }


    private List<AmortizationSchedule> FlatRateSchedule()
    {

        List<AmortizationSchedule> schedules = new List<AmortizationSchedule>();
        decimal cumPrincipal = 0, cumInterest = 0, cumTotal = 0;

        decimal repaymentPeriods = MonthsInPeriod;
        decimal tenureValue = decimal.Round(TenureValue, 3);

        //$"Period Value decimal {TenureValue}".Dump();

        decimal periodPrincipal = decimal.Round((Principal / tenureValue), 3);
        periodPrincipal = decimal.Round((Principal / repaymentPeriods), 3);

        //$"period principal {periodPrincipal:n2}".Dump();


        decimal periodInterest = decimal.Round((InterestEarned / TenureValue), 3);
        periodInterest = decimal.Round((InterestEarned / repaymentPeriods), 3);

        DateTime prevPeriodStartDate = EffectiveDate;

        for (int count = 1; count <= repaymentPeriods; count++)
        {
            var schedule = new AmortizationSchedule();
            schedule.AccountNo = AccountNo;
            schedule.LoanProductType = LoanProductType;
            schedule.Principal = Principal;
            schedule.InterestRate = InterestRate;
            schedule.InterestMethod = InterestMethod;
            schedule.InterestCalculationMethod = InterestCalculationMethod;
            schedule.TenureUnit = TenureUnit;
            schedule.TenureValue = TenureValue;
            schedule.RepaymentPeriod = RepaymentPeriod;
            schedule.DaysInYear = DaysInYear;


            schedule.RepaymentNo = count;
            schedule.PeriodStartDate = prevPeriodStartDate;
            schedule.DueDate = CalculateNextRepaymentDate(count);
            schedule.DaysInPeriod = (schedule.DueDate - schedule.PeriodStartDate).Days;

            schedule.PeriodInterest = decimal.Round(periodInterest, 4);
            cumInterest += schedule.PeriodInterest;
            schedule.CumulativeInterest = decimal.Round(cumInterest, 3);
            schedule.InterestBalance = decimal.Round(InterestEarned - cumInterest, 3);

            schedule.PeriodPrincipal = decimal.Round(periodPrincipal, 3);
            cumPrincipal += schedule.PeriodPrincipal;
            schedule.CumulativePrincipal = decimal.Round(cumPrincipal, 3);
            schedule.PrincipalBalance = decimal.Round(Principal - cumPrincipal, 3);


            schedule.PeriodPayment = decimal.Round(schedule.PeriodPrincipal + schedule.PeriodInterest, 3);
            cumTotal += schedule.PeriodPayment;
            schedule.CumulativeTotal = decimal.Round(cumTotal, 3);
            schedule.TotalBalance = decimal.Round(FutureValue - cumTotal, 3);

            schedules.Add(schedule);

            prevPeriodStartDate = schedule.DueDate.AddDays(1);

        }

        return schedules;

    }


    private List<AmortizationSchedule> ReducingBalanceSchedule()
    {

        List<AmortizationSchedule> schedules = new List<AmortizationSchedule>();

        decimal cumPrincipal = 0, cumInterest = 0, cumTotal = 0;
        decimal principalBalance = 0, interestBalance = 0, totalBalance = 0;


        decimal periodValue = decimal.Round(TenureValue, 3);
        //$"Period Value decimal {periodValue}".Dump();

        decimal periodPrincipal = decimal.Round(Principal / periodValue, 3);
        periodPrincipal = decimal.Round(Principal / MonthsInPeriod, 3);
        //$"period principal {periodPrincipal:n2}".Dump();


        //decimal repaymentPeriodsCeiling, repaymentPeriods;
        //repaymentPeriodsCeiling = Math.Ceiling(MonthsInPeriod);
        //repaymentPeriods = Math.Floor(MonthsInPeriod);
        //repaymentPeriods = Math.Ceiling(loan.PeriodInMonths);
        //$"repaymentPeriods {repaymentPeriods:n2}".Dump();

        decimal periodInterest = decimal.Round(InterestEarned / periodValue, 3);
        //$"period interest 1 {periodInterest:n2}".Dump();
        periodInterest = decimal.Round(InterestEarned / MonthsInPeriod, 3);
        //periodInterest = loan.Principal * loan.RatePerCent * loan.PeriodInYears;
        //$"period interest 2 {periodInterest:n2}".Dump();
        //$"period interest {Math.Round(periodInterest, 2):n2}".Dump();



        DateTime prevPeriodStartDate = EffectiveDate;

        principalBalance = Principal;

        for (int count = 1; count <= MonthsInPeriod; count++)
        {
            var schedule = new AmortizationSchedule();

            schedule.AccountNo = AccountNo;
            schedule.LoanProductType = LoanProductType;
            schedule.Principal = Principal;
            schedule.InterestRate = InterestRate;
            schedule.InterestMethod = InterestMethod;
            schedule.InterestCalculationMethod = InterestCalculationMethod;
            schedule.TenureUnit = TenureUnit;
            schedule.TenureValue = TenureValue;
            schedule.RepaymentPeriod = RepaymentPeriod;
            schedule.DaysInYear = DaysInYear;

            schedule.RepaymentNo = count;
            schedule.PeriodStartDate = prevPeriodStartDate;
            schedule.DueDate = CalculateNextRepaymentDate(Tenure.MONTHLY, count);
            schedule.DaysInPeriod = (schedule.DueDate - schedule.PeriodStartDate).Days;

            //$"principal bal={principalBalance:n2}".Dump();

            var simpleInterest = CalculateReducingInterestOnBalance(principalBalance);
            //$"interest on principal bal={simpleInterest:n2}".Dump();
            periodInterest = decimal.Round(simpleInterest / MonthsInPeriod, 3);
            //$"period interest={periodInterest:n2}".Dump();


            schedule.PeriodInterest = decimal.Round(periodInterest, 3);
            cumInterest += schedule.PeriodInterest;
            schedule.CumulativeInterest = decimal.Round(cumInterest, 3);
            schedule.InterestBalance = decimal.Round(InterestEarned - cumInterest, 3);

            periodPrincipal = EMI - periodInterest;
            //$"principal bal={principalBalance:n2}".Dump();
            //$"period principal={periodPrincipal:n2}".Dump();


            schedule.PeriodPrincipal = decimal.Round(periodPrincipal, 3);
            cumPrincipal += schedule.PeriodPrincipal;
            schedule.CumulativePrincipal = decimal.Round(cumPrincipal, 3);
            schedule.PrincipalBalance = decimal.Round(Principal - cumPrincipal, 3);


            //schedule.PeriodPayment = schedule.PeriodPrincipal + schedule.PeriodInterest;
            schedule.PeriodPayment = decimal.Round(EMI, 3);
            //$"period emi = {Math.Round(emi, 2):n2}, period payment = {schedule.PeriodPayment:n2}".Dump();
            cumTotal += schedule.PeriodPayment;
            schedule.CumulativeTotal = decimal.Round(cumTotal, 3);
            //schedule.TotalBalance = loan.FutureValue - cumTotal;
            //schedule.TotalBalance = decimal.Round((Principal + InterestEarned) - cumTotal, 3);
            schedule.TotalBalance = decimal.Round(FutureValue - cumTotal, 3);



            schedules.Add(schedule);

            principalBalance = principalBalance - periodPrincipal;
            prevPeriodStartDate = schedule.DueDate.AddDays(1);
        }

        return schedules;
    }






    [NotMapped]
    public int PeriodFrequencyPerAnnum
    {

        get
        {

            int periodFrequency = 12;

            switch (TenureUnit)
            {
                case Tenure.DAILY_360:
                    {
                        periodFrequency = 360;
                        break;
                    }

                case Tenure.DAILY_365:
                    {

                        periodFrequency = 365;
                        break;
                    }

                case Tenure.DAILY_366:
                    {
                        periodFrequency = 366;
                        break;
                    }


                case Tenure.WEEKLY:
                    {
                        periodFrequency = 52;
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {
                        periodFrequency = 26;
                        break;
                    }

                case Tenure.MONTHLY:
                    {
                        periodFrequency = 12;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {
                        periodFrequency = 4;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {
                        periodFrequency = 2;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        periodFrequency = 1;
                        break;
                    }

                default: break;
            }

            return periodFrequency;
        }


    }



    [NotMapped]
    public int CompoundingPeriodFrequencyPerAnnum
    {

        get
        {

            int periodFrequency = 12;

            switch (CompoundingPeriod)
            {
                case Tenure.DAILY_360:
                    {
                        periodFrequency = 360;
                        break;
                    }

                case Tenure.DAILY_365:
                    {

                        periodFrequency = 365;
                        break;
                    }

                case Tenure.DAILY_366:
                    {
                        periodFrequency = 366;
                        break;
                    }


                case Tenure.WEEKLY:
                    {
                        periodFrequency = 52;
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {
                        periodFrequency = 26;
                        break;
                    }

                case Tenure.MONTHLY:
                    {
                        periodFrequency = 12;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {
                        periodFrequency = 4;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {
                        periodFrequency = 2;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        periodFrequency = 1;
                        break;
                    }

                default: break;
            }

            return periodFrequency;
        }


    }










    public decimal WeeksInPeriod
    {

        get
        {
            decimal periodInWeeks = 0;

            switch (TenureUnit)
            {
                case Tenure.DAILY_360:
                    //case "day":
                    {

                        periodInWeeks = TenureValue / 7;
                        break;
                    }

                case Tenure.DAILY_365:
                    {

                        periodInWeeks = TenureValue / 7;
                        break;
                    }

                case Tenure.DAILY_366:
                    {
                        periodInWeeks = TenureValue / 7;
                        break;
                    }


                case Tenure.WEEKLY:
                    {
                        periodInWeeks = TenureValue * 1;
                        //$"period in months {periodInMonths} from weeks {TenureValue} ".Dump();
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {
                        periodInWeeks = TenureValue * 2;
                        break;
                    }

                case Tenure.MONTHLY:
                    {
                        //periodFrequency = 12;
                        periodInWeeks = TenureValue * 4;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {
                        //periodFrequency = 4;
                        periodInWeeks = TenureValue * 16;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {
                        //periodFrequency = 2;
                        periodInWeeks = TenureValue * 26;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        //periodFrequency = 1;
                        periodInWeeks = TenureValue * 52;

                        break;
                    }

                default: break;


            }

            return periodInWeeks;

        }

    }



    public decimal BiWeeksInPeriod
    {

        get
        {
            return WeeksInPeriod / 2;
        }
    }

    //	public decimal BiWeeksInPeriod
    //	{
    //
    //		get
    //		{
    //			decimal periodInBiWeeks = 0;
    //
    //			switch (TenureUnit)
    //			{
    //				case Tenure.DAILY_360:
    //					//case "day":
    //					{
    //
    //						periodInBiWeeks = TenureValue / 14;
    //						break;
    //					}
    //
    //				case Tenure.DAILY_365:
    //					{
    //						periodInBiWeeks = TenureValue / 14;
    //						break;
    //					}
    //
    //				case Tenure.DAILY_366:
    //					{
    //						periodInBiWeeks = TenureValue / 14;
    //						break;
    //					}
    //
    //
    //				case Tenure.Week:
    //					//case "week":
    //					{
    //						periodInBiWeeks = TenureValue / 2;
    //						//$"period in months {periodInMonths} from weeks {TenureValue} ".Dump();
    //						break;
    //					}
    //
    //				case Tenure.BiWeekly:
    //					{
    //						periodInBiWeeks = TenureValue * 1;
    //						break;
    //					}
    //
    //				case Tenure.Month:
    //					{
    //						//periodFrequency = 12;
    //						periodInBiWeeks = TenureValue * 2;
    //						break;
    //					}
    //
    //				case Tenure.Quarter:
    //					{
    //						//periodFrequency = 4;
    //						periodInBiWeeks = TenureValue * 8;
    //						break;
    //					}
    //
    //				case Tenure.SemiAnnual:
    //					{
    //						//periodFrequency = 2;
    //						periodInBiWeeks = TenureValue * 13;
    //						break;
    //					}
    //
    //				case Tenure.Annual:
    //					{
    //						//periodFrequency = 1;
    //						periodInBiWeeks = TenureValue * 26;
    //
    //						break;
    //					}
    //
    //				default: break;
    //
    //
    //			}
    //
    //			return periodInBiWeeks;
    //
    //		}
    //
    //	}



    public decimal MonthsInPeriod
    {

        get
        {
            decimal periodInMonths = 0;

            switch (TenureUnit)
            {
                case Tenure.DAILY_360:
                    {

                        periodInMonths = TenureValue / 30;
                        break;
                    }

                case Tenure.DAILY_365:
                    {

                        periodInMonths = TenureValue / 30;
                        break;
                    }

                case Tenure.DAILY_366:
                    {

                        periodInMonths = TenureValue / 30;
                        break;
                    }


                case Tenure.WEEKLY:
                    {

                        periodInMonths = TenureValue / 4;
                        //$"period in months {periodInMonths} from weeks {TenureValue} ".Dump();
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {
                        periodInMonths = TenureValue / 2;
                        break;
                    }

                case Tenure.MONTHLY:
                    {
                        //periodFrequency = 12;
                        periodInMonths = TenureValue * 1;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {
                        //periodFrequency = 4;
                        periodInMonths = TenureValue * 4;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {
                        //periodFrequency = 2;
                        periodInMonths = TenureValue * 6;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        //periodFrequency = 1;
                        periodInMonths = TenureValue * 12;

                        break;
                    }

                default: break;


            }

            return periodInMonths;

        }

    }




    public decimal QuartersInPeriod
    {

        get
        {
            decimal periodInQuarters = 0;

            switch (TenureUnit)
            {
                case Tenure.DAILY_360:
                    {
                        periodInQuarters = TenureValue / 360 * 4;
                        break;
                    }

                case Tenure.DAILY_365:
                    {
                        periodInQuarters = TenureValue / 365 * 4;
                        break;
                    }

                case Tenure.DAILY_366:
                    {

                        periodInQuarters = TenureValue / 366 * 4;
                        break;
                    }


                case Tenure.WEEKLY:
                    {
                        periodInQuarters = TenureValue / 16;
                        //$"period in months {periodInMonths} from weeks {TenureValue} ".Dump();
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {

                        periodInQuarters = TenureValue / 8;
                        break;
                    }

                case Tenure.MONTHLY:
                    {

                        periodInQuarters = TenureValue / 4;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {

                        periodInQuarters = TenureValue * 1;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {

                        periodInQuarters = TenureValue * 2;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        periodInQuarters = TenureValue * 4;

                        break;
                    }

                default: break;


            }

            return periodInQuarters;

        }

    }


    public decimal SemiAnnualYearsInPeriod
    {

        get
        {
            return YearsInPeriod / 2;
        }
    }


    public decimal YearsInPeriod
    {

        get
        {
            decimal periodInYears = 0;

            switch (TenureUnit)
            {
                case Tenure.DAILY_360:
                    //case "day":
                    {


                        periodInYears = TenureValue / 360;
                        break;
                    }

                case Tenure.DAILY_365:
                    {


                        periodInYears = TenureValue / 365;
                        break;
                    }

                case Tenure.DAILY_366:
                    {

                        periodInYears = TenureValue / 366;
                        break;
                    }


                case Tenure.WEEKLY:
                    {

                        periodInYears = TenureValue / 52;
                        //$"period in months {periodInMonths} from weeks {TenureValue} ".Dump();
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {

                        periodInYears = TenureValue / 26;
                        break;
                    }

                case Tenure.MONTHLY:
                    {

                        periodInYears = TenureValue / 12;
                        break;
                    }

                case Tenure.QUARTERLY:
                    {

                        periodInYears = TenureValue / 4;
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {

                        periodInYears = TenureValue / 2;
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        periodInYears = TenureValue * 1;

                        break;
                    }

                default: break;


            }

            return periodInYears;

        }

    }





    //	[NotMapped]
    //	public decimal PeriodInYears
    //	{
    //		get
    //		{
    //			decimal periodInYears = 1;
    //			periodInYears = (decimal)TenureValue / PeriodFrequencyPerAnnum;
    //			return periodInYears;
    //		}
    //
    //	}


    [NotMapped]
    public decimal TotalCompoundingPeriods
    {
        get
        {
            decimal periodTotal = 0;
            periodTotal = YearsInPeriod * CompoundingPeriodFrequencyPerAnnum;
            return periodTotal;
        }

    }



    [NotMapped]
    public decimal RatePerCent
    {
        get
        {
            return InterestRate / 100;
        }
    }


    [NotMapped]
    public decimal EffectiveAnnualRate
    {
        get
        {
            if (InterestRate <= 0) return 0.0000000001m;
            var apr = (decimal)Math.Pow((double)(1 + PeriodicRate), PeriodFrequencyPerAnnum) - 1;
            return apr * 100;
        }
    }


    public decimal AnnualRate
    {
        get
        {

            decimal annualRate = RatePerCent / 12;
            return annualRate;
        }
    }

    public decimal CompoundingAnnualRate
    {
        get
        {

            decimal annualRate = RatePerCent / CompoundingPeriodFrequencyPerAnnum;
            return annualRate;
        }
    }


    [NotMapped]
    public decimal PeriodicRate
    {
        get
        {
            decimal periodicRate = 0;
            periodicRate = RatePerCent / PeriodFrequencyPerAnnum;
            return periodicRate;
        }

    }



    public decimal EMI
    {
        get
        {

            var emi = Principal.ToDouble() * AnnualRate.ToDouble() *
    (Math.Pow(1 + AnnualRate.ToDouble(), MonthsInPeriod.ToDouble()) /
    (Math.Pow(1 + AnnualRate.ToDouble(), MonthsInPeriod.ToDouble()) - 1));

            return emi.ToDecimal();
        }
    }



    [NotMapped]
    public decimal SimpleInterestFlat
    {
        get
        {
            decimal simpleInterest = 0;
            simpleInterest = Principal * RatePerCent * YearsInPeriod;
            return simpleInterest;
        }

    }


    public decimal SimpleInterestReducing
    {
        get
        {

            decimal periodInterest, periodPrincipal, interestPayable = 0, principalBalance = Principal;

            for (int count = 1; count <= MonthsInPeriod; count++)
            {
                var simpleInterest = CalculateReducingInterestOnBalance(principalBalance);
                periodInterest = decimal.Round(simpleInterest / MonthsInPeriod, 3);
                interestPayable += periodInterest;

                periodPrincipal = EMI - periodInterest;
                principalBalance = principalBalance - periodPrincipal;

            }

            return interestPayable;
        }
    }

    public decimal CalculateReducingInterestOnBalance(decimal principalBalance)
    {
        return principalBalance * RatePerCent * YearsInPeriod;
    }


    public decimal CompoundInterest
    {
        get
        {
            var fv = Principal.ToDouble() * Math.Pow((1 + CompoundingAnnualRate).ToDouble(), TotalCompoundingPeriods.ToDouble());
            return fv.ToDecimal() - Principal;
        }
    }


    [NotMapped]
    public decimal FutureValue
    {
        get
        {
            decimal futureValue = 0;

            if (InterestMethod == InterestMethod.SIMPLE)
            {
                decimal simpleInterest = 0;
                if (InterestCalculationMethod == InterestCalculationMethod.FLAT_RATE)
                    simpleInterest = SimpleInterestFlat;
                else
                    simpleInterest = SimpleInterestReducing;

                futureValue = simpleInterest + Principal;
            }
            else
            {
                futureValue = CompoundInterest + Principal;
            }

            return futureValue;
        }

    }




    [NotMapped]
    public decimal InterestEarned
    {
        get
        {
            decimal interestEarned = 0;

            interestEarned = FutureValue - Principal;

            //$"interest earned calculation for acc {AccountNo}->{interestEarned}".Dump();
            return interestEarned;
        }

    }


    //[NotMapped]
    //public DateTimeOffset EffectiveDate => DateCreated ?? DateTimeOffset.Now;

    //[NotMapped]
    //public DateTimeOffset FirstRepaymentDate { get; set; }


    [NotMapped]
    public DateTime FirstRepaymentDate
    {

        get
        {
            DateTime date = EffectiveDate.AddDays(1);

            switch (RepaymentPeriod)
            {
                case Tenure.DAILY_360:
                    {

                        date = EffectiveDate.AddDays(1);
                        break;
                    }

                case Tenure.DAILY_365:
                    {

                        date = EffectiveDate.AddDays(1);
                        break;
                    }

                case Tenure.DAILY_366:
                    {
                        date = EffectiveDate.AddDays(1);
                        break;
                    }


                case Tenure.WEEKLY:
                    {
                        date = EffectiveDate.AddDays(7);
                        break;
                    }

                case Tenure.BI_WEEKLY:
                    {
                        date = EffectiveDate.AddDays(14);
                        break;
                    }

                case Tenure.MONTHLY:
                    {
                        date = EffectiveDate.AddMonths(1);
                        break;
                    }

                case Tenure.QUARTERLY:
                    {
                        date = EffectiveDate.AddMonths(4);
                        break;
                    }

                case Tenure.SEMI_ANNUALLY:
                    {
                        date = EffectiveDate.AddMonths(6);
                        break;
                    }

                case Tenure.ANNUALLY:
                    {
                        date = EffectiveDate.AddMonths(12);
                        break;
                    }

                default: break;
            }

            return date;
        }


    }

    public DateTime CalculateNextRepaymentDate(int paymentNo = 1)
    {
        DateTime date = EffectiveDate.AddDays(1);

        switch (RepaymentPeriod)
        {
            case Tenure.DAILY_360:
                {

                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }

            case Tenure.DAILY_365:
                {

                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }

            case Tenure.DAILY_366:
                {
                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }


            case Tenure.WEEKLY:
                {
                    date = EffectiveDate.AddDays(7 * paymentNo);
                    break;
                }

            case Tenure.BI_WEEKLY:
                {
                    date = EffectiveDate.AddDays(14 * paymentNo);
                    break;
                }

            case Tenure.MONTHLY:
                {
                    date = EffectiveDate.AddMonths(1 * paymentNo);
                    break;
                }

            case Tenure.QUARTERLY:
                {
                    date = EffectiveDate.AddMonths(4 * paymentNo);
                    break;
                }

            case Tenure.SEMI_ANNUALLY:
                {
                    date = EffectiveDate.AddMonths(6 * paymentNo);
                    break;
                }

            case Tenure.ANNUALLY:
                {
                    date = EffectiveDate.AddMonths(12 * paymentNo);
                    break;
                }

            default: break;
        }

        return date;
    }


    public DateTime CalculateNextRepaymentDate(Tenure period, int paymentNo = 1)
    {
        DateTime date = EffectiveDate.AddDays(1);

        switch (period)
        {
            case Tenure.DAILY_360:
                {
                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }

            case Tenure.DAILY_365:
                {

                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }

            case Tenure.DAILY_366:
                {
                    date = EffectiveDate.AddDays(1 * paymentNo);
                    break;
                }

            case Tenure.WEEKLY:
                {
                    date = EffectiveDate.AddDays(7 * paymentNo);
                    break;
                }

            case Tenure.BI_WEEKLY:
                {
                    date = EffectiveDate.AddDays(14 * paymentNo);
                    break;
                }

            case Tenure.MONTHLY:
                {
                    date = EffectiveDate.AddMonths(1 * paymentNo);
                    break;
                }

            case Tenure.QUARTERLY:
                {
                    date = EffectiveDate.AddMonths(4 * paymentNo);
                    break;
                }

            case Tenure.SEMI_ANNUALLY:
                {
                    date = EffectiveDate.AddMonths(6 * paymentNo);
                    break;
                }

            case Tenure.ANNUALLY:
                {
                    date = EffectiveDate.AddMonths(12 * paymentNo);
                    break;
                }

            default: break;
        }

        return date;
    }

    public DateTime CalculateLoanRepaymentDueDate(DateTimeOffset commencementDate, Tenure period, int tenure = 1)
    {
        switch (period)
        {
            case Tenure.DAILY_360:
                {
                    commencementDate = commencementDate.AddDays(1 * tenure);
                    break;
                }

            case Tenure.DAILY_365:
                {

                    commencementDate = commencementDate.AddDays(1 * tenure);
                    break;
                }

            case Tenure.DAILY_366:
                {
                    commencementDate = commencementDate.AddDays(1 * tenure);
                    break;
                }

            case Tenure.WEEKLY:
                {
                    commencementDate = commencementDate.AddDays(7 * tenure);
                    break;
                }

            case Tenure.BI_WEEKLY:
                {
                    commencementDate = commencementDate.AddDays(14 * tenure);
                    break;
                }

            case Tenure.MONTHLY:
                {
                    commencementDate = commencementDate.AddMonths(1 * tenure);
                    break;
                }

            case Tenure.QUARTERLY:
                {
                    commencementDate = commencementDate.AddMonths(4 * tenure);
                    break;
                }

            case Tenure.SEMI_ANNUALLY:
                {
                    commencementDate = commencementDate.AddMonths(6 * tenure);
                    break;
                }

            case Tenure.ANNUALLY:
                {
                    commencementDate = commencementDate.AddMonths(12 * tenure);
                    break;
                }

            default: break;
        }

        return commencementDate.DateTime;
    }
}

