using AP.ChevronCoop.AppDomain.Loans.LoanTopups;
using AP.ChevronCoop.Entities.Loans.LoanTopupTransactions;
using AP.ChevronCoop.Entities.LoanTopupTransactions;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanTopups;

public class LoanTopupMapperProfile : Profile
{
    public LoanTopupMapperProfile()
    {
        CreateMap<LoanTopup, LoanTopupViewModel>().ReverseMap();
        CreateMap<LoanTopup, CreateLoanTopupCommand>().ReverseMap();
        CreateMap<LoanTopup, UpdateLoanTopupCommand>().ReverseMap();
        CreateMap<LoanTopup, LoanTopupMasterView>().ReverseMap();
        CreateMap<LoanTopupViewModel, LoanTopupMasterView>().ReverseMap();
        CreateMap<CreateLoanTopupCommand, LoanTopupMasterView>().ReverseMap();
        CreateMap<UpdateLoanTopupCommand, LoanTopupMasterView>().ReverseMap();
    }
}