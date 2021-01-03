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
            CreateMap<UserCreateDto, IdentityUser>()
                .ForMember(x => x.Email, options => options.MapFrom(x => x.UserEmail))
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.UserName));
                
            CreateMap<UserUpdateDto, IdentityUser>();
            CreateMap<IdentityUser, UserUpdateDto>();
            CreateMap<IdentityUser, UserReadDto>()
               .ForMember(x => x.UserEmail, options => options.MapFrom(x => x.Email))
               .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id))
               .ForMember(x=>x.UserName, options => options.MapFrom(x=>x.UserName));
        }
    }

}
