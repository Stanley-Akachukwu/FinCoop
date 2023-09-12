using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<LoginCommandValidator> logger;
    public LoginCommandValidator(ChevronCoopDbContext appDbContext, ILogger<LoginCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.Email).NotEmpty().MaximumLength(124);
        RuleFor(p => p.Password).NotEmpty().MaximumLength(64);

        RuleFor(p => p).Custom((data, context) =>
        {

        });
    }
}


public class SwitchAccountCommandValidator : AbstractValidator<SwitchAccountCommand>
{

    private readonly ChevronCoopDbContext dbContext;
    private readonly ILogger<SwitchAccountCommandValidator> logger;
    public SwitchAccountCommandValidator(ChevronCoopDbContext appDbContext, ILogger<SwitchAccountCommandValidator> _logger)
    {

        dbContext = appDbContext;
        logger = _logger;

        RuleFor(p => p.UserId).NotEmpty().MaximumLength(124);
        // RuleFor(p => p.SwitchToCorporate);

        RuleFor(p => p).Custom((data, context) =>
        {

        });
    }
}
