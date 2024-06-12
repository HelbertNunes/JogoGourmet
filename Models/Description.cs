namespace JogoGourmet.Models
{
    public class Description
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public List<DishDescription> DishDescriptions { get; set; }
    }
}