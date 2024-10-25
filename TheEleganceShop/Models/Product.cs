using System.ComponentModel.DataAnnotations;

namespace TheEleganceShop.Models
{
    public class Product
    {

        public int ProductID { get; set; }

        [Display(Name = "Name")]
        public string? ProductName { get; set; }

        [Display(Name = "Description")]
        public string? ProductDescription { get; set; }

        [Display(Name = "Price (USD)")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "Current Stock")]
        public int? ProductStockQuantity { get; set; }

        [Display(Name = "Category")]
        public string? ProductCategory { get; set; }

        [Display(Name = "Image")]
        public string? ProductImageUrl { get; set; }


        // List to serve as a navigation property to the OrderDetails junction model
        public List <OrderDetail>? OrderDetails { get; set; }


        // Navigation list to the junction table 
        public List<CartProduct>? CartProducts { get; set; }


    }
}
