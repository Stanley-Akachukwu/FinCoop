using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffSets
{
    public partial class DeleteLoanOffsetCommandValidator : AbstractValidator<DeleteLoanOffsetCommand>
    {

  private readonly ChevronCoopDbContext dbContext;
  private readonly ILogger<DeleteLoanOffsetCommandValidator> logger;
        public DeleteLoanOffsetCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteLoanOffsetCommandValidator> _logger)
  {

    dbContext = appDbContext;
    logger = _logger;


    RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

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