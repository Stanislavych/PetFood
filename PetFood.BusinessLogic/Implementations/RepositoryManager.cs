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
        private UserManager<User> _userManager;
        private IMapper _mapper;

        public RepositoryManager(DatabaseContext context,IUserAuthenticationRepository userAuthenticationRepository, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userAuthenticationRepository = userAuthenticationRepository;
            _userManager = userManager;
            _mapper = mapper;
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

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
