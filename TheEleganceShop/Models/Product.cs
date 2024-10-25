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
    }
}
