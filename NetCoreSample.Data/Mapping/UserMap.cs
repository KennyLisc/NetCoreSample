using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreSample.Core.Domain.User;

namespace NetCoreSample.Data.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        void IEntityTypeConfiguration<ApplicationUser>.Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.Property(x => x.FullName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Department).HasMaxLength(50).IsRequired();

            // builder.HasAnnotation()("Users");


            /*
            // Override nvarchar(max) with nvarchar(15)
            builder.Property(u => u.PhoneNumber).HasMaxLength(15);

            // Make the default table name of AspNetUsers to Users
            builder.ToTable("Users");
            // Make the default column type datetime over datetime2(for some reason).
            builder.Property(o => o.DateTimeOrdered).HasColumnType("datetime");

            // Make the default value 1 for the Quantity property
            builder.Property(o => o.Quantity).HasDefaultValue(1);

            // Make the Primary Key associated with the property UniqueKey
            builder.HasKey(o => o.UniqueKey);

            */
        }
    }
}
