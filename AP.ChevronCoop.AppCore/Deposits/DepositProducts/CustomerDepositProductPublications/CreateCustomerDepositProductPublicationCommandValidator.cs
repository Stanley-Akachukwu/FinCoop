using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.CustomerDepositProductPublications;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.CustomerDepositProductPublications
{
    public partial class CreateCustomerDepositProductPublicationCommandValidator : AbstractValidator<CreateCustomerDepositProductPublicationCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateCustomerDepositProductPublicationCommandValidator> logger;
        public CreateCustomerDepositProductPublicationCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateCustomerDepositProductPublicationCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.PublicationType).IsInEnum();
            RuleFor(p => p.ProductId).NotEmpty().MaximumLength(80);
            RuleFor(p => p.CustomerId).NotEmpty().MaximumLength(80);






            RuleFor(p => p).Custom((data, context) =>
            {


                /*
                var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                if (!parentExists)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.ParentId),
                    "Invalid key.", data.ParentId));

                }

                var checkName = dbContext.CustomerDepositProductPublications.Where(r => r.Name.ToLower() == data.Name.ToLower() 
				&& r.CodeTypeId == data.CodeTypeId).Any();
                if (checkName)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Name),
                    "Duplicate names are not allowed.", data.Name));
                }

                var checkCode = dbContext.CustomerDepositProductPublications.Where(r => r.Code.ToLower() == data.Code.ToLower() 
				&& r.CodeTypeId != data.CodeTypeId).Any();
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


