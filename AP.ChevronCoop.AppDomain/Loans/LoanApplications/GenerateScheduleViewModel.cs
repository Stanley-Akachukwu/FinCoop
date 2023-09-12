namespace AP.ChevronCoop.AppDomain.Loans.LoanApplications;

public class GenerateScheduleViewModel
{
    public bool IsApproved { get; set; }
    public List<AmortizationSchedule> Schedules { get; set; }
}

public class ScheduleModel
{
    public DateTime Date { get; set; }
    public decimal Principal { get; set; }
    public decimal Interest { get; set; }
    public decimal Balance { get; set; }
    public decimal Total { get; set; }
}