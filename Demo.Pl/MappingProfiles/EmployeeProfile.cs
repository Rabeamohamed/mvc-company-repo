using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.VeiwModels;

namespace Demo.Pl.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
