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
                .ForMember(x => x.UserName, options => options.MapFrom(x => x.UserName))
                .ForMember(x => x.PasswordHash, options => options.MapFrom(x => x.UserPassword))
                .ForMember(x => x.NormalizedEmail, options => options.MapFrom(x => x.UserNormalizedEmail))
                .ForMember(x => x.NormalizedUserName, options => options.MapFrom(x => x.UserNormalizedName))
                .ForMember(x => x.LockoutEnabled, options => options.MapFrom(x => x.UserLockoutEnabled));

            CreateMap<UserUpdateDto, IdentityUser>();
            CreateMap<IdentityUser, UserUpdateDto>();
            CreateMap<IdentityUser, UserReadDto>()
               .ForMember(x => x.UserEmail, options => options.MapFrom(x => x.Email))
               .ForMember(x => x.UserId, options => options.MapFrom(x => x.Id))
               .ForMember(x=>x.UserName, options => options.MapFrom(x=>x.UserName));
            
        }
    }

}
