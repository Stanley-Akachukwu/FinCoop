namespace AP.ChevronCoop.AppDomain.Security.Approvals.Approvals;

public class WorkflowSetupApprovalModel
{
	public string ApprovalWorkflowId { get; set; }
}


public class LoanApplicationApprovalModel
{
	public string LoanApplicationId { get; set; }
	public string DefaultCurrencyId { get; set; }
}