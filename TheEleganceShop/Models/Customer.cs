using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace TheEleganceShop.Models
{
    public class Customer
    {
        /* I do NOT use this model anymore, I did in the beginning of this project but I figured it was much more efficient to
         use the ASP.NET built in identity and fetch the current logged in user through the claims method I use throughout the page models. I did not delete it
        because it has relationships to my other models and dont wanr to risk breakingf the application. */
        public int CustomerID { get; set; }
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
