using AutoMapper;
using Backend.Dtos;
using Backend.Models;

namespace Backend.Profiles
{
    public class ExpireProfile : Profile
    {
        public ExpireProfile()
        {
            CreateMap<Expire, ExpireReadDto>();
            CreateMap<ExpireCreateDto, Expire>();
        }
    }
}