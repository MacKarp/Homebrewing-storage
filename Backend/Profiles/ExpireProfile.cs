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
            CreateMap<Expire, ExpireReadDto>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser.Id))
                .ForMember(dst => dst.IdStorage, opt => opt.MapFrom(src => src.IdStorage.StorageId))
                .ForMember(dst => dst.IdItem, opt => opt.MapFrom(src => src.IdItem.ItemId));
            CreateMap<ExpireUpdateDto, Expire>()
                 .ForPath(dest => dest.IdUser.Id, opt => opt.MapFrom(src => src.UserId))
                 .ForPath(dst => dst.IdStorage.StorageId, opt => opt.MapFrom(src => src.IdStorage))
                 .ForPath(dest => dest.IdItem.ItemId, opt => opt.MapFrom(src => src.IdItem));
            CreateMap<Expire, ExpireUpdateDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser.Id))
                .ForMember(dst => dst.IdStorage, opt => opt.MapFrom(src => src.IdStorage.StorageId))
                .ForMember(dst => dst.IdItem, opt => opt.MapFrom(src => src.IdItem.ItemId));
        }
    }
}