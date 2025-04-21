using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.VeiwModels;

namespace Demo.Pl.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>().ReverseMap();
        }
    }
}
