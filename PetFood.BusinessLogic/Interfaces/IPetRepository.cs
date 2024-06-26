﻿using PetFood.DAL.GenericRepository.Interface;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Interfaces
{
    public interface IPetRepository : IRepositoryBase<Pet>
    {
        Task<Pet> FindByIdAsync(int id);
    }
}
