using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepartmentDepositProductPublications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepartmentDepositProductPublications
{
    public partial class DeleteDepartmentDepositProductPublicationCommandValidator : AbstractValidator<DeleteDepartmentDepositProductPublicationCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<DeleteDepartmentDepositProductPublicationCommandValidator> logger;
        public DeleteDepartmentDepositProductPublicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<DeleteDepartmentDepositProductPublicationCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            RuleFor(p => p.Id).NotEmpty(); //.GreaterThan(0);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.DepartmentDepositProductPublications.Where(r => r.Id == data.Id).Any();
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


