using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Backend.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
namespace Backend.Data
{
    public static class PrepDb
    {


        //ILogger<PrepDb> logger= new ILogger<PrepDb>();
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

            Log.Logger = new LoggerConfiguration()
               .WriteTo.Seq("http://seq:5341")
               .CreateLogger();

            System.Console.WriteLine("Applying Migrations...");
            context.Database.Migrate();
            Thread.Sleep(1000);

            //Populating Database
            var identityUsersItemList = new List<IdentityUser>
            {
                new IdentityUser {UserName = "admin@admin.pl", Email = "admin@admin.pl"},
                new IdentityUser {UserName = "user1@test.test", Email = "user1@test.test"},
                new IdentityUser {UserName = "user2@test.test", Email = "user2@test.test"},
                new IdentityUser {UserName = "user3@test.test", Email = "user3@test.test"},
            };
            if (!userManager.Users.Any())
            {
                System.Console.WriteLine("Adding Users...");

                try
                {
                    foreach (var it in identityUsersItemList)
                    {
                        userManager.CreateAsync(it, "Aa123456!");
                        Thread.Sleep(1000);
                        userManager.AddClaimAsync(it, new Claim(ClaimTypes.Role, "User"));
                        Thread.Sleep(1000);
                    }

                    userManager.AddClaimAsync(identityUsersItemList[0], new Claim(ClaimTypes.Role, "Admin"));
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Log.Information(
                        "Error while filling database with USER test data (SeedData). Exception: {exception}",
                        ex.Message);
                    Console.WriteLine("Error while filling database with USER test data.");
                }

            }
            Thread.Sleep(5000);
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
                try
                {
                    context.AddRange(item);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    Log.Information("Error while filling database with CATEGORY test data (SeedData). Exception: {exception}", ex.Message);
                    Console.WriteLine("Error while filling database with CATEGORY test data.");
                }
            }

            if (!context.Storages.Any())
            {
                System.Console.WriteLine("Adding Storage...");
                var storages = new List<Storage>();
                int i = 1;
                foreach (var user in identityUsersItemList)
                {
                    storages.Add(new Storage { IdUser = user, StorageName = "Storage " + i });
                    i++;
                };
                try
                {
                    context.AddRange(storages);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Information("Error while filling database with STORAGES test data (SeedData). Exception: {exception}", ex.Message);
                    Console.WriteLine("Error while filling database with STORAGES test data.");
                }
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
                try
                {
                    context.AddRange(item);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Information("Error while filling database with ITEMS test data (SeedData). Exception: {exception}", ex.Message);
                    Console.WriteLine("Error while filling database with ITEMS test data.");
                }
            }

            List<Storage> storagesList = context.Storages.ToList();
            List<Item> itemsList = context.Items.ToList();
            int idS = storagesList[0].StorageId; // takes 1st storage Id from DB
            int idI = itemsList[0].ItemId;       // takes 1st item Id from DB

            if (!context.Expires.Any())
            {
                System.Console.WriteLine("Adding Expire...");
                var item = new List<Expire>();

                foreach (var user in identityUsersItemList)
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
    }
}