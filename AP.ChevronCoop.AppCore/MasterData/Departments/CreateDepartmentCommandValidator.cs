using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.MasterData.Departments;

public partial class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateDepartmentCommandValidator> logger;
    public CreateDepartmentCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateDepartmentCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(e => e.Name)
          .NotEmpty().WithMessage("Name is required.")
          .MaximumLength(256).WithMessage("Name must be less than or equal to 256 characters.");


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

                    var checkName = dbContext.Departments.Where(r => r.Name.ToLower() == data.Name.ToLower()
            && r.CodeTypeId == data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.Departments.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
