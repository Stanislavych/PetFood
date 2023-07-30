using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetFood.BusinessLogic.Interfaces;
using PetFood.DAL.DatabaseContext;
using PetFood.DAL.Models;

namespace PetFood.BusinessLogic.Implementations
{
    public class RepositoryManager : IRepositoryManager
    {
        private DatabaseContext _context;
        private IUserAuthenticationRepository _userAuthenticationRepository;
        private IPetRepository _petRepository;
        private IFoodTypeRepository _foodTypeRepository;
        private IFoodItemRepository _foodItemRepository;
        private UserManager<User> _userManager;
        private IMapper _mapper;

        public RepositoryManager(DatabaseContext context, IUserAuthenticationRepository userAuthenticationRepository, UserManager<User> userManager, IMapper mapper, IPetRepository petRepository, IFoodTypeRepository foodTypeRepository, IFoodItemRepository foodItemRepository)
        {
            _context = context;
            _userAuthenticationRepository = userAuthenticationRepository;
            _userManager = userManager;
            _mapper = mapper;
            _petRepository = petRepository;
            _foodTypeRepository = foodTypeRepository;
            _foodItemRepository = foodItemRepository;
        }

        public IUserAuthenticationRepository UserAuthentication
        {
            get
            {
                if (_userAuthenticationRepository is null)
                {
                    _userAuthenticationRepository = new UserAuthenticationRepository(_userManager, _mapper);
                }

                return _userAuthenticationRepository;
            }
        }
        public IPetRepository Pet
        {
            get
            {
                if (_petRepository is null)
                {
                    _petRepository = new PetRepository(_context);
                }

                return _petRepository;
            }
        }
        public IFoodItemRepository FoodItem
        {
            get
            {
                if (_foodItemRepository is null)
                {
                    _foodItemRepository = new FoodItemRepository(_context);
                }

                return _foodItemRepository;
            }
        }
        public IFoodTypeRepository FoodType
        {
            get
            {
                if (_foodTypeRepository is null)
                {
                    _foodTypeRepository = new FoodTypeRepository(_context);
                }

                return _foodTypeRepository;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
