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
        [Range(0, 100)]
        public int? ProductStockQuantity { get; set; }

        [Display(Name = "Category")]
        public string? ProductCategory { get; set; }

        [Display(Name = "Image")]
        public string? ProductImageUrl { get; set; }

        public int? ProductshoeSize { get; set; }




        // List to serve as my navigation property to the OrderDetails junction model
        public List <OrderDetail>? OrderDetails { get; set; }


        public List<CartProduct>? CartProducts { get; set; }


    }
}
