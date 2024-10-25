using System.ComponentModel.DataAnnotations;

namespace TheEleganceShop.Models
{
    public class OrderHeader
    {

        public int OrderHeaderId { get; set; }
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Order Status")]
        public string? OrderStatus { get; set; }
        [Display(Name = "Order Total")]
        public decimal? OrderAmount { get; set; }

        [Display(Name = "Payment Method")]
        public string? OrderPaymentMethod { get; set; }
        [Display(Name = "Shipping Address")]

        public string? OrderShippingAddress { get; set; }
        [Display(Name = "City")]

        public string? OrderCity { get; set; }
        [Display(Name = "Zip Code")]
        public string? OrderZipCode { get; set; }


        // Foreign key to Customer
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        // List to serve as a navigation property to the OrderDetails junction model
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
