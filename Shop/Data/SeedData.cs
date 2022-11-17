using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data
{
    public class SeedData
    {
        public static void SeedDataBase(ShopContext context)
        {
            context.Database.Migrate();
            if (!context.Products.Any())
            {
                Category dresses = new Category { Name = "Dresses", Slug = "dresses" };
                Category shoes = new Category { Name = "Shoes", Slug = "shoes" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Long Dress",
                        Slug = "dresses",
                        Description = "Beautiful dress",
                        Price = 100.00M,
                        Category = dresses,
                        Image = "midiDress"

                    },

                     new Product
                     {
                         Name = "Mini Dress",
                         Slug = "dresses",
                         Description = "Salt Crystal Wrap And Roll Woven Dress",
                         Price = 80.00M,
                         Category = dresses,
                         Image = "softDress.webp"

                     },
                      new Product
                      {
                          Name = "Maxi Dress",
                          Slug = "dresses",
                          Description = "Black Asymmetric Satin Dress",
                          Price = 100.00M,
                          Category = dresses,
                          Image = "satindress.webp"


                      },
                      new Product
                      {
                          Name = "Boots",
                          Slug = "shoes",
                          Description = "Footwook",
                          Price = 90.00M,
                          Category = shoes,
                          Image = "footWork.webp"

                      },
                       new Product
                       {
                           Name = "Boots",
                           Slug = "shoes",
                           Description = "Bronx woman",
                           Price = 90.00M,
                           Category = shoes,
                           Image = "bronxWoman.webp"

                       },
                      new Product
                      {
                          Name = "Flat pumps",
                          Slug = "shoes",
                          Description = "Soft style",
                          Price = 50.00M,
                          Category = shoes,
                          Image = "softstyle.webp"

                      }
                      );
                context.SaveChanges();
            }
        }
    }
}
