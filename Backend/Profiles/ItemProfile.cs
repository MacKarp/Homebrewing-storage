using AutoMapper;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemReadDto>()
                .ForMember(dest => dest.IdCategory, opt => opt.MapFrom(src => src.IdCategory.CategoryId));
            CreateMap<ItemUpdateDto, Item>()
                .ForPath(dest => dest.IdCategory.CategoryId, opt => opt.MapFrom(src => src.CategoryId));
            CreateMap<Item, ItemUpdateDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.IdCategory.CategoryId));
        }
    }
}