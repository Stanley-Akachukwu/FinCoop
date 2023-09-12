using AP.ChevronCoop.AppDomain.Security.MemberProfiles.EnrollmentPayments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.EnrollmentPayments
{
    public partial class UpdateEnrollmentPaymentInfoCommandValidator : AbstractValidator<UpdateEnrollmentPaymentInfoCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateEnrollmentPaymentInfoCommandValidator> logger;
        public UpdateEnrollmentPaymentInfoCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateEnrollmentPaymentInfoCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.Evidence).NotNull();
            RuleFor(p => p.MimeType).NotEmpty().MaximumLength(128);
            RuleFor(p => p.FileName).NotEmpty().MaximumLength(512);
            RuleFor(p => p.FileSize).NotNull();






            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.EnrollmentPaymentInfos.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }

                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.EnrollmentPaymentInfos.Where(r => r.Id != data.Id &&
                        r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.EnrollmentPaymentInfos.Where(r => r.Id != data.Id &&
                        r.Code.ToLower() == data.Code.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkCode)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Code),
                            "Duplicate codes are not allowed.", data.Code));
                        }
                */

            });

        }


    }




}
