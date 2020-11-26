using AutoMapper;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Profiles
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<Storage, StorageReadDto>().ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser.UserId));
            CreateMap<StorageCreateDto, Storage>();
            CreateMap<StorageUpdateDto, Storage>();
            CreateMap<Storage, StorageUpdateDto>();
        }
    }
}