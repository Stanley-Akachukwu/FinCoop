using AP.ChevronCoop.AppDomain.Payroll.PayrollDeductionSchedule;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Payroll.PayrollDeductionSchedules;

public class UpdatePayrollDeductionScheduleCommandValidator : AbstractValidator<UpdatePayrollDeductionScheduleCommand>
{
    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdatePayrollDeductionScheduleCommandValidator> logger;

    public UpdatePayrollDeductionScheduleCommandValidator(ChevronCoopDbContext appDbContext,
      ILogger<UpdatePayrollDeductionScheduleCommandValidator> _logger)
    {
        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.ScheduleName).NotEmpty().MaximumLength(512);
        RuleFor(p => p.ScheduleType).NotNull();


        //RuleFor(p => p.DeductionsCount).NotNull();
        //RuleFor(p => p.TotalDeductions).NotNull();
        RuleFor(p => p.MinDecimalPlace).NotNull();
        RuleFor(p => p.MaxDecimalPlace).NotNull();
        RuleFor(p => p.AdviseDate).NotNull();
        RuleFor(p => p.ExpectedDate).NotNull();
       // RuleFor(p => p.IsPosted).NotNull();
        RuleFor(p => p.PayrollDate).NotNull();
        //RuleFor(p => p.IsUploaded).NotNull();
        //RuleFor(p => p.LastUploadedDate).NotNull();
        //RuleFor(p => p.IsProcessed).NotNull();
       // RuleFor(p => p.ProcessedDate).NotNull();
       // RuleFor(p => p.GenerateDeductionCronJobStatus).NotNull();


        //RuleFor(p => p.ProcessDeductionCronJobStatus).NotNull();


        RuleFor(p => p).Custom((data, context) =>
        {
            var checkId = dbContext.PayrollDeductionSchedules.Where(r => r.Id == data.Id).Any();
            if (!checkId)
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));

            /*
          var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
          if (!parentExists)
          {
              context.AddFailure(
              new ValidationFailure(nameof(data.ParentId),
              "Invalid key.", data.ParentId));

          }

          var checkName = dbContext.PayrollDeductionSchedules.Where(r => r.Id != data.Id &&
          r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
          if (checkName)
          {
              context.AddFailure(
              new ValidationFailure(nameof(data.Name),
              "Duplicate names are not allowed.", data.Name));
          }

          var checkCode = dbContext.PayrollDeductionSchedules.Where(r => r.Id != data.Id &&
          r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
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