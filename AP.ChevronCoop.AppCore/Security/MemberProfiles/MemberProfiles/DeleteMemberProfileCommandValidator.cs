using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;

public partial class DeleteMemberProfileCommandValidator : AbstractValidator<DeleteMemberProfileCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteMemberProfileCommandValidator> logger;
    public DeleteMemberProfileCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteMemberProfileCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(x => x.Id)
          .NotEmpty().WithMessage("Id is required.");

        RuleFor(p => p).Custom((data, context) =>
        {
            var checkId = dbContext.MemberProfiles.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }
        });
    }
}
