using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;

public partial class UpdateMemberProfileCommandValidator : AbstractValidator<UpdateMemberProfileCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateMemberProfileCommandValidator> logger;
    public UpdateMemberProfileCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateMemberProfileCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();

        RuleFor(m => m.ApplicationUserId)
            .NotEmpty().WithMessage("Application user is required")
            .MaximumLength(64).WithMessage("Application user Id should not exceed 64 characters.");

        // RuleFor(m => m.DepartmentId)
        //     .NotEmpty().WithMessage("Department is required")
        //     .MaximumLength(64).WithMessage("Department Id should not exceed 64 characters.");

        RuleFor(m => m.Status)
            .NotEmpty().WithMessage("Please provide a Status.")
            .MaximumLength(64).WithMessage("Status should not exceed 64 characters.");

        RuleFor(m => m.Gender)
            .NotEmpty().WithMessage("Please provide a Gender.")
            .MaximumLength(64).WithMessage("Gender should not exceed 64 characters.");

        // RuleFor(m => m.ProfileImageUrl)
        //     .MaximumLength(256).WithMessage("ProfileImageUrl should not exceed 256 characters.");

        RuleFor(m => m.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(256).WithMessage("LastName should not exceed 256 characters.");

        RuleFor(m => m.MiddleName)
            .MaximumLength(256).WithMessage("MiddleName should not exceed 256 characters.");

        RuleFor(m => m.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(256).WithMessage("FirstName should not exceed 256 characters.");

        // RuleFor(m => m.PrimaryPhone)
        //     .NotEmpty().WithMessage("Primary phone is required.")
        //     .MaximumLength(32).WithMessage("Primary phone should not exceed 128 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.MemberProfiles.Any(r => r.Id == data.Id);
            if (!checkId)
            {
                context.AddFailure(
                new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
            }

            var applicationUserId = dbContext.ApplicationUsers.Any(r => r.Id == data.ApplicationUserId);
            if (!applicationUserId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ApplicationUserId),
                        "Selected application user does not exist", data.ApplicationUserId));
            }

            if (!string.IsNullOrEmpty(data.CAI))
            {
                var checkCAI = dbContext.MemberProfiles.Any(r => r.CAI != null && (r.CAI.ToLower() == data.CAI.ToLower()) && r.Id != data.Id);
                if (checkCAI)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.CAI),
                            "Member with the provided CAI exists.", data.CAI));
                }
            }

            // if (!string.IsNullOrEmpty(data.DepartmentId))
            // {
            //     var checkDepartment = dbContext.Departments.Any(r => r.Id == data.DepartmentId);
            //     if (!checkDepartment)
            //     {
            //         context.AddFailure(
            //             new ValidationFailure(nameof(data.DepartmentId),
            //                 "Selected department Id does not exist", data.DepartmentId));
            //     }
            // }
        });

    }


}
