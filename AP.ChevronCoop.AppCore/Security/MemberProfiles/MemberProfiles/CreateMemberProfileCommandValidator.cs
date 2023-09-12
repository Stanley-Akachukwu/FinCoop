using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;

public partial class CreateMemberProfileCommandValidator : AbstractValidator<CreateMemberProfileCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateMemberProfileCommandValidator> logger;
    public CreateMemberProfileCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateMemberProfileCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        //RuleFor(m => m.MemberType)
        //    .NotEmpty().WithMessage("MemberType is required")
        //    .MaximumLength(64).WithMessage("MemberType must not exceed 64 characters");

        // RuleFor(m => m.Status)
        //     .NotEmpty().WithMessage("Status is required")
        //     .MaximumLength(64).WithMessage("Status must not exceed 64 characters");

        RuleFor(m => m.Gender)
            .NotEmpty().WithMessage("Gender is required")
            .MaximumLength(64).WithMessage("Gender must not exceed 64 characters");

        // RuleFor(m => m.ProfileImageUrl)
        //     .MaximumLength(256).WithMessage("ProfileImageUrl must not exceed 256 characters");

        RuleFor(m => m.LastName)
            .MaximumLength(256).WithMessage("LastName must not exceed 256 characters");

        RuleFor(m => m.MiddleName)
            .MaximumLength(256).WithMessage("MiddleName must not exceed 256 characters");

        RuleFor(m => m.FirstName)
            .MaximumLength(256).WithMessage("FirstName must not exceed 256 characters");

        //RuleFor(m => m.PrimaryPhone)
        //    .MaximumLength(128).WithMessage("PrimaryPhone must not exceed 128 characters");

        //RuleFor(m => m.SecondaryPhone)
        //    .MaximumLength(128).WithMessage("SecondaryPhone must not exceed 128 characters");

        //RuleFor(m => m.PrimaryEmail)
        //    .NotEmpty().WithMessage("PrimaryEmail is required")
        //    .MaximumLength(256).WithMessage("PrimaryEmail must not exceed 256 characters")
        //    .EmailAddress().WithMessage("PrimaryEmail must be a valid email address");

        //RuleFor(m => m.SecondaryEmail)
        //    .NotEmpty().WithMessage("SecondaryEmail is required")
        //    .MaximumLength(256).WithMessage("SecondaryEmail must not exceed 256 characters")
        //    .EmailAddress().WithMessage("SecondaryEmail must be a valid email address");

        // RuleFor(m => m.DepartmentId)
        //     .NotEmpty().WithMessage("DepartmentId is required")
        //     .MaximumLength(80).WithMessage("DepartmentId must not exceed 80 characters");

        RuleFor(p => p).Custom((data, context) =>
        {
            var applicationUserId = dbContext.ApplicationUsers.Any(r => r.Id == data.ApplicationUserId);
            if (!applicationUserId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ApplicationUserId),
                        "Selected application user does not exist", data.ApplicationUserId));
            }

            if (!string.IsNullOrEmpty(data.CAI))
            {
                var checkCAI = dbContext.MemberProfiles.Any(r => r.CAI != null && r.CAI.ToLower() == data.CAI.ToLower());
                if (checkCAI)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.CAI),
                            "Member with the provided CAI exists.", data.CAI));
                }
            }


            if (!string.IsNullOrEmpty(data.DepartmentId))
            {
                var checkDepartment = dbContext.Departments.Any(r => r.Id == data.DepartmentId);
                if (!checkDepartment)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.DepartmentId),
                            "Selected department Id does not exist", data.DepartmentId));
                }
            }

        });

    }


}
