using AP.ChevronCoop.AppDomain.Security.Auth.Permissions;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.Permissions
{
    public partial class CreatePermissionCommandValidator : AbstractValidator<CreatePermissionCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreatePermissionCommandValidator> logger;
        public CreatePermissionCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreatePermissionCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.Code).NotEmpty().MaximumLength(256);
            RuleFor(p => p.Name).NotEmpty().MaximumLength(512);

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

                        var checkName = dbContext.Permissions.Where(r => r.Name.ToLower() == data.Name.ToLower()
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.Permissions.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
