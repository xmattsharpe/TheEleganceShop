namespace TheEleganceShop.Models
{
    public class CartProduct
    {
        public int? CartProductID { get; set; }

        public int? Quantity { get; set; }

        // FK to both product and cart

        public int? ProductID { get; set; }
        public int? CartID { get; set; }

        // Navigation properties to both models

        public Product? Product { get; set; }

        

        public Cart? Cart { get; set; }
    }
}
