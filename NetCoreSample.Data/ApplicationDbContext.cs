using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCoreSample.Core.Domain.User;
using System;
using System.Linq;
using System.Reflection;
using NetCoreSample.Core.Domain;
using NetCoreSample.Data.Mapping;

namespace NetCoreSample.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {
        public DbSet<Race> Races { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Database.<ApplicationDbContext>(null);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customizations must go after base.OnModelCreating(builder)

            //builder.ApplyConfiguration(new ApplicationUserConfig());
            //builder.ApplyConfiguration(new OrderItemConfig());

            // Imagine a ton more customizations

            //var configType = typeof(IEntityTypeConfiguration<>);   //any of your configuration classes here
            var typesToRegister = Assembly.GetAssembly(typeof(ApplicationDbContext)).GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType)
                    .Any(i => i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                );
            /*
            var typesToRegister = Assembly.GetAssembly(typeof(ApplicationDbContext)).GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            */
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                builder.ApplyConfiguration(configurationInstance);
            }

            // builder.ApplyConfiguration(new UserScoreMap());
        }
    }
}
