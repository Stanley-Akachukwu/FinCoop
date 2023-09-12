using AP.ChevronCoop.AppDomain.MasterData.Banks;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Banks;

public partial class UpdateBankCommandValidator : AbstractValidator<UpdateBankCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<UpdateBankCommandValidator> logger;
    public UpdateBankCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateBankCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;


        RuleFor(p => p.Id).NotEmpty();
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(32).WithMessage("Code must not exceed 32 characters.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(128).WithMessage("Name must not exceed 128 characters.");

        RuleFor(x => x.Address)
            .MaximumLength(256).WithMessage("Address must not exceed 256 characters.");

        RuleFor(x => x.ContactName)
            .MaximumLength(128).WithMessage("Contact name must not exceed 128 characters.");

        RuleFor(x => x.ContactDetails)
            .MaximumLength(256).WithMessage("Contact details must not exceed 256 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {

            var checkId = dbContext.Banks.Where(r => r.Id == data.Id).Any();
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

                    var checkName = dbContext.Banks.Where(r => r.Id != data.Id &&
                    r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.Banks.Where(r => r.Id != data.Id &&
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
