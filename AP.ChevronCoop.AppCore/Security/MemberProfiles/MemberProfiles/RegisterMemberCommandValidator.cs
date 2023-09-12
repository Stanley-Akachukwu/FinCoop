using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;

public class RegisterMemberCommandValidator : AbstractValidator<RegisterMemberCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<RegisterMemberCommandValidator> logger;

    public RegisterMemberCommandValidator(ChevronCoopDbContext appDbContext,
        ILogger<RegisterMemberCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(m => m.Email)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(128);

        RuleFor(m => m.LastName)
            .MaximumLength(256).WithMessage("LastName must not exceed 256 characters");

        RuleFor(m => m.FirstName)
            .MaximumLength(256).WithMessage("FirstName must not exceed 256 characters");

        RuleFor(m => m.Password)
            .MaximumLength(256).WithMessage("Password must not exceed 256 characters");

        RuleFor(m => m.MembershipId)
            .Matches("^[a-zA-Z0-9]*$").WithMessage("Membership Id must only contain alpha-numeric characters")
            .MaximumLength(10).WithMessage("Membership Id must not exceed 10 characters");


        RuleFor(p => p).Custom((data, context) =>
        {
            var checkMembershipId = dbContext.MemberProfiles.Any(r => r.MembershipId != null && r.MembershipId.ToLower() == data.MembershipId.ToLower());
            if (checkMembershipId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.MembershipId),
                        "Member with the provided membership ID exists.", data.MembershipId));
            }

            var checkEmail = dbContext.MemberProfiles.Any(r => r.PrimaryEmail != null && r.PrimaryEmail.ToLower() == data.Email.ToLower());
            if (checkEmail)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.Email),
                        "Member with the provided email exists.", data.Email));
            }
        });
    }
}