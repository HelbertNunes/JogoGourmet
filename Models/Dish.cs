namespace JogoGourmet.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public List<DishDescription> DishDescriptions { get; set; }
    }
}