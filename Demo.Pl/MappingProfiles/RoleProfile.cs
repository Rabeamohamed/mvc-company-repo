using AutoMapper;
using Demo.Pl.VeiwModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.Pl.MappingProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() 
        {
            CreateMap<IdentityRole, RoleViewModel>()
                .ForMember(d => d.RoleName, O => O.MapFrom(S => S.Name)).ReverseMap();
        }
    }
}
