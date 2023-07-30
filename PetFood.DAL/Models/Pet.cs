namespace PetFood.DAL.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<FoodType> AvailableFoodTypes { get; set; }
    }
}
