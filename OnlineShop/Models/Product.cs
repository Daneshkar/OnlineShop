using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
