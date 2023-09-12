using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public partial class DeleteApplicationUserLoginCommandValidator : AbstractValidator<DeleteApplicationUserLoginCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteApplicationUserLoginCommandValidator> logger;
        public DeleteApplicationUserLoginCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApplicationUserLoginCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            //RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                //var checkId = dbContext.ApplicationUserLogins.Where(r => r.Id == data.Id).Any();
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
