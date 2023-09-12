using AP.ChevronCoop.AppDomain.Deposits.DepositProducts.DepositProductInterestRanges;
using AP.ChevronCoop.Entities.Deposits.Products.DepositProductInterestRanges;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.DepositProducts.DepositProductInterestRanges
{
    public class DepositProductInterestRangeMapperProfile : Profile
    {

        public DepositProductInterestRangeMapperProfile()
        {

            CreateMap<DepositProductInterestRange, DepositProductInterestRangeViewModel>().ReverseMap();
            CreateMap<DepositProductInterestRange, CreateDepositProductInterestRangeCommand>().ReverseMap();
            CreateMap<DepositProductInterestRange, UpdateDepositProductInterestRangeCommand>().ReverseMap();
            CreateMap<DepositProductInterestRange, DepositProductInterestRangeMasterView>().ReverseMap();
            CreateMap<DepositProductInterestRangeViewModel, DepositProductInterestRangeMasterView>().ReverseMap();
            CreateMap<CreateDepositProductInterestRangeCommand, DepositProductInterestRangeMasterView>().ReverseMap();
            CreateMap<UpdateDepositProductInterestRangeCommand, DepositProductInterestRangeMasterView>().ReverseMap();

            //CreateMap<DepositProductInterestRange, DepositProductInterestRangeViewModel>().ReverseMap();
            //CreateMap<DepositProductInterestRange, CreateDepositProductInterestRangeCommand>().ReverseMap();
            //CreateMap<DepositProductInterestRange, UpdateDepositProductInterestRangeCommand>().ReverseMap();
            //CreateMap<DepositProductInterestRange, DepositProductInterestRangeMasterView>().ReverseMap();
            //CreateMap<DepositProductInterestRangeViewModel, DepositProductInterestRangeMasterView>().ReverseMap();
            //CreateMap<CreateDepositProductInterestRangeCommand, DepositProductInterestRangeMasterView>().ReverseMap();
            //CreateMap<UpdateDepositProductInterestRangeCommand, DepositProductInterestRangeMasterView>().ReverseMap();


        }
    }

}