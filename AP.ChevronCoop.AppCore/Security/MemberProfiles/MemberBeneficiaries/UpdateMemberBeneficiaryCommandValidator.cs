using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBeneficiaries;

public class UpdateMemberBeneficiaryCommandValidator : AbstractValidator<UpdateMemberBeneficiaryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateMemberBeneficiaryCommandValidator> logger;
    public UpdateMemberBeneficiaryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateMemberBeneficiaryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();

        RuleFor(p => p.ProfileId).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(128);
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(128);
        RuleFor(p => p.Address).NotEmpty().MaximumLength(512);
        RuleFor(p => p.Phone).NotEmpty().MaximumLength(32);

        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.MemberBeneficiaries.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            // Check ProfileId Exists
            var checkProfileId = dbContext.MemberProfiles.Any(r => r.Id == data.ProfileId);
            if (!checkProfileId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ProfileId),
                        "Selected Profile Id does not exist", data.ProfileId));
            }


        });

    }


}