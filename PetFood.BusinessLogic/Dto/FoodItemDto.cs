namespace PetFood.BusinessLogic.Dto
{
    public class FoodItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int PetId { get; set; }
        public int FoodTypeId { get; set; }
    }
}
