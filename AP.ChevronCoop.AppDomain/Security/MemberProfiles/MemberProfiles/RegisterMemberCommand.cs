using AP.ChevronCoop.Commons;
using MediatR;
using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.Entities.Security;


namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberProfiles;

public class RegisterMemberCommand: IRequest<CommandResult<RegisterMemberViewModel>>
{
	//Please do not remove the validation. I'll remove them myself
	[Required(ErrorMessage = "Please enter {0}.")]
	public string LastName { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	public string FirstName { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	public string MembershipId { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	public string Email { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	public MemberType Role { get; set; }
	[Required(ErrorMessage = "Please select {0}.")]
	public string Location { get; set; }
    public bool IsKycStarted { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	[StringLength(255, ErrorMessage = "Must be between 10 and 255 characters", MinimumLength = 10)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
	[Required(ErrorMessage = "Please enter {0}.")]
	[StringLength(255, ErrorMessage = "Must be between 10 and 255 characters", MinimumLength = 10)]
	[DataType(DataType.Password)]
	public string ConfirmPassword { get; set; }
	[Required(ErrorMessage = "Please check {0}.")]
	public bool TermsAndCondition { get; set; } 

}

