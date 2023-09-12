using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUsers
{
    public partial class DeleteApplicationUserCommandValidator : AbstractValidator<DeleteApplicationUserCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteApplicationUserCommandValidator> logger;
        public DeleteApplicationUserCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteApplicationUserCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApplicationUsers.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }
            });

        }


    }




}
