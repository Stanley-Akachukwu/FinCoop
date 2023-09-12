using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSets
{
    public partial class UpdateLoanOffsetCommandValidator : AbstractValidator<UpdateLoanOffsetCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateLoanOffsetCommandValidator> logger;
        public UpdateLoanOffsetCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateLoanOffsetCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.LoanAccountId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.OffsetAmount).NotNull();
            RuleFor(p => p.OldPrincipalBalance).NotNull();
            RuleFor(p => p.NewPrincipalBalance).NotNull();
            RuleFor(p => p.OldInterestBalance).NotNull();
            RuleFor(p => p.NewInterestBalance).NotNull();
            RuleFor(p => p.TotalOffsetCharges).NotNull();
            RuleFor(p => p.IsLiquidated).NotNull();
            RuleFor(p => p.AllowedOffsetType).NotEmpty().MaximumLength(64);
            RuleFor(p => p.LoanRepaymentMode).NotEmpty().MaximumLength(64);
            RuleFor(p => p.OffSetRepaymentDate).NotNull();
            RuleFor(p => p.ModeOfPayment).NotNull();

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.LoanOffsets.Where(r => r.Id == data.Id).Any();
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

                var checkName = dbContext.LoanOffsets.Where(r => r.Id != data.Id &&
                r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.LoanOffsets.Where(r => r.Id != data.Id &&
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