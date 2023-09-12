using AP.ChevronCoop.AppDomain.Loans.LoanTopupCharges;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopupCharges
{
    public partial class DeleteLoanTopupChargeCommandValidator : AbstractValidator<DeleteLoanTopupChargeCommand>
    {
        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteLoanTopupChargeCommandValidator> logger;
        public DeleteLoanTopupChargeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteLoanTopupChargeCommandValidator> _logger)
        {
            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.LoanTopupCharges.Where(r => r.Id == data.Id).Any();
                if (!checkId)
                {
                    context.AddFailure(
              new ValidationFailure(nameof(data.Id),
                "Selected Id does not exist", data.Id));
                }

                /*
          var checkChild = dbContext.ChildTable.Where(r => r.ChildTableId == data.Id).Any();
                if (checkChild)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected record has dependent records and cannot be deleted", data.Id));

                }
          */

            });
        }
    }
}
