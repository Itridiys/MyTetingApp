using Microsoft.EntityFrameworkCore;
using MyTetingApp.Models;

namespace MyTetingApp.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<MoneyDoc> MoneyTable { get; set; }

        public string DbPath { get; private set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb; Database=Money; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=False;");
        }
    }

    
}
