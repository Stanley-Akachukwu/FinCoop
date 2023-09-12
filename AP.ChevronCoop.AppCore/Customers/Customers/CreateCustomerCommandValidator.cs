using AP.ChevronCoop.AppDomain.Customers.Customers;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Customers.Customers;

public partial class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<CreateCustomerCommandValidator> logger;
    public CreateCustomerCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateCustomerCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        // RuleFor(customer => customer.CustomerNo).NotEmpty().WithMessage("Customer number is required.")
        //     .MaximumLength(100).WithMessage("Customer number must not exceed 100 characters.");
        //
        // RuleFor(customer => customer.LastName).MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
        //
        // RuleFor(customer => customer.MiddleName).MaximumLength(50).WithMessage("Middle name must not exceed 50 characters.");
        //
        // RuleFor(customer => customer.FirstName).MaximumLength(50).WithMessage("First name must not exceed 50 characters.");
        //
        // RuleFor(customer => customer.Dob).NotNull().WithMessage("Date of birth is required.");
        //
        // RuleFor(customer => customer.Gender).NotEmpty().WithMessage("Gender is required.")
        //     .MaximumLength(64).WithMessage("Gender must not exceed 64 characters.");
        //
        // RuleFor(customer => customer.ProfileImageUrl).MaximumLength(512).WithMessage("Profile image URL must not exceed 512 characters.");
        //
        // RuleFor(customer => customer.RegistrationDate).NotNull().WithMessage("Registration date is required.");
        //
        // RuleFor(customer => customer.DepartmentId).MaximumLength(40).WithMessage("Department ID must not exceed 40 characters.");

        RuleFor(customer => customer.ProfileId).MaximumLength(40).WithMessage("Profile ID must not exceed 40 characters.");


        RuleFor(p => p).Custom((data, context) =>
        {
            // Check DepartmentId Exists
            // var checkDepartmentId = dbContext.Departments.Any(r => r.Id == data.DepartmentId);
            // if (!checkDepartmentId)
            // {
            //     context.AddFailure(
            //         new ValidationFailure(nameof(data.DepartmentId),
            //             "Selected Department Id does not exist", data.DepartmentId));
            // }

            // Check ProfileId Exists
            var checkProfileId = dbContext.MemberProfiles.Any(r => r.Id == data.ProfileId);
            if (!checkProfileId)
            {
                context.AddFailure(
                    new ValidationFailure(nameof(data.ProfileId),
                        "Selected Profile Id does not exist", data.ProfileId));
            }
        });

    }


}
