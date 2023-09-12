using AP.ChevronCoop.AppDomain.Employees;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Employees;

public partial class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateEmployeeCommandValidator> logger;
    public CreateEmployeeCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateEmployeeCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(e => e.EmployeeNo).NotEmpty().WithMessage("Employee number is required.").MaximumLength(100).WithMessage("Employee number must not exceed 100 characters.");
        RuleFor(e => e.LastName).MaximumLength(256).WithMessage("Last name must not exceed 256 characters.");
        RuleFor(e => e.MiddleName).MaximumLength(256).WithMessage("Middle name must not exceed 256 characters.");
        RuleFor(e => e.FirstName).MaximumLength(256).WithMessage("First name must not exceed 256 characters.");
        RuleFor(e => e.Dob).NotNull().WithMessage("Date of birth is required.");
        RuleFor(e => e.Gender).NotEmpty().WithMessage("Gender is required.").MaximumLength(64).WithMessage("Gender must not exceed 64 characters.");
        RuleFor(e => e.ProfileImageUrl).MaximumLength(400).WithMessage("Profile image URL must not exceed 400 characters.");
        RuleFor(e => e.EmploymentDate).NotNull().WithMessage("Employment date is required.");
        RuleFor(e => e.DepartmentId).MaximumLength(40).WithMessage("Department ID must not exceed 40 characters.");
        RuleFor(e => e.ProfileId).MaximumLength(40).WithMessage("Profile ID must not exceed 40 characters.");

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

                    var checkName = dbContext.Employees.Where(r => r.Name.ToLower() == data.Name.ToLower()
            && r.CodeTypeId == data.CodeTypeId).Any();
                    if (checkName)
                    {
                        context.AddFailure(
                        new ValidationFailure(nameof(data.Name),
                        "Duplicate names are not allowed.", data.Name));
                    }

                    var checkCode = dbContext.Employees.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
