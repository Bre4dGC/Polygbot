using Microsoft.EntityFrameworkCore;
using Polygbot.Models;

namespace Polygbot.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserSettings> UserSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bot.db");
        }
    }
}
