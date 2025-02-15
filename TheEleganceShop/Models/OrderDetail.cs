﻿using System.ComponentModel.DataAnnotations;
namespace TheEleganceShop.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int? ProductQuantity { get; set; }

        public int? ProductShoeSize { get; set; }


        // FK's to product and orderheader

        
        public int? ProductID { get; set; }

        [Display(Name = "Order Number")]
        public int? OrderHeaderID { get; set; }

        
        public OrderHeader? OrderHeader { get; set; }
  
        public Product? Product { get; set; }

    }
}
