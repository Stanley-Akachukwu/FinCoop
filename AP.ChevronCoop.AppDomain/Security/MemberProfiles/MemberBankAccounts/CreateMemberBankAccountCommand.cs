using System.ComponentModel.DataAnnotations;
using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.Commons;
using MediatR;

namespace AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;

public class CreateMemberBankAccountCommand : CreateCommand, IRequest<CommandResult<MemberBankAccountViewModel>>
{
  public string ProfileId { get; set; }
  public string BankId { get; set; }
  public string BVN { get; set; }
  public string AccountNumber { get; set; }
  public string AccountName { get; set; }

  public string SortCode { get; set; }
  public string Branch { get; set; }
}