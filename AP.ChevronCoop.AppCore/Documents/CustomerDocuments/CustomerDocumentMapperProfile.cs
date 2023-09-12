using AP.ChevronCoop.AppDomain.Documents.CustomerDocuments;
using AP.ChevronCoop.Entities.CoopCustomers.Customers;
using AP.ChevronCoop.Entities.Documents.CustomerDocuments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Documents.CustomerDocuments;

public class CustomerDocumentMapperProfile : Profile
{

    public CustomerDocumentMapperProfile()
    {

        CreateMap<CustomerPaymentDocument, CustomerPaymentDocumentViewModel>()
                .ReverseMap();
        CreateMap<CustomerPaymentDocument, CreateCustomerPaymentDocumentCommand>().ReverseMap();

    }
}

