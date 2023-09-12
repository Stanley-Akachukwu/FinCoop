using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalRoles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalRoles
{
    public partial class DeleteApprovalRoleCommandValidator : AbstractValidator<DeleteApprovalRoleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteApprovalRoleCommandValidator> logger;
        public DeleteApprovalRoleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApprovalRoleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApprovalRoles.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                /*
                var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
                        if (checkChild)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Id),
                            "Selected record has dependent records and cannot be deleted", data.Id));

                        }
                */

            });

        }


    }




}
