using AP.ChevronCoop.Entities;
using ChevronCoop.Web.AppUI.BlazorServer.Data;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class LoanProductBasicInformationValidator : AbstractValidator<CreateLoanProductBasicInfoDTO>
    {
        public LoanProductBasicInformationValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(p => p.Name).Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Product Name is not valid");
            RuleFor(p => p.Code).NotEmpty().WithMessage("Product Code is required");
            RuleFor(p => p.Code).Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Product Code is not valid");
            RuleFor(p => p.TenureUnit).NotEmpty().WithMessage("Tenue is required");
            RuleFor(p => p.MaxTenureValue).NotEmpty().WithMessage("Max Tenue Value is required");
            RuleFor(p => p.MinTenureValue).NotEmpty().WithMessage("Min Tenue Value is required");
            RuleFor(p => p.DefaultCurrencyId).NotEmpty().WithMessage("Currency is required");
            RuleFor(p => p.PrincipalMultiple).NotEmpty().WithMessage("Principal Multiple is required");
            RuleFor(p => p.PrincipalMinLimit).NotEmpty().WithMessage("Minimum Principal is required");
            RuleFor(p => p.PrincipalMaxLimit).NotEmpty().WithMessage("Maximum Principal is required");
            RuleFor(p => p.MaxTenureValue).GreaterThan(p => p.MinTenureValue).WithMessage("Maximum Tenor must be greater than Minimum Tenor");
            RuleFor(p => p.PrincipalMaxLimit).GreaterThan(p => p.PrincipalMinLimit).WithMessage("Maximum Principal Limit must be greater than Minimum Principal Limit");
            //RuleFor(p => p.InterestMethod).NotEmpty().WithMessage("Interest Method is required");
            RuleFor(p => p.InterestRate).NotEmpty().WithMessage("Interest Rate is required");
            RuleFor(p => p.QualificationTargetProduct).NotEmpty().WithMessage("Qualification Target Product is required");
            RuleFor(p => p.QualificationMinBalancePercentage).NotEmpty().WithMessage("Qualification Min Balance Percentage is required");
            RuleFor(p => p.MaxTenureValue).GreaterThanOrEqualTo(1).WithMessage("max tenor cannot be less than 1");
            RuleFor(p => p.PrincipalMultiple).GreaterThanOrEqualTo(1).WithMessage("principal multiple cannot be less than 1");
            RuleFor(p => p.PrincipalMinLimit).GreaterThanOrEqualTo(1).WithMessage("min principal cannot be less than 1");
            RuleFor(p => p.PrincipalMaxLimit).GreaterThanOrEqualTo(1).WithMessage("max principal cannot be less than 1");
            RuleFor(p => p.MinTenureValue).GreaterThanOrEqualTo(1).WithMessage("min tenor cannot be less than 1");
            RuleFor(p => p.InterestRate).GreaterThanOrEqualTo(0).WithMessage("interest rate cannot be less than 0");
            RuleFor(p => p.QualificationMinBalancePercentage).GreaterThanOrEqualTo(0).WithMessage("Qualification MinBalance Percentage cannot be less than 0");
            RuleFor(p => p.LoanProductType).NotEmpty().WithMessage("Loan Product Type is required");
            RuleFor(p => p.PayrollCode).NotEmpty().WithMessage("Payroll Code is required");
            RuleFor(p => p.PayrollCode).Matches("^\\d{4}$").WithMessage("Payroll Code is not valid");
            RuleFor(p => p.CompanyDepositAccountId).NotEmpty().WithMessage("Company Deposit Account is required");
            RuleFor(p => p.CompanyDisbursementAccountId).NotEmpty().WithMessage("Company Disbursement Account is required");




        }
    }
    public class LoanProductTargetSetupValidator : AbstractValidator<CreateLoanProductTargetSetupDTO>
    {
        public LoanProductTargetSetupValidator()
        {

            RuleFor(x => x.BenefitCode).NotEmpty().When(x => x.IsTargetLoan).WithMessage("Benefit Code is required")
           .DependentRules(() =>
           {
               RuleFor(x => x.BenefitCode).Matches("^[a-zA-Z0-9\\s]+$").WithMessage("Invalid Benefit code").When(x => !string.IsNullOrEmpty(x.BenefitCode));
           });




        }
    }
    public class LoanProductSetInfoValidator : AbstractValidator<CreateLoanProductOffSetInfoDTO>
    {
        public LoanProductSetInfoValidator()
        {

            RuleFor(x => x.OffsetPeriodUnit).NotEmpty().When(x => x.AllowedOffsetType != AllowedOffsetType.NONE.ToString()).WithMessage("Off Set Product Unit is required")
           .DependentRules(() =>
           {
               RuleFor(x => x.OffsetPeriodUnit).NotEmpty().WithMessage("Invalid Off Set Product Unit").When(x => !string.IsNullOrEmpty(x.OffsetPeriodUnit));
           });
            RuleFor(x => x.OffsetPeriodValue).NotEmpty().When(x => x.AllowedOffsetType != AllowedOffsetType.NONE.ToString()).WithMessage("Off Set Product value is required");

            RuleFor(x => x.SavingsOffSets).NotEmpty().When(x => x.EnableSavingsOffset).WithMessage("Savings OffSets must contain atleast one charge");
            RuleFor(x => x.WaivedCharges).NotEmpty().When(x => x.EnableChargeWaiver).WithMessage("Charge Waiver must contain atleast one charge");
            RuleFor(p => p.OffsetPeriodValue).GreaterThanOrEqualTo(0).WithMessage("Offset Period Value cannot be less than ");

        }
    }
    public class LoanProductTopUpValidator : AbstractValidator<CreateLoanProductTopUpDTO>
    {
        public LoanProductTopUpValidator()
        {

            RuleFor(x => x.TopUpCharges).NotEmpty().When(x => x.EnableTopUpCharges).WithMessage("Top up charge is required");

        }
    }
    public class LoanProductWhenDueValidator : AbstractValidator<CreateLoanProductWhenDueDTO>
    {
        public LoanProductWhenDueValidator()
        {

            RuleFor(x => x.WaitingPeriodUnit).NotEmpty().When(x => x.EnableWaitingPeriod).WithMessage("Waiting Period Unit is required");
            RuleFor(x => x.WaitingPeriodValue).NotEmpty().When(x => x.EnableWaitingPeriod).WithMessage("Waiting Period value is required");
            RuleFor(p => p.WaitingPeriodValue).GreaterThanOrEqualTo(0).WithMessage("Waiting Period Value cannot be less than 0");
            RuleFor(x => x.WaitingPeriodCharges).NotEmpty().When(x => x.EnableWaitingPeriodCharge).WithMessage("Waiting Period Charge is required");

        }
    }
    public class LoanProductGuarantorValidator : AbstractValidator<CreateLoanProductGuarantorDTO>
    {
        public LoanProductGuarantorValidator()
        {

            RuleFor(x => x.GuarantorMinYear).NotEmpty().When(x => x.IsGuarantorRequired).WithMessage("Guarantor Min Year is required");
            RuleFor(x => x.GuarantorAmountLimit).NotEmpty().When(x => x.IsGuarantorRequired).WithMessage("Guarantor Amount is required");
            RuleFor(p => p.GuarantorMinYear).GreaterThanOrEqualTo(0).WithMessage("Guarantor Min Year cannot be less than 0");
            RuleFor(p => p.EmployeeGuarantorCount).GreaterThanOrEqualTo(0).WithMessage("Employee Guarantor count cannot be less than 0");
            RuleFor(p => p.NonEmployeeGuarantorCount).GreaterThanOrEqualTo(0).WithMessage("NonEmployee Guarantor count cannot be less than 0");
            //RuleFor(x => x.EmployeeGuarantorCount).NotEmpty().When(x => x.IsGuarantorRequired).WithMessage("Waiting Period Charge is required");

        }
    }
    //public class LoanProductWorkFlowValidator : AbstractValidator<CreateLoanProductCommand>
    //{
    //    public LoanProductWorkFlowValidator()
    //    {

    //        RuleFor(x => x.ApprovalWorkFlowId).NotEmpty().WithMessage("Approval workflow is required");


    //    }
    //}
}
