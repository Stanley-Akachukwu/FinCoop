using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserRoles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserRoles
{
    public partial class UpdateApplicationUserRoleCommandValidator : AbstractValidator<UpdateApplicationUserRoleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApplicationUserRoleCommandValidator> logger;
        public UpdateApplicationUserRoleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApplicationUserRoleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.UserId).NotEmpty().MaximumLength(40);
            RuleFor(p => p.RoleId).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {

                // var checkId = dbContext.ApplicationUserRoles.Where(r => r.I == data.Id ).Any();
                // if (!checkId)
                // {
                //     context.AddFailure(
                //     new ValidationFailure(nameof(data.Id),
                //     "Selected User does not exist", data.Id));
                // }

                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.ApplicationUserRoles.Where(r => r.Id != data.Id &&
                        r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationUserRoles.Where(r => r.Id != data.Id &&
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




}
