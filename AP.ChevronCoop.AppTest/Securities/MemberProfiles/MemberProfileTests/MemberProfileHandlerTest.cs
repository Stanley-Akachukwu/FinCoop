using AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;
using AP.ChevronCoop.Commons;
using AP.ChevronCoop.Entities;
using AP.ChevronCoop.Entities.Security.Auth.ApplicationUsers;
using AP.ChevronCoop.Infrastructure.Services.Notification.Email;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit.Abstractions;

namespace AP.ChevronCoop.AppTest.Securities.MemberProfiles.MemberProfileTests;

public class MemberProfileHandlerTest
{
  private readonly ITestOutputHelper _outputHelper;
  private readonly RegisterMemberCommandValidator _validator;
  private readonly ILogger<MemberProfileCommandHandler> _logger = Substitute.For<ILogger<MemberProfileCommandHandler>>();
  private readonly IOptions<CoreAppSettings> _options = Substitute.For<IOptions<CoreAppSettings>>();
  private readonly IEmailService _emailService = Substitute.For<IEmailService>();
  private readonly MemberProfileCommandHandler _handler;
  private readonly IMediator _mediator = Substitute.For<IMediator>();
  
  
  public MemberProfileHandlerTest(ITestOutputHelper outputHelper, IMapper mapper, UserManager<ApplicationUser> userManager, ChevronCoopDbContext context)
  {
    _outputHelper = outputHelper;
    // _handler = new MemberProfileCommandHandler(context, userManager, _logger, mapper, _options, _emailService);
  }
  
  
  // [Fact]
  // public void Create_ShouldCreateMemberProfile_WhenValidationPass()
  // {
  //   // Arrange
  //   var command = new CreateMemberProfileCommand()
  //   {
  //     Gender = "Male",
  //     ApplicationUserId = "testUser",
  //     Address = "testAddress",
  //     Caption = "testCaption",
  //     Comments = "testComments",
  //     Country = "testCountry",
  //     Description = "testDescription",
  //     State = "testState",
  //     Status = "testStatus",
  //     Tags = "testTags",
  //     DepartmentId = "testDepartmentId",
  //     FirstName = "testFirstName",
  //   };
  //   // var result = _mediator.Send(command);
  //
  //   var rest = _handler.Handle(command, new CancellationToken());
  //   
  //   Assert.NotNull(rest);
  // }
}