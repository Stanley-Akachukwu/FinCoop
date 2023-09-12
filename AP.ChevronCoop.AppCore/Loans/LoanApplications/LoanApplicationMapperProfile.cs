using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplications;

public class LoanApplicationMapperProfile : Profile
{
    public LoanApplicationMapperProfile()
    {
        CreateMap<LoanApplication, LoanApplicationViewModel>().ReverseMap();
        CreateMap<LoanApplication, CreateLoanApplicationCommand>().ReverseMap();
        CreateMap<LoanApplication, UpdateLoanApplicationCommand>().ReverseMap();
        CreateMap<LoanApplication, LoanApplicationMasterView>().ReverseMap();
        CreateMap<LoanApplicationViewModel, LoanApplicationMasterView>().ReverseMap();
        CreateMap<CreateLoanApplicationCommand, LoanApplicationMasterView>().ReverseMap();
        CreateMap<UpdateLoanApplicationCommand, LoanApplicationMasterView>().ReverseMap();
    }
}