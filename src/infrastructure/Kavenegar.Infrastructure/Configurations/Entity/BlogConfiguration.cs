using Kavenegar.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kavenegar.Infrastructure.Configurations.Entity
{
    internal class BlogConfiguration : IEntityTypeConfiguration<BLog>
    {
        public void Configure(EntityTypeBuilder<BLog> builder)
        {
            builder.ToTable("BLog", "Entity");
            builder.HasKey(p => p.Id).HasName("PK_Entity_BLog");
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Title).IsRequired().HasColumnType("nvarchar").HasMaxLength(800);
            builder.Property(p => p.Content).IsRequired().HasColumnType("nvarchar").HasMaxLength(4000);
            builder.Property(p => p.AuthorId).IsRequired();
            builder.Property<byte[]>("Version").IsRowVersion();

            builder.HasOne(d => d.Author)
                .WithMany()
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.NoAction);

            /*base my exprience I inclueded author automatically*/
            builder.Navigation(e => e.Author).AutoInclude();
        }
    }
}
