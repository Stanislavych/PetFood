﻿using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Mappings
{
    public class PetMappingProfile : Profile
    {
        public PetMappingProfile()
        {
            CreateMap<PetDto, Pet>().ReverseMap();
        }
    }
}
