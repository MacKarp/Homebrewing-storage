using System;
using System.Linq;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Backend.Profiles
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            Console.WriteLine("StorageProfile constructor");
            CreateMap<Storage, StorageReadDto>()
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser.Id));
            CreateMap<StorageUpdateDto, Storage>()
                .ForPath(dest => dest.IdUser.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<Storage, StorageUpdateDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.IdUser.Id));
        }
    }
}