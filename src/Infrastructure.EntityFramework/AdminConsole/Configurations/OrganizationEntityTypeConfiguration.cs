using Bit.Infrastructure.EntityFramework.AdminConsole.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bit.Infrastructure.EntityFramework.AdminConsole.Configurations;

public class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .Property(o => o.Id)
            .ValueGeneratedNever();

        // Deprecated https://bitwarden.atlassian.net/browse/PM-10863
        builder.Property(c => c.LimitCollectionCreationDeletion)
            .ValueGeneratedNever()
            .HasDefaultValue(false);

        builder.Property<bool>(c => c.LimitCollectionCreation)
            .ValueGeneratedNever()
            .HasDefaultValue(false);

        builder.Property<bool>(c => c.LimitCollectionDeletion)
            .ValueGeneratedNever()
            .HasDefaultValue(false);

        builder.Property(c => c.AllowAdminAccessToAllCollectionItems)
            .ValueGeneratedNever()
            .HasDefaultValue(true);

        NpgsqlIndexBuilderExtensions.IncludeProperties(
            builder.HasIndex(o => new { o.Id, o.Enabled }),
            o => o.UseTotp);

        builder.ToTable(nameof(Organization));
    }
}
