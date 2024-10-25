namespace TheEleganceShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailsID { get; set; }
        public int? ProductQuantity { get; set; }




        // FK's to product and orderheader
        public int? ProductID { get; set; }
        public int? OrderHeaderID { get; set; }

        // Navigation properties to both models
        public OrderHeader? OrderHeader { get; set; }
        // Navigation property to the Product
        public Product? Product { get; set; }

    }
}
