using AP.ChevronCoop.AppDomain.Members.MemberBankAccounts;
using AP.ChevronCoop.AppDomain.Security.MemberProfiles.MemberBankAccounts;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberBankAccounts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.MemberProfiles.MemberBankAccounts;

public class MemberBankAccountMapperProfile : Profile
{
  public MemberBankAccountMapperProfile()
  {
    CreateMap<MemberBankAccount, MemberBankAccountViewModel>().ReverseMap();
    CreateMap<MemberBankAccount, CreateMemberBankAccountCommand>().ReverseMap();
    CreateMap<MemberBankAccount, UpdateMemberBankAccountCommand>().ReverseMap();
    CreateMap<MemberBankAccount, MemberBankAccountMasterView>().ReverseMap();
    CreateMap<MemberBankAccountViewModel, MemberBankAccountMasterView>().ReverseMap();
    CreateMap<CreateMemberBankAccountCommand, MemberBankAccountMasterView>().ReverseMap();
    CreateMap<UpdateMemberBankAccountCommand, MemberBankAccountMasterView>().ReverseMap();
  }
}