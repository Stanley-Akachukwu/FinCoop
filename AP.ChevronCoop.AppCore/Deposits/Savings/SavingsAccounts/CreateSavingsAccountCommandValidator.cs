using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccounts;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccounts;
public partial class CreateSavingsAccountCommandValidator : AbstractValidator<CreateSavingsAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateSavingsAccountCommandValidator> logger;
    public CreateSavingsAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateSavingsAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.ApplicationId).NotEmpty().WithMessage("Application Id required");
      
        RuleFor(p => p.PayrollAmount).NotNull();
      


        RuleFor(p => p).Custom((data, context) =>
        {
          
            var applicationExist = dbContext.SavingsAccountApplications.Where(r => r.Id == data.ApplicationId).Any();
            if (!applicationExist)
            
                context.AddFailure(
                new ValidationFailure(nameof(data.ApplicationId),
                "Saving Application not found", data.ApplicationId));

 
        });

    }


}




