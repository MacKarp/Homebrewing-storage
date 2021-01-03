using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Data
{
    public static class PrepDb
    {
        static readonly UserManager<IdentityUser> userManager;

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


                var items = new List<IdentityUser>
                {
                    new IdentityUser { UserName = "user1@test.test", Email = "user1@test.test"}, //pass: User1password!
                    new IdentityUser { UserName = "user2@test.test", Email = "user2@test.test"}, //pass: User1password!
                    new IdentityUser { UserName = "user3@test.test", Email = "user3@test.test"}, //pass: User1password!
                    new IdentityUser { UserName = "user4@test.test", Email = "user4@test.test"}, //pass: User1password!
                    //new User { UserName = "User 2", UserEmail = "user2@test.test", UserPassword = "user2password"},
                    //new User { UserName = "User 3", UserEmail = "user3@test.test", UserPassword = "user3password"},
                    //new User { UserName = "User 4", UserEmail = "user4@test.test", UserPassword = "user4password"},
                    //new User { UserName = "User 5", UserEmail = "user5@test.test", UserPassword = "user5password"},
                };

                foreach(var item in items)
                {
                    userManager.CreateAsync(item,"UserPassword123!");
                }

                //context.AddRange(item);
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
                var storages = new List<Storage>();
                int i=1;
                foreach (var user in context.Users)
                {
                     storages.Add(new Storage { IdUser = user, StorageName = "Storage " + i });
                     i++;
                };
                //{
                //    new Storage {IdUser = context.Users.Id, StorageName = "Storage 1"},
                //    new Storage {IdUser = context.Users.Find(2), StorageName = "Storage 2"},
                //    new Storage {IdUser = context.Users.Find(3), StorageName = "Storage 3"},
                //    new Storage {IdUser = context.Users.Find(4), StorageName = "Storage 4"},
                   
                //};
                context.AddRange(storages);
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
                var item = new List<Expire>();
                foreach(var user in context.Users)
                {
                    item.Add(new Expire { IdUser = user, IdStorage = context.Storages.Find(1), IdItem = context.Items.Find(1), ExpirationDate = DateTime.Today });
                };
                context.AddRange(item);
                context.SaveChanges();
            }

        }
    }
}