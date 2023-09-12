namespace AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;

public class LoanProductApprovalModel
{
	public string ProductId { get; set; }
}

public class LoanProductApplicationApprovalModel
{
	public string ProductId { get; set; }
	public string LoanApplicationId { get; set; }
}