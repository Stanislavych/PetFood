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
    }
}
