using AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBulkUploads;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBulkUploads
{
    public class ValidateMemberBulkUploadCommandValidator : AbstractValidator<ValidateMemberBulkUploadCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateMemberProfileCommandValidator> logger;
        public ValidateMemberBulkUploadCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateMemberProfileCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleForEach(p => p.MemberDataUploads).ChildRules(child => {
                child.RuleFor(m => m.Status)
             .NotEmpty().WithMessage("Status is required")
             .MaximumLength(64).WithMessage("Status must not exceed 64 characters");

                child.RuleFor(m => m.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(s => s.Equals(nameof(Gender.MALE)) || s.Equals(nameof(Gender.FEMALE)) || s.Equals(nameof(Gender.UNKNOWN)))
                .WithMessage("Invalid gender");

                child.RuleFor(m => m.FirstName).NotEmpty().WithMessage("FirstName is required")
                .MinimumLength(2).WithMessage("Invalid firstname. Firstname shouldn't be less than 2 characters.")
                .MaximumLength(256).WithMessage("FirstName must not exceed 256 characters");

                child.RuleFor(m => m.LastName).NotEmpty().WithMessage("LastName is required")
                .MinimumLength(2).WithMessage("Invalid lastname. lastname shouldn't be less than 2 characters.")
                .MaximumLength(256).WithMessage("LastName must not exceed 256 characters");

                child.RuleFor(m => m.Email)
                .NotEmpty().WithMessage("PrimaryEmail is required")
                .MaximumLength(256).WithMessage("PrimaryEmail must not exceed 256 characters")
                .EmailAddress().WithMessage("PrimaryEmail must be a valid email address");

                child.RuleFor(m => m.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required")
                .MaximumLength(128).WithMessage("PrimaryPhone must not exceed 128 characters");

                child.RuleFor(m => m.MembershipNumber).NotEmpty().WithMessage("MembershipNumber is required")
                .MaximumLength(10).WithMessage("Invalid membership number. Length should not exceed 10");

               
            });

            RuleFor(m => m).Custom(async (data, context) =>
            {
                var MemberDataUploads = data.MemberDataUploads;
                for (int i = 0; i < MemberDataUploads.Count-1; i++)
                {
                    if (!string.IsNullOrEmpty(MemberDataUploads[i].MemberType.ToString()))
                    {
                        var memberType = MemberDataUploads[i].MemberType.ToString().ToUpper();
                        if (memberType != "REGULAR" && memberType != "RETIREE" && memberType != "EXPATRIATE")
                            context.AddFailure(new ValidationFailure($"MemberDataUploads[{i}].MemberType", "Invalid Member type.", nameof(memberType)));
                    }
                  var existingUser=  dbContext.MemberProfiles.Where(u => u.PrimaryEmail == MemberDataUploads[i].Email).FirstOrDefault();
                    if(existingUser!=null)
                        context.AddFailure(new ValidationFailure($"MemberDataUploads[{i}].Email", "Email address has been taken.", nameof(existingUser.PrimaryEmail)));

                    var checkMemberId = dbContext.MemberProfiles.Where(u => u.MembershipId == MemberDataUploads[i].MembershipNumber).FirstOrDefault();
                    if (checkMemberId != null)
                        context.AddFailure(new ValidationFailure($"MemberDataUploads[{i}].MembershipNumber", "MembershipNumber address already exist.", nameof(checkMemberId.MembershipId)));
                }
               
            });
        }
    }
}

