using AP.ChevronCoop.AppDomain.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Loans.LoanApplicationGuarantors;
using AP.ChevronCoop.Entities.Security.MemberProfiles.MemberProfiles;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationGuarantors;

public class LoanApplicationGuarantorMapperProfile : Profile
{
    public LoanApplicationGuarantorMapperProfile()
    {
        CreateMap<LoanApplicationGuarantor, LoanApplicationGuarantorViewModel>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, CreateLoanApplicationGuarantorCommand>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, UpdateLoanApplicationGuarantorCommand>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, LoanApplicationGuarantorMasterView>().ReverseMap();
        CreateMap<LoanApplicationGuarantorViewModel, LoanApplicationGuarantorMasterView>().ReverseMap();
        CreateMap<CreateLoanApplicationGuarantorCommand, LoanApplicationGuarantorMasterView>().ReverseMap();
        CreateMap<UpdateLoanApplicationGuarantorCommand, LoanApplicationGuarantorMasterView>().ReverseMap();

        CreateMap<VerifyLoanApplicationGuarantorViewModel, MemberProfile>().ReverseMap();
        CreateMap<VerifyLoanApplicationGuarantorViewModel, Customer>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, LoanTopupGuarantorApprovalViewModel>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, LoanApplicationGuarantorApprovalViewModel>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, GetLoanApplicationGuarantorViewModel>().ReverseMap();
        CreateMap<LoanApplicationGuarantor, GetLoanApplicationGuarantorsViewModel>().ReverseMap();
    }
}