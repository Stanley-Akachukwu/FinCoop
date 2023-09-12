using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoles
{
    public partial class UpdateApplicationRoleCommandValidator : AbstractValidator<UpdateApplicationRoleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApplicationRoleCommandValidator> logger;
        public UpdateApplicationRoleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApplicationRoleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.IsSystemRole).NotNull();
            RuleFor(p => p.Code).NotNull();
            RuleFor(p => p.Name).NotNull();




            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApplicationRoles.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                var checkName = dbContext.ApplicationRoles
                    .Any(r => r.Name.ToLower() == data.Name.ToLower() && r.Id != data.Id);
                if (checkName)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.ApplicationRoles
                    .Any(r => r.Code.ToLower() == data.Code.ToLower() && r.Id != data.Id);
                if (checkCode)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.Code),
                            "Duplicate codes are not allowed.", data.Code));
                }

                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.ApplicationRoles.Where(r => r.Id != data.Id &&
                        r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationRoles.Where(r => r.Id != data.Id &&
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
