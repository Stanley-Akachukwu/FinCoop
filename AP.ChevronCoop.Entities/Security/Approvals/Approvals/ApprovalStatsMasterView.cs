namespace AP.ChevronCoop.Entities.Security.Approvals;

public class ApprovalStatsMasterView
{
	public long? RowNumber { get; set; }
	public int Size { get; set; }
	public string ApprovalType { get; set; }

	public string Description
	{
		get
		{
			switch (ApprovalType)
			{
				case nameof(Entities.ApprovalType.LOAN_PRODUCT):
					return "Loan products approval";
					break;

				case nameof(Entities.ApprovalType.DEPOSIT_PRODUCT):
					return "Deposit products approval";
					break;
				
				case nameof(Entities.ApprovalType.ADMIN_MEMBER):
					return "Admin member approval";
					break;
				
				case nameof(Entities.ApprovalType.KYC_COMPLETION):
					return "KYC completion approval";
					break;
				
				case nameof(Entities.ApprovalType.RETIREE_SWITCH):
					return "Retiree switch approval";
					break;
				
				case nameof(Entities.ApprovalType.MEMBER_BULK_UPLOAD):
					return "Member bulk approval";
					break;
				
				case nameof(Entities.ApprovalType.MEMBER_ENROLLMENT):
					return "Member enrollment approval";
					break;
				
				case nameof(Entities.ApprovalType.FINANCIAL_TRANSACTION):
					return "Financial transaction approval";
					break;
				
				default:
					return string.Empty;
					break;
					
			}
		}
	}
}