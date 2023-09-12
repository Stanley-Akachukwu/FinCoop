using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanApplication;
using ChevronCoop.Web.AppUI.BlazorServer.Data.LoanSetup;
using FluentValidation;

namespace ChevronCoop.Web.AppUI.BlazorServer.Validation
{

    public class LoanProductApplicationValidator : AbstractValidator<LoanApplicationDTO>
    {
        public LoanProductApplicationValidator()
        {
            RuleFor(p => p.LoanProductId).NotEmpty().WithMessage("Please select Preferred Loan Product Name");
            RuleFor(p => p.RepaymentPeriod).NotEmpty().WithMessage("Repayment Period is required");
            RuleFor(p => p.Amount).NotEmpty().WithMessage("Loan Amount is required");

            RuleFor(p => p.Amount).GreaterThanOrEqualTo(1).WithMessage("Loan Amount cannot be less than 1");
            RuleFor(p => p.RepaymentPeriod).GreaterThanOrEqualTo(1).WithMessage("RepaymentPeriod cannot be less than 1");
            //RuleFor(p => p.RepaymentCommencementDate)
            //.Must(ValidDate).WithMessage("Invalid Commencement Date");

            RuleFor(p => p.DestinationAccountId).NotEmpty().Unless(m => !string.IsNullOrEmpty(m.SpecialDepositId)).WithMessage("Please provide one Account");


        }
        protected bool ValidDate(DateTime date)
        {
            var currentDate = DateTime.Now;
            if (currentDate <= date)
            {
                return true;
            }

            return false;
        }
    }
    public class LoanApplicationTopUpValidator : AbstractValidator<LoanTopUpDTO>
    {
        public LoanApplicationTopUpValidator()
        {
            RuleFor(p => p.Principal).NotEmpty().WithMessage("Loan Amount is required");
            RuleFor(p => p.TopUpAmount).NotEmpty().WithMessage("Top Amount is required");
            RuleFor(p => p.Principal).GreaterThanOrEqualTo(1).WithMessage("Loan Top Up Amount cannot be less than 1");
            RuleFor(p => p.TopUpAmount).GreaterThanOrEqualTo(1).WithMessage("Loan Top Up Amount cannot be less than 1");
            RuleFor(p => p.TenureValue).GreaterThanOrEqualTo(1).WithMessage("Tenure cannot be less than 1");
            //RuleFor(p => p.CommencementDate).Must(ValidDate).WithMessage("Invalid Commencement Date");

            RuleFor(p => p.DestinationAccountId).NotEmpty().Unless(m => !string.IsNullOrEmpty(m.SpecialDepositId)).WithMessage("Please provide Disbursement Account");

        }
        protected bool ValidDate(DateTime date)
        {
            var currentDate = DateTime.Now;
            if (currentDate <= date)
            {
                return true;
            }

            return false;
        }
    }
    public class LoanApplicationOffSetValidator : AbstractValidator<LoanOffSetDTO>
    {
        public LoanApplicationOffSetValidator()
        {
            RuleFor(p => p.OffsetAmount).NotEmpty().WithMessage("Loan offset Amount is required");
            RuleFor(p => p.OffsetAmount).GreaterThanOrEqualTo(1).WithMessage("Loan offset Amount cannot be less than 1");
            RuleFor(p => p.AllowedOffsetType).NotEmpty().WithMessage("Offset Type is required");
        }
        protected bool ValidDate(DateTime date)
        {
            var currentDate = DateTime.Now;
            if (currentDate <= date)
            {
                return true;
            }

            return false;
        }
    }
    public class ApplianceDetailValidator : AbstractValidator<ApplianceDetails>
    {
        public ApplianceDetailValidator()
        {
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model is required");
            RuleFor(p => p.Color).NotEmpty().WithMessage("Color is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Brand name is required");



        }
    }
    public class HomeApplianceApplianceDetailValidator : AbstractValidator<ApplianceDetailDTO>
    {
        public HomeApplianceApplianceDetailValidator()
        {
            RuleFor(p => p.Model).NotEmpty().WithMessage("Model is required");
            RuleFor(p => p.Color).NotEmpty().WithMessage("Color is required");
            RuleFor(p => p.Name).NotEmpty().WithMessage("Item name is required");
            RuleFor(p => p.Amount).NotEmpty().WithMessage("Amount is required");
            RuleFor(p => p.Amount).GreaterThanOrEqualTo(1).WithMessage("Amount cannot be less than 1");
            RuleFor(p => p.BrandName).NotEmpty().WithMessage("Brand Name is required");



        }
    }
}
