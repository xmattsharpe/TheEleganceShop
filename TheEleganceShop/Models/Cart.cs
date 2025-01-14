﻿namespace TheEleganceShop.Models
{
    public class Cart
    {
        public int CartID { get; set; }


        
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public string? UserId { get; set; }

        // 
        public List <CartProduct>? CartProducts { get; set; }


    }
}
