using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.DatabaseContext;
using PetFood.DAL.GenericRepository.Service;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class PetRepository : RepositoryBase<Pet>, IPetRepository
    {
        public PetRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async Task<Pet> FindByIdAsync(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            
            return pet;
        }
    }
}
