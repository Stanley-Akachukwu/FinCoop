using AP.ChevronCoop.AppDomain.Accounting.PaymentModes;
using AP.ChevronCoop.Entities.Accounting.PaymentModes;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Accounting.PaymentModes
{
    public class PaymentModeMapperProfile : Profile
    {

        public PaymentModeMapperProfile()
        {

            CreateMap<PaymentMode, PaymentModeViewModel>().ReverseMap();
            CreateMap<PaymentMode, CreatePaymentModeCommand>().ReverseMap();
            CreateMap<PaymentMode, UpdatePaymentModeCommand>().ReverseMap();
            CreateMap<PaymentMode, PaymentModeMasterView>().ReverseMap();
            CreateMap<PaymentModeViewModel, PaymentModeMasterView>().ReverseMap();
            CreateMap<CreatePaymentModeCommand, PaymentModeMasterView>().ReverseMap();
            CreateMap<UpdatePaymentModeCommand, PaymentModeMasterView>().ReverseMap();




        }
    }


}