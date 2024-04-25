using Kavenegar.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kavenegar.Infrastructure.Configurations.Entity
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Entity");
            builder.HasKey(p => p.Id).HasName("PK_Entity_User");
            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasColumnType("nvarchar").HasMaxLength(800);
            builder.Property<byte[]>("Version").IsRowVersion(); 
            builder.HasData(
                 new User
                 {
                     Id = Domain.Const.UserConst.Admin_Id,
                     Name = Domain.Const.UserConst.Admin_Name,
                 }
            );

        }
    }
}