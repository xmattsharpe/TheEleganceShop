using TheEleganceShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using static System.Net.Mime.MediaTypeNames;

namespace TheEleganceShop.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Checking   if any products already exist 
            if (context.Product.Any())
            {
                return;
            }

            // Seeding my products data if not already present
            var products = new Product[]
            {
                new Product
                {
                    ProductName = "Zebra Yeezy Boost 350 V2",
                    ProductDescription = "The Yeezy Boost 350 V2 'Zebra' is a highly sought-after sneaker designed by Kanye West in collaboration with Adidas. Featuring a distinctive black and white striped upper, the 'Zebra' design stands out with its bold color contrast. The shoe includes a transparent side stripe and the signature Boost cushioning for ultimate comfort and support.",
                    ProductPrice = 300.00m,
                    ProductStockQuantity = 5,
                    ProductCategory = "Mens",
                    ProductImageUrl = "/img/zebra1.jpg",
                    ProductshoeSize = 10
                },

                new Product
                {
                    ProductName = "Jordan 11 Concord",
                    ProductDescription = "The Air Jordan 11 'Concord' is a classic basketball sneaker that first debuted in 1995 and has remained a favorite among sneakerheads and athletes alike. Known for its striking black patent leather upper and white midsole, the 'Concord' features a signature translucent outsole and a Jumpman logo on the ankle.",
                    ProductPrice = 400.00m,
                    ProductStockQuantity = 2,
                    ProductCategory = "Mens",

                    ProductImageUrl = "/img/J11.jpg",
                    ProductshoeSize = 8
                },
                new Product
                {
                    ProductName = "Jordan 4 Lightning",
                    ProductDescription = "The Air Jordan 4 'Lightning' is a striking sneaker that originally released in 2006 and made a highly anticipated return in 2021. This bold colorway features a vibrant yellow nubuck upper, complemented by black accents on the eyelets, mesh panels, and tongue. The sneaker also includes grey details on the midsole and a classic Jumpman logo.",
                    ProductPrice = 500.00m,
                    ProductStockQuantity = 2,
                    ProductCategory = "Mens",
                    ProductImageUrl = "/img/J4.jpg",
                    ProductshoeSize = 4
                },
                new Product
                {
                    ProductName = "Nike Cactus Flee Plant",
                    ProductDescription = "The Nike Cactus Plant Flea Market (CPFM) 'Overgrown' is a unique sneaker collaboration that features an innovative design blending streetwear aesthetics with playful elements. The shoe showcases an oversized silhouette with a vibrant mix of colors and textures, often including fuzzy materials and bold graphic details.",
                    ProductPrice = 300.00m,

                    ProductStockQuantity = 7,
                    ProductCategory = "Womens",
                    ProductImageUrl = "/img/cactus.jpg",
                    ProductshoeSize = 7
                },
                new Product
                {
                    ProductName = "Yeezy Slide Onyx",
                    ProductDescription = "The Yeezy Slide Onyx is a sleek, minimalist slide designed for comfort and style. Crafted from lightweight EVA foam, these slides feature a unique one-piece construction that ensures durability and flexibility. The rich, dark Onyx color adds a sophisticated touch, making them versatile for both casual and sporty outfits.",
                    ProductPrice = 140.00m,
                    ProductStockQuantity = 3,
                    ProductCategory = "Unisex",
                    ProductImageUrl = "/img/onyx1.jpg",
                    ProductshoeSize = 5
                },
                new Product
                {
                    ProductName = "Yeezy Foam Runner 'Slate'",
                    ProductDescription = " The Yeezy Foam Runner  'Slate' brings together innovative design and sustainable materials for a unique, eye-catching sneaker experience. Made from a blend of harvested algae and EVA foam, the 'Slate' colorway has a sleek and neutral look that pairs with almost any style. With its ergonomic slip-on structure, the Foam Runner combines comfort with breathability, thanks to strategically placed cutouts that offer ventilation and a striking aesthetic.",
                    ProductPrice = 220.00m,
                    ProductStockQuantity = 2,
                    ProductCategory = "Unisex",
                    ProductImageUrl = "/img/FoamSlide.Jpeg",
                    ProductshoeSize = 10
                },
                new Product
                {
                    ProductName = "Yeezy Boost 700 'Magnet'",
                    ProductDescription = "The Yeezy Boost 700 'Magnet' delivers a sophisticated blend of earthy tones with the comfort and style that have made the 700 series a fan favorite. This sneaker showcases a rich mix of suede, mesh, and leather in muted grey, cream, and dark grey tones, creating a layered and textured look. The chunky midsole features Adidas’ signature Boost cushioning for optimal support and comfort.",
                    ProductPrice = 300.00m,
                    ProductStockQuantity = 1,
                     ProductCategory = "Mens",
                    ProductImageUrl = "/img/Boost.jpg",
                    ProductshoeSize = 11
                },
                new Product
                {
                    ProductName = "Yeezy Boost 500 'Blush'",
                    ProductDescription = "Our Yeezy Boost 500 'Blush' brings an understated elegance to the chunky sneaker trend, blending vintage vibes with modern comfort. Featuring a tonal, monochromatic 'Blush' colorway, the shoe is crafted from a mix of suede, mesh, and leather, giving it a soft, textured appearance with an airy, breathable feel.",
                    ProductPrice = 320.00m,
                    ProductStockQuantity = 2,
                    ProductCategory = "Mens",
                    ProductImageUrl = "/img/Yeezy500.jpg",
                    ProductshoeSize = 8
                },
                new Product

                {
                    ProductName = "Yeezy Boost 350 'Pirate Black'",
                    ProductDescription = "Yeezy Boost 350 'Pirate Black' is an iconic sneaker known for its sleek, versatile style and superior comfort. It features a stealthy, all-black Primeknit upper with subtle dark grey detailing, creating a textured look that's minimal yet eye-catching.",
                    ProductPrice = 400.00m,
                    ProductStockQuantity = 1,
                    ProductCategory = "Unisex",
                    ProductImageUrl = "/img/Pirates.jpg",
                    ProductshoeSize = 10
                },
                new Product
                {
                    ProductName = "Yeezy Insulated Boots",
                    ProductDescription = "The Yeezy Insulated Boots push the boundaries of cold-weather footwear with a bold, futuristic design and functional build. Constructed with water-resistant, insulated materials, these boots are engineered to keep feet warm and dry in harsh conditions.",
                    ProductPrice = 1125.00m,
                    ProductStockQuantity = 3,
                    ProductCategory = "Mens",
                    ProductImageUrl = "/img/InsulatedBoot.jpg",
                    ProductshoeSize = 5
                },
                new Product
                {
                    ProductName = "Nike Panda 'Dunk'",
                    ProductDescription = "The Nike Panda Dunks are a stylish and versatile pair of sneakers, known for their sleek, monochrome design. Featuring a predominantly black and white color scheme, these Dunks are crafted with premium leather for durability and comfort.",
                    ProductPrice = 150.00m,
                    ProductStockQuantity = 3,
                    ProductCategory = "Unisex",
                    ProductImageUrl = "/img/pandass.jpg",
                    ProductshoeSize = 4
                },

                new Product
                {
                    ProductName = "Yeezy Mysteries?",
                    ProductDescription = "You could win a pair of sneakers that have a face value up to $10,000, or as low as $50. Are you willing to take a risk?",
                    ProductPrice = 500.00m,
                    ProductStockQuantity = 1,
                    ProductCategory = "Unisex",
                    ProductImageUrl = "/img/questionMark.jpg",
                    ProductshoeSize = 10
                }
            };


            context.Product.AddRange(products);
            context.SaveChanges();






            // Seed  data for roles and Users

            if (!context.Roles.Any())
            {
                var AdminRole = new IdentityRole("Admin");
                var customerRole = new IdentityRole("Customer");

                context.Roles.AddRange(AdminRole, customerRole);
                context.SaveChanges();







                var user = new IdentityUser
                {
                    UserName = "matthewsharpe@TES.com",
                    Email = "matthewsharpe@TES.com",
                    NormalizedUserName = "matthewsharpe@TES.com",
                    // Its upper cased in the asp.net table by default so I assume it should be....
                    NormalizedEmail = "matthewsharpe@TES.com".ToUpper(),
                    EmailConfirmed = true
                };


                var Customer = new IdentityUser
                {
                    UserName = "example123@example.com",
                    Email = "example123@example.com",
                    NormalizedUserName = "example123@example.com",
                    NormalizedEmail = "example123@example.com".ToUpper(),
                    EmailConfirmed = true
                };

              

                

                // user
                var PHasher = new PasswordHasher<IdentityUser>();
                user.PasswordHash = PHasher.HashPassword(user, "MSHARPE");
                user.SecurityStamp = Guid.NewGuid().ToString();

                // customer

                var PHasher1 = new PasswordHasher<IdentityUser>();
                Customer.PasswordHash = PHasher1.HashPassword(Customer, "MSHARPE");
                Customer.SecurityStamp = Guid.NewGuid().ToString();


                context.Users.AddRange(user, Customer);
                context.SaveChanges();


                // ADD ADMIN TO USER

                var userRole = new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = AdminRole.Id
                };

                


                // AGAIN FOR CUSTOMER

                var Customerassign = new IdentityUserRole<string>
                {
                    UserId = Customer.Id,
                    RoleId = customerRole.Id

                };

                context.UserRoles.AddRange(userRole, Customerassign);
                context.SaveChanges();



            }
        }
    }
}