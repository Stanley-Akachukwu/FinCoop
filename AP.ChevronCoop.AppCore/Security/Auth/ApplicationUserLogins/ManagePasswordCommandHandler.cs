using AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email.Templates.dto;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace AP.ChevronCoop.AppCore.Security.Auth.ApplicationUserLogins
{
    public class ManagePasswordCommandHandler : IRequestHandler<ResetPasswordCommand, CommandResult<string>>,
         IRequestHandler<ForgetPasswordCommand, CommandResult<ForgetPasswordViewModel>>,
        IRequestHandler<ValidateForgetPasswordOTPCommand, CommandResult<ValidateForgetPasswordOTPViewModel>>,
        IRequestHandler<ChangePasswordCommand, CommandResult<string>>

    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly CoreAppSettings _options;
        private readonly ChevronCoopDbContext _dbContext;

        public ManagePasswordCommandHandler(UserManager<ApplicationUser> userManager, ChevronCoopDbContext dbContext,
        ILogger<ManagePasswordCommandHandler> logger, IMapper mapper, IEmailService emailService, IOptions<CoreAppSettings> options)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _emailService = emailService;
            _options = options.Value;
            _dbContext = dbContext;

        }

        public async Task<CommandResult<ForgetPasswordViewModel>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ForgetPasswordViewModel>();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No user associated with email.";
                    return rsp;
                }


                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);
                var userResetPasswordOTPValue = StringHelper.GenerateRandomNumberOTP(6).ToUpper();

                var applicationUserToken = new Entities.Security.Auth.ApplicationUserTokens.ApplicationUserToken
                {
                    LoginProvider = "ChevronCoop SQL Auth",
                    Name = userResetPasswordOTPValue,
                    User = user,
                    UserId = user.Id,
                    Value = validToken,
                };

                await _dbContext.ApplicationUserTokens.AddAsync(applicationUserToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                var memberProfile = await _dbContext.MemberProfiles.FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken: cancellationToken);

                var props = new ForgotPasswordEmailDto
                {
                    OTP = userResetPasswordOTPValue,
                    Name = memberProfile?.FirstName
                };
                _ = _emailService.SendEmailAsync(EmailTemplateType.FORGOT_PASSWORD, request.Email, props);

                if (!string.IsNullOrEmpty(validToken))
                {
                    rsp.ErrorFlag = false;
                    var response = new ForgetPasswordViewModel
                    {
                        Email = request.Email,
                        OneTimePasswordCopy = userResetPasswordOTPValue
                    };
                    rsp.Response = response;
                    rsp.StatusCode = (int)HttpStatusCode.OK;
                    rsp.Message = "Reset password URL has been sent to the email successfully!";
                }
                return rsp;


            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                return rsp;
            }
        }

        public async Task<CommandResult<ValidateForgetPasswordOTPViewModel>> Handle(ValidateForgetPasswordOTPCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<ValidateForgetPasswordOTPViewModel>();
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No user associated with email.";
                    return rsp;
                }



                var applicationUserToken = await _dbContext.ApplicationUserTokens.FirstOrDefaultAsync(p => p.Name.ToUpper() == request.OneTimePassword.ToLower());
                if (applicationUserToken == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No OTP generated associated with email.";
                    return rsp;
                }


                if (string.IsNullOrEmpty(applicationUserToken.Value))
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No OTP generated associated with email.";
                    return rsp;
                }


                rsp.ErrorFlag = false;
                var response = new ValidateForgetPasswordOTPViewModel
                {
                    Email = user.Email,
                    OneTimePasswordCopy = request.OneTimePassword,
                    OneTimePassword = request.OneTimePassword,
                    IsValidOneTimePassword = true
                };
                rsp.Response = response;
                rsp.StatusCode = (int)HttpStatusCode.OK;
                rsp.Message = "OTP verified successfully!";
                return rsp;
            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                return rsp;
            }
        }

        public async Task<CommandResult<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Password doesn't match its confirmation.";
                    return rsp;
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null || user.PasswordHash is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No user associated with email.";
                    return rsp;
                }

                var applicationUserToken = await _dbContext.ApplicationUserTokens.FirstOrDefaultAsync(p => p.Name.ToLower() == request.OneTimePassword.ToLower(), cancellationToken);
                if (applicationUserToken == null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No OTP generated associated with email.";
                    return rsp;
                }


                if (string.IsNullOrEmpty(applicationUserToken.Value))
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No OTP generated associated with email.";
                    return rsp;
                }
                var decodedToken = WebEncoders.Base64UrlDecode(applicationUserToken.Value);
                string normalToken = Encoding.UTF8.GetString(decodedToken);

                var result = await _userManager.ResetPasswordAsync(user, normalToken, request.NewPassword);

                if (result.Succeeded)
                {
                    var entity = await _dbContext.ApplicationUserTokens.FirstOrDefaultAsync(p => p.Name.ToLower() == request.OneTimePassword.ToLower(), cancellationToken);
                    _dbContext.ApplicationUserTokens.Remove(entity);
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    rsp.StatusCode = (int)HttpStatusCode.OK;
                    rsp.Message = "Password has been reset successfully!";
                    rsp.Response = "Password has been reset successfully!";
                }
                else
                {
                    var failureMessage = string.Join("|", result.Errors.Select(x => x.Description));
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = failureMessage;
                }
                return rsp;
            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                return rsp;
            }
        }

        public async Task<CommandResult<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var rsp = new CommandResult<string>();
            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Password doesn't match its confirmation.";
                    return rsp;
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null || user.PasswordHash is null)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No user associated with email.";
                    return rsp;
                }


                var validPassword = await _userManager.CheckPasswordAsync(user, request.OldPassword);

                if (!validPassword)
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "No user associated with old password provided.";
                    return rsp;
                }

                if (validPassword)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var changePasswordResult = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
                    if (changePasswordResult.Succeeded)
                    {
                        rsp.StatusCode = (int)HttpStatusCode.OK;
                        rsp.Message = "Password has been reset successfully!";
                        rsp.Response = "Password has been reset successfully!";
                        return rsp;
                    }
                }
                else
                {
                    rsp.ErrorFlag = true;
                    rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                    rsp.Message = "Invalid token!";
                }
                return rsp;
            }
            catch (Exception e)
            {
                rsp.ErrorFlag = true;
                rsp.StatusCode = (int)HttpStatusCode.BadRequest;
                rsp.Message = e.Message;
                return rsp;
            }
        }
    }


}

