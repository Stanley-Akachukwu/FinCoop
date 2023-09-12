using System.ComponentModel.DataAnnotations;

namespace ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup.Edit
{

    public class EditLoanProductBasicInfoDTO
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string DefaultCurrencyId { get; set; }

        public string ApplicationUserId { get; set; }

        public string ShortName { get; set; }

        public string PrincipalLimitType { get; set; }

        public decimal PrincipalMultiple { get; set; }

        public decimal PrincipalMinLimit { get; set; }

        public decimal PrincipalMaxLimit { get; set; }

        public string TenureUnit { get; set; }

        public int MinTenureValue { get; set; }

        public int MaxTenureValue { get; set; }
        public string QualificationTargetProduct { get; set; }

        public decimal QualificationMinBalancePercentage { get; set; }
        public string InterestMethod { get; set; }

        public decimal InterestRate { get; set; }

        public bool HasAdminCharges { get; set; }

        public bool IsTargetLoan { get; set; }

        public string BenefitCode { get; set; }
        public List<ChargeLookup> AdminCharges { get; set; }
        [MaxLength(512)]
        public string? Description { get; set; }
    }
    public class ChargeLookup
    {
        public string ProductId { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
