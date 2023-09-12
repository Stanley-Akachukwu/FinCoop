using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductCharges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductCharges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductCharges
{
    public class DepositProductChargeMapperProfile : Profile
    {

        public DepositProductChargeMapperProfile()
        {

            //CreateMap<DepositProductCharge, DepositProductChargeViewModel>().ReverseMap();
            //CreateMap<DepositProductCharge, CreateDepositProductChargeCommand>().ReverseMap();
            //CreateMap<DepositProductCharge, UpdateDepositProductChargeCommand>().ReverseMap();
            //CreateMap<DepositProductCharge, DepositProductChargeMasterView>().ReverseMap();
            //CreateMap<DepositProductChargeViewModel, DepositProductChargeMasterView>().ReverseMap();
            //CreateMap<CreateDepositProductChargeCommand, DepositProductChargeMasterView>().ReverseMap();
            //CreateMap<UpdateDepositProductChargeCommand, DepositProductChargeMasterView>().ReverseMap();

            CreateMap<DepositProductCharge, DepositProductChargeViewModel>().ReverseMap();
            CreateMap<DepositProductCharge, CreateDepositProductChargeCommand>().ReverseMap();
            CreateMap<DepositProductCharge, UpdateDepositProductChargeCommand>().ReverseMap();
            CreateMap<DepositProductCharge, DepositProductChargeMasterView>().ReverseMap();
            CreateMap<DepositProductChargeViewModel, DepositProductChargeMasterView>().ReverseMap();
            CreateMap<CreateDepositProductChargeCommand, DepositProductChargeMasterView>().ReverseMap();
            CreateMap<UpdateDepositProductChargeCommand, DepositProductChargeMasterView>().ReverseMap();



        }
    }

}