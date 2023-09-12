using AP.ChevronCoop.AppDomain.Loans.LoanApplicationApprovals;
using AP.ChevronCoop.Entities.Loans.LoanApplicationApprovals;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanApplicationApprovals;

public class LoanApplicationApprovalMapperProfile : Profile
{
  public LoanApplicationApprovalMapperProfile()
  {
    CreateMap<LoanApplicationApproval, LoanApplicationApprovalViewModel>().ReverseMap();
    CreateMap<LoanApplicationApproval, CreateLoanApplicationApprovalCommand>().ReverseMap();
    CreateMap<LoanApplicationApproval, UpdateLoanApplicationApprovalCommand>().ReverseMap();
    CreateMap<LoanApplicationApproval, LoanApplicationApprovalMasterView>().ReverseMap();
    CreateMap<LoanApplicationApprovalViewModel, LoanApplicationApprovalMasterView>().ReverseMap();
    CreateMap<CreateLoanApplicationApprovalCommand, LoanApplicationApprovalMasterView>().ReverseMap();
    CreateMap<UpdateLoanApplicationApprovalCommand, LoanApplicationApprovalMasterView>().ReverseMap();
  }
}