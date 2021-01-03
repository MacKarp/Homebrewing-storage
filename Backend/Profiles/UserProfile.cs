using AutoMapper;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserReadDto>();
            CreateMap<UserCreateDto, IdentityUser>();
            CreateMap<UserUpdateDto, IdentityUser>();
            CreateMap<IdentityUser, UserUpdateDto>();
        }
    }

}
