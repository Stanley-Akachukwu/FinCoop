using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoles
{
    public partial class CreateApplicationRoleCommandValidator : AbstractValidator<CreateApplicationRoleCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApplicationRoleCommandValidator> logger;
        public CreateApplicationRoleCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApplicationRoleCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.IsSystemRole).NotNull();
            RuleFor(p => p.Code).NotNull();
            RuleFor(p => p.Name).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {
                var checkName = dbContext.ApplicationRoles.Any(r => r.Name.ToLower() == data.Name.ToLower());
                if (checkName)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.ApplicationRoles.Any(r => r.Code.ToLower() == data.Code.ToLower());
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

                        
                */

            });

        }


    }




}
