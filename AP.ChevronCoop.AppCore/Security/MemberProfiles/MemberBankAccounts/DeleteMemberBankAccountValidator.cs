using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBankAccounts

{
    public class DeleteMemberBankAccountValidator : AbstractValidator<DeleteMemberBankAccountCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteMemberBankAccountValidator> logger;
        public DeleteMemberBankAccountValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteMemberBankAccountValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(p => p).Custom((data, context) =>
            {
                var checkId = dbContext.MemberBankAccounts.Any(r => r.Id == data.Id);
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