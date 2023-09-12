using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public partial class UpdateApplicationUserLoginCommandValidator : AbstractValidator<UpdateApplicationUserLoginCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApplicationUserLoginCommandValidator> logger;
        public UpdateApplicationUserLoginCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApplicationUserLoginCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            //RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.LoginProvider).NotEmpty().MaximumLength(900);
            RuleFor(p => p.ProviderKey).NotEmpty().MaximumLength(900);

            RuleFor(p => p.UserId).NotEmpty().MaximumLength(900);

            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApplicationUserLogins.Where(r => r.UserId == data.UserId).Any();
                if (!checkId)
                {
                    context.AddFailure(
                    new ValidationFailure(nameof(data.Id),
                    "Selected UserId does not exist", data.Id));
                }

                /*
                        var parentExists = dbContext.Parent.Where(r => r.Id == data.ParentId).Any();
                        if (!parentExists)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.ParentId),
                            "Invalid key.", data.ParentId));

                        }

                        var checkName = dbContext.ApplicationUserLogins.Where(r => r.Id != data.Id &&
                        r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationUserLogins.Where(r => r.Id != data.Id &&
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




}
