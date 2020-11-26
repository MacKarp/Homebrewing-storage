using AutoMapper;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Backend.Profiles
{
    public class ExpireProfile : Profile
    {
        public ExpireProfile()
        {
            CreateMap<Expire, ExpireReadDto>().ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser.UserId)).ForMember(dst => dst.IdStorage, opt => opt.MapFrom(src => src.IdStorage.StorageId)).ForMember(dst => dst.IdItem, opt => opt.MapFrom(src => src.IdItem.ItemId));
            CreateMap<ExpireCreateDto, Expire>();
            CreateMap<ExpireUpdateDto, Expire>();
            CreateMap<Expire, ExpireUpdateDto>();
        }
    }
}