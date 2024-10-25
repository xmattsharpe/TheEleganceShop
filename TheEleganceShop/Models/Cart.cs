namespace TheEleganceShop.Models
{
    public class Cart
    {
        public int CartID { get; set; }


        // Customer PK serving as a FK
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }


        // Navigation list to the junction table 
        public List <CartProduct>? CartProducts { get; set; }


    }
}
