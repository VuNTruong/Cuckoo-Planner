using System;
using AutoMapper;
using Planner.Models;
using Planner.ViewModels;

namespace Planner.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<SignUpViewModel, User>()
                .ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Email)
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email)
                );

            CreateMap<Models.UserProfile, UserProfileViewModel>();
        }
    }
}
