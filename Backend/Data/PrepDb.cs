using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Backend.Controllers;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
namespace Backend.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<BackendContext>(),
                         serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>());
            }
        }

        public static void SeedData(BackendContext context, UserManager<IdentityUser> userManager)
        {
            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();

            //Populating Database
            try
            {


                if (!context.Users.AnyAsync().Result)
                {
                    System.Console.WriteLine("Adding Users...");

                    var items = new List<IdentityUser>
                {
                    new IdentityUser { UserName = "admin@admin.pl", Email = "admin@admin.pl" },
                    new IdentityUser { UserName = "user1@test.test", Email = "user1@test.test"},
                    new IdentityUser { UserName = "user2@test.test", Email = "user2@test.test"},
                    new IdentityUser { UserName = "user3@test.test", Email = "user3@test.test"}, 
                    //new User { UserName = "User 2", UserEmail = "user2@test.test", UserPassword = "user2password"},
                    //new User { UserName = "User 3", UserEmail = "user3@test.test", UserPassword = "user3password"},
                    //new User { UserName = "User 4", UserEmail = "user4@test.test", UserPassword = "user4password"},
                    //new User { UserName = "User 5", UserEmail = "user5@test.test", UserPassword = "user5password"},
                };
                    foreach (var it in items)
                    {
                        userManager.CreateAsync(it, "Aa123456!");
                        userManager.AddClaimAsync(it, new Claim(ClaimTypes.Role, "User"));
                    }
                    userManager.AddClaimAsync(items[0], new Claim(ClaimTypes.Role, "Admin"));

                    //context.AddRange(items);
                    //context.SaveChanges();
                }

                if (!context.Categories.AnyAsync().Result)
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

                if (!context.Storages.AnyAsync().Result)
                {
                    System.Console.WriteLine("Adding Storage...");
                    var storages = new List<Storage>();
                    int i = 1;
                    foreach (var user in context.Users)
                    {
                        storages.Add(new Storage { IdUser = user, StorageName = "Storage " + i });
                        i++;
                    };

                    context.AddRange(storages);
                    context.SaveChanges();
                }

                if (!context.Items.AnyAsync().Result)
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

                List<Storage> storagesList = context.Storages.ToList();
                List<Item> itemsList = context.Items.ToList();
                int idS = storagesList[0].StorageId; // takes 1st storage Id from DB
                int idI = itemsList[0].ItemId;       // takes 1st item Id from DB

                if (!context.Expires.AnyAsync().Result)
                {
                    System.Console.WriteLine("Adding Expire...");
                    var item = new List<Expire>();

                    foreach (var user in context.Users)
                    {
                        item.Add(new Expire
                        {
                            IdUser = user,
                            IdStorage = context.Storages.Find(idS),
                            IdItem = context.Items.Find(idI),
                            ExpirationDate = DateTime.Today.AddDays(4)
                        });
                    };
                    context.AddRange(item);
                    context.SaveChanges();
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Error while filling database with test data.");
            }

        }
    }
}