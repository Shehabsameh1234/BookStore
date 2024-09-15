using BookStore.Core.Entities.Books;
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

    }
}
