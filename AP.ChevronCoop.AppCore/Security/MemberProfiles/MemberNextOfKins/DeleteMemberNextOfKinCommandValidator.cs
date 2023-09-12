using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberNextOfKins;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberNextOfKins;

public class DeleteMemberNextOfKinCommandValidator : AbstractValidator<DeleteMemberNextOfKinCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<DeleteMemberNextOfKinCommandValidator> logger;
    public DeleteMemberNextOfKinCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteMemberNextOfKinCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.MemberNextOfKins.Any(r => r.Id == data.Id);
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