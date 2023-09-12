using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionItems;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollCronJobConfigs
{
    public partial class ImportPayrollDeductionItemCommandValidator : AbstractValidator<ImportPayrollDeductionItemCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<ImportPayrollDeductionItemCommandValidator> logger;
        public ImportPayrollDeductionItemCommandValidator(ChevronCoopDbContext appDbContext, ILogger<ImportPayrollDeductionItemCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleForEach(p => p.PayrollDeductionItems).ChildRules(child =>
            {
                child.RuleFor(p => p.PayrollDeductionScheduleId).NotEmpty().MaximumLength(80);
                child.RuleFor(p => p.MemberId).NotEmpty().MaximumLength(80);
                child.RuleFor(p => p.MemberName).NotEmpty().MaximumLength(240);
                // child.RuleFor(p => p.AccountNo).NotEmpty().MaximumLength(64);
                child.RuleFor(p => p.Amount).NotNull();
                child.RuleFor(p => p.PayrollCode).NotEmpty().MaximumLength(510);
                // child.RuleFor(p => p.Narration).NotEmpty().MaximumLength(510);
                child.RuleFor(p => p.PayrollDate).NotNull();
                // child.RuleFor(p => p.AccountDueDate).NotNull();
                // child.RuleFor(p => p.DeductionType).NotEmpty().MaximumLength(64);
            });

            RuleFor(p => p).Custom((data, context) =>
            {

                if (data.PayrollDeductionItems.Count < 1)
                    context.AddFailure(new ValidationFailure(nameof(data.PayrollDeductionItems), "Invalid list of items", data.PayrollDeductionItems));

                //foreach(var item in data.PayrollDeductionItems)
                //{
                //    if (data.PayrollDeductionItems.Where(p => p.EmployeeNo == item.EmployeeNo).ToList().Count > 1)
                //        context.AddFailure(new ValidationFailure(nameof(data.PayrollDeductionItems), "Duplicate not allowed", data.PayrollDeductionItems));
                //}

            });
        }


    }


}



