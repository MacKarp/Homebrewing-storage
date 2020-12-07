using System;
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
            if (!context.Users.Any())
            {
                System.Console.WriteLine("Adding Users...");
                var item = new List<User>
                {
                    new User { UserName = "User 1", UserEmail = "user1@test.test", UserPassword = "user1password"},
                    new User { UserName = "User 2", UserEmail = "user2@test.test", UserPassword = "user2password"},
                    new User { UserName = "User 3", UserEmail = "user3@test.test", UserPassword = "user3password"},
                    new User { UserName = "User 4", UserEmail = "user4@test.test", UserPassword = "user4password"},
                    new User { UserName = "User 5", UserEmail = "user5@test.test", UserPassword = "user5password"},
                };
                context.AddRange(item);
                context.SaveChanges();
            }
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
                    new Storage {IdUser = context.Users.Find(1), StorageName = "Storage 1"},
                    new Storage {IdUser = context.Users.Find(2), StorageName = "Storage 2"},
                    new Storage {IdUser = context.Users.Find(3), StorageName = "Storage 3"},
                    new Storage {IdUser = context.Users.Find(4), StorageName = "Storage 4"},
                    new Storage {IdUser = context.Users.Find(5), StorageName = "Storage 5"},
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
                    new Expire { IdUser = context.Users.Find(1), IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today},
                    new Expire { IdUser = context.Users.Find(2), IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today},
                    new Expire { IdUser = context.Users.Find(3), IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today},
                    new Expire { IdUser = context.Users.Find(4), IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today},
                    new Expire { IdUser = context.Users.Find(5), IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today},
                };
                context.AddRange(item);
                context.SaveChanges();
            }

        }
    }
}