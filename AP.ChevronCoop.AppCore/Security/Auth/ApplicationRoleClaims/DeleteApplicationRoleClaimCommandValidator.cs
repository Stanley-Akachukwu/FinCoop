using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationRoleClaims;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationRoleClaims
{
    public partial class DeleteApplicationRoleClaimCommandValidator : AbstractValidator<DeleteApplicationRoleClaimCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteApplicationRoleClaimCommandValidator> logger;
        public DeleteApplicationRoleClaimCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApplicationRoleClaimCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                int id = int.Parse(data.Id);

                var checkId = dbContext.ApplicationRoleClaims.Where(r => r.Id == id).Any();
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
