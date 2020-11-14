using System.Collections.Generic;
using Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<BackendContext>());
            }
        }

        public static void SeedData(BackendContext context)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();
            
            //Populating Database
            if (!context.Categories.Any())
            {
                System.Console.WriteLine("Adding Categories...");
                var item = new List<Category>
                {
                    new Category {CategoryName = "Category 1"},
                    new Category {CategoryName = "Category 2"},
                    new Category {CategoryName = "Category 3"},
                    new Category {CategoryName = "Category 4"},
                    new Category {CategoryName = "Category 5"},
                };
                context.AddRange(item);
                context.SaveChanges();
            }

            if (!context.Storages.Any())
            {
                System.Console.WriteLine("Adding Storage...");
                var item = new List<Storage>
                {
                    new Storage {UserID = 0, StorageName = "Storage 1"},
                    new Storage {UserID = 0, StorageName = "Storage 2"},
                    new Storage {UserID = 0, StorageName = "Storage 3"},
                    new Storage {UserID = 0, StorageName = "Storage 4"},
                    new Storage {UserID = 0, StorageName = "Storage 5"},
                };
                context.AddRange(item);
                context.SaveChanges();
            }

            if (!context.Items.Any())
            {
                System.Console.WriteLine("Adding Item...");
                var item = new List<Item>
                {
                    new Item { ItemName = "Item 1", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Item 2", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Item 3", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Item 4", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Item 5", IdCategory = context.Categories.Find(5)},
                };
                context.AddRange(item);
                context.SaveChanges();
            }


            if (!context.Expires.Any())
            {
                System.Console.WriteLine("Adding Expire...");
                var item = new List<Expire>
                {
                    new Expire { UserId = 0, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = "12345"},
                    new Expire { UserId = 0, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = "12345"},
                    new Expire { UserId = 0, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = "12345"},
                    new Expire { UserId = 0, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = "12345"},
                    new Expire { UserId = 0, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = "12345"},
                };
                context.AddRange(item);
                context.SaveChanges();
            }

        }
    }
}