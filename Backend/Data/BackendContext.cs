using Backend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class BackendContext : IdentityDbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Expire> Expires { get; set; }
    }
}