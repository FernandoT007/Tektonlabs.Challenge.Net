using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tektonlabs.Challenge.Net.Domain.Products;

namespace Tektonlabs.Challenge.Net.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .HasConversion(n => n.Value, value => Name.Create(value).Value);

        builder.Property(p => p.Stock)
            .HasConversion(n => n.Value, value => Stock.Create(value).Value);

        builder.Property(p => p.Description)
            .HasMaxLength(200)
           .HasConversion(n => n.Value, value => Description.Create(value).Value);

        builder.Property(p => p.Price)
            .HasConversion(n => n.Value, value => Price.Create(value).Value);

        //builder.Property(p => p.Discount)
        //    .HasConversion(n => n!.Value, value => Discount.Create(value).Value);

        //builder.Property(p => p.FinalPrice)
        //   .HasConversion(n => n.Value, value => Price.Create(value).Value);
    }
}
