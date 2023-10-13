using System;
namespace OnlineShop.Models
{
	public class Favorite
	{
		public int Id { get; set; }
		public List<Product> MyFavoritProperty { get; set; }
	}
}

