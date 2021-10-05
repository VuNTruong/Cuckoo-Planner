using System;
using AutoMapper;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>()
                .ForMember(dest =>
                    dest.RoleId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest =>
                    dest.RoleName,
                    opt => opt.MapFrom(src => src.Name));
        }
    }
}
