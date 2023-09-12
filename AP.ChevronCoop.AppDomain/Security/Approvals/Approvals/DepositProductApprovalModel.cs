namespace AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;

public class DepositProductApprovalModel
{
	public string ProductId { get; set; }
}

public class DepositProductApplicationApprovalModel
{
	public string ProductId { get; set; }
	public string LoanApplicationId { get; set; }
} 