using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Mappings
{
    public class FoodItemMappingProfile : Profile
    {
        public FoodItemMappingProfile()
        {
            CreateMap<FoodItemDto, FoodItem>().ReverseMap();
        }
    }
}
