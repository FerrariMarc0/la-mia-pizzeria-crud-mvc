using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {   
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
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
        public Pizza(){}
    }
}
