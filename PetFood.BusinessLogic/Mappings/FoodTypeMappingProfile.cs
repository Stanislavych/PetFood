﻿using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Mappings
{
    public class FoodTypeMappingProfile : Profile
    {
        public FoodTypeMappingProfile()
        {
            CreateMap<FoodTypeDto,FoodType>().ReverseMap();
        }
    }
}
