using AP.ChevronCoop.AppDomain.Loans.LoanOffsets;
using AP.ChevronCoop.Entities.LoanOffsetTransactions;
using AP.ChevronCoop.Entities.Loans.LoanOffSetTransactions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanOffsets;

public class LoanOffsetMapperProfile : Profile
{
  public LoanOffsetMapperProfile()
  {
    CreateMap<LoanOffset, LoanOffsetViewModel>().ReverseMap();
    CreateMap<LoanOffset, CreateLoanOffsetCommand>().ReverseMap();
    CreateMap<LoanOffset, UpdateLoanOffsetCommand>().ReverseMap();
    CreateMap<LoanOffset, LoanOffsetMasterView>().ReverseMap();
    CreateMap<LoanOffsetViewModel, LoanOffsetMasterView>().ReverseMap();
    CreateMap<CreateLoanOffsetCommand, LoanOffsetMasterView>().ReverseMap();
    CreateMap<UpdateLoanOffsetCommand, LoanOffsetMasterView>().ReverseMap();
  }
}