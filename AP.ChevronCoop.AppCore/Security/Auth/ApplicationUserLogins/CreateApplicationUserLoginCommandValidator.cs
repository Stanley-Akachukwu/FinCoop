﻿using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public partial class CreateApplicationUserLoginCommandValidator : AbstractValidator<CreateApplicationUserLoginCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<CreateApplicationUserLoginCommandValidator> logger;
        public CreateApplicationUserLoginCommandValidator(ChevronCoopDbContext appDbContext, ILogger<CreateApplicationUserLoginCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;

            RuleFor(p => p.LoginProvider).NotEmpty().MaximumLength(900);
            RuleFor(p => p.ProviderKey).NotEmpty().MaximumLength(900);

            RuleFor(p => p.UserId).NotEmpty().MaximumLength(900);

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

                        var checkName = dbContext.ApplicationUserLogins.Where(r => r.Name.ToLower() == data.Name.ToLower()
                && r.CodeTypeId == data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationUserLogins.Where(r => r.Code.ToLower() == data.Code.ToLower()
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
