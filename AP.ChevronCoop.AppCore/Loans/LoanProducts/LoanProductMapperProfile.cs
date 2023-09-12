using AP.ChevronCoop.AppDomain.Loans.LoanProducts;
using AP.ChevronCoop.Entities.Loans.LoanProductPublications.MemberLoanProductPublications;
using AP.ChevronCoop.Entities.Loans.LoanProducts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Loans.LoanProducts;

public class LoanProductMapperProfile : Profile
{
  public LoanProductMapperProfile()
  {
    CreateMap<LoanProduct, LoanProductViewModel>().ReverseMap();
    CreateMap<LoanProduct, CreateLoanProductCommand>().ReverseMap();
    CreateMap<LoanProduct, UpdateLoanProductCommand>().ReverseMap();
    CreateMap<LoanProduct, LoanProductMasterView>().ReverseMap();
    CreateMap<LoanProduct, GetLoanProductViewModel>().ReverseMap();
    CreateMap<LoanProductViewModel, LoanProductMasterView>().ReverseMap();
    CreateMap<CreateLoanProductCommand, LoanProductMasterView>().ReverseMap();
    CreateMap<UpdateLoanProductCommand, LoanProductMasterView>().ReverseMap();


    CreateMap<CustomerLoanProductPublication, PublishLoanProductCommand>().ReverseMap();
    CreateMap<CustomerLoanProductPublication, LoanProductViewModel>().ReverseMap();
    CreateMap<GetLoanProductViewModel, LoanProductMasterView>().ReverseMap();
  }
}