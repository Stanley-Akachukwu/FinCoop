using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserClaims;
using AP.ChevronCoop.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserClaims
{
    public partial class UpdateApplicationUserClaimCommandValidator : AbstractValidator<UpdateApplicationUserClaimCommand>
    {

        private readonly ChevronCoopDbContext dbContext;
        private readonly ILogger<UpdateApplicationUserClaimCommandValidator> logger;
        public UpdateApplicationUserClaimCommandValidator(ChevronCoopDbContext appDbContext, ILogger<UpdateApplicationUserClaimCommandValidator> _logger)
        {

            dbContext = appDbContext;
            logger = _logger;


            //RuleFor(p => p.Id).NotEmpty();

            RuleFor(p => p.UserId).NotEmpty().MaximumLength(900);



            RuleFor(p => p).Custom((data, context) =>
            {

                var checkId = dbContext.ApplicationUserClaims.Where(r => r.UserId == data.UserId).Any();
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

                        var checkName = dbContext.ApplicationUserClaims.Where(r => r.Id != data.Id &&
                        r.Name.ToLower() == data.Name.ToLower() && r.CodeTypeId != data.CodeTypeId).Any();
                        if (checkName)
                        {
                            context.AddFailure(
                            new ValidationFailure(nameof(data.Name),
                            "Duplicate names are not allowed.", data.Name));
                        }

                        var checkCode = dbContext.ApplicationUserClaims.Where(r => r.Id != data.Id &&
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
