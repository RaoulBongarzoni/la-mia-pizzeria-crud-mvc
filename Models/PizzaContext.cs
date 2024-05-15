using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaContext : DbContext
    {


        public DbSet<Pizza> Pizza { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=db-pizza;");
        }


    }
}
