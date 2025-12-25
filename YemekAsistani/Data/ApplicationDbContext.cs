using Microsoft.EntityFrameworkCore;
using YemekAsistani.Models;

namespace YemekAsistani.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
    }
}