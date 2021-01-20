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
                    new Category {CategoryName = "Słody"},
                    new Category {CategoryName = "Chmiele"},
                    new Category {CategoryName = "Drożdże"},
                    new Category {CategoryName = "Dodatki"},
                    new Category {CategoryName = "Ekstrakty"},
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
                    // Słody:
                    new Item { ItemName = "Słód Monachijski Jasny", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Pszenica Niesłodowana", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Caramel Pils", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Pale Ale", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Red Active", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Monachijski Ciemny", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Carafa typ II", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Red Active", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Whisky", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Owsiany", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Wędzony Torfem", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Red Ale", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Karmelowy 30EBC", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Karmelowy 300EBC", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód CaraBody", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Wędzony Bukiem", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Cara Special typ II", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Abbey", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Wędzony Olchą", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Wędzony Jabłonią", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Pilzneński", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Karmelowy 150EBC", IdCategory = context.Categories.Find(1)},
                    new Item { ItemName = "Słód Wiedeński", IdCategory = context.Categories.Find(1)},

                    // Chmiele:
                    new Item { ItemName = "Tomyski", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Zeus", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Sorachi Ace", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Bravo", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Chmiel do lambica", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Herkules", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Agnus", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Vital", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Lubelski", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Willamette", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Warrior", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Vanguard", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Summit", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Sterling", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Strata", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Simcoe", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Sorachi Ace", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Sabro", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Pekko", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Palisade", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Nugget", IdCategory = context.Categories.Find(2)},
                    new Item { ItemName = "Mosaic", IdCategory = context.Categories.Find(2)},

                    // Drożdże:
                    new Item { ItemName = "Mangrove Jack's Bohemian Lager M84", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM55 Zielone wzgórze", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM54 Gorączka kalifornijska", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM52 Amerykański sen", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM42 Stare nadreńskie", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM41 Gwoździe i banany", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM31 Bawarska dolina", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM30 Bohemska rapsodia", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM23 Magiczny ogród", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM21 Odkrycie sezonu", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Fermentum Mobile FM13 Irlandzkie ciemności", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "White Labs  WLP1983 Charlie's fist bump Yeast", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Wyeast 3726 Farmhouse Ale", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Wyeast 2308 Munich Lager", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Wyeast 2124 Bohemian Lager", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Wyeast 2112 California Lager", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "Wyeast 1028 London Ale", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "White Labs WLP810 San Francisco Lager", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "White Labs WLP940 Mexican Lager", IdCategory = context.Categories.Find(3)},
                    new Item { ItemName = "White Labs WLP850 Copenhagen Lager", IdCategory = context.Categories.Find(3)},

                    // Dodatki:
                    new Item { ItemName = "Suszone skórki pomarańczy Bergamotki", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat Manuka Honey", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat Passion Fruit", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat Coffe", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat Cherry", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat Apricot", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Aromat American OAK", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Kwiat wrzosu", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Werbena cytrynowa", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Trawa cytrynowa", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Pożywka dla drożdży", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Suszone skórki pomarańczy", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Suszone skórki cytryny", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Mech irlandzki", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Lukrecja", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Laktoza", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Kolendra", IdCategory = context.Categories.Find(4)},
                    new Item { ItemName = "Jagody jałowca", IdCategory = context.Categories.Find(4)},

                    // Ekstrakty:
                    new Item { ItemName = "Pszeniczny płynny ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
                    new Item { ItemName = "Jasny płynny ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
                    new Item { ItemName = "Jasny suchy ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
                    new Item { ItemName = "Ciemny płynny ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
                    new Item { ItemName = "Bursztynowy suchy ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
                    new Item { ItemName = "Bursztynowy płynny ekstrakt słodowy", IdCategory = context.Categories.Find(5)},
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