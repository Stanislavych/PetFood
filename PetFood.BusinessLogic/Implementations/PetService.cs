using AutoMapper;
using PetFood.BusinessLogic.Dto;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class PetService : IPetService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public PetService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<PetDto> AddPetAsync(PetDto petDto)
        {
            var newPet = _mapper.Map<Pet>(petDto);

            await _repositoryManager.Pet.CreateAsync(newPet);
            await _repositoryManager.SaveAsync();

            var createdPet = _mapper.Map<PetDto>(newPet);

            return createdPet;
        }

        public async Task<bool> DeletePetAsync(int petId)
        {
            var existingPet = (await _repositoryManager.Pet.FindByConditionAsync(p => p.Id == petId)).SingleOrDefault();

            if (existingPet == null)
            {
                return false;
            }

            await _repositoryManager.Pet.RemoveAsync(existingPet);
            await _repositoryManager.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
        {
            var pets = await _repositoryManager.Pet.FindAllAsync();
            var petsDto = _mapper.Map<IEnumerable<PetDto>>(pets);

            return petsDto;
        }

        public async Task<PetDto> UpdatePetAsync(PetDto petDto)
        {
            var existingPet = (await _repositoryManager.Pet.FindByConditionAsync(p => p.Id == petDto.Id)).SingleOrDefault();

            if (existingPet==null)
            {
                throw new ArgumentNullException("Pet not found");
            }

            var updatedPet = _mapper.Map<Pet>(petDto);

            await _repositoryManager.Pet.UpdateAsync(updatedPet);
            await _repositoryManager.SaveAsync();

            var updatedPetDto = _mapper.Map<PetDto>(updatedPet);

            return updatedPetDto;
        }
    }
}
