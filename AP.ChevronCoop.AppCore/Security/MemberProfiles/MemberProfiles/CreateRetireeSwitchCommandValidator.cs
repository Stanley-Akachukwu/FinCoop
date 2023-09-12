using AP.ChevronCoop.AppCore.Employees;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles
{

    public partial class CreateRetireeSwitchCommandValidator : AbstractValidator<CreateRetireeSwitchCommand>
    {

        private readonly ChevronCoopDbContext _dbContext;
        private readonly ILogger<CreateEmployeeCommandValidator> _logger;
        public CreateRetireeSwitchCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateEmployeeCommandValidator> logger)
        {
            _dbContext = appDbContext;
            _logger = logger;

            RuleFor(e => e.MemberProfileId).NotEmpty().NotNull().WithMessage("Member profile required.");
            RuleFor(e => e.Description).NotEmpty().NotNull().WithMessage("Description is required.");
            //RuleFor(e => e.ApprovalWorkflowId).NotEmpty().NotNull().WithMessage("Required Approval Workflow ID.");
            RuleFor(e => e.InitiatedBy).NotEmpty().NotNull().WithMessage("Required initiator's User ID.");

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkId = _dbContext.MemberProfiles.Any(r => r.Id == data.MemberProfileId);
                if (!checkId)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.MemberProfileId),
                            "Selected Id does not exist", data.MemberProfileId));
                }

                var entity = _dbContext.MemberProfiles.FirstOrDefault(r => r.Id == data.MemberProfileId);
                if (entity!.MemberType == MemberType.RETIREE)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.MemberProfileId),
                            "Selected member is already retired!", data.MemberProfileId));
                }
                
                if (entity.SwitchToRetireeRequested)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.MemberProfileId),
                            "Switch to retiree request already submitted", data.MemberProfileId));
                }

            });

        }


    }

}
