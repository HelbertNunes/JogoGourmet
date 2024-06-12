namespace JogoGourmet.Models
{
    public class DishDescription
    {
        public int DishId { get; set; }
        public Dish Dish { get; set; }
        public int DescriptionId { get; set; }
        public Description Description { get; set; }
    }
}