using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBankAccounts

{
    public class UpdateMemberBankAccountValidator : AbstractValidator<UpdateMemberBankAccountCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateMemberBankAccountValidator> logger;
        public UpdateMemberBankAccountValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateMemberBankAccountValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty();
            RuleFor(x => x.ProfileId)
                .NotEmpty().WithMessage("Profile ID is required.")
                .MaximumLength(40).WithMessage("Profile ID must not exceed 40 characters.");

            RuleFor(x => x.BankId)
                .NotEmpty().WithMessage("Bank ID is required.")
                .MaximumLength(40).WithMessage("Bank ID must not exceed 40 characters.");

            // RuleFor(x => x.BVN)
            //     .NotEmpty().WithMessage("BVN is required.")
            //     .MaximumLength(64).WithMessage("BVN must not exceed 64 characters.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.")
                .MaximumLength(32).WithMessage("Account number must not exceed 32 characters.");

            RuleFor(x => x.AccountName)
                .NotEmpty().WithMessage("Account name is required.")
                .MaximumLength(128).WithMessage("Account name must not exceed 128 characters.");

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.MemberBankAccounts.Any(r => r.Id == data.Id);
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected Id does not exist", data.Id));
                }


                if (!string.IsNullOrWhiteSpace(data.AccountName))
                {

                    //var checkAccountName = dbContext.MemberBankAccounts.Any(r => r.AccountName.ToLower() == data.AccountName.ToLower()
                    //    && r.Id != data.Id);
                    //if (checkAccountName)
                    //{
                    //    context.AddFailure(
                    //    new ValidationFailure(nameof(data.AccountName),
                    //    "Duplicate accounts are not allowed.", data.AccountName));
                    //}

                    // var checkBVN = dbContext.MemberBankAccounts.Any(r => r.BVN == data.BVN && r.Id != data.Id);
                    // if (checkBVN)
                    // {
                    //     context.AddFailure(
                    //         new ValidationFailure(nameof(data.BVN),
                    //             "Duplicate account are not allowed.", data.BVN));
                    // }

                    // Check checkDocumentTypeId Exists
                    var checkBankId = dbContext.Banks.Any(r => r.Id == data.BankId);
                    if (!checkBankId)
                    {
                        context.AddFailure(
                            new ValidationFailure(nameof(data.BankId),
                                "Selected Bank Id does not exist", data.BankId));
                    }

                    // Check ProfileId Exists
                    var checkProfileId = dbContext.MemberProfiles.Any(r => r.Id == data.ProfileId);
                    if (!checkProfileId)
                    {
                        context.AddFailure(
                            new ValidationFailure(nameof(data.ProfileId),
                                "Selected Profile Id does not exist", data.ProfileId));
                    }
                }
            });

        }


    }

}