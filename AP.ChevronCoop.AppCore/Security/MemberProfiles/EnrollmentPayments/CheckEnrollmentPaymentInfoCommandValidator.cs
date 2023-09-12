using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.EnrollmentPayments
{
    public partial class CheckEnrollmentPaymentInfoCommandValidator : AbstractValidator<CheckEnrollmentPaymentInfoCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CheckEnrollmentPaymentInfoCommandValidator> logger;
        public CheckEnrollmentPaymentInfoCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CheckEnrollmentPaymentInfoCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.MembershipId).NotNull();
            RuleFor(p => p.Email).NotNull();


            RuleFor(p => p).Custom((data, context) =>
            {

                var memberProfile = dbContext.MemberProfiles.Where(m => m.PrimaryEmail == data.Email && m.MembershipId == data.MembershipId).FirstOrDefault();
                if (memberProfile != null)
                {
                    var paymentInfo = dbContext.EnrollmentPaymentInfos.Any(m => m.ProfileId == memberProfile.Id);
                    if (!paymentInfo)
                    {
                        context.AddFailure(new ValidationFailure(nameof(data.Email), $"No enrollment payment info could be associated with {data.Email}.", data.MembershipId));
                    }
                }
                else if(memberProfile==null)  
                {
                    context.AddFailure(new ValidationFailure(nameof(data.Email), $"No member user could be associated with {data.Email}.", data.Email));
                }
               
                
            });

        }


    }




}
