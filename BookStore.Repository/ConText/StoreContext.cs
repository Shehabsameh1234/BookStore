using BookStore.Core.Entities.Books;
using BookStore.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace BookStore.Repository.ConText
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> option):base(option)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Book> Books { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order>  Orders { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }



    }
}
