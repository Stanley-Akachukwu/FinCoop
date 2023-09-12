namespace ChevronCoop.Web.AppUI.BlazorServer.Helper
{
    public static class DatabaseFields
    {
        public static string UserGridMemberProfileFields = "FullName,ApplicationUserId_Email,MembershipId,PrimaryPhone,Status,DateCreated,LastName,MiddleName,FirstName";

        public static string MemberProfileFields2 = "PrimaryEmail,Id,FullName,ApplicationUserId,MembershipId,ApplicationUserId_IsAdmin,LastName,MiddleName,FirstName";

        public static string LoanUserDashboard = "applicationNo,principal,tenureUnit,status,loanProductId_Name,loanProductId_TenureUnit,loanProductId_LoanProductType,loanProductId_Status,dateCreated";
        public static string LoanSchedulesProperties = "id,loanAccountId,repaymentNo,dueDate,periodPayment";
        public static string LoanProductProperties = "id,name,interestMethod,interestCalculationMethod,interestRate";

        public static string LedgerGrid = "";
    }
}
