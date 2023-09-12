using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUsers
{
    public partial class UpdateApplicationUserCommandValidator : AbstractValidator<UpdateApplicationUserCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApplicationUserCommandValidator> logger;
        public UpdateApplicationUserCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApplicationUserCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.PhoneNumber).NotEmpty();
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            // RuleFor(p => p.MiddleName).NotEmpty();
            RuleFor(p => p.MembershipId).NotEmpty().MaximumLength(10).WithMessage("Membership ID is required");
            RuleFor(p => p.Status).NotEmpty();
            RuleFor(p => p.Gender).NotEmpty();
            RuleFor(p => p.Address).NotEmpty();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApplicationUsers.Any(r => r.Id == data.ApplicationUserId);
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected user Id does not exist", data.Id));
                }

                var checkMembershipId = dbContext.MemberProfiles.Any(r => r.MembershipId != null && r.MembershipId.ToLower() == data.MembershipId.ToLower() && r.Id != data.Id);
                if (checkMembershipId)
                {
                    context.AddFailure(
                        new ValidationFailure(nameof(data.MembershipId),
                            "User with the provided Membership Id exists.", data.MembershipId));
                }
            });

        }


    }




}
