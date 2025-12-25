using Microsoft.EntityFrameworkCore;
using YemekAsistani.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace YemekAsistani.Data
{
     public class ApplicationDbContext : IdentityDbContext     
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<ShoppingItem> ShoppingItems { get; set; }
    }
}