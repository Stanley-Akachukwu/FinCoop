using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoleClaims
{
    public partial class CreateApplicationRoleClaimCommandValidator : AbstractValidator<CreateApplicationRoleClaimCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApplicationRoleClaimCommandValidator> logger;
        public CreateApplicationRoleClaimCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApplicationRoleClaimCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.RoleId).NotEmpty().MaximumLength(900);



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

                        var checkName = dbContext.ApplicationRoleClaims.Where(r => r.Name.ToLower() == data.Name.ToLower()
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationRoleClaims.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
