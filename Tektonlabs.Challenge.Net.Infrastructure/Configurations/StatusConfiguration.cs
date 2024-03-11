using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tektonlabs.Challenge.Net.Domain.Status;

namespace Tektonlabs.Challenge.Net.Infrastructure.Configurations;

internal sealed class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("status");
        builder.HasKey(p => p.Id);
    }
}
