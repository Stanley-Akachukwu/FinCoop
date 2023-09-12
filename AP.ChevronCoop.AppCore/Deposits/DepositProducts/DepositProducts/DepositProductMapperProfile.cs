using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProducts;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProducts;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProducts
{
    public class DepositProductMapperProfile : Profile
    {

        public DepositProductMapperProfile()
        {

            //CreateMap<DepositProduct, DepositProductViewModel>().ReverseMap();
            //CreateMap<DepositProduct, CreateDepositProductCommand>().ReverseMap();
            //CreateMap<DepositProduct, UpdateDepositProductCommand>().ReverseMap();
            //CreateMap<DepositProduct, DepositProductMasterView>().ReverseMap();
            //CreateMap<DepositProductViewModel, DepositProductMasterView>().ReverseMap();
            //CreateMap<CreateDepositProductCommand, DepositProductMasterView>().ReverseMap();
            //CreateMap<UpdateDepositProductCommand, DepositProductMasterView>().ReverseMap();

            CreateMap<DepositProduct, DepositProductViewModel>().ReverseMap();
            CreateMap<DepositProduct, GetDepositProductViewModel>().ReverseMap();
            CreateMap<DepositProduct, CreateDepositProductCommand>().ReverseMap();
            CreateMap<DepositProduct, UpdateDepositProductCommand>().ReverseMap();
            CreateMap<DepositProduct, DepositProductMasterView>().ReverseMap();
            CreateMap<DepositProductViewModel, DepositProductMasterView>().ReverseMap();
            CreateMap<CreateDepositProductCommand, DepositProductMasterView>().ReverseMap();
            CreateMap<UpdateDepositProductCommand, DepositProductMasterView>().ReverseMap();


        }
    }

}