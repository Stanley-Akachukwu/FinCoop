using AP.ChevronCoop.AppDomain.Security.Approvals.ApprovalDocuments;
using AP.ChevronCoop.Entities.Security.Approvals.ApprovalDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Security.Approvals.ApprovalDocuments
{
    public class ApprovalDocumentMapperProfile : Profile
    {

        public ApprovalDocumentMapperProfile()
        {

            CreateMap<ApprovalDocument, ApprovalDocumentViewModel>().ReverseMap();
            CreateMap<ApprovalDocument, CreateApprovalDocumentCommand>().ReverseMap();
            CreateMap<ApprovalDocument, UpdateApprovalDocumentCommand>().ReverseMap();
            CreateMap<ApprovalDocument, ApprovalDocumentMasterView>().ReverseMap();
            CreateMap<ApprovalDocumentViewModel, ApprovalDocumentMasterView>().ReverseMap();
            CreateMap<CreateApprovalDocumentCommand, ApprovalDocumentMasterView>().ReverseMap();
            CreateMap<UpdateApprovalDocumentCommand, ApprovalDocumentMasterView>().ReverseMap();




        }
    }

}
