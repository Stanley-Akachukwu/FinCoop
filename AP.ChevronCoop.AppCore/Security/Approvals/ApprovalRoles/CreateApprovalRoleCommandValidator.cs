using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalRoles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalRoles
{
    public partial class CreateApprovalRoleCommandValidator : AbstractValidator<CreateApprovalRoleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApprovalRoleCommandValidator> logger;
        public CreateApprovalRoleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApprovalRoleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.EventGlobalCodeId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.RoleId).NotEmpty().MaximumLength(900);
            RuleFor(p => p.Order).NotNull();






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

                        var checkName = dbContext.ApprovalRoles.Where(r => r.Name.ToLower() == data.Name.ToLower()
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApprovalRoles.Where(r => r.Code.ToLower() == data.Code.ToLower()
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




}
