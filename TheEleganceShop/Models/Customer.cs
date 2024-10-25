using System.ComponentModel.DataAnnotations;


namespace TheEleganceShop.Models
{
    public class Customer
    {

        public int? CustomerID { get; set;}
        // FULL NAME
        [Display(Name = "Full Name")]
        public string? CustomerName { get; set;}

        [Display(Name = "Phone Number")]
        public string? CustomerPhone { get; set;}

        [Display(Name = "Address")]
        public string? CustomerAddress { get; set; }

        [Display(Name = "City")]
        public string? CustomerCity { get; set; }

        [Display(Name = "Postal Code")]
        public string? CustomerZip { get; set; }


        


    }
}
