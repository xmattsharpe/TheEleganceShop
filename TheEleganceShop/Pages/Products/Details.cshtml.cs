﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TheEleganceShop.Data;
using TheEleganceShop.Models;

namespace TheEleganceShop.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly TheEleganceShop.Data.ApplicationDbContext _context;

        public DetailsModel(TheEleganceShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id)
        {
            // Grab information about the currently logged in user 
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Forbid(); //      No logged in user
            }

            // Searching for the users cart, return only 1 cart with first or default 
            var cart = await _context.Cart
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            // adding the cart if it did not exist
            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
            }

            // grab the product to be added to their cart based on the passed id parameter and query the db
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Adding the product to the user's cart and setting the quantity property of the product
            var cartItem = new CartProduct
            {
                ProductID = product.ProductID,
                CartID = cart.CartID,
                Quantity = 1

            };

            _context.CartProduct.Add(cartItem);
            await _context.SaveChangesAsync();

            // Redirecting userr to the cart page
            return RedirectToPage("/Carts/Index");
        }
    }
}
