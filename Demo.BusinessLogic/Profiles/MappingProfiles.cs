using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDtos;
using Demo.DataAccess.Models.EmployeeModels;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.EmpType, options => options.MapFrom(emp => emp.EmployeeType))
.ForMember(dest => dest.EmpGender, options => options.MapFrom(emp => emp.Gender));

            CreateMap<Employee, EmployeeDetailsDto>()
            .ForMember(dest => dest.EmployeeType, options => options.MapFrom(emp => emp.EmployeeType))
.ForMember(dest => dest.Gender, options => options.MapFrom(emp => emp.Gender))
.ForMember(dest => dest.HiringDate, options => options.MapFrom(emp => DateOnly.FromDateTime(emp.HiringDate)));

            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateEmployeeDto, Employee>()
                                .ForMember(dest => dest.HiringDate, options => options.MapFrom(empDto => empDto.HiringDate.ToDateTime(TimeOnly.MinValue)));



        }

    }
}
