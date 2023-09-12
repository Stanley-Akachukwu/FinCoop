using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBeneficiaries;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBeneficiaries;

public class CreateMemberBeneficiaryCommandValidator : AbstractValidator<CreateMemberBeneficiaryCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateMemberBeneficiaryCommandValidator> logger;
    public CreateMemberBeneficiaryCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateMemberBeneficiaryCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.ProfileId).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty().MaximumLength(128);
        RuleFor(p => p.FirstName).NotEmpty().MaximumLength(128);
        RuleFor(p => p.Address).NotEmpty().MaximumLength(512);
        RuleFor(p => p.Phone).NotEmpty().MaximumLength(32);

        RuleFor(p => p).Custom((data, context) =>
        {
            // Check ProfileId Exists
            var checkProfileId = dbContext.MemberProfiles.Any(r => r.Id == data.ProfileId);
            if (!checkProfileId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ProfileId),
                        "Selected Profile Id does not exist", data.ProfileId));
            }


            // Validate user can't add more than 5 beneficiary
            var beneficiaryCount = dbContext.MemberBeneficiaries.Count(x => x.ProfileId == data.ProfileId);
            if (beneficiaryCount >= 5)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ProfileId),
                        "Max number of beneficiary exceeded", data.ProfileId));
            }
        });
    }
}