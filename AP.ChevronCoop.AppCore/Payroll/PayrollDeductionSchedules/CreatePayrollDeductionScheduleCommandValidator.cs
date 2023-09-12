using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedules;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionSchedules;

public class CreatePayrollDeductionScheduleCommandValidator : AbstractValidator<CreatePayrollDeductionScheduleCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreatePayrollDeductionScheduleCommandValidator> logger;

    public CreatePayrollDeductionScheduleCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<CreatePayrollDeductionScheduleCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.ScheduleName).NotEmpty().MaximumLength(512);
        RuleFor(p => p.ScheduleType).NotNull();


        //RuleFor(p => p.DeductionsCount).NotNull();
        //RuleFor(p => p.TotalDeductions).NotNull();
        RuleFor(p => p.MinDecimalPlace).NotNull();
        RuleFor(p => p.MaxDecimalPlace).NotNull();
        RuleFor(p => p.AdviseDate).NotNull();
        RuleFor(p => p.ExpectedDate).NotNull();
        //RuleFor(p => p.IsPosted).NotNull();
        RuleFor(p => p.PayrollDate).NotNull();

        RuleFor(p => p.SpecialDepositBankAccountId).NotEmpty().NotEqual("string").NotNull();
        RuleFor(p => p.FixedDepositBankAccountId).NotEmpty().NotEqual("string").NotNull();
        RuleFor(p => p.BankAccountId).NotEmpty().NotEqual("string").NotNull();

        //RuleFor(p => p.IsUploaded).NotNull();
        //RuleFor(p => p.LastUploadedDate).NotNull();
        //RuleFor(p => p.IsProcessed).NotNull();
        //RuleFor(p => p.ProcessedDate).NotNull();
        //RuleFor(p => p.GenerateDeductionCronJobStatus).NotNull();
        //RuleFor(p => p.ProcessDeductionCronJobStatus).NotNull();


        RuleFor(p => p).Custom((data, context) =>
        {
            /*
                  var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                  if (!parentExists)
                  {
                      context.AddFailure(
                      new ValidationFailure(nameof(data.ParentId),
                      "Invalid key.", data.ParentId));

                  }

                  var checkName = dbContext.PayrollDeductionSchedules.Where(r => r.Name.ToLower() == data.Name.ToLower() 
          && r.CodeTypeId == data.CodeTypeId).Any();
                  if (checkName)
                  {
                      context.AddFailure(
                      new ValidationFailure(nameof(data.Name),
                      "Duplicate names are not allowed.", data.Name));
                  }

                  var checkCode = dbContext.PayrollDeductionSchedules.Where(r => r.Code.ToLower() == data.Code.ToLower() 
          && r.CodeTypeId != data.CodeTypeId).Any();
                  if (checkCode)
                  {
                      context.AddFailure(
                      new ValidationFailure(nameof(data.Code),
                      "Duplicate codes are not allowed.", data.Code));
                  }
          */
        });
    }
}