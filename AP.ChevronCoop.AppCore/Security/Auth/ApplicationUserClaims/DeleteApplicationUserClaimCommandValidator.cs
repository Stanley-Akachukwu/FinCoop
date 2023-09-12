using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserClaims
{
    public partial class DeleteApplicationUserClaimCommandValidator : AbstractValidator<DeleteApplicationUserClaimCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteApplicationUserClaimCommandValidator> logger;
        public DeleteApplicationUserClaimCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApplicationUserClaimCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            //RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                //var checkId = dbContext.ApplicationUserClaims.Where(r => r.Id == data.Id).Any();
                //if (!checkId)
                //{
                //    context.AddFailure(
                //    new ValidationFailure(nameof(data.Id),
                //    "Selected Id does not exist", data.Id));
                //}

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
