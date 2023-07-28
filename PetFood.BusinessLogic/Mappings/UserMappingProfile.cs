using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
