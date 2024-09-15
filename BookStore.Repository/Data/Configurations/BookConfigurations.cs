using BookStore.Core.Entities.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Data.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(25);

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.PictureUrl)
                   .IsRequired();

            builder.Property(b => b.Price)
                .HasColumnType("decimal (18,2)");

            builder.HasOne(b => b.Category)
                .WithMany()
                .HasForeignKey(b => b.CategoryId);
        }
    }
}
