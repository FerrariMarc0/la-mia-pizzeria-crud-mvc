namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public decimal Price { get; set; }

        public Pizza(string name, string description, string image ,decimal price)
        {
            Name = name;
            Description = description;
            UrlImage = image;
            Price = price;
        }
    }
}
