using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetFood.DAL.Models;

namespace PetFood.DAL.DatabaseContext
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
    }
}
