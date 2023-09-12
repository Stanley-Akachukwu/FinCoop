using AP.ChevronCoop.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class CreateLoanProductBasicInfoDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }
        public string PayrollCode { get; set; }

        public string DefaultCurrencyId { get; set; }

        public string ApplicationUserId { get; set; }

        public string ShortName { get; set; }

        public string PrincipalLimitType { get; set; }

        public decimal PrincipalMultiple { get; set; }

        public decimal PrincipalMinLimit { get; set; }

        public decimal PrincipalMaxLimit { get; set; }

        public string TenureUnit { get; set; }

        public decimal MinTenureValue { get; set; }

        public decimal MaxTenureValue { get; set; }
        public string QualificationTargetProduct { get; set; }

        public decimal QualificationMinBalancePercentage { get; set; }
        public string InterestMethod { get; set; }

        public decimal InterestRate { get; set; }

        public bool HasAdminCharges { get; set; }

        public bool IsTargetLoan { get; set; }

        public string BenefitCode { get; set; }
        public List<string> AdminCharges { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }
        public List<string> MemberTypes { get; set; }
        public string LoanProductType { get; set; }
        public bool EnableAdminOffsetCharge { get; set; }
        public List<string> OffsetAdminCharges { get; set; }
        public string CompanyDisbursementAccountId { get; set; }
        public string CompanyDepositAccountId { get; set; }
        public DaysInYear DaysInYear { get; set; }
        public string RepaymentPeriod { get; set; }
        public string InterestCalculationMethod { get; set; }
    }
}
