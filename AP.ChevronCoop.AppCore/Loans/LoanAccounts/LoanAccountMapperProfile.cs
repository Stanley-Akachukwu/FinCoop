using AP.ChevronCoop.AppDomain.Loans.LoanAccounts;
using AP.ChevronCoop.AppDomain.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanAccounts;
using AP.ChevronCoop.Entities.Loans.LoanApplications;
using AP.ChevronCoop.Entities.Loans.LoanRepaymentSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanAccounts;

public class LoanAccountMapperProfile : Profile
{
    public LoanAccountMapperProfile()
    {
        CreateMap<LoanAccount, LoanAccountViewModel>().ReverseMap();
        CreateMap<LoanAccount, CreateLoanAccountCommand>().ReverseMap();
        CreateMap<LoanAccount, UpdateLoanAccountCommand>().ReverseMap();
        CreateMap<LoanAccount, LoanAccountMasterView>().ReverseMap();
        CreateMap<LoanAccountViewModel, LoanAccountMasterView>().ReverseMap();
        CreateMap<CreateLoanAccountCommand, LoanAccountMasterView>().ReverseMap();
        CreateMap<UpdateLoanAccountCommand, LoanAccountMasterView>().ReverseMap();
        CreateMap<LoanApplication, LoanAccount>().ReverseMap();
        CreateMap<GetLoanAccountViewModel, LoanAccountMasterView>().ReverseMap();
        CreateMap<AmortizationSchedule, LoanRepaymentSchedule>().ReverseMap();
    }
}