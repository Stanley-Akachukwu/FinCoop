using AP.ChevronCoop.AppDomain.Employees;
using AP.ChevronCoop.Entities.Employees;
using AutoMapper;

namespace AP.ChevronCoop.AppCore.Employees
{
    public class EmployeeMapperProfile : Profile
    {

        public EmployeeMapperProfile()
        {

            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, EmployeeMasterView>().ReverseMap();
            CreateMap<EmployeeViewModel, EmployeeMasterView>().ReverseMap();
            CreateMap<CreateEmployeeCommand, EmployeeMasterView>().ReverseMap();
            CreateMap<UpdateEmployeeCommand, EmployeeMasterView>().ReverseMap();




        }
    }


}