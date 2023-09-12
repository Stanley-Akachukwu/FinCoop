using AP.ChevronCoop.AppDomain.Deposits.Savings.SavingsAccountDeductionSchedules;
using AP.ChevronCoop.Entities.Deposits.Savings.SavingsAccountDeductionSchedules;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Deposits.Savings.SavingsAccountDeductionSchedules;

public class SavingsAccountDeductionScheduleMapperProfile : Profile
{

    public SavingsAccountDeductionScheduleMapperProfile()
    {

        CreateMap<SavingsAccountDeductionSchedule, SavingsAccountDeductionScheduleViewModel>().ReverseMap();
        CreateMap<SavingsAccountDeductionSchedule, CreateSavingsAccountDeductionScheduleCommand>().ReverseMap();
        CreateMap<SavingsAccountDeductionSchedule, UpdateSavingsAccountDeductionScheduleCommand>().ReverseMap();
        CreateMap<SavingsAccountDeductionSchedule, SavingsAccountDeductionScheduleMasterView>().ReverseMap();
        CreateMap<SavingsAccountDeductionScheduleViewModel, SavingsAccountDeductionScheduleMasterView>().ReverseMap();
        CreateMap<CreateSavingsAccountDeductionScheduleCommand, SavingsAccountDeductionScheduleMasterView>().ReverseMap();
        CreateMap<UpdateSavingsAccountDeductionScheduleCommand, SavingsAccountDeductionScheduleMasterView>().ReverseMap();




    }
}
