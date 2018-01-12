using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NetCoreSample.Data;

namespace DummyDB
{
    class ApiContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApiContextFactory()
        {
            if (AppState.DefaultConnection == null)
            {
                AppState.BuildSetting();
            }
        }


        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // builder.UseSqlServer("Server=.\\sqlexpress2008R2;Database=NetCoreSampleSecurityDB;Trusted_Connection=True;MultipleActiveResultSets=true"
            builder.UseSqlServer(AppState.DefaultConnection, x => x.MigrationsAssembly("DummyDB"));

            return new ApplicationDbContext(builder.Options);
        }
    }
}
