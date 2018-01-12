using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreSample.Core.Domain;

namespace NetCoreSample.Data.Mapping
{
    public class UserScoreMap : IEntityTypeConfiguration<UserScore>
    {
        void IEntityTypeConfiguration<UserScore>.Configure(EntityTypeBuilder<UserScore> builder)
        {
            // Override nvarchar(max) with nvarchar(15)
            builder.ToTable("UserScore");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(15).IsRequired();

            builder.HasIndex(x => new { x.UserName, x.Score }).HasName("UX_UserName_Score").IsUnique();

            builder.Property(x => x.Score).IsRequired();

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
