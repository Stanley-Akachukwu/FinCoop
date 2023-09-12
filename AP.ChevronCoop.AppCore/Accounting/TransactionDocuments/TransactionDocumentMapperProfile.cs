using AP.ChevronCoop.AppDomain.Accounting.TransactionDocuments;
using AP.ChevronCoop.Entities.Accounting.TransactionDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.TransactionDocuments
{
    public class TransactionDocumentMapperProfile : Profile
    {

        public TransactionDocumentMapperProfile()
        {

            CreateMap<TransactionDocument, TransactionDocumentViewModel>().ReverseMap();
            CreateMap<TransactionDocument, CreateTransactionDocumentCommand>().ReverseMap();
            CreateMap<TransactionDocument, UpdateTransactionDocumentCommand>().ReverseMap();
            CreateMap<TransactionDocument, TransactionDocumentMasterView>().ReverseMap();
            CreateMap<TransactionDocumentViewModel, TransactionDocumentMasterView>().ReverseMap();
            CreateMap<CreateTransactionDocumentCommand, TransactionDocumentMasterView>().ReverseMap();
            CreateMap<UpdateTransactionDocumentCommand, TransactionDocumentMasterView>().ReverseMap();




        }
    }


}