using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if(!context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    context.ProductBrands.AddRange(brands);

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    context.ProductTypes.AddRange(types);

                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    context.Products.AddRange(products);

                    await context.SaveChangesAsync();
                }


                if(!context.DeliveryMethod.Any())
                {
                    var delivermMethodData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    List<DeliveryMethod> deliveryMethod = JsonSerializer
                        .Deserialize<List<DeliveryMethod>>(delivermMethodData);

                    context.DeliveryMethod.AddRange(deliveryMethod);

                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }




    }
}