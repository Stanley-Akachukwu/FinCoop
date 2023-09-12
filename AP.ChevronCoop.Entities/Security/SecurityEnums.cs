
namespace AP.ChevronCoop.Entities.Security
{
    public enum MemberType
    {
        ADMIN = 1, REGULAR = 2, EXPATRIATE = 3, RETIREE = 4, MFB = 5,
    }

    public enum MemberProfileStatus
    {
        DEACTIVATED = 1, 
        ACTIVE = 2, 
        SUSPENDED = 3, 
        PENDING_APPROVAL = 4, 
        AWAITING_KYC_COMPLETION = 5, 
        AWAITING_KYC_APPROVAL = 6, 
        APPROVED = 7, 
        REJECTED = 8, 
    }
	
}

