using AP.ChevronCoop.AppDomain.MasterData.Departments;
using AP.ChevronCoop.Entities.MasterData.Departments;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.MasterData.Departments;

public class DepartmentMapperProfile : Profile
{

    public DepartmentMapperProfile()
    {

        CreateMap<Department, DepartmentViewModel>().ReverseMap();
        CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
        CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
        CreateMap<Department, DepartmentMasterView>().ReverseMap();
        CreateMap<DepartmentViewModel, DepartmentMasterView>().ReverseMap();
        CreateMap<CreateDepartmentCommand, DepartmentMasterView>().ReverseMap();
        CreateMap<UpdateDepartmentCommand, DepartmentMasterView>().ReverseMap();




    }
}
